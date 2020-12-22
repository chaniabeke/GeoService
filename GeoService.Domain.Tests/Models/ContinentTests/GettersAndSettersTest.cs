using FluentAssertions;
using GeoService.Domain.Exceptions;
using GeoService.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoService.Domain.Tests.Models.ContinentTests
{
    [TestClass()]
    public class GettersAndSettersTest
    {
        #region SetId

        [TestMethod]
        public void SetId_ShouldBeCorrect_IfIdIsValid()
        {
            Continent continent = new Continent(5, "Asia");

            continent.Id.Should().Be(5);

            continent.Id = 10;

            Assert.AreEqual(continent.Id, 10);
            continent.Id.Should().Be(10);

        }

        [TestMethod]
        public void SetId_ShouldThrowException_IfIdIsZero()
        {
            Continent continent = new Continent(5, "Asia");

            Action act = () =>
            {
                continent.Id = 0;
            };

            act.Should().Throw<ContinentException>().WithMessage("Continent - Id invalid");
        }

        [TestMethod]
        public void SetId_ShouldThrowException_IfIdIsNegative()
        {
            Continent continent = new Continent(5, "Asia");

            Action act = () =>
            {
                continent.Id = -8;
            };

            act.Should().Throw<ContinentException>().WithMessage("Continent - Id invalid");
        }

        #endregion SetId

        #region SetName

        [TestMethod]
        public void SetName_ShouldBeCorrect_IfNameIsValid()
        {
            Continent continent = new Continent(5, "Asia");

            continent.Name.Should().Be("Asia");

            continent.Name = "Russia";

            continent.Name.Should().Be("Russia");
        }

        [TestMethod]
        public void SetName_ShouldThrowException_IfNameIsNull()
        {
            Continent continent = new Continent(5, "Asia");

            Action act = () =>
            {
                continent.Name = null;
            };

            act.Should().Throw<ContinentException>().WithMessage("Continent - name invalid");
        }

        [TestMethod]
        public void SetName_ShouldThrowException_IfNameIsEmpty()
        {
            Continent continent = new Continent(5, "Asia");

            Action act = () =>
            {
                continent.Name = "        ";
            };

            act.Should().Throw<ContinentException>().WithMessage("Continent - name invalid");
        }

        #endregion SetName

        #region SetCountries

        [TestMethod]
        public void SetCountries_ShouldBeCorrect_IfCountriesListIsValid()
        {
            List<Country> countries = new List<Country>();
            Country country = new Country(5, "China");
            countries.Add(country);
            Continent continent = new Continent(5, "Asia", countries);

            continent.Countries.Count.Should().Be(1);
            continent.Countries.First().Continent.Should().Be("China");
        }

        [TestMethod]
        public void SetCountries_ShouldThrowException_IfCountiesListCountIsZero()
        {
            List<Country> countries = new List<Country>();

            Action act = () =>
            {
                Continent continent = new Continent(5, "Asia", countries);
            };

            act.Should().Throw<ContinentException>().WithMessage("Continent - invalid countriesList");
        }

        [TestMethod]
        public void SetCountries_ShouldThrowException_IfCountriesListIsNull()
        {
            List<Country> countries = new List<Country>();

            Action act = () =>
            {
                Continent continent = new Continent(5, "Asia", null);
            };

            act.Should().Throw<ContinentException>().WithMessage("Continent - invalid countriesList");
        }

        #endregion
    }
}
