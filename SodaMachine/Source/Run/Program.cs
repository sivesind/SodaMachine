using SodaMachine.DomainModel;
using SodaMachine.Repository;

namespace SodaMachine.Run
{
    /// <summary>
    /// Bootloader to start the machine.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Instantiates the core domain classes and injects them into the sodamachine before starting it.
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            DomainModel.SodaMachine sodaMachine = new DomainModel.SodaMachine(new CashRegister(), new SodaRepository(), new UserInterface.UserInterfaceImplementation());
            sodaMachine.Start();
        }
    }
}
