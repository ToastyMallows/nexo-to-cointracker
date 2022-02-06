using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NexoToCoinTracker
{
	public sealed class HostedService : IHostedService
	{
		private readonly IHostApplicationLifetime _applicationLifetime;
		private readonly ILogger<HostedService> _logger;
		private readonly NexoParser _nexoParser;
		private readonly CoinTrackerHelper _coinTrackerHelper;

		private bool hadSomeError = false;


		public HostedService(
			ILogger<HostedService> logger,
			IHostApplicationLifetime applicationLifetime,
			NexoParser nexoParser,
			CoinTrackerHelper coinTrackerHelper
		)
		{
			_applicationLifetime = applicationLifetime;
			_logger = logger;
			_nexoParser = nexoParser;
			_coinTrackerHelper = coinTrackerHelper;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			_applicationLifetime.ApplicationStarted.Register(() =>
			{
				Task.Run(async () =>
				{
					try
					{
						IEnumerable<NexoCSV> allNexoLines = await _nexoParser.Parse();
						IEnumerable<CoinTrackerCSV> allCoinTrackerLines = _coinTrackerHelper.Convert(allNexoLines);
						await _coinTrackerHelper.WriteFile(allCoinTrackerLines);
					}
					catch (Exception e)
					{
						_logger.LogError(e.ToString());
						_logger.LogError("An exception occurred, check output");
						hadSomeError = true;
					}
					finally
					{
						_applicationLifetime.StopApplication();
					}
				});
			});

			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			if (hadSomeError || cancellationToken.IsCancellationRequested)
			{
				_logger.LogInformation("Program ran with errors.");
			}
			else
			{
				_logger.LogInformation("Program ran successfully!");
			}
			return Task.CompletedTask;
		}
	}
}
