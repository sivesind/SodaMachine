using SodaMachine.DomainModel.UserCommand.Base;
using SodaMachine.Repository;
using SodaMachine.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SodaMachine.DomainModel
{
    /// <summary>
    /// The shell of the SodaMachine, utilizing the available commands, the cashregister and the
    /// userinterface. 
    /// </summary>
    public class SodaMachine
    {
        private readonly CashRegister _cashRegister;
        private readonly IUserInterface _userInterface;
        private readonly IList<IUserCommand> _userCommands;
        private readonly SodaRepository _sodaRepository;

        public SodaMachine(CashRegister cashRegister, SodaRepository repository, IUserInterface userInterface, IEnumerable<IUserCommand> userCommands)
        {
            _cashRegister = cashRegister;
            _sodaRepository = repository;
            _userInterface = userInterface;
            _userCommands = userCommands.ToList();
        }

        /// <summary>
        /// This is the starter method for the machine, used for production. Tests can disable
        /// looping. The loop displays menu and current amount to user, and then handles one usercommand
        /// before repeating it all.
        /// </summary>
        /// <param name="runContinuously">Set to false for testing, only one iterration is run. Defaults 
        /// to true</param>
        public void Start(bool runContinuously = true)
        {
            do
            {
                try
                {
                    //show menu
                    string userInputString = _userInterface.ShowMainMenuAndWaitForInput(
                        _userCommands.Select(command => command.MenuItemText).ToList(),
                        _cashRegister.CurrentAmount);

                    //handle command in userinput, if given usercommand is supported, otherwise ignore it
                    foreach (IUserCommand userCommand in _userCommands)
                    {
                        if (userInputString.ToLower().StartsWith(userCommand.CommandText))
                        {
                            userCommand.Execute(userInputString.Split(' '));
                        }
                    }
                }
                catch (Exception unexpectedException)
                {
                    //unknown exception at outermost flow of control, displays errormessage to user
                    _userInterface.ShowMessage(
                        $"Error in sodamachine, contact sivesind@gmail.com, with " +
                        $"this info '{unexpectedException.Message}'");
                    _userInterface.ShowMessage("Press any key to continue and retry...");
                }
            }
            while (runContinuously);

        }
    }
}
