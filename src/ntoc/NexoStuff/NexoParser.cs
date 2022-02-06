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
		private readonly IFileWrapper _fileWrapper;

		public NexoParser(
			ILogger<NexoParser> logger,
			Options options,
			IFileWrapper fileWrapper
		)
		{
			_logger = logger;
			_options = options;
			_fileWrapper = fileWrapper;
		}

		public async Task<IEnumerable<NexoCSV>> Parse()
		{
			if (!_fileWrapper.Exists(_options.InputFile))
			{
				_logger.LogError("File {InputFile} does not exist.", _options.InputFile);
				throw new FileNotFoundException("File not found", _options.InputFile);
			}

			IEnumerable<string> allLines = await _fileWrapper.ReadAllLinesAsync(_options.InputFile, Encoding.UTF8);

			// The first line is the CSV column headers, skip it
			allLines = allLines.Skip(1);

			return allLines.Select(l => NexoCSV.FromCSVLine(l)).ToList();
		}
	}
}
