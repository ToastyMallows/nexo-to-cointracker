using System;
namespace NexoToCoinTracker
{
	public sealed class NexoCSV
	{
		public NexoCSV(
			string transaction,
			string type,
			string currency,
			string amount,
			string usdEquivalent,
			string details,
			string outstandingLoan,
			DateTime dateTime
		)
		{
			Transaction = transaction;
			Type = type;
			Currency = currency;
			Amount = amount;
			USDEquivalent = usdEquivalent;
			Details = details;
			OutstandingLoan = outstandingLoan;
			DateTime = dateTime;
		}

		public string Transaction { get; }

		public string Type { get; }

		public string Currency { get; }

		public string Amount { get; }

		public string USDEquivalent { get; }

		public string Details { get; }

		public string OutstandingLoan { get; }

		/// <remarks>
		/// Assuming Nexo Date/Time is in UTC
		/// </remarks>
		public DateTime DateTime { get; }

		public static NexoCSV FromCSVLine(string csvLine)
		{
			string[] columns = csvLine.Split(',');

			if (columns.Length != 8)
			{
				throw new InvalidOperationException("Nexo line does not have 8 columns");
			}

			if (!decimal.TryParse(columns[3], out decimal amount))
			{
				throw new FormatException($"Amount decimal is in an invalid format {columns[3]}");
			}

			if (!decimal.TryParse(columns[4].TrimStart('$'), out decimal usdEquivalent))
			{
				throw new FormatException($"USD Equivalent decimal is in an invalid format {columns[4]}");
			}

			if (!decimal.TryParse(columns[6].TrimStart('$'), out decimal outstandingLoan))
			{
				throw new FormatException($"Outstanding Loan decimal is in an invalid format {columns[6]}");
			}

			if (!DateTime.TryParse(columns[7], out DateTime dateTime))
			{
				throw new FormatException($"Date / Time column is in an invalid format {columns[7]}");
			}

			return new NexoCSV(
				columns[0],
				columns[1],
				columns[2],
				amount.ToString(),
				usdEquivalent.ToString(),
				columns[5],
				outstandingLoan.ToString(),
				dateTime
			);
		}
	}
}
