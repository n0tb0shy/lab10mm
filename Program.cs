using System;
using System.Collections.Generic;
using System.IO;

public class GraphConnectivityChecker
{
    private int[,] _adjacencyMatrix;
    private int _vertexCount;

    public void LoadGraphFromFile(string filePath);//Z:\matmod\1.txt
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Файл не найден.");
        }

        string[] lines = File.ReadAllLines(filePath);
        _vertexCount = int.Parse(lines[0]);//Z:\matmod\2.txt
        _adjacencyMatrix = new int[_vertexCount, _vertexCount];

        for (int i = 0; i < _vertexCount; i++)
        {
            string[] values = lines[i + 1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
            for (int j = 0; j < _vertexCount; j++)
            {
                _adjacencyMatrix[i, j] = int.Parse(values[j]);
            }
        }
    }

    public bool IsStronglyConnected()
    {
        for (int i = 0; i < _vertexCount; i++)
        {
            var visited = new bool[_vertexCount];
            DepthFirstSearch(i, visited);

            if (Array.Exists(visited, node => !node))
            {
                return false;
            }
        }
        return true;
    }

    private void DepthFirstSearch(int vertex, bool[] visited)
    {
        visited[vertex] = true;

        for (int i = 0; i < _vertexCount; i++)
        {
            if (_adjacencyMatrix[vertex, i] == 1 && !visited[i])
            {
                DepthFirstSearch(i, visited);
            }
        }
    }

    public void PrintAdjacencyMatrix()
    {
        Console.WriteLine("Матрица смежности графа:");
        
        for (int i = 0; i < _vertexCount; i++)
        {
            for (int j = 0; j < _vertexCount; j++)
            {
                Console.Write(_adjacencyMatrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Программа проверки графа на сильную связность");
        Console.WriteLine("---------------------------------------------");

        var checker = new GraphConnectivityChecker();

        while (true)
        {
            Console.WriteLine("Введите путь к файлу с матрицей смежности или 'exit' для выхода:");
            string input = Console.ReadLine()?.Trim();

            if (input?.ToLower() == "exit")
            {
                break;
            }

            try
            {
                checker.LoadGraphFromFile(input);
                checker.PrintAdjacencyMatrix();

                bool isStronglyConnected = checker.IsStronglyConnected();
                Console.WriteLine(isStronglyConnected
                    ? "Граф является сильно связным."
                    : "Граф НЕ является сильно связным.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}
