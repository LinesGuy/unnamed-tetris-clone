using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTetris
{
    public class BagRandomizer
    {
        private Stack<int> _bag = new Stack<int>();
        public int Next()
        {
            if (_bag.Count == 0)
            {
                int[] pieces = { 0, 1, 2, 3, 4, 5, 6 };
                Random random = new Random();
                pieces = pieces.OrderBy(p => random.Next()).ToArray();
                foreach(int p in pieces)
                {
                    _bag.Push(p);
                }
            }
            return _bag.Pop();
        }
    }
}
