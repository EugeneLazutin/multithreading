using System.Threading;
using multithreding.Interfaces;

namespace multithreding.Components.Multithreaded
{
    public abstract class ItemListMultithreaded : ItemList, IItemList
    {
        protected abstract void ReadThread();
        protected abstract void WriteThread();

        public void Write(int limit)
        {
            for (var i = 0; i < limit; i++)
            {
                new Thread(WriteThread).Start();
            }
        }

        public void Read(int limit)
        {
            NextUnreadedIndex = 0;
            for (var i = 0; i < limit; i++)
            {
                new Thread(ReadThread).Start();
            }
        }


    }
}
