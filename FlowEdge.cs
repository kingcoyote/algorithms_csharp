using System;

namespace Algorithms
{
    public class FlowEdge
    {
        public int From { get; private set; }
        public int To { get; private set; }
        public double Capacity { get; private set; }
        public double Flow { get; private set; }

        public FlowEdge(int @from, int to, double capacity, double flow = 0.0)
        {
            if (@from < 0)           throw new IndexOutOfRangeException();
            if (to < 0)              throw new IndexOutOfRangeException();
            if (!(capacity >= 0.0))  throw new ArgumentException();
            if (!(flow <= capacity)) throw new ArgumentException();
            if (!(flow >= 0.0))      throw new ArgumentException();

            From = @from;
            To = to;
            Capacity = capacity;
            Flow = flow;
        }

        public FlowEdge(FlowEdge e) : this (e.From, e.To, e.Capacity, e.Flow)
        {

        }

        public int Other(int vertex)
        {
            if (vertex == From) return To;
            if (vertex == To)   return From;
            
            throw new ArgumentException();
        }

        public double ResidualCapacityTo(int vertex)
        {
            if (vertex == From) return Flow;
            if (vertex == To)   return Capacity - Flow;

            throw new ArgumentException();
        }

        public void AddResidualFlowTo(int vertex, double delta)
        {
            if      (vertex == From) Flow -= delta;
            else if (vertex == To)   Flow += delta;
            else throw new ArgumentException();

            if (Double.IsNaN(delta)) throw new ArgumentException();
            if (!(Flow >= 0.0))      throw new ArgumentException();
            if (!(Flow <= Capacity)) throw new ArgumentException();
        }

        public new string ToString()
        {
            return From + "->" + To + " " + Flow + "/" + Capacity;
        }
    }
}
