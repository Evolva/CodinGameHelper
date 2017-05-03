using System;
using System.Collections.Generic;
using System.Linq;
using Datastructures;

// ReSharper disable once CheckNamespace
namespace Algorithms.PathFinding.AStar
{
    public static class AStar
    {
        public static IEnumerable<TResult> FindPath<TNode, TResult>(
            TNode start,
            TNode goal,
            Func<TNode, TNode, bool> doesReachGoal,
            Func<TNode, TNode, double> cost,
            Func<TNode, TNode, double> heuristic,
            Func<TNode, IEnumerable<TNode>> getNeighbors,
            Func<TNode, TResult> backtrackSelector)
        {
            var frontier = new PriorityQueue<TNode, double>(ComparerOrder.Default);
            frontier.Enqueue(start, 0);
            var cameFrom = new Dictionary<TNode, TNode>();
            var costSoFar = new Dictionary<TNode, double>();
            cameFrom.Add(start, default(TNode));
            costSoFar[start] = 0;

            while (frontier.Any())
            {
                var current = frontier.Dequeue();

                if (doesReachGoal(current, goal))
                {
                    var stack = new Stack<TNode>();

                    while (current != null)
                    {
                        stack.Push(current);
                        current = cameFrom[current];
                    }

                    return stack.Skip(1).Select(backtrackSelector);
                }

                foreach (var next in getNeighbors(current))
                {
                    var newCost = costSoFar[current] + cost(current, next);

                    if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                    {
                        costSoFar[next] = newCost;
                        var priority = newCost + heuristic(goal, next);
                        frontier.Enqueue(next, priority);
                        cameFrom[next] = current;
                    }
                }
            }

            return Enumerable.Empty<TResult>();
        }
    }
}