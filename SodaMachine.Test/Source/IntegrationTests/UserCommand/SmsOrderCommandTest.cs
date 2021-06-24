using NSubstitute;
using SodaMachine.DomainModel.UserCommand;
using Xunit;

namespace SodaMachine.Test
{
    public class SmsOrderCommandTest : BaseIntegrationTest
    {
        private readonly SmsOrderCommand _smsOrderCommand;

        public SmsOrderCommandTest() : base()
        {
            _smsOrderCommand = new SmsOrderCommand(_cashRegister, _sodaRepository, _userInterface);
        }

        [Fact]
        public void ExecutePositive()
        {
            //act positive
            _smsOrderCommand.Execute(new string[] { "sms", "order", "pepsi" });

            //assert
            _userInterface.Received().ShowMessage(Arg.Is<string>(s => s.Contains("pepsi out")));
        }

        [Fact]
        public void ExecuteNegative()
        {
            //act/assert negative
            _smsOrderCommand.Execute(new string[] { "smsorder", "pepsi" });
            _userInterface.DidNotReceive().ShowMessage(Arg.Any<string>());

            _smsOrderCommand.Execute(new string[] { "sms", "order", "" });
            _userInterface.DidNotReceive().ShowMessage(Arg.Any<string>());

            _smsOrderCommand.Execute(new string[] { "", "smsorder", "" });
            _userInterface.DidNotReceive().ShowMessage(Arg.Any<string>());

            _smsOrderCommand.Execute(new string[] { "sms", "order", "beer" });
            _userInterface.DidNotReceive().ShowMessage(Arg.Any<string>());
        }


    }
}
