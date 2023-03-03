namespace web.app.dataobjectes;

public class ZahlungsRange
{
	public DateTimeOffset Start { get; set; }
	public DateTimeOffset EndIncl { get; set; }
	public IList<IZahlung> Zahlungen { get; set; }
}


public class ZahlungenProMonat
{
	public required DateTimeOffset Monat { get; set; }
	public required IList<IZahlung> Zahlungen { get; set; }
}
