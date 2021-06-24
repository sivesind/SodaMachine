namespace SodaMachine.DomainModel
{
    /// <summary>
    /// The register in the machine, amount can be added and the removed entirely.
    /// </summary>
    public class CashRegister
    {
        //current amount of cash in machine
        private uint _amount = 0;

        /// <summary>
        /// Current amount of cash in machine
        /// </summary>
        public uint CurrentAmount => _amount;

        /// <summary>
        /// Parses an inputstring, if it representsd a valid amount it is added.
        /// </summary>
        /// <returns>true if amountstring was valid and therefore added</returns>
        public bool AddAmount(string amountAsString)
        {
            var parsedOk = uint.TryParse(amountAsString, out var validAmount);
            if (parsedOk)
            {
                _amount += validAmount;
            }
            return parsedOk;
        }

        /// <summary>
        /// Sets amount to zero, everything returned to user.
        /// </summary>
        public void ReturnEntireAmount()
        {
            _amount = 0;
        }

        /// <summary>
        /// Calcucate change for a given price.
        /// </summary>
        public uint GetChangeForTotal(uint price)
        {
            if (price > _amount)
            {
                return 0;
            }
            return _amount - price;
        }
    }
}
