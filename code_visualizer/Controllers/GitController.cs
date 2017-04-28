using System;
using System.IO;

namespace code_visualizer
{
	public static class GitController
	{
		public static string InitRepository(string repoUrl)
		{
			var zipUrl = repoUrl + "/archive/master.zip";
			var splitUrl = repoUrl.Split('/');
			var folderPath = Path.GetTempPath() + "codeVisualizer" + Path.DirectorySeparatorChar;
			GitSharp.Git.Clone(repoUrl, folderPath);
			return folderPath;
		}


		public static void TimeTravelCommits(string repositoryPath, uint difference)
		{
			var repo = new GitSharp.Repository(repositoryPath);
			var commit = repo.Head.CurrentCommit;
			GetCommit(commit, difference);
		}


		private static GitSharp.Commit GetCommit(GitSharp.Commit commit, uint difference)
		{
			for (int i = 0; i < difference; i++)
			{
				commit = commit.Parent;
			}
			return commit;
		}
		                 
	}
}
