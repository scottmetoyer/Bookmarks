<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bookmarks.Domain.Entities.Bookmark>" %>
<div class="item">
    <h4>
        <a href="<%: Model.Url %>">
            <%: Model.Name %></a></h4>
    <div class="tags">
        <% Html.RenderAction("GetTags", "Bookmarks", new { Model.BookmarkID });%>
    </div>
</div>
