using System.Web.Http;
using System.Collections.Generic;
using System.Linq;

namespace code_visualizer.Controllers
{
	public class AnalyseController : ApiController
	{
		private List<List<KeyValuePair<string, int>>> Sort(List<Dictionary<string, int>> list) 
		{
			var ordered = new List<List<KeyValuePair<string, int>>>();
			for (int i = 0; i < list.Count; ++i)
			{
				ordered.Add(list[i].ToList());
				ordered[i].Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
			}
			return ordered;
		}


		/// <summary>
		/// Count todos
		/// </summary>
		/// <param name="repoUrl">string that contains repo url</param>
		/// <param name="diff">uint number to commits between time travel</param>
		/// <param name="times">uint of number of time travels</param>
		/// <returns>list that contains count of todos</returns>
		[HttpGet]
		public IHttpActionResult CodeDebt(string repoUrl, uint diff = 10, uint times = 10)
		{
			var path = GitController.InitRepository(repoUrl);
			var todoCounts = new List<int>();
			for (var i = 0; i < times; i++)
			{
				var srcmlPath = SrcmlController.ParseSources(path);
				todoCounts.Add(XmlController.CountTodo(srcmlPath));
				GitController.TimeTravelCommits(path, diff);
			}
			return Ok(new XmlOutput<int>(todoCounts.ToArray(), JobType.ToDo));
		}


		/// <summary>
		/// Count of rows per project
		/// </summary>
		/// <param name="repoUrl">string that contains repo url</param>
		/// <param name="diff">uint number to commits between time travel</param>
		/// <param name="times">uint of number of time travels</param>
		/// <returns>list that contains number of occurances</returns>
		[HttpGet]
		public IHttpActionResult RowCount(string repoUrl, uint diff = 10, uint times = 10)
		{
			var path = GitController.InitRepository(repoUrl);
			var rowCounts = new List<int>();
			for (var i = 0; i < times; i++)
			{
				var srcmlPath = SrcmlController.ParseSources(path);
				rowCounts.Add(XmlController.CountRows(srcmlPath));
				GitController.TimeTravelCommits(path, diff);
			}
			return Ok(new XmlOutput<int>(rowCounts.ToArray(), JobType.RowCount));
		}


		/// <summary>
		/// Count of funcitons in project
		/// </summary>
		/// <param name="repoUrl">string that contains repo url</param>
		/// <param name="diff">uint number to commits between time travel</param>
		/// <param name="times">uint of number of time travels</param>
		/// <returns>list that contains number of occurances</returns>
		[HttpGet]
		public IHttpActionResult FuncCount(string repoUrl, uint diff = 10, uint times = 10)
		{
			var path = GitController.InitRepository(repoUrl);
			var funcCounts = new List<int>();
			for (var i = 0; i < times; i++)
			{
				var srcmlPath = SrcmlController.ParseSources(path);
				funcCounts.Add(XmlController.CountFuncs(srcmlPath));
				GitController.TimeTravelCommits(path, diff);
			}
			return Ok(new XmlOutput<int>(funcCounts.ToArray(), JobType.FuncCount));
		}


		/// <summary>
		/// Count of imports per project
		/// </summary>
		/// <param name="repoUrl">string that contains repo url</param>
		/// <param name="diff">uint number to commits between time travel</param>
		/// <param name="times">uint of number of time travels</param>
		/// <returns>dictionary that contains type of parameter and number of occurances</returns>
		[HttpGet]
		public IHttpActionResult Imports(string repoUrl, uint diff = 10, uint times = 10)
		{
			var path = GitController.InitRepository(repoUrl);
			var importCounts = new List<Dictionary<string, int>>();
			for (var i = 0; i < times; i++)
			{
				var srcmlPath = SrcmlController.ParseSources(path);
				importCounts.Add(XmlController.CountImports(srcmlPath));
				GitController.TimeTravelCommits(path, diff);
			}
			var orderedCounts = Sort(importCounts);
			return Ok(new XmlOutput<List<KeyValuePair<string, int>>>(orderedCounts.ToArray(), JobType.Imports));
		}


