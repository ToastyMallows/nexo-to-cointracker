using System;
using System.Globalization;

namespace NexoToCoinTracker
{
	public sealed class CoinTrackerCSV
	{
		public CoinTrackerCSV(
			DateTime date,
			string receivedQuantity,
			string receivedCurrency,
			string sentQuantity,
			string sentCurrency,
			string feeAmount,
			string feeCurrency,
			string tag
		)
		{
			Date = date;
			ReceivedQuantity = receivedQuantity;
			ReceivedCurrency = receivedCurrency;
			SentQuantity = sentQuantity;
			SentCurrency = sentCurrency;
			FeeAmount = feeAmount;
			FeeCurrency = feeCurrency;
			Tag = tag;
		}

		DateTime Date { get; }

		string ReceivedQuantity { get; }

		string ReceivedCurrency { get; }

		string SentQuantity { get; }

		string SentCurrency { get; }

		string FeeAmount { get; }

		string FeeCurrency { get; }

		string Tag { get; }

		public override string ToString()
		{
			return $"{Date.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture)},{ReceivedQuantity},{ReceivedCurrency},{SentQuantity},{SentCurrency},{FeeAmount},{FeeCurrency},{Tag}";
		}
	}
}
