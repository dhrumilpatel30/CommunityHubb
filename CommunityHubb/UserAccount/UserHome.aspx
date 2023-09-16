<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserHome.aspx.cs" Inherits="CommunityHubb.UserAccount.UserHome" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main>

        <div class="container p-3">

            <div class="row">
                <div class="col-8">
                    <div class="card m-2" style="height: 250px">
                        <div class="card-body">
                            <div class="row border-bottom border-dark m-1">
                                <div class="col-9">
                                    <asp:Label class="fw-bolder fs-1 mb-2" runat="server" Text="User Name" ID="namebox"></asp:Label>
                                </div>
                                <div runat="server" class="col-3" id="updatebox">
                                    <a class="btn btn-dark p-2 d-block" href="UserUpdate.aspx">Update Your Profile</a>
                                </div>
                            </div>
                            <div class="m-2 ms-3 fs-5">
                                <div class="fw-bold">About the user:</div>
                                <asp:Label ID="userAbout" runat="server" Text="This is description" CssClass="m-2 fs-5"></asp:Label>
                            </div>

                        </div>
                    </div>
                    <div class="card m-2">
                        <div class="card-body">
                            <div class="border-bottom border-dark mb-2">
                                <asp:Label class="fw-bolder fs-3" runat="server" Text="Posts by user" ID="Label2"></asp:Label>
                            </div>
                            <table class="table-borderless w-100">
                                <asp:Repeater runat="server" ID="postsOfUser">
                                    <ItemTemplate>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <div onclick='window.location=`<%#Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/" %>ManagePost/PostHome.aspx?id=<%#Eval("Id") %>`'
                                                        class="btn btn-light border border-1 container p-3 rounded-4 m-1">
                                                        <div class="d-flex justify-content-between">
                                                            <div style='font-weight: 600'>
                                                                In:<a href='../ManageCommunity/CommunityHome.aspx?id=<%#Eval("CommunityId")%>' class="text-decoration-none"><span class="fw-bolder"> <%# Eval("CommunityName") %></span> </a>, 
                                                                By <a href='../UserAccount/UserHome.aspx?id=<%#Eval("AutorId") %>' class="text-decoration-none"><span class="fw-bolder"><%# Eval("Author") %></span> </a>
                                                            </div>
                                                            <div style='font-weight: 500'>On: <%# Eval("Date") %> </div>
                                                        </div>
                                                        <h2 class="fw-bold pt-1 d-flex"><%# Eval("Title") %></h2>
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
                <div class="col-4">
                    <div class="card m-2">
                        <div class="card-body">
                            <div class="border-bottom border-dark mt-3">
                                <asp:Label class="fw-bolder fs-3 mb-3" runat="server" Text="Member of" ID="Label1"></asp:Label>
                            </div>
                            <table class="table-borderless w-100">
                                <asp:Repeater runat="server" ID="commlistforuser">
                                    <ItemTemplate>
                                        <tbody class="table">
                                            <tr>
                                                <td>
                                                    <div onclick='window.location=`<%#Request.Url.Scheme + "://" + Request.Url.Authority + 
    Request.ApplicationPath.TrimEnd('/') + "/" %>ManageCommunity/CommunityHome.aspx?id=<%#Eval("Id") %>`'
                                                        class="btn btn-light container rounded-4 m-2">
                                                        <h4 class="fw-bold d-flex"><%# Eval("Name") %></h4>

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
