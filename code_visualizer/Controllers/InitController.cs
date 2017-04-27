using System;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Diagnostics;
using Ionic.Zip;

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
		[System.Web.Http.HttpPost]
		public IHttpActionResult PostInit([FromBody] Repositories repo)
		{
			var os = DetectOS();
			try {
				foreach (var url in repo.Repository)
				{
					var path = DownloadRepository(url);
					ParseSources(path, os);
				}				
				return Ok();
			} catch (Exception e) {
				Console.WriteLine("{0} Second exception caught.", e);
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
		    case PlatformID.Unix:
				return OS_T.Unix;
			case PlatformID.MacOSX:
				return OS_T.OSX;
		    default:
				throw new OSDetectionException();
		    }
		}

		private void ParseSources(string path, OS_T ostype)
		{
			ProcessStartInfo processInfo = new ProcessStartInfo();
			processInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "libs/osxsrcml";
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
			var zipPath = Path.GetTempPath() + splitUrl[splitUrl.Length-1] + ".zip";
			var filePath = Path.GetTempPath() + splitUrl[splitUrl.Length-1];

			WebClient Client = new WebClient();
			Client.DownloadFile(zipUrl, zipPath);

			using (ZipFile zip = ZipFile.Read(zipPath))
			{
				foreach (ZipEntry e in zip)
				{
					e.Extract(filePath, ExtractExistingFileAction.OverwriteSilently);
				}
			}
			File.Delete(zipPath);

			return filePath;
		}
	}
}

class OSDetectionException : Exception {}
class SrcMlException : Exception {}