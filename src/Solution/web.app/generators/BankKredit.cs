using web.app.dataobjectes;

namespace web.app.generators;

public class BankKredit : IGenerateZahlungen
{
	public required string Name { get; set; }

	public required int Beginn { get; set; }
	public int AbbruchNach { get; set; }


	public required decimal KreditBetrag { get; set; }
	public required int LaufzeitInMonaten { get; set; }
	public required double ZinsSatz { get; set; } //0.035
	


	
	public IEnumerable<IGeneratorZahlung> Enumerate(DateTimeOffset referenceMonth)
	{
		decimal monatlicheRate;

		if (ZinsSatz == 0)
		{
			monatlicheRate = KreditBetrag / LaufzeitInMonaten;
		}
		else
		{
			var k = Math.Pow(1 + ZinsSatz, LaufzeitInMonaten / 12.0);
			var anuitätenFaktor = k * ZinsSatz / (k - 1);

			var jährlicheRate = KreditBetrag * (decimal)anuitätenFaktor;
			monatlicheRate = jährlicheRate / 12;
		}

		for (var i = 0; i < Beginn; i++)
			yield return new GeneratorZahlung { Generator = this, Monat = referenceMonth.AddMonths(i), Betrag = 0 };

		for (var i = 0; i < LaufzeitInMonaten; i++)
		{
			if (i >= AbbruchNach && AbbruchNach != 0)
				yield break;

			yield return new GeneratorZahlung { Generator = this, Monat = referenceMonth.AddMonths(Beginn + i), Betrag = monatlicheRate };
		}
	}
}
