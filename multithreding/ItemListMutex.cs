using System;
using System.Threading;

namespace multithreding
{
    class ItemListMutex : ItemList, IItemList
    {

        private readonly Mutex _mutex = new Mutex();
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
                _mutex.WaitOne();
                if (Id >= 1000)
                {
                    _mutex.ReleaseMutex();
                    break;
                }
                Add(new Item { Name = "Name" });
                _mutex.ReleaseMutex();
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
                _mutex.WaitOne();
                var item = GetNext();
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
