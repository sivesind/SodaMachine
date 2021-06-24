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
        private readonly SodaRepository _sodaRepository;

        public SodaMachine(CashRegister cashRegister, SodaRepository repository, IUserInterface userInterface)
        {
            _cashRegister = cashRegister;
            _sodaRepository = repository;
            _userInterface = userInterface;
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
            //get usercommands in machine
            List<IUserCommand> availableUserCommands = DiscoverUserCommands();
            do
            {
                try
                {
                    //show menu
                    string userInputString = _userInterface.ShowMainMenuAndWaitForInput(
                        availableUserCommands.Select(command => command.MenuItemText).ToList(),
                        _cashRegister.CurrentAmount);

                    //handle command in userinput, if given usercommand is supported, otherwise ignore it
                    foreach (IUserCommand userCommand in availableUserCommands)
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

        /// <summary>
        /// Emulate DI framework, it's considered overkill to add DI to entire app. This methods
        /// discovers implementations of <see cref="IUserCommand"/> and dynamically instantiates them. 
        /// This is done to minimize the process of adding new functions to the sodamachine.
        /// </summary>
        /// <returns>List of available usercommands in the machine</returns>
        private List<IUserCommand> DiscoverUserCommands()
        {
            //find all types for implementations of IUserCommand
            IEnumerable<Type> listUserCommandTypes = System.Reflection.Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(type => typeof(IUserCommand).IsAssignableFrom(type) && !type.IsInterface);

            List<IUserCommand> availableUserCommands = new List<IUserCommand>();

            //instantiate all usercommands and add to returnlist
            foreach (Type type in listUserCommandTypes)
            {
                IUserCommand instance = (IUserCommand)Activator.CreateInstance(type, new object[] { _cashRegister, _sodaRepository, _userInterface });
                availableUserCommands.Add(instance);
            }

            return availableUserCommands;
        }
    }
}
