using System;
using System.Threading;

namespace multithreding.Components.Multithreaded
{
    class ItemListLock : ItemListMultithreaded
    {
        private readonly object _lock = new object();

        protected override void WriteThread()
        {
            while (true)
            {
                lock (_lock)
                {
                    if (Id >= MaxCount)
                    {
                        break;
                    }
                    Add(new Item { Name = "Name" });
                }
            }
        }

        protected override void ReadThread()
        {
            do
            {
                Item item;
                lock (_lock)
                {
                    item = GetNextUnreaded();
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
