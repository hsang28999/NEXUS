using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NEXUS.Controllers
{
    public class BaseController : ApiController
    {
        // GET api/<controller>
        public void ExceptionContent(HttpStatusCode code, string message)
        {
            var response = new HttpResponseMessage(code)
            {
                ReasonPhrase = "Tai khoan hoac mat khau sai"
            };
            response.Headers.Add("status",message);
            throw new HttpResponseException(response);
        }
    }
}