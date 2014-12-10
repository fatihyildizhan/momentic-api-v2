using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MomenticAPI.Controllers
{
    public class SearchStoryTagController : ApiController
    {
        // GET: api/SearchStoryTag
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SearchStoryTag/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SearchStoryTag
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SearchStoryTag/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SearchStoryTag/5
        public void Delete(int id)
        {
        }
    }
}
