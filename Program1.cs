// Simple weighted graph representation 
// Uses an Adjacency Linked Lists, suitable for sparse graphs

using System;
using System.IO;

class Graph
{
    class Node
    {

        public int vert;//vertex
        public int wgt;
        public Node next;
    }
    private Node z;
    int V, E;
    Node[] adj;

    // used for traversing graph
    private int[] visited;
    private int id;


    // default constructor
    public Graph(string graphFile)
    {
        int u, v;
        int e, wgt;
        Node t;

        StreamReader reader = new StreamReader(graphFile);

        char[] splits = new char[] { ' ', ',', '\t' };
        string line = reader.ReadLine();
        string[] parts = line.Split(splits, StringSplitOptions.RemoveEmptyEntries);

        // find out number of vertices and edges
        V = int.Parse(parts[0]);
        E = int.Parse(parts[1]);

        // create sentinel node
        z = new Node();
        z.next = z;

        // Create adjacency lists, initialised to sentinel node z
        // Dynamically allocate array 
        adj = new Node[V + 1];
        for (v = 1; v <= V; ++v)
        {
            adj[v] = z;
        }
        //finish this

        // read the edges
        Console.WriteLine("Reading edges from text file");
        for (e = 1; e <= E; ++e)
        {
            line = reader.ReadLine();
            parts = line.Split(splits, StringSplitOptions.RemoveEmptyEntries);
            u = int.Parse(parts[0]);
            v = int.Parse(parts[1]);
            wgt = int.Parse(parts[2]);

            Console.WriteLine("Edge {0}--({1})--{2}", toChar(u), wgt, toChar(v));
            t = new Node();
            t.vert = v; t.wgt = wgt; t.next = adj[u];
            adj[u] = t;
            t = new Node();
            t.vert = u;
            t.wgt = wgt;
            t.next = adj[v];
            adj[v] = t;


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
        int v;
        Node n;

        for (v = 1; v <= V; ++v)
        {
            Console.Write("\nadj[{0}] ->", toChar(v));
            for (n = adj[v]; n != z; n = n.next)
                Console.Write(" |{0} | {1}| ->", toChar(n.vert), n.wgt);
            Console.WriteLine(" z");
        }
    }
    public void DF(int s)
    {

        id = 0;
        visited = new int[V + 1];

        for (int v = 1; v <= V; ++v)
        {
            visited[v] = 0;


        }
        dfVisit(0, s);


    }
    private void dfVisit(int prev, int v)
    {
        int u;
        Node t;
        visited[v] = ++id;
        Console.WriteLine("DF just visited vertex {0} and {1}", toChar(v), toChar(prev)); 
        for (t = adj[v]; t != t.next; t = t.next)
        {
            u = t.vert;
            if (visited[u] == 0)
            {
                dfVisit(v, u);
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

