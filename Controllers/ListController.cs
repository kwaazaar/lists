using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using list.Managers;
using list.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace list.Controllers
{
    [Route("api/list")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly IListManager _listManager;

        public ListController(IListManager listManager)
        {
            _listManager = listManager;
        }

        // GET: api/List
        [HttpGet]
        [Route("")]
        public IEnumerable<ListSummary> GetListSummaries()
        {
            return _listManager.GetAllLists();
        }

        // GET: api/List/1
        [HttpGet]
        [Route("{id:int}")]
        public ListModel GetList(int id)
        {
            return _listManager.GetList(id);
        }

        // POST: api/List
        [HttpPost]
        [Route("")]
        public IActionResult AddList([FromBody] ListModel list)
        {
            var newList = _listManager.AddList(list);
            return Ok(newList);
        }

        [HttpDelete]
        [Route("{listId:int}")]
        public IActionResult DeleteList(int listId)
        {
            var deleted = _listManager.DeleteList(listId);
            return deleted ? (IActionResult)Ok() : (IActionResult)NotFound();
        }

        // POST: api/List/1
        [HttpPost]
        [Route("{listId:int}")]
        public IActionResult UpsertListItem(int listId, [FromBody] ListItem listItem)
        {
            if (listItem.ListId == default(int))
                listItem.ListId = listId;

            var newListItem = _listManager.UpsertListItem(listItem);
            return Ok(newListItem);
        }

        [HttpDelete]
        [Route("{listId:int}/{listItemId:int}")]
        public IActionResult DeleteListItem(int listId, int listItemId)
        {
            var deleted = _listManager.DeleteListItem(listId, listItemId);
            return deleted ? (IActionResult)Ok() : (IActionResult)NotFound();
        }

        /*
        // GET: api/List/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/List
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/List/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
