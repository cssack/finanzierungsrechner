namespace web.app.dataobjectes;

public class GeneratorZahlung : IGeneratorZahlung
{
	public required IGenerateZahlungen Generator { get; set; }
	public required DateTimeOffset Monat { get; set; }
	public required decimal Betrag { get; set; }
}
