using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace Datastructures
{
    public class PriorityQueue<TElement, TPriority> : IEnumerable<TElement>
        where TPriority : IComparable<TPriority>
    {
        private readonly SortedDictionary<TPriority, Queue<TElement>> _dict;

        public PriorityQueue(ComparerOrder comparerOrder)
        {
            var defaultComparer = Comparer<TPriority>.Default;
            var comparer = comparerOrder == ComparerOrder.Default
                ? defaultComparer
                : Comparer<TPriority>.Create((x, y) => defaultComparer.Compare(y, x));

            _dict = new SortedDictionary<TPriority, Queue<TElement>>(comparer);
        }

        public void Enqueue(TElement element, TPriority priority)
        {
            if (!_dict.ContainsKey(priority))
            {
                _dict.Add(priority, new Queue<TElement>());
            }

            _dict[priority].Enqueue(element);
        }

        public TElement Peek()
        {
            if (!_dict.Any()) throw new InvalidOperationException("the queue is empty");

            return _dict.First().Value.Peek();
        }

        public TElement Dequeue()
        {
            if (!_dict.Any()) throw new InvalidOperationException("the queue is empty");

            var queueWithPriority = _dict.First();

            var priority = queueWithPriority.Key;
            var queue = queueWithPriority.Value;

            var result = queue.Dequeue();

            if (!queue.Any())
            {
                _dict.Remove(priority);
            }

            return result;
        }


        public IEnumerator<TElement> GetEnumerator()
        {
            return _dict.Values.SelectMany(queue => queue).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public enum ComparerOrder
    {
        Default,
        Reverse
    }
}