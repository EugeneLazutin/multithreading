using System;
using System.Collections.Generic;

namespace multithreding
{
    class Program
    {

        static void Main()
        {
            var list = new List<IItemList>
            {
                new ItemListResetEvent(),
                new ItemListReaderWriterLock(),
                new ItemListMutex(),
                new ItemListLock()
            };

            int writeLimit = 2;
            int readLimit = 3;

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
