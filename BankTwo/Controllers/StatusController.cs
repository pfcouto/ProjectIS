using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankOne.Controllers
{
    public class StatusController : ApiController
    {
        public IHttpActionResult GetStatus()
        {
            return Ok();
        }
    }
}
