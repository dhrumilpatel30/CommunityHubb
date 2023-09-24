<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="CommunityHubb.Account.Home" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="container p-3">
            <div class="row">
                <div class="col-8">
                    <div class="card m-2" style="height: 250px">
                        <div class="card-body bg-light">
                            <div class="row border-bottom border-dark m-1">
                                <div class="col-9">
                                    <asp:Label class="fw-bolder fs-1 mb-2" runat="server" Text="User Name" ID="namebox"></asp:Label>
                                </div>
                            </div>
                            <div class="m-2 ms-3 fs-5">
                                <div class="fw-bold">About the user:</div>
                                <asp:Label ID="userAbout" runat="server" Text="This is description" CssClass="m-2 fs-5"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="card m-2 border-0">
                        <div class="card-body">
                            <div class="border-bottom border-dark mb-2">
                                <div class="fw-bolder fs-3" >User Creations</div>
                            </div>
                            <div class="d-flex m-2 justify-content-around">
                                <asp:Button runat="server" OnClick="NavPost_Click" ID="navPost" Text="Posts" CssClass="btn btn-dark fs-5 w-100 m-2"></asp:Button>
                                <asp:Button runat="server" OnClick="NavReply_Click" ID="navReply" Text="Replies" CssClass="btn btn-dark fs-5 w-100 m-2"></asp:Button>
                            </div>
                            <table class="table-borderless w-100">
                                <asp:Repeater runat="server" ID="postsOfUser">
                                    <ItemTemplate>
                                        <tr>
                                            <div class="row mb-1">
                                                <div class="col-12">
                                                    <div onclick='window.location=`<%#Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/" %>ManagePost/Home.aspx?id=<%#Eval("Id") %>`'
                                                        class="btn btn-light border border-1 p-3 rounded-4 w-100">
                                                        <div class="d-flex" style='font-weight: 600'>
                                                            In Community: <a href='../ManageCommunity/CommunityHome.aspx?id=<%#Eval("CommunityId")%>' class="text-decoration-none ms-1"><span class="fw-bolder"><%# Eval("Community.Name") %></span> </a>
                                                        </div>
                                                        <h4 class="fw-bold d-flex"><%#Eval("Title") %></h4>
                                                        <div class="d-flex" style='font-weight: 500'>Posted On: <%#Eval("CreatedAt") %></div>
                                                    </div>
                                                </div>
                                            </div>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Repeater runat="server" ID="replyForUser">
                                    <ItemTemplate>
                                        <tr>
                                            <div class="row mb-1">
                                                <div class="col-12">
                                                    <div onclick='window.location=`<%#Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/" %>ManagePost/Home.aspx?id=<%#Eval("PostId") %>`'
                                                        class="btn btn-light border border-1 p-3 rounded-4 w-100">
                                                        <div class="d-flex" style='font-weight: 600'>
                                                            On Post: <%#Eval("Post.Title")%>
                                                        </div>
                                                        <h5 class="fw-bold pt-1 d-flex"><%#Eval("Content") %></h5>
                                                        <div class="d-flex" style='font-weight: 500'>Replied On: <%#Eval("CreatedAt") %></div>
                                                    </div>
                                                </div>
                                            </div>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="col-4">
                    <div class="card m-2">
                        <div class="card-body bg-light">
                            <div class="border-bottom border-dark mt-3">
                                <asp:Label class="fw-bolder fs-3 mb-3" runat="server" Text="Member of" ID="Label1"></asp:Label>
                            </div>
                            <table class="table-borderless w-100">
                                <asp:Repeater runat="server" ID="commlistforuser">
                                    <ItemTemplate>
                                        <tr>
                                            <div onclick='window.location=`<%#Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/" %>ManageCommunity/Home.aspx?id=<%#Eval("Id") %>`'
                                                class="btn btn-link text-decoration-none m-2 d-block text-start">
                                                <div class="fw-bold fs-5"><%# Eval("Name") %></div>
                                            </div>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
