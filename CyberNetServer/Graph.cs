using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace CyberNet
{
	/// <summary>
	/// Граф
	/// </summary>
	[DataContract]
	[KnownType(typeof(ArrayList))]
	[KnownType(typeof(Node))]
	[KnownType(typeof(Edge))]
	public class Graph
	{
		[DataMember]
		public ArrayList Nodes = new ArrayList(); // Все вершины в графе
		[DataMember]
		public ArrayList Edges = new ArrayList(); // Все ребра в графе
	}

	/// <summary>
	/// Вершина
	/// </summary>
	[DataContract]
	public class Node
	{
		[DataMember]
		public int ID = 0; // CategoryID, OptionsID (или из OptionsConditionsActionExe, или из OptionsReceivingProduct)
		[DataMember]
		public string Name = ""; // CategoryName, для OptionsConditionsActionExe и OptionsReceivingProduct - ID в текстовом представлении
		[DataMember]
		public NodeTypes NodeType = NodeTypes.Category; // тип вершины
	}

	/// <summary>
	/// Ребро
	/// </summary>
	[DataContract]
	public class Edge
	{
		[DataMember]
		public Node From; // от этой вершины
		[DataMember]
		public Node Till; // до этой вершины
		[DataMember]
		public int ID = 0; // ActionID, допустимо пустое
		[DataMember]
		public string Name = ""; //ActionName, допустимо пустое
	}

	/// <summary>
	/// Типы вершин
	/// </summary>
	public enum NodeTypes
	{
		Category,	// Категория
		OptionsConditionsActionExe, // Вариант условий выполнения действий
		OptionsReceivingProduct // Вариант получения продукта
	}


}
