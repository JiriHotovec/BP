﻿using System.Collections;
using Hotovec.Orders.Domain.Orders;

namespace Hotovec.Orders.Domain.Test.Orders;

/// <summary>
/// Test data for <see cref="Currency"/>
/// Test data are represented as run-time result of GetEnumerator method
/// This approach is useful when:
/// - there is large number of test values
/// - test values cannot be expressed as compile-time constants
/// </summary>
public sealed class CurrencyTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return [ "AED" ];
        yield return [ "AFN" ];
        yield return [ "ALL" ];
        yield return [ "AMD" ];
        yield return [ "ANG" ];
        yield return [ "AOA" ];
        yield return [ "ARS" ];
        yield return [ "AUD" ];
        yield return [ "AWG" ];
        yield return [ "AZN" ];
        yield return [ "BAM" ];
        yield return [ "BBD" ];
        yield return [ "BDT" ];
        yield return [ "BGN" ];
        yield return [ "BHD" ];
        yield return [ "BIF" ];
        yield return [ "BMD" ];
        yield return [ "BND" ];
        yield return [ "BOB" ];
        yield return [ "BOV" ];
        yield return [ "BRL" ];
        yield return [ "BSD" ];
        yield return [ "BTN" ];
        yield return [ "BWP" ];
        yield return [ "BYN" ];
        yield return [ "BZD" ];
        yield return [ "CAD" ];
        yield return [ "CDF" ];
        yield return [ "CHE" ];
        yield return [ "CHF" ];
        yield return [ "CHW" ];
        yield return [ "CLF" ];
        yield return [ "CLP" ];
        yield return [ "CNY" ];
        yield return [ "COP" ];
        yield return [ "COU" ];
        yield return [ "CRC" ];
        yield return [ "CUP" ];
        yield return [ "CVE" ];
        yield return [ "CZK" ];
        yield return [ "DJF" ];
        yield return [ "DKK" ];
        yield return [ "DOP" ];
        yield return [ "DZD" ];
        yield return [ "EGP" ];
        yield return [ "ERN" ];
        yield return [ "ETB" ];
        yield return [ "EUR" ];
        yield return [ "FJD" ];
        yield return [ "FKP" ];
        yield return [ "GBP" ];
        yield return [ "GEL" ];
        yield return [ "GHS" ];
        yield return [ "GIP" ];
        yield return [ "GMD" ];
        yield return [ "GNF" ];
        yield return [ "GTQ" ];
        yield return [ "GYD" ];
        yield return [ "HKD" ];
        yield return [ "HNL" ];
        yield return [ "HTG" ];
        yield return [ "HUF" ];
        yield return [ "IDR" ];
        yield return [ "ILS" ];
        yield return [ "INR" ];
        yield return [ "IQD" ];
        yield return [ "IRR" ];
        yield return [ "ISK" ];
        yield return [ "JMD" ];
        yield return [ "JOD" ];
        yield return [ "JPY" ];
        yield return [ "KES" ];
        yield return [ "KGS" ];
        yield return [ "KHR" ];
        yield return [ "KMF" ];
        yield return [ "KPW" ];
        yield return [ "KRW" ];
        yield return [ "KWD" ];
        yield return [ "KYD" ];
        yield return [ "KZT" ];
        yield return [ "LAK" ];
        yield return [ "LBP" ];
        yield return [ "LKR" ];
        yield return [ "LRD" ];
        yield return [ "LSL" ];
        yield return [ "LYD" ];
        yield return [ "MAD" ];
        yield return [ "MDL" ];
        yield return [ "MGA" ];
        yield return [ "MKD" ];
        yield return [ "MMK" ];
        yield return [ "MNT" ];
        yield return [ "MOP" ];
        yield return [ "MRU" ];
        yield return [ "MUR" ];
        yield return [ "MVR" ];
        yield return [ "MWK" ];
        yield return [ "MXN" ];
        yield return [ "MXV" ];
        yield return [ "MYR" ];
        yield return [ "MZN" ];
        yield return [ "NAD" ];
        yield return [ "NGN" ];
        yield return [ "NIO" ];
        yield return [ "NOK" ];
        yield return [ "NPR" ];
        yield return [ "NZD" ];
        yield return [ "OMR" ];
        yield return [ "PAB" ];
        yield return [ "PEN" ];
        yield return [ "PGK" ];
        yield return [ "PHP" ];
        yield return [ "PKR" ];
        yield return [ "PLN" ];
        yield return [ "PYG" ];
        yield return [ "QAR" ];
        yield return [ "RON" ];
        yield return [ "RSD" ];
        yield return [ "RUB" ];
        yield return [ "RWF" ];
        yield return [ "SAR" ];
        yield return [ "SBD" ];
        yield return [ "SCR" ];
        yield return [ "SDG" ];
        yield return [ "SEK" ];
        yield return [ "SGD" ];
        yield return [ "SHP" ];
        yield return [ "SLE" ];
        yield return [ "SOS" ];
        yield return [ "SRD" ];
        yield return [ "SSP" ];
        yield return [ "STN" ];
        yield return [ "SVC" ];
        yield return [ "SYP" ];
        yield return [ "SZL" ];
        yield return [ "THB" ];
        yield return [ "TJS" ];
        yield return [ "TMT" ];
        yield return [ "TND" ];
        yield return [ "TOP" ];
        yield return [ "TRY" ];
        yield return [ "TTD" ];
        yield return [ "TWD" ];
        yield return [ "TZS" ];
        yield return [ "UAH" ];
        yield return [ "UGX" ];
        yield return [ "USD" ];
        yield return [ "USN" ];
        yield return [ "UYI" ];
        yield return [ "UYU" ];
        yield return [ "UYW" ];
        yield return [ "UZS" ];
        yield return [ "VED" ];
        yield return [ "VES" ];
        yield return [ "VND" ];
        yield return [ "VUV" ];
        yield return [ "WST" ];
        yield return [ "XAF" ];
        yield return [ "XAG" ];
        yield return [ "XAU" ];
        yield return [ "XBA" ];
        yield return [ "XBB" ];
        yield return [ "XBC" ];
        yield return [ "XBD" ];
        yield return [ "XCD" ];
        yield return [ "XDR" ];
        yield return [ "XOF" ];
        yield return [ "XPD" ];
        yield return [ "XPF" ];
        yield return [ "XPT" ];
        yield return [ "XSU" ];
        yield return [ "XTS" ];
        yield return [ "XUA" ];
        yield return [ "XXX" ];
        yield return [ "YER" ];
        yield return [ "ZAR" ];
        yield return [ "ZMW" ];
        yield return [ "ZWG" ];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
