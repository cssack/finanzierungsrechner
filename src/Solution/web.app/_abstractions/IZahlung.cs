namespace web.app._abstractions;

public interface IZahlung
{
	public string Name { get; }
	public DateTimeOffset Monat { get; }
	public decimal Betrag { get; }
	
}





public interface IGeneratorZahlung : IZahlung
{
	public IGenerateZahlungen Generator { get; }


	string IZahlung.Name => Generator.Name;
}
