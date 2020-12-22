using FluentAssertions;
using GeoService.Domain.Managers;
using GeoService.Domain.Models;
using GeoService.EF.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSTests.Domain.Managers
{
    [TestClass()]
    public class managerT
    {

        [TestMethod]
        public void MethodName_condition_expectedValue()
        {
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            //ContinentManager manager = new ContinentManager(new UnitOfWork(ctx));
            // Act
            //manager.AddContinent(new Continent(5, "bob"));
            // Assert
            Continent continent = ctx.Continents.Local.First();
            continent.Name.Should().Be("bob");
        }
    }
}

