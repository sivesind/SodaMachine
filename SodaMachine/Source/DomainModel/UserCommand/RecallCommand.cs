using SodaMachine.DomainModel.UserCommand.Base;
using SodaMachine.Repository;
using SodaMachine.UserInterface;

namespace SodaMachine.DomainModel.UserCommand
{
    /// <summary>
    /// Like a cancelbutton, thisd command returns the remaining amount to the user.
    /// </summary>
    public class RecallCommand : UserCommandBase, IUserCommand
    {
        /// <summary>
        /// <see cref="UserCommandBase.UserCommandBase(CashRegister, SodaRepository, IUserInterface)"/>
        /// </summary>
        public RecallCommand(CashRegister cashRegister, SodaRepository repository, IUserInterface userInterface) : base(cashRegister, repository, userInterface)
        {
            CommandText = "recall";
            MenuItemText = $"{CommandText} - gives money back";
        }

        /// <summary>
        /// Simple method, sets the amount to 0.
        /// </summary>
        /// <param name="userCommandLine"><see cref="IUserCommand.Execute(string[])"/></param>
        public void Execute(string[] userCommandLine)
        {
            //Give money back
            _userInterface.ShowMessage("Returning " + _cashRegister.CurrentAmount + " to customer");
            _cashRegister.ReturnEntireAmount();
        }
    }
}
