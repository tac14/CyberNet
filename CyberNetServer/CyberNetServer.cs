﻿using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using MySql.Data.MySqlClient;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace CyberNet 
{

    public class HttpProcessor 
	{
        public TcpClient socket;        
        public HttpServer srv;

        private Stream inputStream;
        public StreamWriter outputStream;

        public String http_method;
        public String http_url;
        public String http_protocol_versionstring;
        public Hashtable httpHeaders = new Hashtable();


        private static int MAX_POST_SIZE = 10 * 1024 * 1024; // 10MB

        public HttpProcessor(TcpClient s, HttpServer srv) 
		{
            this.socket = s;
            this.srv = srv;                   
        }
        

        private string streamReadLine(Stream inputStream) 
		{
            int next_char;
            string data = "";
            while (true) 
			{
                next_char = inputStream.ReadByte();
                if (next_char == '\n') { break; }
                if (next_char == '\r') { continue; }
                if (next_char == -1) { Thread.Sleep(1); continue; };
                data += Convert.ToChar(next_char);
            }            
            return data;
        }
        public void process() 
		{                        
            // we can't use a StreamReader for input, because it buffers up extra data on us inside it's
            // "processed" view of the world, and we want the data raw after the headers
            inputStream = new BufferedStream(socket.GetStream());

            // we probably shouldn't be using a streamwriter for all output from handlers either
            outputStream = new StreamWriter(new BufferedStream(socket.GetStream()));
            try 
			{
                parseRequest();
                readHeaders();
                if (http_method.Equals("GET")) 
				{
                    handleGETRequest();
                } 
				else if (http_method.Equals("POST")) 
				{
                    handlePOSTRequest();
                }
            } 
			catch (Exception e) 
			{
                Console.WriteLine("Exception: " + e.ToString());
                writeFailure();
            }
            outputStream.Flush();
            // bs.Flush(); // flush any remaining output
            inputStream = null; outputStream = null; // bs = null;            
            socket.Close();             
        }

        public void parseRequest() 
		{
            String request = streamReadLine(inputStream);
            string[] tokens = request.Split(' ');
            if (tokens.Length != 3) {
                throw new Exception("invalid http request line");
            }
            http_method = tokens[0].ToUpper();
            http_url = tokens[1];
            http_protocol_versionstring = tokens[2];

            Console.WriteLine("starting: " + request);
        }

        public void readHeaders() 
		{
            Console.WriteLine("readHeaders()");
            String line;
            while ((line = streamReadLine(inputStream)) != null) {
                if (line.Equals("")) {
                    Console.WriteLine("got headers");
                    return;
                }
                
                int separator = line.IndexOf(':');
                if (separator == -1) {
                    throw new Exception("invalid http header line: " + line);
                }
                String name = line.Substring(0, separator);
                int pos = separator + 1;
                while ((pos < line.Length) && (line[pos] == ' ')) {
                    pos++; // strip any spaces
                }
                    
                string value = line.Substring(pos, line.Length - pos);
                Console.WriteLine("header: {0}:{1}",name,value);
                httpHeaders[name] = value;
            }
        }

        public void handleGETRequest() 
		{
            srv.handleGETRequest(this);
        }

        private const int BUF_SIZE = 4096;
        public void handlePOSTRequest() 
		{
            // this post data processing just reads everything into a memory stream.
            // this is fine for smallish things, but for large stuff we should really
            // hand an input stream to the request processor. However, the input stream 
            // we hand him needs to let him see the "end of the stream" at this content 
            // length, because otherwise he won't know when he's seen it all! 

            Console.WriteLine("get post data start");
            int content_len = 0;
            MemoryStream ms = new MemoryStream();
            if (this.httpHeaders.ContainsKey("Content-Length")) 
			{
                 content_len = Convert.ToInt32(this.httpHeaders["Content-Length"]);
                 if (content_len > MAX_POST_SIZE) 
				 {
                     throw new Exception(
                         String.Format("POST Content-Length({0}) too big for this simple server",
                           content_len));
                 }
                 byte[] buf = new byte[BUF_SIZE];              
                 int to_read = content_len;
                 while (to_read > 0) 
				 {  
                     Console.WriteLine("starting Read, to_read={0}",to_read);

                     int numread = this.inputStream.Read(buf, 0, Math.Min(BUF_SIZE, to_read));
                     Console.WriteLine("read finished, numread={0}", numread);
                     if (numread == 0) 
					 {
                         if (to_read == 0) 
						 {
                             break;
                         } 
						 else 
						 {
                             throw new Exception("client disconnected during post");
                         }
                     }
                     to_read -= numread;
                     ms.Write(buf, 0, numread);
                 }
                 ms.Seek(0, SeekOrigin.Begin);
            }
            Console.WriteLine("get post data end");
            srv.handlePOSTRequest(this, new StreamReader(ms));

        }

        public void writeSuccess() 
		{
            outputStream.Write("HTTP/1.0 200 OK\n");
            outputStream.Write("Content-Type: text/html\n");
            outputStream.Write("Connection: close\n");
            outputStream.Write("\n");
        }

        public void writeFailure() 
		{
            outputStream.Write("HTTP/1.0 404 File not found\n");
            outputStream.Write("Connection: close\n");
            outputStream.Write("\n");
        }
    }

    public abstract class HttpServer 
	{

        protected int port;
        TcpListener listener;
        bool is_active = true;
       
        public HttpServer(int port) {
            this.port = port;
        }

        public void listen() 
		{
            listener = new TcpListener(port);
            listener.Start();
            while (is_active) 
			{                
                TcpClient s = listener.AcceptTcpClient();
                HttpProcessor processor = new HttpProcessor(s, this);
                Thread thread = new Thread(new ThreadStart(processor.process));
                thread.Start();
                Thread.Sleep(1);
            }
        }

        public abstract void handleGETRequest(HttpProcessor p);
        public abstract void handlePOSTRequest(HttpProcessor p, StreamReader inputData);
    }

    public class MyHttpServer : HttpServer 
	{
        public MyHttpServer(int port)
            : base(port) 
		{
        }
        public override void handleGETRequest(HttpProcessor p) 
		{
            Console.WriteLine("request: {0}", p.http_url);
            p.writeSuccess();
			p.outputStream.WriteLine("{}");

			/*
            p.outputStream.WriteLine("<html><body><h1>test server</h1>");
            p.outputStream.WriteLine("Current Time: " + DateTime.Now.ToString());
            p.outputStream.WriteLine("url : {0}", p.http_url);

            p.outputStream.WriteLine("<form method=post action=/form>");
            p.outputStream.WriteLine("<input type=text name=foo value=foovalue>");
            p.outputStream.WriteLine("<input type=submit name=bar value=barvalue>");
            p.outputStream.WriteLine("</form>");*/
        }

        public override void handlePOSTRequest(HttpProcessor p, StreamReader inputData) 
		{
            Console.WriteLine("POST request: {0}", p.http_url);
            string data = inputData.ReadToEnd();

            p.outputStream.WriteLine("<html><body><h1>test server</h1>");
            p.outputStream.WriteLine("<a href=/test>return</a><p>");
            p.outputStream.WriteLine("postbody: <pre>{0}</pre>", data);
            

        }

		public void ConectDB(string argCommand)
		{
			MySqlCommand command = new MySqlCommand(); ;
			string connectionString, commandString;
			connectionString = "Data source=localhost;UserId=root;Password=nt[yj14;database=CyberNetDB;";
			MySqlConnection connection = new MySqlConnection(connectionString);
			commandString = argCommand;
			command.CommandText = commandString;
			command.Connection = connection;
			MySqlDataReader reader;
			try
			{
				command.Connection.Open();
				reader = command.ExecuteReader();
				/*while (reader.Read())
				{
					int ID = (int)reader["ID"];
					string Name = (string)reader["Name"];
				}*/
				reader.Close();
			}
			catch (MySqlException ex)
			{
				Console.WriteLine("Error: \r\n{0}", ex.ToString());
			}
			finally
			{
				command.Connection.Close();
			}
		}


		public void SaveCity(string argName)
		{
			MySqlCommand command = new MySqlCommand(); ;
			string connectionString, commandString;
			connectionString = "Data source=localhost;UserId=root;Password=nt[yj14;database=CyberNetDB;";
			MySqlConnection connection = new MySqlConnection(connectionString);
			commandString = "INSERT Cities (Name) VALUES('"+argName+"');";
			command.CommandText = commandString;
			command.Connection = connection;
			MySqlDataReader reader;
			try
			{
				command.Connection.Open();
				reader = command.ExecuteReader();
				reader.Close();
			}
			catch (MySqlException ex)
			{
				Console.WriteLine("Error: \r\n{0}", ex.ToString());
			}
			finally
			{
				command.Connection.Close();
			}
		}

		public void SaveCities()
		{
			string[] locFile1 = File.ReadAllLines("Cities1.txt");
			string[] locFile2 = File.ReadAllLines("Cities2.txt");

			for (int i = 0; i < locFile1.Length; i++)
			{
				SaveCity(locFile1[i]);
				if (i != locFile1.Length - 1)
				{
					SaveCity(locFile2[i]);
				}
			}

		}
		
		public void JSONTest()
		{
			Node locNode = new Node();
			locNode.ID = 1;
			locNode.Name = "1";
			locNode.NodeType = NodeTypes.Category;
			Node locNode2 = new Node();
			locNode2.ID = 2;
			locNode2.Name = "2";
			locNode2.NodeType = NodeTypes.OptionsConditionsActionExe;


			Edge locEdge = new Edge();
			locEdge.From = locNode;
			locEdge.Till = locNode2;
			locEdge.ID = 1;
			locEdge.Name = "1";

			Graph locGraph = new Graph();
			locGraph.Nodes.Add(locNode);
			locGraph.Nodes.Add(locNode2);
			locGraph.Edges.Add(locEdge);

			MemoryStream stream1 = new MemoryStream();
			DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Graph));
			ser.WriteObject(stream1, locGraph);

			stream1.Position = 0;
			StreamReader sr = new StreamReader(stream1);

			string locText = sr.ReadToEnd();


		}

		public void NextTime()
		{
			while (1 == 1)
			{
				Stopwatch stopWatch = new Stopwatch();
				Stopwatch stopWatch2 = new Stopwatch();
				stopWatch.Start();
				stopWatch2.Start();

				//ConectDB("call CallStepForAllAgent()");
				Console.WriteLine("NextTime {0}", DateTime.Now.Date.ToShortDateString() + " " + DateTime.Now.TimeOfDay.ToString());

				stopWatch.Stop();

				TimeSpan ts = stopWatch.Elapsed;

				int time = 60000 * 60 - (int)ts.TotalMilliseconds; // 60000 - 1 минута
				if (time > 0)
				{
					Thread.Sleep(time); // 1 час
				}
				stopWatch2.Stop();

			}
		}


    }

	/*
    public class TestMain 
	{
        public static int Main(String[] args) 
		{
			MyHttpServer httpServer = new MyHttpServer(8081);

			Console.CursorVisible = false;
			Console.CancelKeyPress += Console_CancelKeyPress;
			Console.BackgroundColor = ConsoleColor.Blue;
			Console.ForegroundColor = ConsoleColor.White;
			//Console.

			//httpServer.ConectDB();
			//httpServer.JSONTest();
			//httpServer.SaveCities();

            Thread thread = new Thread(new ThreadStart(httpServer.listen));
            thread.Start();

			Thread locTimeThread = new Thread(new ThreadStart(httpServer.NextTime));
			locTimeThread.Start();

            return 0;
        }

		static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
		{
			Console.WriteLine("NoExit");
			e.Cancel = true;
		}

    }*/

}



