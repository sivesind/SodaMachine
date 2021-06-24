using SodaMachine.Repository;
using SodaMachine.UserInterface;

namespace SodaMachine.DomainModel.UserCommand.Base
{
    /// <summary>
    /// Common properties for usercommands. A usercommand is a command/action that the user can perform
    /// with the sodamachine.
    /// </summary>
    public abstract class UserCommandBase
    {
        protected CashRegister _cashRegister;
        protected SodaRepository _repository;
        protected IUserInterface _userInterface;

        public string CommandText { get; protected set; }
        public string MenuItemText { get; protected set; }

        /// <summary>
        /// All inheritors of this class must have a constructor with this signature. Set to protected
        /// to hint of that. Unfortunately there is no way to force a constructor signature.
        /// </summary>
        protected UserCommandBase(CashRegister cashRegister, SodaRepository repository, IUserInterface userInterface)
        {
            _cashRegister = cashRegister;
            _repository = repository;
            _userInterface = userInterface;
        }
    }
}
