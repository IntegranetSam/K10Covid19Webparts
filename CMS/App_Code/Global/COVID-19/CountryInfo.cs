using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CountryInfo
/// </summary>
public class CountryInfo
{
    public string Location { get; set; }
    public string Logo { get; set; }
    public int Recovered { get; set; }
    public int Deaths { get; set; }
    public int Confirmed { get; set; }
    public DateTime LastChecked { get; set; }
    public DateTime LastReported { get; set; }
}