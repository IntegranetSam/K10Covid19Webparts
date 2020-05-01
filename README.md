# Kentico  Covid-19 Webparts
A web part for Kentico 10 that adds COVID-19 Coronavirus Statistics to your web site.
![Test Image 4](https://github.com/vasu-rbt/K10Covid19Webparts/blob/master/SampleView.png)
# Installation
1. Download the latest package from Release
2. In Kentico, go to the Sites application
3. Select "Import sites or objects"
4. Upload the package and import it (don't forget to check the "Import code files" checkbox)
5. Now you are ready to use it in the Pages application
6. Install Newtonsoft.Json.12.0.3 NuGet Package
# Compatibility
Tested with Kentico 10.0
# Prerequisites
1. Browse https://rapidapi.com/ website
2. Sign Up with GitHub/Facebook/Gmail/Your work email
3. Search for Coronavirus COVID-19 APIs
4. Subscribe "COVID-19 Coronavirus Statistics"
# Configurations from CMS Admin
1. Goto Settings under Configuration mobule
2. Select "COVID-19 APIs"
3. Add the following Keys under Coronavirus Statistics 
   - API
   - X-RapidAPI-Host
   - X-RapidAPI-Key
 4. You can copy these values from https://rapidapi.com/KishCom/api/covid-19-coronavirus-statistics --> Code Snippet --> (C#)RestSharp
# Configure web part on Page
1. Drag and drop "Coronavirus Country Statistics" web part on page
2. Add Country name (if country name is not mactched then it returns Global statistics)
3. Select Transformation name "CountryOrGlobalTotals" from COVID19.Transformations container
4. If you want to new UI then create new transformation under COVID19.Transformations container

# Compatibility
Tested with Kentico 10.0 and 12.0.29

# [License](LICENSE)
