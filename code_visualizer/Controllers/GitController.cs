using System;
using System.IO;

namespace code_visualizer
{
    public static class GitController
    {
        public static string InitRepository(string repoUrl)
        {
            var splitUrl = repoUrl.Split('/');
            var folderPath = Path.GetTempPath() + "codeVisualizer" + Path.DirectorySeparatorChar;

            LibGit2Sharp.Repository.Clone(repoUrl, folderPath);
            return folderPath + splitUrl[splitUrl.Length - 1];
        }


        public static void TimeTravelCommits(string repositoryPath, uint difference)
        {
            var repo = new LibGit2Sharp.Repository(repositoryPath);
            var commit = repo.Head.Tip;
            GetCommit(commit, difference);
        }


        private static LibGit2Sharp.Commit GetCommit(LibGit2Sharp.Commit commit, uint difference)
        {
            // TODO: can fail
            foreach (var com in commit.Parents)
            {
                return com;
            }
            return null;
        }

    }
}
