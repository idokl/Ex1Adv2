using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    //Credit to: http://stackoverflow.com/questions/102398/priority-queue-in-net
    public class PriorityQueue<T>
    {
        IComparer<T> comparer;
        T[] heap;
        public int Count { get; private set; }
        public PriorityQueue() : this(null) { }
        public PriorityQueue(int capacity) : this(capacity, null) { }
        public PriorityQueue(IComparer<T> comparer) : this(16, comparer) { }
        public PriorityQueue(int capacity, IComparer<T> comparer)
        {
            this.comparer = (comparer == null) ? Comparer<T>.Default : comparer;
            this.heap = new T[capacity];
        }
        public void push(T v)
        {
            if (Count >= heap.Length) Array.Resize(ref heap, Count * 2);
            heap[Count] = v;
            SiftUp(Count++);
        }
        public T pop()
        {
            var v = top();
            heap[0] = heap[--Count];
            if (Count > 0) SiftDown(0);
            return v;
        }
        public T top()
        {
            if (Count > 0) return heap[0];
            throw new InvalidOperationException("");
        }
        void SiftUp(int n)
        {
            var v = heap[n];
            for (var n2 = n / 2; n > 0 && comparer.Compare(v, heap[n2]) < 0; n = n2, n2 /= 2) heap[n] = heap[n2];
            heap[n] = v;
        }
        void SiftDown(int n)
        {
            var v = heap[n];
            for (var n2 = n * 2; n2 < Count; n = n2, n2 *= 2)
            {
                if (n2 + 1 < Count && comparer.Compare(heap[n2 + 1], heap[n2]) < 0) n2++;
                if (comparer.Compare(v, heap[n2]) <= 0) break;
                heap[n] = heap[n2];
            }
            heap[n] = v;
        }

        //my implementation (not efficient: O(n))
        public bool DoesContain(T element)
        {
            return heap.Contains(element);
        }

        //my implementation (not efficient: O(n))
        public void DecreaseKey(T oldElement, T newElement)
        {
            int indexOfElement = Array.IndexOf(heap, oldElement);
            heap[indexOfElement] = newElement;
            SiftUp(indexOfElement);
        }

        //my implementation (not efficient: O(n))
        public void AddElementOrTryToDecreaseItsKey(T element)
        {
            if (!heap.Contains(element))
            {
                this.push(element);
            }
            else
            {
                int indexOfElement = Array.IndexOf(heap, element);
                if (comparer.Compare(element, heap[indexOfElement]) < 0)
                {
                    heap[indexOfElement] = element;
                    SiftUp(indexOfElement);
                }
            }
        }

        //////////////////////////////////////////////////////////////////
    }
}
