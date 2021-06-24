using NSubstitute;
using SodaMachine.DomainModel.UserCommand;
using Xunit;

namespace SodaMachine.Test
{
    public class OrderCommandTest : BaseIntegrationTest
    {
        private readonly OrderCommand _orderCommand;

        public OrderCommandTest() : base()
        {
            _orderCommand = new OrderCommand(_cashRegister, _sodaRepository, _userInterface);
        }


        [Fact]
        public void ExecutePositive()
        {
            //arrange
            _cashRegister.AddAmount("20");

            //act
            _orderCommand.Execute(new string[] { "order", "pepsi" });

            //assert
            _userInterface.Received().ShowMessage(Arg.Is<string>(s => s.Contains("pepsi out")));
            _userInterface.Received().ShowMessage(Arg.Is<string>(s => s.Contains("10 out in change")));
            _userInterface.Received().ShowMessage(Arg.Is<string>(s => s.Contains("pepsi out")));
        }

        [Fact]
        public void Execute_OutOfStock()
        {
            //arrange
            _cashRegister.AddAmount("10");

            //buy the last pepsi
            _orderCommand.Execute(new string[] { "order", "pepsi" });
            _userInterface.ClearReceivedCalls();

            //act, try to buy another pepsi when out
            _cashRegister.AddAmount("10");
            _orderCommand.Execute(new string[] { "order", "pepsi" });
            _userInterface.Received().ShowMessage(Arg.Is<string>(s => s.Contains("No pepsi left")));
        }

        [Fact]
        public void Execute_UnknownSoda()
        {
            //don't have beer
            _orderCommand.Execute(new string[] { "order", "beer" });

            _userInterface.Received().ShowMessage(Arg.Is<string>(s => s.Contains("No such soda")));
        }


        [Fact]
        public void Execute_LackingMoney()
        {
            //act
            _orderCommand.Execute(new string[] { "order", "pepsi" });

            //assert
            _userInterface.Received().ShowMessage(Arg.Is<string>(s => s.Contains("Need 10 more")));
        }

        [Fact]
        public void ExecuteNegative()
        {
            //act/assert negative
            _orderCommand.Execute(new string[] { "order", "" });
            _userInterface.Received().ShowMessage(Arg.Is<string>(s => s.Contains("No such soda")));
            _userInterface.ClearReceivedCalls();

            _orderCommand.Execute(new string[] { "order", null, "" });
            _userInterface.Received().ShowMessage(Arg.Is<string>(s => s.Contains("No such soda")));
            _userInterface.ClearReceivedCalls();

            _orderCommand.Execute(new string[] { "order", "", "pepsi" });
            _userInterface.Received().ShowMessage(Arg.Is<string>(s => s.Contains("No such soda")));
            _userInterface.ClearReceivedCalls();

        }


    }
}
