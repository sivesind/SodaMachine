using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using SodaMachine.DomainModel.UserCommand;
using SodaMachine.DomainModel.UserCommand.Base;
using System;
using System.Collections.Generic;
using Xunit;

namespace SodaMachine.Test
{

    public class SodaMachineTest : BaseIntegrationTest
    {
        private readonly DomainModel.SodaMachine _sodaMachine;

        //TODO: add DI
        public SodaMachineTest() : base()
        {
            var commands = new List<IUserCommand>() { 
                new InsertCommand(_cashRegister, _sodaRepository, _userInterface),
                new OrderCommand(_cashRegister, _sodaRepository, _userInterface),
                new RecallCommand(_cashRegister, _sodaRepository, _userInterface),
                new SmsOrderCommand(_cashRegister, _sodaRepository, _userInterface)
            };
            _sodaMachine = new DomainModel.SodaMachine(_cashRegister, _sodaRepository, _userInterface, commands);
        }

        /// <summary>
        /// This tests discovery of all commands and dispatch to the correct command based on user input
        /// </summary>
        [Fact]
        public void SmokeTestInsertCommand()
        {
            //arrange
            _userInterface.ShowMainMenuAndWaitForInput(Arg.Any<List<string>>(), Arg.Any<uint>()).Returns("insert 40");

            //act
            _sodaMachine.Start(false);

            //assert
            _cashRegister.CurrentAmount.Should().Be(40);
        }

        /// <summary>
        /// Checks that errorhandling in outermost flow of control works.
        /// </summary>
        [Fact]
        public void ErrorHandlingInUserInteraction()
        {
            //arrange
            _userInterface.ShowMainMenuAndWaitForInput(Arg.Any<List<string>>(), Arg.Any<uint>()).Throws(new Exception("Testexception"));

            //act
            _sodaMachine.Start(false);

            //assert
            _userInterface.Received().ShowMessage(Arg.Is<string>(s => s.Contains("Error in sodamachine")));
            _userInterface.Received().ShowMessage(Arg.Is<string>(s => s.Contains("Testexception")));
        }


    }
}
