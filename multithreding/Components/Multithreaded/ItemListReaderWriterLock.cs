using System;
using System.Threading;

namespace multithreding.Components.Multithreaded
{
    class ItemListReaderWriterLock : ItemListMultithreaded
    {
        private readonly ReaderWriterLockSlim _rwl = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

        protected override void WriteThread()
        {
            while (true)
            {
                _rwl.EnterWriteLock();
                if (Id >= MaxCount)
                {
                    _rwl.ExitWriteLock();
                    break;
                }
                Add(new Item { Name = "Name" });
                _rwl.ExitWriteLock();
            }
        }

        protected override void ReadThread()
        {
            do
            {
                _rwl.EnterReadLock();
                var item = GetNextUnreaded();
                _rwl.ExitReadLock();
                if (item == null)
                {
                    Console.WriteLine($"ReaderWriterLock finished. Thread id = {Thread.CurrentThread.ManagedThreadId}");
                    break;
                }
                Console.WriteLine($"Thread id = {Thread.CurrentThread.ManagedThreadId}. Item = {item}");
            } while (true);
        }
    }
}
