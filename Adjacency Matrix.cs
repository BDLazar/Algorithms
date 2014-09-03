// Simple weighted graph representation 
// Uses an Adjacency Matrix, suitable for dense graphs

using System;
using System.IO;

class Graph
{
    // V = number of vertices
    // E = number of edges
    // adj[ , ] is the adjacency matrix
    private int V, E;
    private int[,] adj;

    // used for traversing graph
    private int[] visited;
    private int id;



    // default constructor
    public Graph(string graphFile)
    {
        int u, v;
        int e, wgt;

        StreamReader reader = new StreamReader(graphFile);

        char[] splits = new char[] { ' ', ',', '\t' };
        string line = reader.ReadLine();
        string[] parts = line.Split(splits, StringSplitOptions.RemoveEmptyEntries);

        // find out number of vertices and edges
        V = int.Parse(parts[0]);
        E = int.Parse(parts[1]);

        // create adjacency matrix, initialised to 0's
        adj = new int[V + 1, V + 1];


        // read the edges
        Console.WriteLine("Reading edges from text file");
        for (e = 1; e <= E; ++e)
        {
            line = reader.ReadLine();
            parts = line.Split(splits, StringSplitOptions.RemoveEmptyEntries);
            u = int.Parse(parts[0]);
            v = int.Parse(parts[1]);
            wgt = int.Parse(parts[2]);
            adj[v, u] = wgt;
            adj[u, v] = wgt;

            Console.WriteLine("Edge {0}--({1})--{2}", toChar(u), wgt, toChar(v));
            //put edge into adjacency matrix               
        }
    }

    // convert vertex into char for pretty printing
    private char toChar(int u)
    {
        return (char)(u + 64);
    }

    // method to display the graph representation
    public void display()
    {
        int u, v;

        for (v = 1; v <= V; ++v)
        {
            Console.Write("\nadj[{0}, ] = ", v);
            for (u = 1; u <= V; ++u)
                Console.Write("  {0}", adj[v, u]);
        }
        Console.WriteLine("");
    }



    public void DF(int s)
    {
        id = 0;
        visited = new int[V + 1];
        // declared array initialized all elements to 0. 
        visited[1] = 1;
        dfVisit(0, 1);

    }


    // DF for adjacency matrix
    private void dfVisit(int prev, int v)
    {
        int u;
        visited[v] = id++;

        Console.WriteLine("Visited vertex {0} Along Edge {1}", toChar(v), toChar(prev));
        for (int j = 1; j < adj.Length - 1; j++)
        {
            if (j < 8)
            {
                if (adj[v, j] != 0)
                {
                    if (visited[j] == 0)
                    {
                        dfVisit(v, j);
                    }
                }
            }
        }
    }


    public static void Main()
    {
        int s = 1;
        string fname = "wGraph3.txt";

        Graph g = new Graph(fname);

        g.display();

        g.DF(s);
        Console.ReadLine();
    }

}


