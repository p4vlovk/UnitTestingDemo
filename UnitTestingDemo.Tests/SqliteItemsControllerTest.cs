namespace UnitTestingDemo.Tests;

using Microsoft.EntityFrameworkCore;

using UnitTestingDemo.Api.Data;

public class SqliteItemsControllerTest : ItemsControllerTest
{
    public SqliteItemsControllerTest()
        : base(new DbContextOptionsBuilder<ItemsContext>()
            .UseSqlite("Filename=Test.db")
            .Options)
    {
    }
}