using System;
using System.Threading;

namespace multithreding
{
    class ItemListResetEvent : ItemList, IItemList
    {
        private readonly AutoResetEvent _are = new AutoResetEvent(true);

        public void Write(int limit)
        {
            for (var i = 0; i < limit; i++)
            {
                new Thread(WriteThread).Start();
            }
        }

        private void WriteThread()
        {
            while (true)
            {
                _are.WaitOne();
                if (Id >= 1000)
                {
                    _are.Set();
                    break;
                }
                Add(new Item {Name = "Name"});
                _are.Set();
            }
        }

        public void Read(int limit)
        {
            NextIndex = 0;
            for (var i = 0; i < limit; i++)
            {
                new Thread(ReadThread).Start();
            }
        }

        private void ReadThread()
        {
            do
            {
                _are.WaitOne();
                var item = GetNext();
                _are.Set();
                if (item == null)
                {
                    Console.WriteLine($"AutoResetEvent finished. Thread id = {Thread.CurrentThread.ManagedThreadId}");
                    break;
                }
                Console.WriteLine($"Thread id = {Thread.CurrentThread.ManagedThreadId}. Item = {item}");
            } while (true);
        }
    }
}
