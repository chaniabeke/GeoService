using GeoService.EF.DataAccess;

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