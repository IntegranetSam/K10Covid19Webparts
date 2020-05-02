using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.PortalEngine.Web.UI;
using CMS.Helpers;
using System.Net;
using CMS.DataEngine;
using CMS.SiteProvider;
using Newtonsoft.Json;
using System.Collections.Generic;
using CMS.DocumentEngine.Web.UI;
using System.Reflection;

public partial class CMSWebParts_COVID_19_CoronavirusWorldDataCountryWise : CMSAbstractWebPart
{
    #region "Properties"
    /// <summary>
    /// Gets or sets the name of the transforamtion which is used for displaying the World results.
    /// </summary>
    public string TransformationForWorldData
    {
        get
        {
            return ValidationHelper.GetString(GetValue("TransformationWorldName"), "");
        }
        set
        {
            SetValue("TransformationWorldName", value);
        }
    }

    /// <summary>
    /// Gets or sets the name of the transforamtion which is used for displaying the results.
    /// </summary>
    public string TransformationForCountryWise
    {
        get
        {
            return ValidationHelper.GetString(GetValue("TransformationCounywiseName"), "");
        }
        set
        {
            SetValue("TransformationCounywiseName", value);
        }
    }


    #endregion


    #region "Methods"

    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            // Do not process
        }
        else
        {
            using (WebClient webClient = new WebClient())
            {

                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                webClient.Headers.Add("x-rapidapi-host", SettingsKeyInfoProvider.GetValue(SiteContext.CurrentSiteName + ".WorldIndiaXRapidAPIHost"));
                webClient.Headers.Add("x-rapidapi-key", SettingsKeyInfoProvider.GetValue(SiteContext.CurrentSiteName + ".XRapidAPIKey"));
                var response = webClient.DownloadString(SettingsKeyInfoProvider.GetValue(SiteContext.CurrentSiteName + ".CVWIAPI"));
                var countrywiseData = JsonConvert.DeserializeObject<WorldDataCountryWise>(response);
                List<WorldData> worldDataList = new List<WorldData>();
                if (countrywiseData.World_Total != null)
                {
                    WorldData wd = new WorldData()
                    {
                        Total_Cases = countrywiseData.World_Total.Total_Cases,
                        Total_Deaths = countrywiseData.World_Total.Total_Deaths,
                        Total_Recovered = countrywiseData.World_Total.Total_Recovered,
                        New_Cases = countrywiseData.World_Total.New_Cases,
                        New_Deaths = countrywiseData.World_Total.New_Deaths,
                        Statistic_Taken_At = countrywiseData.World_Total.Statistic_Taken_At
                    };
                    worldDataList.Add(wd);
                }
                brWorldData.DataSource = GetWorldData(worldDataList);
                brWorldData.ItemTemplate = TransformationHelper.LoadTransformation(brWorldData, TransformationForWorldData);
                List<Countries> allCountriesData = new List<Countries>();
                if (countrywiseData.Countries_Stat.Count > 0)
                {
                    foreach(var c in countrywiseData.Countries_Stat)
                    {
                        Countries countryInfo = new Countries()
                        {
                            Country_Name = c.Country_Name,
                            Cases = c.Cases,
                            Deaths = c.Deaths,
                            Region = c.Region,
                            Total_Recovered = c.Total_Recovered,
                            New_Deaths = c.New_Deaths,
                            New_Cases = c.New_Cases,
                            Serious_Critical = c.Serious_Critical,
                            Active_Cases = c.Active_Cases,
                            Deaths_Per_1m_Population = c.Deaths_Per_1m_Population,
                            Total_Tests = c.Total_Tests,
                            Total_Cases_Per_1m_Population = c.Total_Cases_Per_1m_Population,
                            Tests_Per_1m_Population = c.Tests_Per_1m_Population
                        };
                        allCountriesData.Add(countryInfo);
                    }
                   
                   
                }
                brCountryWiseData.DataSource = GetCountryWiseData(allCountriesData);
                brCountryWiseData.ItemTemplate = TransformationHelper.LoadTransformation(brCountryWiseData, TransformationForCountryWise);


            }
        }
    }

    private object GetCountryWiseData(List<Countries> allCountriesData)
    {
        DataTable dt = new DataTable();
        try
        {
            foreach (PropertyInfo property in allCountriesData[0].GetType().GetProperties())
            {
                dt.Columns.Add(new DataColumn(property.Name, property.PropertyType));
            }

            foreach (var country in allCountriesData)
            {
                DataRow newRow = dt.NewRow();
                foreach (PropertyInfo property in country.GetType().GetProperties())
                {
                    newRow[property.Name] = country.GetType().GetProperty(property.Name).GetValue(country, null);
                }
                dt.Rows.Add(newRow);
            }
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    /// <summary>
    /// Covert Object to data table
    /// </summary>
    /// <param name="CountryInfo"></param>
    /// <returns></returns>
    public DataTable GetWorldData(List<WorldData> WorldData)
    {
        DataTable dt = new DataTable();
        try
        {
            foreach (PropertyInfo property in WorldData[0].GetType().GetProperties())
            {
                dt.Columns.Add(new DataColumn(property.Name, property.PropertyType));
            }

            foreach (var vehicle in WorldData)
            {
                DataRow newRow = dt.NewRow();
                foreach (PropertyInfo property in vehicle.GetType().GetProperties())
                {
                    newRow[property.Name] = vehicle.GetType().GetProperty(property.Name).GetValue(vehicle, null);
                }
                dt.Rows.Add(newRow);
            }
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    /// <summary>
    /// Reloads the control data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();

        SetupControl();
    }

    #endregion
}



