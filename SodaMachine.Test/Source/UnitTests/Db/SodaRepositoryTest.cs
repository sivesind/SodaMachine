using SodaMachine.Repository;
using System.Linq;
using Xunit;
using FluentAssertions;
using SodaMachine.DomainModel;
using System;

namespace SodaMachine.Test
{
    /// <summary>
    /// See testinventory.json for testdata, which is the foundation for these
    /// tests.
    /// </summary>
    public class SodaRepositoryTest
    {
        private SodaRepository _sodaRepository;

        public SodaRepositoryTest()
        {
            _sodaRepository = new SodaRepository("Source/UnitTests/Db/testinventory.json");
        }

        [Fact]
        public void ListSodaNames()
        {
            //act
            var sodaNames = _sodaRepository.ListSodaNames().Split(",");

            //assert
            sodaNames.Count().Should().Be(3);
            sodaNames[0].Should().Be("pepsi");
            sodaNames[1].Should().Be("drpepper");
            sodaNames[2].Should().Be("7up");
        }

        [Fact]
        public void ValidSodaName()
        {
            //assert negative
            _sodaRepository.ValidSodaName("").Should().Be(false);
            _sodaRepository.ValidSodaName(null).Should().Be(false);

            //assert positve
            _sodaRepository.ValidSodaName("pepsi").Should().Be(true);
            _sodaRepository.ValidSodaName("Pepsi").Should().Be(true);
            _sodaRepository.ValidSodaName("7up").Should().Be(true);
        }

        [Fact]
        public void HaveInStockNoOutParam()
        {
            //assert negative
            _sodaRepository.HaveInStock(1, "").Should().Be(false);
            _sodaRepository.HaveInStock(10000, null).Should().Be(false);

            //assert positive
            _sodaRepository.HaveInStock(1, "pepsi").Should().Be(true);
            _sodaRepository.HaveInStock(2, "pepsi").Should().Be(false, because: "There's not enough in stock");
            _sodaRepository.HaveInStock(3, "7up").Should().Be(true);
            _sodaRepository.HaveInStock(4, "7up").Should().Be(false, because: "There's only 3 in stock");
        }

        [Fact]
        public void HaveInStockWithOutParam()
        {
            //arrange
            Soda sodaInStock = null;

            //assert negative
            _sodaRepository.HaveInStock(1, "", out sodaInStock).Should().Be(false);
            sodaInStock.Should().BeNull(because: "Empty string as inparam should not find a soda");
            
            _sodaRepository.HaveInStock(10000, null, out sodaInStock).Should().Be(false);
            sodaInStock.Should().BeNull(because: "null as inparam should not find a soda");

            //assert positive
            _sodaRepository.HaveInStock(1, "pepsi", out sodaInStock).Should().Be(true);
            sodaInStock.Should().NotBeNull();
            sodaInStock.Name.Should().Be("pepsi");
            sodaInStock.Price.Should().Be(10);

            _sodaRepository.HaveInStock(2, "pepsi", out sodaInStock).Should().Be(false, because: "There's only one in stock");
            sodaInStock.Should().BeNull(because: "There's only one in stock");
        }

        [Fact]
        public void RemoveFromStock()
        {
            //act
            _sodaRepository.RemoveFromStock(1, "pepsi");
            
            //assert positive
            //stock is now 0
            _sodaRepository.HaveInStock(1, "pepsi").Should().Be(false);

            //stock is now 0, ignore overdraw since not supported
            _sodaRepository.RemoveFromStock(1, "pepsi");
            _sodaRepository.HaveInStock(1, "pepsi").Should().Be(false);

            _sodaRepository.RemoveFromStock(100, "pepsi");
            _sodaRepository.HaveInStock(1, "pepsi").Should().Be(false);

            //assert negative
            _sodaRepository
                .Invoking(repo => repo.RemoveFromStock(1, ""))
                .Should().NotThrow<Exception>(because: "Empty string argument should be ignored");
            _sodaRepository
                .Invoking(repo => repo.RemoveFromStock(1, null))
                .Should().NotThrow<Exception>(because: "Null argument should be ignored");
        }

    }
}
