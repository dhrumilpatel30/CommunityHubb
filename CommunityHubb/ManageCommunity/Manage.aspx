<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="CommunityHubb.ManageCommunity.Manage" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container p-3">
        <div class="row">
            <div class="col-8">
                <div class="card m-2">
                    <div class="card-body bg-light">
                        <div class="border-bottom border-dark m-1">
                            <a class="fw-bolder fs-1 mb-2 col-8 text-decoration-none" runat="server" Text="Community Name" ID="titlebox"></a>
                        </div>
                        <div class="m-2 ms-3 fs-5">
                            <div class="fw-bold">Description:</div>
                            <asp:Label ID="commdesc" runat="server" Text="" CssClass="m-2 fs-5"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="card m-2 border-0">
            <div class="card-body">
                <div class="border-bottom border-dark mb-3 d-flex justify-content-between">
                    <div class="fw-bolder fs-2 mb-2">Manage Community </div>
                </div>
                <div class="d-flex m-2 justify-content-around">
                    <asp:Button runat="server" OnClick="NavPost_Click" ID="navPost" Text="Posts" CssClass="btn btn-dark fs-5 w-100 m-2"></asp:Button>
                    <asp:Button runat="server" OnClick="NavUser_Click" ID="navUser" Text="Members" CssClass="btn btn-dark fs-5 w-100 m-2"></asp:Button>
                </div>
                <div id="postsView" runat="server">
                    <h4 class="fw-bold m-2">Posts</h4>
                    <table class="table-borderless w-100">
                        <asp:Repeater runat="server" ID="postsOfCommunity">
                            <ItemTemplate>
                                <tbody>
                                    <tr>
                                        <td>
                                            <div class="row mb-1">
                                                <div class="col-10">
                                                    <div onclick='window.location=`<%#Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/" %>ManagePost/Home.aspx?id=<%#Eval("Id") %>`'
                                                        class="btn btn-light border border-1 p-3 rounded-4 w-100">
                                                        <div class="d-flex" style='font-weight: 600'>
                                                            By User: <a href='../Account/Home.aspx?id=<%#Eval("UserId")%>' class="text-decoration-none"><span class="fw-bolder"><%# Eval("User.Name") %></span> </a>
                                                        </div>
                                                        <h4 class="fw-bold d-flex"><%#Eval("Title") %></h4>
                                                        <div class="d-flex" style='font-weight: 500'>Posted On: <%#Eval("CreatedAt") %></div>
                                                    </div>
                                                </div>
                                                <div class="col-2">
                                                    <asp:Button runat="server" CssClass="btn btn-danger" Text="Delete Post" ID="deletePost" OnClick="DeletePost_Click" CommandArgument='<%#Eval("Id") %>'></asp:Button>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <div id="userView" runat="server">
                    <h5 class="fw-bold m-2">Admin Members</h5>
                    <table class="table-borderless w-100">
                        <asp:Repeater runat="server" ID="usersOfCommAdmin">
                            <ItemTemplate>
                                <tbody>
                                    <tr>
                                        <td>
                                            <div class="row mb-1">
                                                <div class="col-10">
                                                    <div onclick='window.location=`<%#Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/" %>Account/Home.aspx?id=<%#Eval("Id") %>`'
                                                        class="btn btn-light border border-1 p-3 rounded-4 w-100">

                                                        <h4 class="fw-bold d-flex"><%#Eval("Name") %></h4>
                                                        <div class="d-flex" style='font-weight: 500'>About User: <%#Eval("About") %></div>
                                                    </div>
                                                </div>
                                                <div class="col-2">
                                                    <asp:Button runat="server" CssClass="btn btn-danger mb-1" Text="Remove User" ID="deleteUser" OnClick="DeleteUser_Click" CommandArgument='<%#Eval("Id") %>'></asp:Button>
                                                    <asp:Button runat="server" CssClass="btn btn-dark" Text="Remove From Admin" ID="removeAdmin" OnClick="RemoveAdmin_Click" CommandArgument='<%#Eval("Id") %>'></asp:Button>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <h5 class="fw-bold m-2">Non Admin Members</h5>
                    <table class="table-borderless w-100">
                        <asp:Repeater runat="server" ID="userOfCommNonAdm">
                            <ItemTemplate>
                                <tbody>
                                    <tr>
                                        <td>
                                            <div class="row mb-1">
                                                <div class="col-10">
                                                    <div onclick='window.location=`<%#Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/" %>Account/Home.aspx?id=<%#Eval("Id") %>`'
                                                        class="btn btn-light border border-1 p-3 rounded-4 w-100">

                                                        <h4 class="fw-bold d-flex"><%#Eval("Name") %></h4>
                                                        <div class="d-flex" style='font-weight: 500'>About User: <%#Eval("About") %></div>
                                                    </div>
                                                </div>
                                                <div class="col-2">
                                                    <asp:Button runat="server" CssClass="btn btn-danger mb-1" Text="Remove User" ID="deleteUser" OnClick="DeleteUser_Click" CommandArgument='<%#Eval("Id") %>'></asp:Button>
                                                    <asp:Button runat="server" CssClass="btn btn-dark" Text="Make an Admin" ID="addAdmin" OnClick="AddAdmin_Click" CommandArgument='<%#Eval("Id") %>'></asp:Button>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <h5 class="fw-bold m-2">Non member users</h5>
                    <table class="table-borderless w-100">
                        <asp:Repeater runat="server" ID="nonUserOfComm">
                            <ItemTemplate>
                                <tbody>
                                    <tr>
                                        <td>
                                            <div class="row mb-1">
                                                <div class="col-10">
                                                    <div onclick='window.location=`<%#Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/" %>Account/Home.aspx?id=<%#Eval("Id") %>`'
                                                        class="btn btn-light border border-1 p-3 rounded-4 w-100">

                                                        <h4 class="fw-bold d-flex"><%#Eval("Name") %></h4>
                                                        <div class="d-flex" style='font-weight: 500'>About User: <%#Eval("About") %></div>
                                                    </div>
                                                </div>
                                                <div class="col-2">
                                                    <asp:Button runat="server" CssClass="btn btn-dark" Text="Add in Community" ID="addUser" OnClick="AddUser_Click" CommandArgument='<%#Eval("Id") %>'></asp:Button>
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
</asp:Content>
