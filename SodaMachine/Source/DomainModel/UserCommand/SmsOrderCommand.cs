using SodaMachine.Repository;
using SodaMachine.DomainModel.UserCommand.Base;
using SodaMachine.UserInterface;

namespace SodaMachine.DomainModel.UserCommand
{
    /// <summary>
    /// Handles ordering by sms, a simpler procedure than on-prem ordering. 
    /// </summary>
    public class SmsOrderCommand : UserCommandBase, IUserCommand
    {
        /// <summary>
        /// <see cref="UserCommandBase.UserCommandBase(CashRegister, SodaRepository, IUserInterface)"/>
        /// </summary>
        public SmsOrderCommand(CashRegister cashRegister, SodaRepository repository, IUserInterface userInterface) : base(cashRegister, repository, userInterface)
        {
            CommandText = "sms order";
            MenuItemText = $"{CommandText} ({_repository.ListSodaNames()}) - Order sent by sms";
        }

        /// <summary>
        /// Validates stock and returns a soda, without handling payment.
        /// </summary>
        /// <param name="userCommandLine"><see cref="IUserCommand.Execute(string[])"/></param>
        public void Execute(string[] userCommandLine)
        {
            string requestedSodaName = userCommandLine.Length > 2 ? userCommandLine[2] : "";
            if (
                _repository.ValidSodaName(requestedSodaName)
                &&
                _repository.HaveInStock(1, requestedSodaName))
            {
                _userInterface.ShowMessage($"Giving {requestedSodaName} out");
                _repository.RemoveFromStock(1, requestedSodaName);
            }
        }
    }
}
