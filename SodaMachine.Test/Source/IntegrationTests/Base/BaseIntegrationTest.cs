using NSubstitute;
using SodaMachine.DomainModel;
using SodaMachine.Repository;
using SodaMachine.UserInterface;

namespace SodaMachine.Test
{
    /// <summary>
    /// A pragmatic approach for testing is done here, the userinterface is mocked,
    /// other dependencies are used directly. 
    /// </summary>
    public class BaseIntegrationTest
    {
        protected SodaRepository _sodaRepository;
        protected CashRegister _cashRegister;
        protected IUserInterface _userInterface;

        public BaseIntegrationTest()
        {
            _sodaRepository = new SodaRepository("Source/UnitTests/Db/testinventory.json");
            _cashRegister = new CashRegister();
            _userInterface = Substitute.For<IUserInterface>();
        }
    }
}
