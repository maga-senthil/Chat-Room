using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApplication
{
    class Node
    {
        public int value;
        public Node right;
        public Node left;

        public Node (int initial)
        {
            value = initial;
            left = null;
            right = null;
        }
    }
}
