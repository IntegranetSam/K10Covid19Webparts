using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for WorldDataCountryWise
/// </summary>
public class WorldDataCountryWise
{
    public DateTime Statistic_Taken_At { get; set; }
    public WorldData World_Total { get; set; }
    public List<Countries> Countries_Stat { get; set; }
}