namespace Core.Entities
{
    public class Device
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }
        public string Brand { get; private set; }
        public DateTime CreationTime { get; private set; }

        public Device(string name, 
            string brand)
        {
            Name = name;
            Brand = brand;

            Id = Guid.NewGuid();
            CreationTime = DateTime.Now;
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateBrand(string brand)
        {
            Brand = brand;
        }
    }
}
