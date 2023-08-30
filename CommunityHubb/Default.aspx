<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CommunityHubb._Default" %>
<%@ Register Src="~/postlist.ascx" TagName="posts" TagPrefix="uc" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>

        <div class="container p-3">
            <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                SelectCommand="SELECT Id, * FROM Post WHERE (CommunityId IN (SELECT Id FROM Community WHERE (isPrivate = 'False'))) ORDER BY Date DESC"
                ID="postslistrec"></asp:SqlDataSource>
            <div class="fw-bolder display-6 mb-3">Recent Posts</div>
            <uc:posts runat="server" ID="posts" DataSourceID="postslistrec" />
        </div>

    </main>

</asp:Content>
