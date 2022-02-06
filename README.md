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

`> ntoc -i nexo.csv -o cointracker.csv`

This will convert the given, existing Nexo CSV file to a valid CoinTracker CSV import file.  Output file is created if it doesn't exist.  Any existing output file will be overwritten.
