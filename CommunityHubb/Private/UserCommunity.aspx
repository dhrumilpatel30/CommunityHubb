<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserCommunity.aspx.cs" MasterPageFile="~/Site.Master" Inherits="CommunityHubb.Private.UserCommunity" %>
<%@ Register Src="~/postlist.ascx" TagName="posts" TagPrefix="uc" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <div class="container p-3">
            <asp:Label ID="successlabel" class="alert alert-success alert-dismissible fade show" Text="Login successful" runat="server" Visible="false" />
            <asp:Label ID="FailLabel" class="alert alert-danger alert-dismissible fade show" Text="Error" runat="server" Visible="false" />




            <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                SelectCommand="SELECT Id, * FROM Post WHERE (CommunityId IN (SELECT CommunityId FROM CommunityUser WHERE (UserId=@Id))) ORDER BY Date DESC"
                ID="postslistcomm">
                <SelectParameters>
                    <asp:SessionParameter SessionField="UserId" Name="Id"></asp:SessionParameter>
                </SelectParameters>
            </asp:SqlDataSource>
            <div class="fw-bolder display-6 mb-3">Your Followed Posts</div>
            
            <uc:posts runat="server" ID="posts" DataSourceID="postslistcomm" />

        </div>


    </main>

</asp:Content>
