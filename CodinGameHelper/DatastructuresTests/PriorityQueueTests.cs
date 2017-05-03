using System;
using Datastructures;
using NFluent;
using NUnit.Framework;

namespace DatastructuresTests
{
    public class PriorityQueueTests
    {
        [Test]
        public void Must_Return_Smallest_Priority_When_Using_CamparerOrder_Default()
        {
            var prioQueue = new PriorityQueue<string, int>(ComparerOrder.Default);

            prioQueue.Enqueue("fizz", 3);
            prioQueue.Enqueue("1", 1);
            prioQueue.Enqueue("2", 2);
            prioQueue.Enqueue("buzz", 5);
            prioQueue.Enqueue("4", 4);

            Check.That(prioQueue).ContainsExactly("1", "2", "fizz", "4", "buzz");

            Check.That(prioQueue.Peek()).IsEqualTo("1");
            Check.That(prioQueue.Dequeue()).IsEqualTo("1");

            Check.That(prioQueue.Peek()).IsEqualTo("2");
            Check.That(prioQueue.Dequeue()).IsEqualTo("2");

            Check.That(prioQueue.Peek()).IsEqualTo("fizz");
            Check.That(prioQueue.Dequeue()).IsEqualTo("fizz");

            Check.That(prioQueue.Peek()).IsEqualTo("4");
            Check.That(prioQueue.Dequeue()).IsEqualTo("4");

            Check.That(prioQueue.Peek()).IsEqualTo("buzz");
            Check.That(prioQueue.Dequeue()).IsEqualTo("buzz");

            Check.That(prioQueue).IsEmpty();

            Check.ThatCode(() => prioQueue.Peek()).Throws<InvalidOperationException>();
            Check.ThatCode(() => prioQueue.Dequeue()).Throws<InvalidOperationException>();
        }

        [Test]
        public void Must_Return_Highest_Priority_When_Using_CamparerOrder_Reverse()
        {
            var prioQueue = new PriorityQueue<string, int>(ComparerOrder.Reverse);

            prioQueue.Enqueue("fizz", 3);
            prioQueue.Enqueue("1", 1);
            prioQueue.Enqueue("2", 2);
            prioQueue.Enqueue("buzz", 5);
            prioQueue.Enqueue("4", 4);

            Check.That(prioQueue).ContainsExactly("buzz", "4", "fizz", "2", "1");
        }

        [Test]
        public void Must_Be_Stable()
        {
            var prioQueue = new PriorityQueue<string, int>(ComparerOrder.Reverse);

            prioQueue.Enqueue("1", 0);
            prioQueue.Enqueue("2", 0);
            prioQueue.Enqueue("fizz", 0);
            prioQueue.Enqueue("4", 0);
            prioQueue.Enqueue("buzz", 0);

            Check.That(prioQueue).ContainsExactly("1", "2", "fizz", "4", "buzz");
        }
    }
}