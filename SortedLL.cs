// Sorted linked list with a sentinel node
using System;

class SortedLL
{
    // internal data structure
	class Node 
	{
        public int data;
        public Node next;
    }

	private Node z;
    private Node head;
	private Node curr;
	private Node prev;

    // constructor 
	public SortedLL() 
	{
        z = new Node();
		curr = new Node();
		prev = new Node();
		z.data = int.MaxValue;
        z.next = z;
        head = z;
    }
    
    public void insert(int x)
    {
        Node temp;
		temp = new Node();
        temp.data = x;	

		if (head == head.next)
		{
			temp.next = z;
			head = temp;
		}
		else
		{
			prev = head;
			curr = head.next;

			//Console.WriteLine("prev = {0}\t curr = {1} \t z = {2}",prev.data, curr.data, z.data);

		while(prev.data != curr.data || curr != z)
		{
			if( head.data > temp.data)
			{
				temp.next = prev;
				head = temp;
				//Console.WriteLine("head = {0}", head.data);

			}
			if (prev.data < temp.data && temp.data <= curr.data)
			{
				temp.next = curr;
				prev.next = temp;
			}

			curr = curr.next;
			prev = prev.next;

			//Console.WriteLine("prev = {0}\t curr = {1} \t temp = {2}",prev.data, curr.data, temp.data);
		}
		}

    }

    public void display()
    {
        Node t = head;
        Console.Write("\nHead -> ");
        while( t != z) {
            Console.Write("{0} -> ", t.data);
            t = t.next;
        }
        Console.Write("Z\n");
    }

	public bool isEmpty() 
	{
        return head == head.next;
    }
    
    public static void Main()
    {
        SortedLL list = new SortedLL();
        list.display();

        int i, x;
        Random r = new Random();
        
        for(i=0; i<10; ++i) {
            x = r.Next(20);
            list.insert(x);
            Console.Write("\nInserting {0}", x);
            list.display();
        }
        Console.ReadKey();
    }
}
