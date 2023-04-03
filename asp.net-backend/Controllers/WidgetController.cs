using asp.net_backend.Models;
using WoCevv.DAL;
using MessagePack.Formatters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace asp.net_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WidgetController : ControllerBase
    {
        private readonly DatamodelDAL _db = new();

        //GET: api/Widget/Category
        [HttpGet("Category")]
        public async Task<ActionResult<List<string>>> ReturnCategory()
        {
            return _db.ReadAllDatamodelNames();
        }

        [HttpGet("test")]
        public List<string> CategoryTest() 
        { 
            return _db.ReadAllDatamodelNames();
        }

        #region Testing

        //private readonly WidgetContext _context;

        //public WidgetController(WidgetContext context)
        //{
        //    _context = context;
        //}

        ////GET: api/Widget
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Widget>>> GetWidgetItems()
        //{
        //    return await _context.WidgetsItems.ToListAsync();
        //}

        ////GET: api/Widget/id
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Widget>> FindWidget(int id)
        //{
        //    var widget = await _context.WidgetsItems.FindAsync(id);
        //    if (widget == null)
        //    {
        //        return NotFound();
        //    }

        //    return widget;
        //}

        ////POST: api/Widget
        //[HttpPost]
        //public async Task<ActionResult<Widget>> PostWidget(Widget widget)
        //{
        //    _context.WidgetsItems.Add(widget);
        //    await _context.SaveChangesAsync();

        //    //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        //    return CreatedAtAction(nameof(FindWidget), new { id = widget.Id }, widget);
        //}
        #endregion
    }
}