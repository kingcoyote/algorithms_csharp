using System;
using System.Collections;

namespace Algorithms {
    public class CC {
        public int count { get; private set; }          // number of connected components

        private bool[] _marked;   // marked[v] = has vertex v been marked?
        private int[] _id;           // id[v] = id of connected component containing v
        public int[] _size;         // size[id] = number of vertices in given component

        public CC(Graph graph) {
            _marked = new bool[graph.Vertices];
            _id = new int[graph.Vertices];
            _size = new int[graph.Vertices];
            for (int v = 0; v < graph.Vertices; v++) {
                if (!_marked[v]) {
                    dfs(graph, v);
                    count++;
                }
            }
        }

        // depth-first search for a Graph
        private void dfs(Graph graph, int v) {
            _marked[v] = true;
            _id[v] = count;
            _size[count]++;
            foreach (int w in graph.adj(v)) {
                if (!_marked[w]) {
                    dfs(graph, w);
                }
            }
        }

        public bool connected(int v, int w)
        {
            return _id[v] == _id[w];
        }

        public int size(int v)
        {
            return _size[_id[v]];
        }

        public int id(int v)
        {
            return _id[v];
        }
    }
}