<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Bookmarks.Models.BookmarkViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit Bookmark
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Edit Bookmark</h2>
    <%Html.EnableClientValidation(); %>
    <%using (Html.BeginForm("Edit", "Admin", FormMethod.Post))
      { %>
    <%: Html.ValidationSummary() %>
    <h3>
        Bookmark Details</h3>
    <%: Html.HiddenFor(x => x.Bookmark.BookmarkID) %>
    <%: Html.HiddenFor(x => x.Bookmark.UserID) %>
    <div>
        Name:
        <%:Html.EditorFor(x => x.Bookmark.Name) %>
    </div>
    <div>
        Url:
        <%:Html.EditorFor(x => x.Bookmark.Url) %></div>
    <div>
        Tags:
        <%: Html.EditorFor(x => x.Tags) %>
    </div>
    <p align="center">
        <input type="submit" value="Save" /></p>
    <%:Html.ActionLink("Cancel and return to list", "Index")%>
    <%} %>
</asp:Content>
