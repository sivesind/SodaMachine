using FluentAssertions;
using SodaMachine.DomainModel;
using SodaMachine.Repository;
using System;
using Xunit;

namespace SodaMachine.Test
{

    public class CashRegisterTest
    {
        private CashRegister _cashRegister = new CashRegister();

        [Fact]
        public void Init()
        {
            //assert
            _cashRegister.CurrentAmount.Should().Be(0, because: "No cash inserted on startup");

        }

        [Fact]
        public void AddAmountNegative()
{
            //assert negative
            _cashRegister
                .Invoking(register=> register.AddAmount(null))
                .Should().NotThrow<Exception>(because: "Null argument should be ignored");
            _cashRegister.CurrentAmount.Should().Be(0, because: "Invalid input ignored and not adding to amount");
            _cashRegister
                .Invoking(register => register.AddAmount(""))
                .Should().NotThrow<Exception>(because: "Empty argument should be ignored");
            _cashRegister.CurrentAmount.Should().Be(0, because: "Invalid input ignored and not adding to amount");
            _cashRegister
                .Invoking(register => register.AddAmount("1.3"))
                .Should().NotThrow<Exception>(because: "Wrong format argument should be ignored");
            _cashRegister.CurrentAmount.Should().Be(0, because: "Invalid input ignored and not adding to amount");
            _cashRegister
                .Invoking(register => register.AddAmount("-1"))
                .Should().NotThrow<Exception>(because: "Negative argument should be ignored");
            _cashRegister.CurrentAmount.Should().Be(0, because: "Invalid input ignored and not adding to amount");
            _cashRegister
                .Invoking(register => register.AddAmount("hey Joe"))
                .Should().NotThrow<Exception>(because: "String argument should be ignored");
            _cashRegister.CurrentAmount.Should().Be(0, because: "Invalid input ignored and not adding to amount");
            _cashRegister
                .Invoking(register => register.AddAmount("2139847723642783642378654"))
                .Should().NotThrow<Exception>(because: "Too large uint should be ignored");
            _cashRegister.CurrentAmount.Should().Be(0, because: "Invalid input ignored and not adding to amount");
        }

        [Fact]
        public void AddAmountPostive()
        {
            _cashRegister.AddAmount("0");
            _cashRegister.CurrentAmount.Should().Be(0);

            _cashRegister.AddAmount("3");
            _cashRegister.CurrentAmount.Should().Be(3);

            _cashRegister.AddAmount("4");
            _cashRegister.CurrentAmount.Should().Be(7);
        }

        [Fact]
        public void ReturnEntireAmount()
        {
            //assert positive
            _cashRegister.AddAmount("3");
            _cashRegister.CurrentAmount.Should().Be(3);

            _cashRegister.ReturnEntireAmount();
            _cashRegister.CurrentAmount.Should().Be(0);

            _cashRegister.AddAmount("4");
            _cashRegister.CurrentAmount.Should().Be(4);
        }

        [Fact]
        public void GetChangeForTotal()
        {
            //arrange
            _cashRegister.AddAmount("3");
            _cashRegister.CurrentAmount.Should().Be(3);

            //assert postive
            _cashRegister.GetChangeForTotal(1).Should().Be(2);
            
            //assert negative
            _cashRegister.GetChangeForTotal(4).Should().Be(0, because: "Method ignores too high price, it's not its concern");
        }


    }
}
