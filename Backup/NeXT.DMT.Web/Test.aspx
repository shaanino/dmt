<%@ Page Title="" Language="C#" MasterPageFile="~/DMT.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="NeXT.DMT.Web.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderJavaScript" runat="server">

    <script type="text/javascript">

        $(document).ready(function myfunction() {

            $.ajax({
                type: "POST",
                url: "AjaxService.svc/GetAllApplications",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    alert(data);
                }

            });

        });
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:DropDownList runat="server" ID="DropDownTest">
    </asp:DropDownList>
    <asp:Button Text="Test" runat="server" ID="ButtonTest" CssClass="btn" />
    <asp:HyperLink NavigateUrl="navigateurl" runat="server" CssClass="btn" Text="Hello World" />
    <asp:TextBox runat="server" CssClass="textbox" />
</asp:Content>
