<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSWebParts_COVID_19_CoronavirusCountryStatistics"  CodeFile="~/CMSWebParts/COVID-19/CoronavirusCountryStatistics.ascx.cs" %>
<asp:Label ID="lbl_Error" runat="server" ForeColor="#CC0000"></asp:Label>
<br />
<asp:Label ID="lbl_CountryStas" runat="server"></asp:Label>

<cms:BasicRepeater ID="basicRepeater" runat="server" />
