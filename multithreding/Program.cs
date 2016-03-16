using System;
using System.Collections.Generic;
using multithreding.Components.Multithreaded;
using multithreding.Interfaces;

namespace multithreding
{
    class Program
    {

        static void Main()
        {
            const int writeLimit = 2;
            const int readLimit = 3;

            var list = new List<IItemList>
            {
                new ItemListResetEvent(),
                new ItemListReaderWriterLock(),
                new ItemListMutex(),
                new ItemListLock()
            };

            foreach (var item in list)
            {
                item.Write(writeLimit);
                item.Read(readLimit);
                Console.ReadKey();
            }
            Console.WriteLine("\nThe End!");
            Console.ReadKey();
        }
    }
}
