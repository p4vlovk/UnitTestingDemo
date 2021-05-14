namespace UnitTestingDemo.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using UnitTestingDemo.Api.Data;
    using UnitTestingDemo.Api.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ItemsContext context;

        public ItemsController(ItemsContext context) => this.context = context;

        [HttpGet]
        public IEnumerable<Item> Get()
            => this.context.Items
                .Include(e => e.Tags)
                .OrderBy(e => e.Name);

        [HttpGet]
        public Item Get(string itemName)
            => this.context.Items
                .Include(e => e.Tags)
                .FirstOrDefault(e => e.Name == itemName);

        [HttpPost]
        public ActionResult<Item> PostItem(string itemName)
        {
            var item = this.context.Add(new Item(itemName)).Entity;
            this.context.SaveChanges();

            return item;
        }

        [HttpPost]
        public ActionResult<Tag> PostTag(string itemName, string tagLabel)
        {
            var tag = this.context
                .Items
                .Include(e => e.Tags)
                .Single(e => e.Name == itemName)
                .AddTag(tagLabel);

            this.context.SaveChanges();

            return tag;
        }

        [HttpDelete("{itemName}")]
        public ActionResult<Item> DeleteItem(string itemName)
        {
            var item = this.context
                .Items
                .SingleOrDefault(e => e.Name == itemName);

            if (item == null)
            {
                return NotFound();
            }

            this.context.Remove(item);
            this.context.SaveChanges();

            return item;
        }
    }
}
