using NSubstitute;
using SodaMachine.DomainModel.UserCommand;
using Xunit;

namespace SodaMachine.Test
{
    public class RecallCommandTest : BaseIntegrationTest
    {
        private readonly RecallCommand _recallCommand;

        public RecallCommandTest() : base()
        {
            _recallCommand = new RecallCommand(_cashRegister, _sodaRepository, _userInterface);
        }

        [Fact]
        public void ExecutePositive()
        {
            //arrange
            _cashRegister.AddAmount("10");

            //act positive
            _recallCommand.Execute(new string[] { "recall" });

            //assert
            _userInterface.Received().ShowMessage(Arg.Is<string>(s => s.Contains("10 to customer")));
        }

        [Fact]
        public void ExecuteNegative()
        {
            //arrange
            _cashRegister.AddAmount("10");

            //act negative
            _recallCommand.Execute(new string[] { "sdg" });

            //assert input is ignored
            _userInterface.Received().ShowMessage(Arg.Is<string>(s => s.Contains("10 to customer")));
        }


    }
}
