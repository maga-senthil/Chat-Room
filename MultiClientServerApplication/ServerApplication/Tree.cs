using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApplication
{
    class Tree
    {
        Node top;

        public Tree(int initial)
        {
            top = new Node(initial);
        }

        //public void Add(int value)
        //{
        //    if(top == null)
        //    {
        //        Node newNode = new Node(value);
        //        top = newNode;
        //        return;
        //    }
        //    Node currentNode = top;
        //    bool added = false;
        //    do
        //    {
        //        if (value < currentNode.value)
        //        {
        //            if (currentNode.left == null)
        //            {
        //                Node newNode = new Node(value);
        //                currentNode.left = newNode;
        //                added = true;
        //            }
        //            else
        //            {
        //                currentNode = currentNode.left;
        //            }

        //            if (value >= currentNode.value)
        //            {
        //                if (currentNode.right == null)
        //                {
        //                    Node newNode = new Node(value);
        //                    currentNode.right = newNode;
        //                    added = true;
        //                }
        //                else
        //                {
        //                    currentNode = currentNode.right;
        //                }
        //            }
        //        }
        //    } while (!added);
        //}

        public void AddRecursive(int value)
        {
            AddRecvalue(ref top, value);
        }
        private void AddRecvalue(ref Node N, int value)
        {
            if(N == null)
            {
                Node newNode = new Node(value);
                N = newNode;
                return;
            }
            if (value < N.value)
            {
                AddRecvalue(ref N.left, value);
                return;
            }      
            if(value >= N.value )
            {
                AddRecvalue(ref N.right, value);
                return;  
            }
           
        }

        public void send(Node N, ref string newstring)
        {
            if (N == null)
            {
                N = top;
            }
            if(N.left != null)
            {
                send(N.left, ref newstring );
                newstring = N.value.ToString().PadLeft(2);
            }
            else
            {
                newstring = N.value.ToString().PadLeft(2);
            }
            if(N.right != null)
            {
                send(N.right, ref newstring);
            }
        }
        
    }
}
