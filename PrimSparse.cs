// Prim's MST Algorithm on Adjacency Lists representation 
// Uses an Adjacency Linked Lists, suitable for sparse graphs
// PrimSparse.cs

using System;
using System.IO;

// Heap code adapted for Prim's algorithm
// on adjacency lists graph
class Heap
{
    private int[] h;	   // heap array
    private int[] hPos;	   // hPos[h[k]] == k
    private int[] dist;    // dist[v] = priority of v

    private int N;         // heap size

    // The heap constructor gets passed from the Graph:
    //    1. maximum heap size
    //    2. reference to the dist[] array
    //    3. reference to the hPos[] array
    public Heap(int maxSize, int[] _dist, int[] _hPos)
    {
        N = 0; //size of the heap
        h = new int[maxSize + 1];
        dist = _dist;
        hPos = _hPos;
    }


    public bool isEmpty()
    {
        return N == 0;
    }

    //siftUp from position k the key or node value at position k
    //may be greater than that of its parent at k/2
    //k is a position in the heap array h
    public void siftUp(int k)
    {
        int v = h[k];

        h[0] = 0;  // put dummy vertes before top of heap
        dist[0] = int.MinValue;

        while (dist[v] < dist[h[k / 2]])
        {
            h[k] = h[k / 2];
            hPos[h[k]] = k;
            k = k / 2;
        }

        h[k] = v;
        hPos[v] = k;
    }

    //key of node at position k may be less than that of
    //its children and may need to be moved down some levels
    //k is a position in the heap array h
    public void siftDown(int k)
    {
        int v, j; //j = the index of the biggest child node, either left or right
        v = h[k];//stores the values temporarly

        while (k <= N / 2)  //while node at position k has a left child node
        {
            j = 2 * k;//index of the right child

            if (j < N && dist[h[j]] < dist[h[j + 1]])
            {
                ++j;
            }

            if (dist[v] >= dist[h[j]])
            {
                break;
            }

            h[k] = h[j];
            hPos[h[k]] = k;
            k = j;
        }

        h[k] = v;
        hPos[v] = k;
    }

    public void insert(int x)
    {
        h[++N] = x;
        siftUp(N);
    }


    public int remove()
    {
        int v = h[1];
        hPos[v] = 0; // v is no longer in heap
        h[N + 1] = 0;  // put null node into empty spot

        h[1] = h[N--];
        siftDown(1);

        return v;
    }
}  // end of Heap class


// Graph code to support Prim's MSt Alg
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

    // convert vertex into char
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


    // Prim's algorithm to compute MST
    // Code most of this yourself

    int[] MST_Prim(int s)
    {
        int v;
        int wgt_sum = 0;
        int[] dist, parent, hPos;
        Node t;

        //the distance from node to node 
        dist = new int[V + 1];
        //the parent node 
        parent = new int[V + 1];
        //current heap position 
        hPos = new int[V + 1];

        // initialising parent and position to zero, and dist to the max value 
        for (v = 1; v <= V; v++)
        {
            dist[v] = int.MaxValue;
            parent[v] = 0;
            hPos[v] = 0;
        }

        Heap heap = new Heap(V + 1, dist, hPos);
        heap.insert(s);
        dist[s] = 0;

        while (!heap.isEmpty())
        {
            v = heap.remove();


            //calculates the sum of the weights 
            wgt_sum += dist[v];
            dist[v] = -dist[v];

            Console.Write("\nAdding edge {0}--({1})--{2}", toChar(parent[v]), dist[v], toChar(v));

            for (t = adj[v]; t != z; t = t.next)
            {

                if (t.wgt < dist[t.vert])
                {
                    dist[t.vert] = t.wgt;
                    parent[t.vert] = v;

                    //If the vertex is empty, insert next vertex 
                    if (hPos[t.vert] == 0)
                    {
                        heap.insert(t.vert);
                    }
                    else //Else call sift up 
                    {
                        heap.siftUp(hPos[t.vert]);
                    }
                }
            }
        }

        Console.Write("\n\nWeight = {0}\n", wgt_sum);
        return parent;
    }



    public void showMST(int[] mst)
    {
        Console.Write("\n\nMinimum Spanning tree parent array is:\n");
        for (int v = 1; v <= V; ++v)
            Console.Write("{0} -> {1}\n", toChar(v), toChar(mst[v]));
        Console.WriteLine("");
    }


    public static void Main()
    {
        int s = 1;
        int[] mst;
        string fname = "myGraph.txt";

        Graph g = new Graph(fname);

        g.display();

        mst = g.MST_Prim(s);

        g.showMST(mst);

        Console.ReadLine();
    }

} // end of Graph class