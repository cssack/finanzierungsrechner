using System.Text.Json.Serialization;

namespace web.app._abstractions;


public interface IHaveKreditBetrag
{
	public decimal KreditBetrag { get; set; }
}

[JsonDerivedType(typeof(BankKredit), typeDiscriminator: nameof(BankKredit))]
[JsonDerivedType(typeof(SfgKredit), typeDiscriminator: nameof(SfgKredit))]
public interface IGenerateZahlungen : IHaveKreditBetrag
{
	public string Name { get; }
	

	public IEnumerable<IGeneratorZahlung> Enumerate(DateTimeOffset referenceMonth);
}
