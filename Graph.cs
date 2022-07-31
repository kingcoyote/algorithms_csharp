using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithms {
    public class Graph {
        public int Vertices { get; private set; }

        private List<int>[] Edges;
        
        public Graph(int verts) {
            Vertices = verts;
            Edges = new List<int>[Vertices];
            for (int v = 0; v < verts; v++) {
                Edges[v] = new List<int>();
            }
        }

        public static Graph From2DArray(int w, int h, bool allowDiag) {
            var graph = new Graph(w*h);

            for (int y = 0; y < h; y++) {
                for (int x = 0; x < w; x++) {
                
                    int v = y * w + x;

                    if (x > 0)     graph.addEdge(v, v-1);
                    if (x < w - 1) graph.addEdge(v, v+1);
                    if (y > 0)     graph.addEdge(v, v-w);
                    if (y < h - 1) graph.addEdge(v, v+w);

                    if (!allowDiag) continue;
                }
            }

            return graph;
        }

        public void addEdge(int v, int w) {
            if (Edges[v].Contains(w) == false) Edges[v].Add(w);
            if (Edges[w].Contains(v) == false) Edges[w].Add(v);
        }

        public List<int> adj(int v) {
            return Edges[v];
        }
    }
}