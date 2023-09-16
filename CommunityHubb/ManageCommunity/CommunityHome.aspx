<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CommunityHome.aspx.cs" Inherits="CommunityHubb.ManageCommunity.CommunityHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>

        <div class="container p-3">

            <div class="row">
                <div class="col-8">
                    <div class="card m-2" style="height: 200px">
                        <div class="card-body">
                            <div class="border-bottom border-dark row m-1">
                                <asp:Label class="fw-bolder fs-1 mb-2 col-9" runat="server" Text="Community Name" ID="titlebox"></asp:Label>
                                <div class="col-3">
                                    <asp:Button CssClass="btn btn-dark d-block" runat="server" ID="followbtn" OnClick="ToggleFollow" />
                                </div>

                            </div>
                            <div class="m-2 ms-3 fs-5">
                                <div class="fw-bold">Description:</div>
                                <asp:Label ID="commdesc" runat="server" Text="This is description" CssClass="m-2 fs-5"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="card m-2">
                        <div class="card-body">
                            <div class="border-bottom border-dark mb-2 ">
                                <asp:Label class="fw-bolder fs-3" runat="server" Text="Posts" ID="Label2"></asp:Label>
                            </div>

                            <table class="table-borderless w-100">
                                <asp:Repeater runat="server" ID="postlist">
                                    <ItemTemplate>
                                        <tbody class="table">
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
                                <asp:Label class="fw-bolder fs-3 mb-3" runat="server" Text="Members" ID="Label1"></asp:Label>
                            </div>
                            <table class="table-borderless w-100">
                                <asp:Repeater runat="server" ID="userlist">
                                    <ItemTemplate>
                                        <tbody class="table">
                                            <tr>
                                                <td>
                                                    <div onclick='window.location=`<%#Request.Url.Scheme + "://" + Request.Url.Authority + 
    Request.ApplicationPath.TrimEnd('/') + "/" %>UserAccount/UserHome.aspx?id=<%#Eval("Id") %>`'
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
