using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace NexoToCoinTracker
{
	public sealed class CoinTrackerHelper
	{
		private readonly ILogger<CoinTrackerHelper> _logger;
		private readonly Options _options;

		public CoinTrackerHelper(
			ILogger<CoinTrackerHelper> logger,
			Options options
		)
		{
			_logger = logger;
			_options = options;
		}

		public IEnumerable<CoinTrackerCSV> Convert(IEnumerable<NexoCSV> nexoLines)
		{
			List<CoinTrackerCSV> coinTrackerLines = new();

			foreach (NexoCSV line in nexoLines)
			{
				if (isSkippableNexoType(line.Type))
				{
					continue;
				}

				switch (line.Type)
				{
					case NexoConstants.Types.FixedTermInterest:
					case NexoConstants.Types.Interest:
						coinTrackerLines.Add(fromInterest(line));
						break;
					case NexoConstants.Types.Deposit:
						coinTrackerLines.Add(fromDeposit(line));
						break;
					case NexoConstants.Types.Withdrawal:
						coinTrackerLines.Add(fromWithdrawal(line));
						break;
					default:
						_logger.LogWarning("Unknown Nexo transaction type: {Type}", line.Type);
						continue;
				}
			}

			return coinTrackerLines;
		}

		public async Task WriteFile(IEnumerable<CoinTrackerCSV> coinTrackerLines)
		{
			if (File.Exists(_options.OutputFile))
			{
				_logger.LogInformation("Output file {File} will be overwritten.", _options.OutputFile);
			}

			using (FileStream fs = File.Create(_options.OutputFile))
			using (StreamWriter sw = new StreamWriter(fs))
			{
				await sw.WriteLineAsync(CoinTrackerConstants.CSVHeaderLine);

				foreach (CoinTrackerCSV line in coinTrackerLines)
				{
					await sw.WriteLineAsync(line.ToString());
				}
			}
		}

		private static CoinTrackerCSV fromInterest(NexoCSV line)
		{
			decimal receivedQuantity = decimal.Round(decimal.Parse(line.Amount), 8);

			return new CoinTrackerCSV(
				line.DateTime,
				receivedQuantity.ToString(),
				line.Currency,
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty,
				CoinTrackerConstants.Tags.Staked
			);
		}

		private static CoinTrackerCSV fromDeposit(NexoCSV line)
		{
			decimal receivedQuantity = decimal.Round(decimal.Parse(line.Amount), 8);

			return new CoinTrackerCSV(
				line.DateTime,
				receivedQuantity.ToString(),
				line.Currency,
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty
			);
		}

		private static CoinTrackerCSV fromWithdrawal(NexoCSV line)
		{
			decimal amount = decimal.Parse(line.Amount);

			if (amount < 0)
			{
				amount *= -1;
			}

			decimal sentQuantity = decimal.Round(amount);

			return new CoinTrackerCSV(
				line.DateTime,
				string.Empty,
				string.Empty,
				sentQuantity.ToString(),
				line.Currency,
				string.Empty,
				string.Empty,
				string.Empty
			);
		}

		private static bool isSkippableNexoType(string type)
		{
			return type == NexoConstants.Types.LockingTermDeposit || type == NexoConstants.Types.UnlockingTermDeposit;
		}
	}
}
