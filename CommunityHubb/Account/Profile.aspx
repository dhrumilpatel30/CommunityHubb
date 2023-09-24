<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="CommunityHubb.Account.Profile" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="container p-3">
            <div class="row">
                <div class="col-12">
                    <div class="card m-2" style="height: 250px">
                        <div class="card-body bg-light">
                            <div class="row border-bottom border-dark m-1">
                                <div class="col-9">
                                    <asp:Label class="fw-bolder fs-1 mb-2" runat="server" Text="User Name" ID="namebox"></asp:Label>
                                </div>
                                <div runat="server" class="col-3" id="updatebox">
                                    <a class="btn btn-dark p-2 d-block" href="Update.aspx">Update Your Profile</a>
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
                                <asp:Label class="fw-bolder fs-3" runat="server" Text="Your Creations" ID="Label2"></asp:Label>
                            </div>
                            <div class="d-flex m-2 justify-content-around">
                                <asp:Button runat="server" OnClick="NavPost_Click" ID="navPost" Text="Posts" CssClass="btn btn-dark fs-5 w-100 m-2"></asp:Button>
                                <asp:Button runat="server" OnClick="NavReply_Click" ID="navReply" Text="Replies" CssClass="btn btn-dark fs-5 w-100 m-2"></asp:Button>
                                <asp:Button runat="server" OnClick="NavComm_Click" ID="navComm" Text="Communities" CssClass="btn btn-dark fs-5 w-100 m-2"></asp:Button>
                            </div>
                            <table class="table-borderless w-100">
                                <asp:Repeater runat="server" ID="postsOfUser">
                                    <ItemTemplate>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <div class="row mb-1">
                                                        <div class="col-10">
                                                            <div onclick='window.location=`<%#Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/" %>ManagePost/Home.aspx?id=<%#Eval("Id") %>`'
                                                                class="btn btn-light border border-1 p-3 rounded-4 w-100">
                                                                <div class="d-flex" style='font-weight: 600'>
                                                                    In Community: <a href='../ManageCommunity/Home.aspx?id=<%#Eval("CommunityId")%>' class="text-decoration-none"><span class="fw-bolder"><%# Eval("Community.Name") %></span> </a>
                                                                </div>
                                                                <h4 class="fw-bold d-flex"><%#Eval("Title") %></h4>
                                                                <div class="d-flex" style='font-weight: 500'>Posted On: <%#Eval("CreatedAt") %></div>
                                                            </div>
                                                        </div>
                                                        <div class="col-2">
                                                            <asp:Button runat="server" CssClass="fs-5 fw-bold btn btn-danger" Text="Delete" ID="deletePost" OnClick="DeletePost_Click" CommandArgument='<%#Eval("Id") %>'></asp:Button>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Repeater runat="server" ID="replyForUser">
                                    <ItemTemplate>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <div class="row mb-1">
                                                        <div class="col-10">
                                                            <div onclick='window.location=`<%#Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/" %>ManagePost/Home.aspx?id=<%#Eval("PostId") %>`'
                                                                class="btn btn-light border border-1 p-3 rounded-4 w-100">
                                                                <div class="d-flex" style='font-weight: 600'>
                                                                    On Post: <%# Eval("Post.Title") %>
                                                                </div>
                                                                <h5 class="fw-bold pt-1 d-flex"><%#Eval("Content") %></h5>
                                                                <div class="d-flex" style='font-weight: 500'>Replied On: <%#Eval("CreatedAt") %></div>
                                                            </div>
                                                        </div>
                                                        <div class="col-2">
                                                            <asp:Button runat="server" CssClass="fs-5 fw-bold btn btn-danger" Text="Delete" ID="deleteReply" OnClick="DeleteReply_Click" CommandArgument='<%#Eval("Id") %>'></asp:Button>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Repeater runat="server" ID="commListOfUser">
                                    <ItemTemplate>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <div class="row">
                                                        <div class="col-10 mb-1">
                                                            <div onclick='window.location=`<%#Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/" %>ManageCommunity/Home.aspx?id=<%#Eval("Id") %>`'
                                                                class="btn btn-light border border-1 p-3 rounded-4 w-100">
                                                                <div class="d-flex" style='font-weight: 500'>Created On: <%#Eval("CreatedAt") %></div>
                                                                <h4 class="fw-bold pt-1 d-flex"><%#Eval("Name") %></h4>
                                                                <h6 class="d-flex"><%#Eval("Description") %></h6>
                                                            </div>
                                                        </div>
                                                        <div class="col-2">
                                                            <asp:Button runat="server" CssClass="fs-5 fw-bold btn btn-danger mb-1" Text="Delete" ID="deleteCommunity" OnClick="DeleteCommunity_Click" CommandArgument='<%#Eval("Id") %>'></asp:Button>
                                                            <a class="fs-5 fw-bold btn btn-dark" href='../ManageCommunity/Manage.aspx?id=<%#Eval("Id") %>'>Manage</a>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </tbody>
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
