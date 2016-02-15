using System;
using System.Collections.Generic;

namespace multithreding
{
    public class ItemList
    {
        public ItemList()
        {
            List = new List<Item>();
        }

        public void Add(Item item)
        {
            item.Id = Id++;
            List.Add(item);
        }

        public Item GetNext()
        {
            if (NextIndex >= List.Count || NextIndex < 0)
            {
                return null;
            }
            return List[NextIndex++];
        }

        public void Show()
        {
            List.ForEach(Console.WriteLine);
        }

        public void Reset()
        {
            NextIndex = 0;
        }

        protected int NextIndex ;
        protected int Id;
        protected List<Item> List;
    }
}
