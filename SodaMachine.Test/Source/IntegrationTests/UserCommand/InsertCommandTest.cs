using FluentAssertions;
using NSubstitute;
using SodaMachine.DomainModel.UserCommand;
using Xunit;

namespace SodaMachine.Test
{
    public class InsertCommandTest : BaseIntegrationTest
    {
        private readonly InsertCommand _insertCommand;

        public InsertCommandTest() : base()
        {
            _insertCommand = new InsertCommand(_cashRegister, _sodaRepository, _userInterface);
        }

        [Fact]
        public void ExecutePositive()
        {
            //act positive
            _insertCommand.Execute(new string[] { "insert", "10" });

            //assert
            _userInterface.Received().ShowMessage(Arg.Is<string>(s => s.Contains("10 to credit")));
            _cashRegister.CurrentAmount.Should().Be(10);
        }

        [Fact]
        public void ExecuteNegative()
        {
            //act/assert negative
            _insertCommand.Execute(new string[] { "insert", "sdg", "10" });
            _userInterface.DidNotReceive().ShowMessage(Arg.Any<string>());
            _cashRegister.CurrentAmount.Should().Be(0);

            //act/assert negative
            _insertCommand.Execute(new string[] { "insert" });
            _userInterface.DidNotReceive().ShowMessage(Arg.Any<string>());
            _cashRegister.CurrentAmount.Should().Be(0);

            _insertCommand.Execute(new string[] { "" });
            _userInterface.DidNotReceive().ShowMessage(Arg.Any<string>());
            _cashRegister.CurrentAmount.Should().Be(0);
        }


    }
}
