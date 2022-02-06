using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NexoToCoinTracker
{
	public interface IFileWrapper
	{
		bool Exists(string path);
		FileStream Create(string path);

		Task<string[]> ReadAllLinesAsync(string path, Encoding encoding);
	}

	public sealed class FileWrapper : IFileWrapper
	{
		public bool Exists(string path)
		{
			return File.Exists(path);
		}

		public FileStream Create(string path)
		{
			return File.Create(path);
		}

		public Task<string[]> ReadAllLinesAsync(string path, Encoding encoding)
		{
			return File.ReadAllLinesAsync(path, encoding);
		}
	}
}
