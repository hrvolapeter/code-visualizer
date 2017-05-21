using System;
using System.Diagnostics;
using System.IO;

namespace code_visualizer
{
    public static class GitController
    {

		/// <summary>
		/// Initialize new local repo
		/// </summary>
		/// <param name="repoUrl">string that contains url of github repository</param>
		/// <returns>string to local repo path</returns>
		public static string InitRepository(string repoUrl)
        {
            var folderPath = GetFolderPath(repoUrl);
            if (Directory.Exists(folderPath) && LibGit2Sharp.Repository.IsValid(folderPath))
            {
                Reset(repoUrl);
                return folderPath;
            }
            ClearFolder(folderPath);
           			
			ProcessStartInfo processInfo = new ProcessStartInfo();
			processInfo.FileName = "git";
            processInfo.Arguments = "clone " + repoUrl + " " + folderPath;
			Process proc = Process.Start(processInfo);
			proc.WaitForExit();
            return folderPath;
        }


        private static void Reset(string repoUrl)
        {
            var path = GetFolderPath(repoUrl);
            var repo = new LibGit2Sharp.Repository(path);
            LibGit2Sharp.Commands.Checkout(repo, "master");

		}


        private static void ClearFolder(string FolderName)
		{
            if (!Directory.Exists(FolderName))
            {
                return;
            }

            DirectoryInfo dir = new DirectoryInfo(FolderName);

			foreach (FileInfo fi in dir.GetFiles())
			{
				fi.Delete();
			}

			foreach (DirectoryInfo di in dir.GetDirectories())
			{
				ClearFolder(di.FullName);
				di.Delete();
			}
		}


		/// <summary>
		/// Moving in repositary tree
		/// </summary>
		/// <param name="repositoryPath">string local path to repository</param>
		/// <param name="difference>uint to move given number of commits</param>
		public static void TimeTravelCommits(string repositoryPath, uint difference)
        {
            var repo = new LibGit2Sharp.Repository(repositoryPath);
            var enu = repo.Commits.GetEnumerator();
            for (var i = 0; i < difference; i++)
            {
                enu.MoveNext();
            }
            LibGit2Sharp.Commands.Checkout(repo, enu.Current);
        }


        private static string GetFolderPath(string repoUrl)
        {
			var splitUrl = repoUrl.Split('/');
			return Path.GetTempPath() + "codeVisualizer" + Path.DirectorySeparatorChar + splitUrl[splitUrl.Length - 1];
		}

    }
}
