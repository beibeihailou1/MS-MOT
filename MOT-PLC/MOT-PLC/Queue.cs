using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Queue
{
    class Queue
    {
        private ManualResetEvent mre = new ManualResetEvent(false);
        private ConcurrentQueue<int> queue = new ConcurrentQueue<int>();

        public void Wait()
        {
            mre.WaitOne();
        }


        public void Reset()
        {
            mre.Reset();
        }

        public void Enqueue(int state)
        {
            queue.Enqueue(state);
            mre.Set();
        }

        public bool Dequeue()
        {
            return queue.TryDequeue(out int state);
        }



    }
}
