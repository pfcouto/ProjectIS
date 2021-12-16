using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;

namespace VCardsMiddleware.Controllers
{
    public class LogsController : ApiController
    {
        [Authorize(Roles = "admin")]
        public IHttpActionResult GetLogs()
        {
            return Ok(XmlHelper.ReadLogs());
        }
    }
}