		/// <summary>
		/// Count of params and type
		/// </summary>
		/// <param name="repoUrl">string that contains repo url</param>
		/// <param name="diff">uint number to commits between time travel</param>
		/// <param name="times">uint of number of time travels</param>
		/// <returns>dictionary that contains type of parameter and number of occurances</returns>
		[HttpGet]
		public IHttpActionResult Params(string repoUrl, uint diff = 10, uint times = 10)
		{
			var path = GitController.InitRepository(repoUrl);
			var paramCounts = new List<Dictionary<string, int>>();
			for (var i = 0; i < times; i++)
			{
				var srcmlPath = SrcmlController.ParseSources(path);
				paramCounts.Add(XmlController.CountParamTypes(srcmlPath));
				GitController.TimeTravelCommits(path, diff);
			}
			var orderedCounts = Sort(paramCounts);
			return Ok(new XmlOutput<List<KeyValuePair<string, int>>>(orderedCounts.ToArray(), JobType.ParamTypesCount));
		}


		/// <summary>
		/// Average Row count per function
		/// </summary>
		/// <param name="repoUrl">string that contains repo url</param>
		/// <param name="diff">uint number to commits between time travel</param>
		/// <param name="times">uint of number of time travels</param>
		/// <returns>list that contains number of occurances</returns>
		[HttpGet]
		public IHttpActionResult AverageRowCountPerFunction(string repoUrl, uint diff = 10, uint times = 10)
		{
			var path = GitController.InitRepository(repoUrl);
			var avgRowCounts = new List<double>();
			for (var i = 0; i < times; i++)
			{
				var srcmlPath = SrcmlController.ParseSources(path);
				avgRowCounts.Add(XmlController.GetAverageRowsPerFunction(srcmlPath));
				GitController.TimeTravelCommits(path, diff);
			}
			return Ok(new XmlOutput<double>(avgRowCounts.ToArray(), JobType.AvgRowsPerFunction));
		}


		/// <summary>
		/// Count of loops per project
		/// </summary>
		/// <param name="repoUrl">string that contains repo url</param>
		/// <param name="diff">uint number to commits between time travel</param>
		/// <param name="times">uint of number of time travels</param>
		/// <returns>dictionary that contains type of loops and number of occurances</returns>
		[HttpGet]
		public IHttpActionResult LoopsCount(string repoUrl, uint diff = 10, uint times = 10)
		{
			var path = GitController.InitRepository(repoUrl);
			var loopsCounts = new List<Dictionary<string, int>>();
			for (var i = 0; i < times; i++)
			{
				var srcmlPath = SrcmlController.ParseSources(path);
				loopsCounts.Add(XmlController.CountLoops(srcmlPath));
				GitController.TimeTravelCommits(path, diff);
			}
			var orderedCounts = Sort(loopsCounts);
			return Ok(new XmlOutput<List<KeyValuePair<string, int>>>(orderedCounts.ToArray(), JobType.Loops));
		}


		/// <summary>
		/// Count of ifs per project
		/// </summary>
		/// <param name="repoUrl">string that contains repo url</param>
		/// <param name="diff">uint number to commits between time travel</param>
		/// <param name="times">uint of number of time travels</param>
		/// <returns>list that contains number of occurances</returns>
		[HttpGet]
		public IHttpActionResult IfCount(string repoUrl, uint diff = 10, int times = 10)
		{
			var path = GitController.InitRepository(repoUrl);
			var ifCounts = new List<int>();
			for (var i = 0; i < times; i++)
			{
				var srcmlPath = SrcmlController.ParseSources(path);
				ifCounts.Add(XmlController.CountIfs(srcmlPath));
				GitController.TimeTravelCommits(path, diff);
			}
			return Ok(new XmlOutput<int>(ifCounts.ToArray(), JobType.Ifs));
		}


		/// <summary>
		/// counts of return types per project
		/// </summary>
		/// <param name="repoUrl">string that contains repo url</param>
		/// <param name="diff">uint number to commits between time travel</param>
		/// <param name="times">uint of number of time travels</param>
		/// <returns>dictionary that contains type of return and number of occurances</returns>
		[HttpGet]
		public IHttpActionResult ReturnTypesCount(string repoUrl, uint diff = 10, int times = 10)
		{
			var path = GitController.InitRepository(repoUrl);
			var returnTypesCounts = new List<Dictionary<string, int>>();
			for (var i = 0; i < times; i++)
			{
				var srcmlPath = SrcmlController.ParseSources(path);
				returnTypesCounts.Add(XmlController.CountReturnTypes(srcmlPath));
				GitController.TimeTravelCommits(path, diff);
			}
			var orderedCounts = Sort(returnTypesCounts);
			return Ok(new XmlOutput<List<KeyValuePair<string, int>>>(orderedCounts.ToArray(), JobType.ReturnTypesCount));
		}
	}
}