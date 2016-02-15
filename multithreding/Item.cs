namespace multithreding
{
    public class Item
    {
        public Item(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Item(){}

        public override string ToString()
        {
            return $"{{ Name: '{Name}', Id: {Id}}}";
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
