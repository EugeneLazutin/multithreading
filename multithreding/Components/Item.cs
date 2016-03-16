namespace multithreding.Components
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Item() { }

        public Item(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"{{ Name: '{Name}', Id: {Id}}}";
        }
    }
}
