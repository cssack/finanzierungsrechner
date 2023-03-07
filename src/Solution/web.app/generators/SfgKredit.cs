using web.app.dataobjectes;

namespace web.app.generators;

public class SfgKredit : IGenerateZahlungen, IHaveKreditBetrag
{
	public required string Name { get; set; }
	public required int Beginn { get; set; }
	public required int VorlaufDauer { get; set; }
	public int AbbruchNach { get; set; }

	public required decimal KreditBetrag { get; set; }
	public required int LaufzeitInMonaten { get; set; }
	public required double ZinsSatz { get; set; } //0.035
	
	
	
	
	public IEnumerable<IGeneratorZahlung> Enumerate(DateTimeOffset referenceMonth)
	{
		for (var i = 0; i < Beginn; i++)
			yield return new GeneratorZahlung { Generator = this, Monat = referenceMonth.AddMonths(i), Betrag = 0 };
		
		
		var vorlaufMonate = VorlaufDauer;
		var vorlaufZahlung = (KreditBetrag * (decimal)ZinsSatz) / 12;

		for (int i = 0; i < vorlaufMonate; i++)
			yield return new GeneratorZahlung {Monat = referenceMonth.AddMonths(i + Beginn), Betrag = vorlaufZahlung, Generator = this};
		
		
		var reguläreZahlung = new BankKredit
		{
			Beginn = 0,
			AbbruchNach = AbbruchNach,
			KreditBetrag = KreditBetrag,
			LaufzeitInMonaten = LaufzeitInMonaten,
			Name = Name,
			ZinsSatz = ZinsSatz
		};
		
		foreach (var (zahlung, idx) in reguläreZahlung.Enumerate(referenceMonth).Select((x, idx) => (x, idx)))
			yield return new GeneratorZahlung { Betrag = zahlung.Betrag, Generator = this, Monat = referenceMonth.AddMonths(idx + vorlaufMonate + Beginn) };
	}
}
