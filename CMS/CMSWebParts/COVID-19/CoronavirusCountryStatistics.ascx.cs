using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.PortalEngine.Web.UI;
using CMS.Helpers;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using CMS.Localization;
using CMS.DataEngine;
using CMS.SiteProvider;
using CMS.DocumentEngine.Web.UI;
using System.Reflection;
using System.Collections.Generic;

public partial class CMSWebParts_COVID_19_CoronavirusCountryStatistics : CMSAbstractWebPart
{
    #region "Properties"
    public string Country
    {
        get
        {
            return ValidationHelper.GetString(GetValue("Country"), "");
        }
        set
        {
            SetValue("Country", value);

        }
    }

    public string Logo
    {
        get
        {
            return ValidationHelper.GetString(GetValue("Image"), "");
        }
        set
        {
            SetValue("Image", value);

        }
    }

    /// <summary>
    /// Gets or sets the name of the transforamtion which is used for displaying the results.
    /// </summary>
    public string TransformationName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("TransformationName"), "");
        }
        set
        {
            SetValue("TransformationName", value);
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
                webClient.Headers.Add("x-rapidapi-host", SettingsKeyInfoProvider.GetValue(SiteContext.CurrentSiteName + ".XRapidAPIHost"));
                webClient.Headers.Add("x-rapidapi-key", SettingsKeyInfoProvider.GetValue(SiteContext.CurrentSiteName + ".XRapidAPIKey"));
                var response = webClient.DownloadString(SettingsKeyInfoProvider.GetValue(SiteContext.CurrentSiteName + ".CSAPI") + Country);
                var list = JsonConvert.DeserializeObject<JSONResponse>(response);
                if (!list.Error)
                {
                    if (!list.Message.Equals("OK"))
                    {
                        //lbl_Error.Text = list.Message;
                    }
                    List<CountryInfo> countryInfo = new List<CountryInfo>();
                    CountryInfo cc = new CountryInfo
                    {
                        Location = list.Data.Location,
                        Confirmed = list.Data.Confirmed,
                        Logo=Logo,
                        Deaths = list.Data.Deaths,
                        Recovered = list.Data.Recovered,
                        LastChecked = list.Data.LastChecked,
                        LastReported = list.Data.LastReported
                    };
                    countryInfo.Add(cc);
                    basicRepeater.DataSource = SetVehiclesDetails(countryInfo);
                    basicRepeater.ItemTemplate = TransformationHelper.LoadTransformation(basicRepeater, TransformationName);
                }

            }
        }
    }
    /// <summary>
    /// Covert Object to data table
    /// </summary>
    /// <param name="CountryInfo"></param>
    /// <returns></returns>
    public DataTable SetVehiclesDetails(List<CountryInfo> CountryInfo)
    {
        DataTable dt = new DataTable();
        try
        {
            foreach (PropertyInfo property in CountryInfo[0].GetType().GetProperties())
            {
                dt.Columns.Add(new DataColumn(property.Name, property.PropertyType));
            }

            foreach (var vehicle in CountryInfo)
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





