using System;
using System.Threading;

namespace multithreding.Components.Multithreaded
{
    class ItemListResetEvent : ItemListMultithreaded
    {
        private readonly AutoResetEvent _are = new AutoResetEvent(true);

        protected override void WriteThread()
        {
            while (true)
            {
                _are.WaitOne();
                if (Id >= MaxCount)
                {
                    _are.Set();
                    break;
                }
                Add(new Item { Name = "Name" });
                _are.Set();
            }
        }

        protected override void ReadThread()
        {
            do
            {
                _are.WaitOne();
                var item = GetNextUnreaded();
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
