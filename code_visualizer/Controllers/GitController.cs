using System;
using System.IO;

namespace code_visualizer
{
    public static class GitController
    {
        public static string InitRepository(string repoUrl)
        {
            var folderPath = GetFolderPath(repoUrl);
            if (Directory.Exists(folderPath) && LibGit2Sharp.Repository.IsValid(folderPath))
            {
                Reset(repoUrl);
                return folderPath;
            }
            ClearFolder(folderPath);

            LibGit2Sharp.Repository.Clone(repoUrl, folderPath);
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


        public static void TimeTravelCommits(string repositoryPath, uint difference)
        {
            var repo = new LibGit2Sharp.Repository(repositoryPath);
            var enu = repo.Commits.GetEnumerator();
            for (var i = 0; i < difference; i++)
            {
                enu.MoveNext();
            }
        }

        private static string GetFolderPath(string repoUrl)
        {
			var splitUrl = repoUrl.Split('/');
			return Path.GetTempPath() + "codeVisualizer" + Path.DirectorySeparatorChar + splitUrl[splitUrl.Length - 1];
		}

    }
}
