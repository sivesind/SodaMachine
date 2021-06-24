using FluentAssertions;
using SodaMachine.DomainModel;
using SodaMachine.DomainModel.UserCommand.Base;
using SodaMachine.Repository;
using SodaMachine.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SodaMachine.Test
{
    /// <summary>
    /// All <see cref="IUserCommand"/> implementations must have a constructor on this form:
    /// Constructor(CashRegister cashRegister, SodaRepository repository, IUserInterface userInterface)
    /// in order to be discoverable and available to the user. This test ensures this rule.
    /// </summary>
    public class AssertCorrectConstructorsOnUserCommands
    {
        [Fact]
        public void AssertConstructorPresent()
        {
            //find all types for implementations of IUserCommand
            IEnumerable<Type> listUserCommandTypes = System.Reflection.Assembly.GetAssembly(typeof(IUserCommand))
                    .GetTypes()
                    .Where(type => typeof(IUserCommand).IsAssignableFrom(type) && !type.IsInterface);

            foreach (var userCommandType in listUserCommandTypes)
            {
                //Constructor(CashRegister cashRegister, SodaRepository repository, IUserInterface userInterface)
                var constructor = userCommandType.GetConstructor(new[] { typeof(CashRegister), typeof(SodaRepository), typeof(IUserInterface) });
                constructor.Should().NotBeNull(because: $"The constructor of {userCommandType.Name} must adhere to this siganture, " +
                    $"in order to be instantiated properly:" +
                    $"Const(CashRegister, SodaRepository, IUserInterface )");

            }
        }
    }
}
