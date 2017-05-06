using System.Web.Http;
using System.Collections.Generic;
using System.Linq;

namespace code_visualizer.Controllers
{
	public class AnalyseController : ApiController
	{
		public IHttpActionResult GetAnalyse(string repoUrl)
        {
            
			return Ok(Imports(repoUrl));
        }

        [HttpGet]
        public int[] CodeDebt(string repoUrl, uint diff = 10, int times = 10)
        {
            var path = GitController.InitRepository(repoUrl);
			var todoCounts = new List<int>();
            for (var i = 0; i < times; i++)
            {
                var srcmlPath = SrcmlController.ParseSources(path);
                todoCounts.Add(XmlController.CountTodo(srcmlPath));
                GitController.TimeTravelCommits(path, diff);
            }
            return todoCounts.ToArray();
        }

        [HttpGet]
        public int[] RowCount(string repoUrl, uint diff = 10, int times = 10)
        {
            var path = GitController.InitRepository(repoUrl);
            var rowCounts = new List<int>();
            for (var i = 0; i < times; i++)
            {
                var srcmlPath = SrcmlController.ParseSources(path);
                rowCounts.Add(XmlController.CountRows(srcmlPath));
                GitController.TimeTravelCommits(path, diff);
            }
            return rowCounts.ToArray();
        }

        [HttpGet]
        public int[] FuncCount(string repoUrl, uint diff = 10, int times = 10)
        {
            var path = GitController.InitRepository(repoUrl);
            var funcCounts = new List<int>();
            for (var i = 0; i < times; i++)
            {
                var srcmlPath = SrcmlController.ParseSources(path);
                funcCounts.Add(XmlController.CountFuncs(srcmlPath));
                GitController.TimeTravelCommits(path, diff);
            }
            return funcCounts.ToArray();
        }

        [HttpGet]
        public List<KeyValuePair<string, int>>[] Imports(string repoUrl, uint diff = 10, int times = 10)
        {
            var path = GitController.InitRepository(repoUrl);
            var importCounts = new List<Dictionary<string, int>>();
            for (var i = 0; i < times; i++)
            {
                var srcmlPath = SrcmlController.ParseSources(path);
                importCounts.Add(XmlController.CountImports(srcmlPath));
                GitController.TimeTravelCommits(path, diff);
            }
            var orderedCounts = new List<List<KeyValuePair<string, int>>>();
            for (int i = 0; i < importCounts.Count; ++i)
            {
                orderedCounts.Add(importCounts[i].ToList());
                orderedCounts[i].Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
            }
            return orderedCounts.ToArray();
        }

        [HttpGet]
        public List<KeyValuePair<string, int>>[] Params(string repoUrl, uint diff = 10, int times = 10)
        {
            var path = GitController.InitRepository(repoUrl);
            var paramCounts = new List<Dictionary<string, int>>();
            for (var i = 0; i < times; i++)
            {
                var srcmlPath = SrcmlController.ParseSources(path);
                paramCounts.Add(XmlController.CountParamTypes(srcmlPath));
                GitController.TimeTravelCommits(path, diff);
            }
            var orderedCounts = new List<List<KeyValuePair<string, int>>>();
            for (int i = 0; i < paramCounts.Count; ++i)
            {
                orderedCounts.Add(paramCounts[i].ToList());
                orderedCounts[i].Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
            }
            return orderedCounts.ToArray();
        }
    }
}