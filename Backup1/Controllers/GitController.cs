using System;
using System.IO;

namespace code_visualizer
{
    public static class GitController
    {
        public static string InitRepository(string repoUrl)
        {
            var splitUrl = repoUrl.Split('/');
            var folderPath = Path.GetTempPath() + "codeVisualizer" + Path.DirectorySeparatorChar + splitUrl[splitUrl.Length - 1];
            if (Directory.Exists(folderPath) && LibGit2Sharp.Repository.IsValid(folderPath))
            {
                return folderPath;
            }
            clearFolder(folderPath);

            LibGit2Sharp.Repository.Clone(repoUrl, folderPath);
            return folderPath;
        }

        private static void clearFolder(string FolderName)
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
				clearFolder(di.FullName);
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

    }
}
