﻿using System.Text.Json.Serialization;

namespace web.app._abstractions;

[JsonDerivedType(typeof(BankKredit), typeDiscriminator: nameof(BankKredit))]
[JsonDerivedType(typeof(SfgKredit), typeDiscriminator: nameof(SfgKredit))]
public interface IGenerateZahlungen 
{
	public string Name { get; }

	public IEnumerable<IGeneratorZahlung> Enumerate(DateTimeOffset referenceMonth);
}