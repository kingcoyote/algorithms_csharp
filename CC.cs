using System;
using System.Collections;

namespace Algorithms {
    public class CC {
        private bool[] marked;   // marked[v] = has vertex v been marked?
        public int[] id;           // id[v] = id of connected component containing v
        private int[] size;         // size[id] = number of vertices in given component
        public int count {get; private set;}          // number of connected components

        public CC(Graph graph) {
            marked = new bool[graph.Vertices];
            id = new int[graph.Vertices];
            size = new int[graph.Vertices];
            for (int v = 0; v < graph.Vertices; v++) {
                if (!marked[v]) {
                    dfs(graph, v);
                    count++;
                }
            }
        }

        // depth-first search for a Graph
        private void dfs(Graph graph, int v) {
            marked[v] = true;
            id[v] = count;
            size[count]++;
            foreach (int w in graph.adj(v)) {
                if (!marked[w]) {
                    dfs(graph, w);
                }
            }
        }

        public bool connected(int v, int w) {
            return id[v] == id[w];
        }
    }
}