using System;
using System.Threading;

namespace multithreding
{
    class ItemListReaderWriterLock : ItemList, IItemList
    {
        private readonly ReaderWriterLockSlim _rwl = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

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
                _rwl.EnterWriteLock();
                if (Id >= 1000)
                {
                    _rwl.ExitWriteLock();
                    break;
                }
                Add(new Item { Name = "Name" });
                _rwl.ExitWriteLock();
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
                _rwl.EnterReadLock();
                var item = GetNext();
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
