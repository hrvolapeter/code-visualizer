using System;
using System.Web.Http;

public enum Analyse_T {
	CodeDebt,
	ClassDependency
}

namespace code_visualizer.Controllers
{
	public class AnalyzeController : ApiController
	{
		public IHttpActionResult GetStats(Analyse_T analyse)
		{
			return StatusCode(System.Net.HttpStatusCode.NotImplemented);
		}
	}
}
