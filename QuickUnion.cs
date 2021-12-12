using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    class QuickUnion
    {
        private readonly int[] _parent;
        private readonly int[] _size;
        public int Count { get; private set; }

        public QuickUnion(int n)
        {
            Count = n;
            _parent = new int[n];
            _size = new int[n];
            for (var i = 0; i < n; i++)
            {
                _parent[i] = i;
                _size[i] = 1;
            }
        }

        public int Find(int p)
        {
            while (p != _parent[p])
            {
                _parent[p] = _parent[_parent[p]];
                p = _parent[p];
            }
            return p;
        }

        public bool Connected(int p, int q)
        {
            return Find(p) == Find(q);
        }

        public void Union(int p, int q)
        {
            int rootP = Find(p);
            int rootQ = Find(q);
            if (rootP == rootQ) return;

            if (_size[rootP] < _size[rootQ])
            {
                _parent[rootP] = rootQ;
                _size[rootQ] += _size[rootP];
            }
            else
            {
                _parent[rootQ] = rootP;
                _size[rootP] += _size[rootQ];
            }
        }
    }
}
