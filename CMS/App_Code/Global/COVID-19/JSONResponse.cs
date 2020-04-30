using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for JSONResponse
/// </summary>
public class JSONResponse
{
    public bool Error { get; set; }
    public long StatusCode { get; set; }
    public string Message { get; set; }
    public CountryInfo Data { get; set; }
}