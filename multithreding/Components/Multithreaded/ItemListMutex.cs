using System;
using System.Threading;

namespace multithreding.Components.Multithreaded
{
    class ItemListMutex : ItemListMultithreaded
    {

        private readonly Mutex _mutex = new Mutex();

        protected override void WriteThread()
        {
            while (true)
            {
                _mutex.WaitOne();
                if (Id >= MaxCount)
                {
                    _mutex.ReleaseMutex();
                    break;
                }
                Add(new Item { Name = "Name" });
                _mutex.ReleaseMutex();
            }
        }

        protected override void ReadThread()
        {
            do
            {
                _mutex.WaitOne();
                var item = GetNextUnreaded();
                _mutex.ReleaseMutex();
                if (item == null)
                {
                    Console.WriteLine($"Mutex finished. Thread id = {Thread.CurrentThread.ManagedThreadId}");
                    break;
                }
                Console.WriteLine($"Thread id = {Thread.CurrentThread.ManagedThreadId}. Item = {item}");
            } while (true);
        }
    }
}
