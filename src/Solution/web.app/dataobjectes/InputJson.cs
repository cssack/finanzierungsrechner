namespace web.app.dataobjectes;

public class InputJson
{
	public required DateTimeOffset ReferenceMonth { get; set; }
	public required List<IGenerateZahlungen> Generators { get; set; }


	public IEnumerable<ZahlungenProMonat> ZahlungenProMonat()
	{
		var enumerators = Generators
			.Select(x => (x, x.Enumerate(ReferenceMonth).GetEnumerator()))
			.ToList();

		var month = ReferenceMonth;
		while (true)
		{
			var any = false;
			var zahlungen = new List<IZahlung>();

			foreach (var (generator, enumerator) in enumerators)
			{
				if (!enumerator.MoveNext())
				{
					zahlungen.Add(new GeneratorZahlung(){Betrag = 0, Generator = generator, Monat = month});
					continue;
				}

				any = true;
				zahlungen.Add(enumerator.Current);
			}
			month = month.AddMonths(1);

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
