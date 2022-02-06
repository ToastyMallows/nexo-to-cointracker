using System.Threading.Tasks;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NexoToCoinTracker
{
	public sealed class Program
	{
		public static async Task<int> Main(string[] args)
		{
			int exitCode = 0;

			ParserResult<Options> parserResult = Parser.Default.ParseArguments<Options>(args);

			parserResult
				.WithNotParsed((errors) =>
				{
					exitCode = 1;
				});

			await parserResult
				.WithParsedAsync(async (options) =>
				{
					await Host
						.CreateDefaultBuilder()
						.ConfigureServices((hostContext, services) =>
						{
							services
								.AddHostedService<HostedService>()
								.AddSingleton(options)
								.AddSingleton<NexoParser>()
								.AddSingleton<CoinTrackerHelper>();
						})
						.RunConsoleAsync();
				});

			return exitCode;
		}
	}
}
