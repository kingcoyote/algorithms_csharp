using System;
using System.Collections.Generic;

namespace Algorithms
{
    public class FordFulkerson
    {
        // current value of maxflow
        public double Value { get; private set; }

        // marked[v] = true iff s->v path in residual graph
        private bool[] _marked;
        // edgeTo[v] = last edge on shortest residual s->v path
        private FlowEdge[] _edgeTo;

        public FordFulkerson(FlowNetwork G, int s, int t)
        {
            Validate(s, G.V);
            Validate(t, G.V);
            if (s == t)               throw new ArgumentException("Source equals sink");
            if (!IsFeasible(G, s, t)) throw new ArgumentException("Initial flow is infeasible");

            Value = Excess(G, t);
            while (HasAugmentingPath(G, s, t))
            {
                var bottle = Double.PositiveInfinity;
                
                // compute bottleneck capacity;
                for (var v = t; v != s; v = _edgeTo[v].Other(v))
                {
                    if (_edgeTo != null) bottle = Math.Min(bottle, _edgeTo[v].ResidualCapacityTo(v));
                }

                // augment flow
                for (var v = t; v != s; v = _edgeTo[v].Other(v))
                {
                    _edgeTo[v].AddResidualFlowTo(v, bottle);
                }

                Value += bottle;
            }
        }

        /// <summary>
        /// Is vertex v on the s side of the minimum st-cut?
        /// </summary>
        /// <param name="v">The vertex to check</param>
        /// <returns>true if vertex v is on the s side of the mincut, 
        /// and false if vertex v is on the t side.</returns>
        public bool InCut(int v)
        {
            Validate(v, _marked.Length);
            return _marked[v];
        }

        private void Validate(int v1, int v2)
        {
            if (v1 < 0 || v1 >= v2) throw new IndexOutOfRangeException("Vertex " + v1 + " is not between 0 and " + (v2-1));
        }

        private bool HasAugmentingPath(FlowNetwork G, int s, int t)
        {
            _edgeTo = new FlowEdge[G.V];
            _marked = new bool[G.V];

            var queue = new Queue<int>();
            queue.Enqueue(s);
            _marked[s] = true;
            while (queue.Count > 0 && !_marked[t])
            {
                var v = queue.Dequeue();

                foreach (var e in G.Adj(v))
                {
                    var w = e.Other(v);

                    if (e.ResidualCapacityTo(w) > 0)
                    {
                        if (!_marked[w])
                        {
                            _edgeTo[w] = e;
                            _marked[w] = true;
                            queue.Enqueue(w);
                        }
                    }
                }
            }

            return _marked[t];
        }

        private double Excess(FlowNetwork G, int v)
        {
            var excess = 0.0;
            foreach (var e in G.Adj(v))
            {
                if (v == e.From) excess -= e.Flow;
                else             excess += e.Flow;
            }
            return excess;
        }

        private bool IsFeasible(FlowNetwork G, int s, int t)
        {
            for (var v = 0; v < G.V; v++)
            {
                foreach (var e in G.Adj(v))
                {
                    if (e.Flow < Double.Epsilon || e.Flow > e.Capacity + Double.Epsilon)
                    {
                        // Sytem.err.println("Edge does not satisfy capacity constraints: " + e);
                        return false;
                    }
                }
            }

            if (Math.Abs(Value + Excess(G, s)) > Double.Epsilon)
            {
                // System.err.println("Excess at source = " + Excess(G, s));
                // System.err.println("Mas flow = " + Value);
                return false;
            }

            if (Math.Abs(Value - Excess(G, t)) > Double.Epsilon)
            {
                //System.err.println("Excess at sink   = " + excess(G, t));
                //System.err.println("Max flow         = " + value);
                return false;
            }

            for (int v = 0; v < G.V; v++)
            {
                if (v == s || v == t) continue;
                
                if (Math.Abs(Excess(G, v)) > Double.Epsilon)
                {
                    //System.err.println("Net flow out of " + v + " doesn't equal zero");
                    return false;
                }
            }

            return true;
        }
    }
}
