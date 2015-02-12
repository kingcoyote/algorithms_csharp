using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    public class FlowNetwork
    {
        public int V { get; private set; }
        public int E { get; private set; }
        
        private readonly List<FlowEdge>[] _adj;

        public FlowNetwork(int v)
        {
            if (V < 0) throw new ArgumentException();
            V = v;
            E = 0;
            _adj = new List<FlowEdge>[V];
            for (var i = 0; i < V; i++)
            {
                _adj[i] = new List<FlowEdge>();
            }
        }

        public void AddEdge(FlowEdge e)
        {
            int v = e.From;
            int w = e.To;
            ValidateVertex(v);
            ValidateVertex(w);
            _adj[v].Add(e);
            _adj[w].Add(e);
            E++;
        }

        public IEnumerable<FlowEdge> Adj(int v)
        {
            ValidateVertex(v);
            return _adj[v];
        }

        public IEnumerable<FlowEdge> Edges()
        {
            var list = new List<FlowEdge>();
            for (var i = 0; i < V; i++)
            {
                foreach (var e in _adj[i])
                {
                    if (e.To != i)
                    {
                        list.Add(e);
                    }
                }
            }
            return list;
        }

        public new string ToString()
        {
            var s = new StringBuilder();
            for (var v = 0; v < V; v++)
            {
                s.Append(v + ": ");
                foreach (var e in _adj[v])
                {
                    if (e.To != v) s.Append(e + " ");
                }
                s.AppendLine();
            }
            return s.ToString();
        }

        private void ValidateVertex(int v)
        {
            if (v < 0 || v >= V) throw new IndexOutOfRangeException();
        }


    }
}
