namespace web.app.dataobjectes;

public class ZahlungsRange
{
	public DateTimeOffset Start { get; set; }
	public DateTimeOffset EndIncl { get; set; }
	public IList<IZahlung?> Zahlungen { get; set; }
}


public class ZahlungenProMonat
{
	public DateTimeOffset Monat { get; set; }
	public IList<IZahlung?> Zahlungen { get; set; }
}
