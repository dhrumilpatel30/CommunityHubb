<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserHome.aspx.cs" Inherits="CommunityHubb.UserAccount.UserHome" %>

<%@ Register Src="~/postlist.ascx" TagName="posts" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main>

        <div class="container p-3">

            <div class="row">
                <div class="col-8">
                    <div class="card m-2" style="height:250px">
                        <div class="card-body">
                            <asp:SqlDataSource ID="postslistuser" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Post] WHERE ([AutorId] = @AutorId) ORDER BY [Date] DESC">
                                <SelectParameters>
                                    <asp:QueryStringParameter QueryStringField="id" Name="AutorId" Type="Int32"></asp:QueryStringParameter>
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT Name, Id FROM Community WHERE Id IN( SELECT CommunityId FROM CommunityUser WHERE UserId = @Id) AND isPrivate = 'false'

"
                                ID="commlistforuser">
                                <SelectParameters>
                                    <asp:QueryStringParameter QueryStringField="id" Name="Id"></asp:QueryStringParameter>
                                </SelectParameters>
                            </asp:SqlDataSource>

                            <div class="border-bottom border-dark">
                                <asp:Label class="fw-bolder fs-1 mb-2" runat="server" Text="User Name" ID="titlebox"></asp:Label>
                            </div>                            <div class="m-2 ms-3 fs-5">
                                <div class="fw-bold">About the user:</div>
                                <asp:Label ID="commdesc" runat="server" Text="This is description" CssClass="m-2 fs-5"></asp:Label>
                            </div>

                        </div>
                    </div>
                    <div class="card m-2">
                        <div class="card-body">
                            <div class="border-bottom border-dark mb-2">
                                <asp:Label class="fw-bolder fs-3" runat="server" Text="Posts by user" ID="Label2"></asp:Label>
                            </div>

                            <uc:posts runat="server" ID="posts" DataSourceID="postslistuser" />
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
    <asp:Repeater runat="server" DataSourceID="commlistforuser">
        <ItemTemplate>
            <tbody class="table">
                <tr>
                    <td>
                            <div onclick='window.location=`<%#Request.Url.Scheme + "://" + Request.Url.Authority + 
    Request.ApplicationPath.TrimEnd('/') + "/" %>ManageCommunity/CommunityHome.aspx?id=<%#Eval("Id") %>`' class="btn btn-light container rounded-4 m-2">
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
