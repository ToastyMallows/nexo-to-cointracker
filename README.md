# Nexo To CoinTracker

Convert Nexo Transactions CSV file to a CoinTracker CSV file

Pull Requests welcome!

## NOTICE

**USE AT YOUR OWN RISK.**

**I am not a tax professional.  I am not your tax preparer.  Please do not ask me tax questions.**

**By using, modifying, or distributing this software, you acknowledge that I will not be held liable if you are audited.**

I was only able to use a Nexo CSV file that I could generate from my personal account.  Yours may look different if:

- You speak a different language
- You use a different base currency than USD
- You took out a loan on Nexo
- You purchased NEXO coin
- You exchanged between various types of crypto on Nexo

Your mileage my vary!

CoinTracker cannot classify Interest on Nexo to Interest on CoinTracker, so it is classified as 'staking'.

## USAGE

`> ntoc.exe -i nexo.csv -o cointracker.csv`

This will convert the given, existing Nexo CSV file to a valid CoinTracker CSV import file.  Output file is created if it doesn't exist.  Any existing output file will be overwritten.

You can control the logging output level using an `appsettings.json` file in the same directory as the executable.  You can find out more information about controlling the logging output at Microsoft's [Logging in .NET](https://docs.microsoft.com/en-us/dotnet/core/extensions/logging) page.  A sample [appsettings.json](./src/ntoc/appsettings.json) file is given in source.

## BUILD LOCALLY

`> dotnet publish -c Release -r (RID) --self-contained true`

`(RID)` can be replaced with a runtime identifier for your system.  See the [.NET RID Catalog](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog) for more information.

## TODO

- Unit tests
- Integration tests
