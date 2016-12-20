using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace BT
{
    public class Node
    {
        public float f;
        public object obj = null;
        public Node parent = null;
        public Node left = null;
        public Node right = null;
        public Node pre = null;
        public Node next = null;
    }
    public class BinaryTree
    {
        Node root = null;
        Node head = null;
        int count = 0;
        public void Push(float t, object obj)
        {
            count++;
            Node n = new Node();
            n.f = t;
            n.obj = obj;
            if (root == null)
            {
                root = n;
                head = n;
            }
            else
            {
                InsertNode(root, n);
            }
        }
        public object Pop()
        {
            Node n = PopNode();
            if (n == null)
            {
                return null;
            }
            return n.obj;
        }
        public Node PopNode()
        {
            if (head == null)
            {
                return null;
            }
            count--;
            Node n = head;
            if (head.parent == null)
            {
                root = root.right;
                if (root != null)
                {
                    root.parent = null;
                }
            }
            else
            {
                head.parent.left = head.right;
                if (head.right != null)
                {
                    head.right.parent = head.parent;
                }
            }
            head = head.next;
            if (head != null)
            {
                head.pre = null;
            }
            return n;
        }
        void InsertNode(Node parent, Node n)
        {
            if (n.f < parent.f)
            {
                if (parent.left == null)
                {
                    parent.left = n;
                    n.parent = parent;

                    n.pre = parent.pre;
                    n.next = parent;

                    if (parent.pre == null)
                    {
                        head = n;
                    }
                    else
                    {
                        parent.pre.next = n;

                    }
                    parent.pre = n;
                    return;
                }
                else
                {
                    InsertNode(parent.left, n);
                }

            }
            else
            {
                if (parent.right == null)
                {
                    parent.right = n;
                    n.parent = parent;

                    n.pre = parent;
                    n.next = parent.next;

                    if (parent.next != null)
                    {
                        parent.next.pre = n;

                    }
                    parent.next = n;
                    return;
                }
                else
                {
                    InsertNode(parent.right, n);
                }

            }
        }
        public int Count
        {
            get
            {
                return count;
            }
        }
        public void DebugPrint()
        {
            int iPrint = 0;
            //Debug.Log("begin");
            Node n = head;
            while (true)
            {
                if (n == null)
                {
                    break;
                }
                //lst.Remove(n);
                //Debug.Log(n.obj + "   "+n.f);
                n = n.next;
                iPrint++;
            }
            //Debug.Log(iPrint+"end");
            /*
            foreach(Node node in lst)
            {
                Debug.Log(node.obj + "   "+node.f);
                bool b =	FindNode(root,node);
                Debug.Log(b);
                if(!b)
                {
                    Push(node.f,node.obj);
                }
            }
            */
        }
        bool FindNode(Node parent, Node n)
        {
            if (parent == null)
            {
                return false;
            }
            if (parent == n)
            {
                return true;
            }
            return FindNode(parent.right, n) || FindNode(parent.left, n);
        }
        public void Clear()
        {
            head = null;
            root = null;
            count = 0;
        }
        public Node HeadNode()
        {
            return head;
        }
        static System.Diagnostics.Stopwatch watch =null;
        public static void BeginWatch()
        {
            watch= new Stopwatch();
            watch.Start();
        }
        public static long EndWatch()
        {
            watch.Stop();
            long tick   =   watch.ElapsedTicks;
            watch=null;
            return tick;
        }
    }

}
