using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CyberNet
{
	/// <summary>
	/// Граф
	/// </summary>
	public class Graph
	{
		public ArrayList Nodes = new ArrayList(); // Все вершины в графе
		public ArrayList Edges = new ArrayList(); // Все ребра в графе
	}

	/// <summary>
	/// Вершина
	/// </summary>
	public class Node
	{
		public int ID = 0; // CategoryID, OptionsID (или из OptionsConditionsActionExe, или из OptionsReceivingProduct)
		public string Name = ""; // CategoryName, для OptionsConditionsActionExe и OptionsReceivingProduct - ID в текстовом представлении
		public NodeTypes NodeType = NodeTypes.Category; // тип вершины
	}

	/// <summary>
	/// Ребро
	/// </summary>
	public class Edge
	{
		public Node From; // от этой вершины
		public Node Till; // до этой вершины
		public int ID = 0; // ActionID, допустимо пустое
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
