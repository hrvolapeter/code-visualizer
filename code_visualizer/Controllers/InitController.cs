using System;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Diagnostics;
using Ionic.Zip;
using System.Collections.Generic;

enum OS_T {
	Win,
	Unix,
	OSX
}


public class Repositories
{
	public string[] Repository { get; set; }
}


namespace code_visualizer.Controllers
{
	public class InitController : ApiController
	{
		public IHttpActionResult GetInit()
		{
			return Ok();
		}


		[System.Web.Http.HttpPost]
		public IHttpActionResult PostInit([FromBody] Repositories repo)
		{
			var os = DetectOS();
			try {
				List<String> paths = new List<string>();

				foreach (var url in repo.Repository)
				{
					var path = DownloadRepository(url);
					var splitPath = path.Split(Path.DirectorySeparatorChar);
					paths.Add(splitPath[splitPath.Length - 1]);
					ParseSources(path, os);
				}				

				return Ok(paths);
			} catch (Exception e) {
				Console.WriteLine("{0} exception caught.", e);
				return StatusCode(HttpStatusCode.InternalServerError);
			}
		}

		private OS_T DetectOS()
		{
			OperatingSystem os = Environment.OSVersion;
			PlatformID pid = os.Platform;
			switch (pid) 
		    {
			    case PlatformID.Win32NT:
			    case PlatformID.Win32S:
			    case PlatformID.Win32Windows:
			    case PlatformID.WinCE:
					return OS_T.Win;
				case PlatformID.MacOSX:
					return OS_T.OSX;
			    case PlatformID.Unix:
					return OS_T.Unix;
			    default:
					throw new OSDetectionException();
		    }
		}


		private void ParseSources(string path, OS_T ostype)
		{
			ProcessStartInfo processInfo = new ProcessStartInfo();
			switch (ostype)
			{
				case OS_T.Unix:
					processInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "libs" + Path.DirectorySeparatorChar + "osxsrcml";
					break;
				case OS_T.OSX:
					processInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "libs" + Path.DirectorySeparatorChar + "unixsrcml";
					break;
				case OS_T.Win:
					processInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "libs" + Path.DirectorySeparatorChar + "winsrcml.exe";
					break;
				default:
					break;
			}
			processInfo.Arguments = " " + path + " -o " + path + ".xml";
			Process proc = Process.Start(processInfo);
			proc.WaitForExit();
			if(proc.ExitCode == 0) {
				return;
			}
			throw new SrcMlException();
		}


		private string DownloadRepository(String url)
		{
			var zipUrl = url + "/archive/master.zip";
			var splitUrl = url.Split('/');
			var folderPath = Path.GetTempPath() + "codeVisualizer" + Path.DirectorySeparatorChar;
			var zipPath = folderPath + splitUrl[splitUrl.Length-1] + ".zip";
			var extractPath = folderPath + splitUrl[splitUrl.Length-1];

			WebClient Client = new WebClient();
			Directory.CreateDirectory(folderPath);
			Client.DownloadFile(zipUrl, zipPath);

			using (ZipFile zip = ZipFile.Read(zipPath))
			{
				foreach (ZipEntry e in zip)
				{
					e.Extract(extractPath, ExtractExistingFileAction.OverwriteSilently);
				}
			}
			File.Delete(zipPath);

			return extractPath;
		}
	}
}

class OSDetectionException : Exception {}
class SrcMlException : Exception {}