namespace web.app.dataobjectes;

public class InputJson
{
	public required DateTimeOffset ReferenceMonth { get; set; }
	public required List<IGenerateZahlungen> Generators { get; set; }


	public IEnumerable<ZahlungenProMonat> ZahlungenProMonat()
	{
		var enumerators = Generators
			.Select(x => x.Enumerate(ReferenceMonth).GetEnumerator())
			.ToList();

		while (true)
		{
			var any = false;
			var zahlungen = new List<IZahlung?>();

			foreach (var enumerator in enumerators)
			{
				if (!enumerator.MoveNext())
				{
					zahlungen.Add(null);
					continue;
				}

				any = true;
				zahlungen.Add(enumerator.Current);
			}

			if (!any)
				yield break;

			yield return new()
			{
				Monat = zahlungen.First().Monat,
				Zahlungen = zahlungen
			};
		}
	}
}
