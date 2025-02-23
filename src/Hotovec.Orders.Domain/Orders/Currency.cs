namespace Hotovec.Orders.Domain.Orders;

public readonly record struct Currency
{
    private static readonly string[] AllCurrencies =
    [
        "AED", "AFN", "ALL", "AMD", "ANG", "AOA", "ARS", "AUD", "AWG", "AZN", "BAM", "BBD",
        "BDT", "BGN", "BHD", "BIF", "BMD", "BND", "BOB", "BOV", "BRL", "BSD", "BTN", "BWP",
        "BYN", "BZD", "CAD", "CDF", "CHE", "CHF", "CHW", "CLF", "CLP", "CNY", "COP", "COU",
        "CRC", "CUP", "CVE", "CZK", "DJF", "DKK", "DOP", "DZD", "EGP", "ERN", "ETB", "EUR",
        "FJD", "FKP", "GBP", "GEL", "GHS", "GIP", "GMD", "GNF", "GTQ", "GYD", "HKD", "HNL",
        "HTG", "HUF", "IDR", "ILS", "INR", "IQD", "IRR", "ISK", "JMD", "JOD", "JPY", "KES",
        "KGS", "KHR", "KMF", "KPW", "KRW", "KWD", "KYD", "KZT", "LAK", "LBP", "LKR", "LRD",
        "LSL", "LYD", "MAD", "MDL", "MGA", "MKD", "MMK", "MNT", "MOP", "MRU", "MUR", "MVR",
        "MWK", "MXN", "MXV", "MYR", "MZN", "NAD", "NGN", "NIO", "NOK", "NPR", "NZD", "OMR",
        "PAB", "PEN", "PGK", "PHP", "PKR", "PLN", "PYG", "QAR", "RON", "RSD", "RUB", "RWF",
        "SAR", "SBD", "SCR", "SDG", "SEK", "SGD", "SHP", "SLE", "SOS", "SRD", "SSP", "STN",
        "SVC", "SYP", "SZL", "THB", "TJS", "TMT", "TND", "TOP", "TRY", "TTD", "TWD", "TZS",
        "UAH", "UGX", "USD", "USN", "UYI", "UYU", "UYW", "UZS", "VED", "VES", "VND", "VUV",
        "WST", "XAF", "XAG", "XAU", "XBA", "XBB", "XBC", "XBD", "XCD", "XDR", "XOF", "XPD",
        "XPF", "XPT", "XSU", "XTS", "XUA", "XXX", "YER", "ZAR", "ZMW", "ZWG"
    ];

    /// <summary>
    ///     Creates new instance of <see cref="Currency" />
    /// </summary>
    /// <param name="code">A three-letter currency code defined in ISO 4217</param>
    /// <exception cref="ArgumentException">Thrown when provided code is not valid ISO 4217 currency code</exception>
    /// <example>var c = new Currency("JPY");</example>
    public Currency(string code)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        
        var normalized = NormalizeInput(code);
        if (!IsAmongAllowedCodes(normalized))
        {
            throw new ArgumentException($"Code '{code}' is not a valid currency code.", nameof(code));
        }

        Code = normalized;
    }

    /// <summary>
    /// Currency code as string
    /// </summary>
    public string Code { get; }

    private static bool IsAmongAllowedCodes(string code)
    {
        return AllCurrencies.Contains(code, StringComparer.OrdinalIgnoreCase);
    }

    private static string NormalizeInput(string code)
    {
        return code.ToUpperInvariant();
    }

    public override string ToString()
    {
        return Code;
    }
}
