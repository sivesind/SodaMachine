using SodaMachine.DomainModel.UserCommand.Base;
using SodaMachine.Repository;
using SodaMachine.UserInterface;

namespace SodaMachine.DomainModel.UserCommand
{
    /// <summary>
    /// Insert money into the sodamachine.
    /// </summary>
    public class InsertCommand : UserCommandBase, IUserCommand
    {
        /// <summary>
        /// <see cref="UserCommandBase.UserCommandBase(CashRegister, SodaRepository, IUserInterface)"/>
        /// </summary>
        public InsertCommand(CashRegister cashRegister, SodaRepository repository, IUserInterface userInterface) : base(cashRegister, repository, userInterface)
        {
            CommandText = "insert";
            MenuItemText = $"{CommandText} (money) - Money put into money slot";
        }

        /// <summary>
        /// Validates amount in commandline, adds it if valid.
        /// </summary>
        /// <param name="userCommandLine"><see cref="IUserCommand.Execute(string[])"/></param>
        public void Execute(string[] userCommandLine)
        {
            string amount = userCommandLine.Length > 1 ? userCommandLine[1] : "";
            if (_cashRegister.AddAmount(amount))
            {
                _userInterface.ShowMessage($"Adding {amount} to credit");
            }
            else
            {
                //If handling of invalid amount string should be added as a user requirement, do it here
                //not done now since task is refactor only, not changing behaviour
            }
        }
    }
}
