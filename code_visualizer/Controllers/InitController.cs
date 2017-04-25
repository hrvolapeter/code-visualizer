using System;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace code_visualizer.Controllers
{
	public class InitController : ApiController
	{
		public IHttpActionResult GetInit(int id)
		{
			return Ok("hello world"+id.ToString());
		}
	}
}
