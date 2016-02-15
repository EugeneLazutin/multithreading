using System;
using System.Threading;

namespace multithreding
{
    class ItemListLock : ItemList, IItemList
    {
        private object _lock = new object();
        public void Read(int limit)
        {
            NextIndex = 0;
            for (var i = 0; i < limit; i++)
            {
                new Thread(ReadThred).Start();
            }
        }

        private void WriteThread()
        {
            while (true)
            {
                lock (_lock)
                {
                    if (Id >= 1000)
                    {
                        break;
                    }
                    Add(new Item { Name = "Name" });
                }
            }
        }

        public void Write(int limit)
        {
            for (var i = 0; i < limit; i++)
            {
                new Thread(WriteThread).Start();
            }
        }

        private void ReadThred()
        {
            do
            {
                Item item;
                lock (_lock)
                {
                    item = GetNext();
                }
                if (item == null)
                {
                    Console.WriteLine($"Lock finished. Thread id = {Thread.CurrentThread.ManagedThreadId}");
                    break;
                }
                Console.WriteLine($"Thread id = {Thread.CurrentThread.ManagedThreadId}. Item = {item}");
            } while (true);
        }
    }
}
