using GeoService.EF.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSTests.Domain
{
    public class DataContextTests : DataContext
    {
        public DataContextTests(bool keepExistingDB = false) : base("Test")
        {
            if (keepExistingDB)
            {
                Database.EnsureCreated();
            }
            else
            {
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }
    }
}
