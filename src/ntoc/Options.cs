using CommandLine;

namespace NexoToCoinTracker
{
	public sealed class Options
	{
		public Options()
		{
			InputFile = string.Empty;
			OutputFile = string.Empty;
		}

		[Option('i', "input", Required = true, HelpText = "The Nexo formatted CSV file you wish to convert.")]
		public string InputFile { get; set; }

		[Option('o', "output", Required = true, HelpText = "The output file you want the results written to.  Will be created if it does not exist.")]
		public string OutputFile { get; set; }
	}
}
