using System.Web.Http;
using System.Collections.Generic;

namespace code_visualizer.Controllers
{
	public class AnalyseController : ApiController
	{
		public IHttpActionResult GetAnalyse(string repoUrl)
        {

			return Ok(AnalyseCodeDebt(repoUrl));
        }

         private int[] AnalyseCodeDebt(string repoUrl)
        {
            var path = GitController.InitRepository(repoUrl);
			var countTodo = new List<int>();
            for (var i = 0; i < 10; i++)
            {
                var srcmlPath = SrcmlController.ParseSources(path);
                countTodo.Add(XmlController.CountTodo(srcmlPath));
                GitController.TimeTravelCommits(path, 10);
            }
            return countTodo.ToArray();
        }
    }
}
