using SodaMachine.DomainModel.UserCommand.Base;
using SodaMachine.Repository;
using SodaMachine.UserInterface;

namespace SodaMachine.DomainModel.UserCommand
{
    /// <summary>
    /// Handles the main flow of the machine, the ordering of a soda.
    /// </summary>
    public class OrderCommand : UserCommandBase, IUserCommand
    {
        /// <summary>
        /// <see cref="UserCommandBase.UserCommandBase(CashRegister, SodaRepository, IUserInterface)"/>
        /// </summary>
        public OrderCommand(CashRegister cashRegister, SodaRepository repository, IUserInterface userInterface) : base(cashRegister, repository, userInterface)
        {
            CommandText = "order";
            MenuItemText = $"{CommandText} ({_repository.ListSodaNames()}) - Order from machine's buttons";
        }

        /// <summary>
        /// Checks amount of entered cash, checks inventory, gets soda, updates inventory and returns change
        /// to the user.
        /// </summary>
        /// <param name="userCommandLine"><see cref="IUserCommand.Execute(string[])"/></param>
        public void Execute(string[] userCommandLine)
        {
            string requestedSodaName = userCommandLine.Length > 1 ? userCommandLine[1] : "";

            if (!_repository.ValidSodaName(requestedSodaName))
            {
                _userInterface.ShowMessage($"No such soda");
                _cashRegister.ReturnEntireAmount();
                return;
            }

            if (!_repository.HaveInStock(1, requestedSodaName, out Soda sodaInStock))
            {
                _userInterface.ShowMessage($"No {requestedSodaName} left");
                return;
            }

            //soda in stock, check if enough money inserted
            if (_cashRegister.CurrentAmount < sodaInStock.Price)
            {
                _userInterface.ShowMessage($"Need {sodaInStock.Price - _cashRegister.CurrentAmount} more");
                return;
            }

            //close deal
            _userInterface.ShowMessage($"Giving {requestedSodaName} out");
            _repository.RemoveFromStock(1, sodaInStock.Name);
            uint change = _cashRegister.GetChangeForTotal(sodaInStock.Price);
            if (change > 0)
            {
                _userInterface.ShowMessage($"Giving {change} out in change");
            }
            _cashRegister.ReturnEntireAmount();
        }
    }
}
