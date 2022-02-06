using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace NexoToCoinTracker
{
	public sealed class NexoParser
	{
		private readonly ILogger<NexoParser> _logger;
		private readonly Options _options;

		public NexoParser(ILogger<NexoParser> logger, Options options)
		{
			_logger = logger;
			_options = options;
		}

		public async Task<IEnumerable<NexoCSV>> Parse()
		{
			if (!File.Exists(_options.InputFile))
			{
				_logger.LogError("File {InputFile} does not exist.", _options.InputFile);
				throw new FileNotFoundException("File not found", _options.InputFile);
			}

			IEnumerable<string> allLines = await File.ReadAllLinesAsync(_options.InputFile, Encoding.UTF8);

			// The first line is the CSV column headers, skip it
			allLines = allLines.Skip(1);

			return allLines.Select(l => NexoCSV.FromCSVLine(l)).ToList();
		}
	}
}
