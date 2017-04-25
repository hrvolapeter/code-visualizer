using System.Web.Http;
using System.Collections.Generic;
using System.IO;

public class Repositories
{
	public string[] Repository { get; set; }
}


namespace code_visualizer.Controllers
{
	public class  InitController : ApiController
	{
		[System.Web.Http.HttpPost]
		public IHttpActionResult PostInit([FromBody] Repositories repositories)
		{
			var names = new List<string>();
			foreach (var repository in repositories.Repository)
			{
				var path = GitController.InitRepository(repository);
				var srcmlxml = SrcmlController.ParseSources(path);

				var splitPath = path.Split(Path.DirectorySeparatorChar);
				names.Add(splitPath[splitPath.Length - 1]);

			}
			return Ok(names);
		}
	}
}
