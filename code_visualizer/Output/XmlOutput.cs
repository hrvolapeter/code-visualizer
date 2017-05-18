using System;

namespace code_visualizer
{
	public class XmlOutput<T>
	{
		public JobType Type;
		public T[] Data;

		public XmlOutput(T[] data, JobType type)
		{
			Data = data;
			Type = type;
		}
	}
}

