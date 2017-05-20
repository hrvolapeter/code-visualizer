using System;
using System.IO;
using System.Diagnostics;


namespace code_visualizer
{
	enum OS_T
	{
		Win,
		Unix,
		OSX
	}


	public static class SrcmlController
	{

		private static OS_T DetectOS()
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

		/// <summary>
		/// Parse source using right srcml binary
		/// </summary>
		/// <param name="path">string with path to repo</param>
		/// <returns>path to xml file</returns>
		public static string ParseSources(string path)
		{
			var ostype = DetectOS();
			ProcessStartInfo processInfo = new ProcessStartInfo();
			switch (ostype)
			{
				case OS_T.Unix:
					processInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "libs" + Path.DirectorySeparatorChar + "unixsrcml";
					break;
				case OS_T.OSX:
					processInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "libs" + Path.DirectorySeparatorChar + "osxsrcml";
					break;
				case OS_T.Win:
					processInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "libs" + Path.DirectorySeparatorChar + "winsrcml.exe";
					break;
				default:
					break;
			}
			var filePath = path + ".xml";
			processInfo.Arguments = " " + path + " -o " + filePath;
			Process proc = Process.Start(processInfo);
			proc.WaitForExit();
			if (proc.ExitCode == 0)
			{
				return filePath;
			}
			throw new SrcMlException();
		}

	}
}
class SrcMlException : Exception { }
class OSDetectionException : Exception { }
