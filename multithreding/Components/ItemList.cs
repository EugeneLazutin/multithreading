using System;
using System.Collections.Generic;

namespace multithreding.Components
{
    public class ItemList
    {
        protected const int MaxCount = 1000;
        protected int NextUnreadedIndex;
        protected int Id;
        protected List<Item> List;

        public ItemList()
        {
            List = new List<Item>();
        }

        public void Add(Item item)
        {
            item.Id = Id++;
            List.Add(item);
        }

        public Item GetNextUnreaded()
        {
            if (NextUnreadedIndex >= List.Count || NextUnreadedIndex < 0)
            {
                return null;
            }
            return List[NextUnreadedIndex++];
        }

        public void Show()
        {
            List.ForEach(Console.WriteLine);
        }

        public void Reset()
        {
            NextUnreadedIndex = 0;
        }
    }
}
