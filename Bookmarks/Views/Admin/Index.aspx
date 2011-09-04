<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Bookmarks.Models.BookmarksListViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Admin : All Bookmarks
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Bookmarks</h2>
    <%foreach (var bookmark in Model.Bookmarks)
      {%>
    <% Html.RenderPartial("BookmarkSummary", bookmark); %>
    <%: Html.ActionLink("Edit", "Edit", new { bookmark.BookmarkID })%>&nbsp;
    <%: Html.ActionLink("Delete", "Delete", new { bookmark.BookmarkID })%>
    <%} %>
    <div class="pager">
        <%: Html.PageLinks(
            Model.PagingInfo,
            x => Url.Action("List", new { page = x }
            )
        )%>
    </div>
    <p>
        <%: Html.ActionLink("Add a new bookmark", "New") %>
    </p>
</asp:Content>
