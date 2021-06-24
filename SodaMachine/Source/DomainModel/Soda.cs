namespace SodaMachine.DomainModel
{
    /// <summary>
    /// The Soda domain oject, given by a name and a price.
    /// </summary>
    public class Soda
    {
        public Soda(string name, uint price)
        {
            Name = name;
            Price = price;
        }
        /// <summary>
        /// Name and unique identifier of a sodatype.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The price of on unit of the soda.
        /// </summary>
        public uint Price { get; }
    }
}
