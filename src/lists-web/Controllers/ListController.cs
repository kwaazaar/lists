using list.Managers;
using list.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        [Authorize(Policy = "ListReader")]
        public Task<IEnumerable<ListSummary>> GetListSummaries()
        {
            return _listManager.GetAllLists(this.UserId);
        }

        // GET: api/List/1
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Policy = "ListReader")]
        public Task<ListModel> GetList(Guid id)
        {
            return _listManager.GetList(this.UserId, id);
        }

        // POST: api/List
        [HttpPost]
        [Route("")]
        [Authorize(Policy = "ListWriter")]
        public async Task<IActionResult> AddList([FromBody] ListModel list)
        {
            var newList = await _listManager.AddList(this.UserId, list);
            return Ok(newList);
        }

        [HttpDelete]
        [Route("{listId:Guid}")]
        [Authorize(Policy = "ListWriter")]
        public async Task<IActionResult> DeleteList(Guid listId)
        {
            var deleted = await _listManager.DeleteList(this.UserId, listId);
            return deleted ? (IActionResult)Ok() : (IActionResult)NotFound();
        }

        // POST: api/List/1
        [HttpPost]
        [Route("{listId:Guid}")]
        [Authorize(Policy = "ListWriter")]
        public async Task<IActionResult> UpsertListItem(Guid listId, [FromBody] ListItem listItem)
        {
            if (listItem.ListId == Guid.Empty)
                listItem.ListId = listId;

            var newListItem = await _listManager.UpsertListItem(this.UserId, listItem);
            return Ok(newListItem);
        }

        [HttpDelete]
        [Route("{listId:Guid}/{listItemId:Guid}")]
        [Authorize(Policy = "ListWriter")]
        public async Task<IActionResult> DeleteListItem(Guid listId, Guid listItemId)
        {
            var deleted = await _listManager.DeleteListItem(this.UserId, listId, listItemId);
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

        private string UserId
        {
            get
            {
                return User.Identity.Name;
            }
        }
    }
}
