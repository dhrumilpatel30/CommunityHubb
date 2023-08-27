<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminCommunity.aspx.cs" MasterPageFile="~/Site.Master" Inherits="CommunityHubb.Private.AdminCommunity" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <div class="container p-3">
            <asp:Label ID="successlabel" class="alert alert-success alert-dismissible fade show" Text="Login successful" runat="server" Visible="false" />
            <asp:Label ID="FailLabel" class="alert alert-danger alert-dismissible fade show" Text="Error" runat="server" Visible="false" />




            <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                SelectCommand="SELECT Id, Name, Description, CreatedDate, isPrivate FROM Community WHERE (Id IN (SELECT CommunityId FROM CommunityUser WHERE (UserId = @Id) AND (IsAdmin = 'True')))"
                ID="userCommunities">
                <SelectParameters>
                    <asp:SessionParameter SessionField="UserId" Name="Id"></asp:SessionParameter>
                </SelectParameters>
            </asp:SqlDataSource>
            <div class="fw-bolder display-6 mb-3">Your Communities</div>
            <table class="table-borderless w-100">
                <asp:Repeater runat="server" DataSourceID="userCommunities">
                    <ItemTemplate>

                        <tbody class="table">
                            <tr>
                                <td>
                                    <a href="#" class="text-decoration-none">
                                        <div class="btn btn-outline-dark container p-3 rounded-4 m-1">

                                            <div class="fw-bold pt-1 h3 d-flex"><%# Eval("Name") %></div>

                                            <div class="d-flex justify-content-md-start">
                                                <div style='font-weight: 600'>Description:<span class="fw-bolder"> <%# Eval("Description") %></span></div>
                                            </div>
                                        </div>
                                    </a>
                                </td>
                            </tr>
                        </tbody>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>


    </main>

</asp:Content>
