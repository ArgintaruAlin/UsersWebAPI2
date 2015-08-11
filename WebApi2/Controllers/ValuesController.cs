﻿using System.Web.Http;
using System.Web.Mvc;

namespace WebApi2.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public JsonResult Get()
        {
            return new JsonResult { Data = "string1" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
