<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserCommunity.aspx.cs" MasterPageFile="~/Site.Master" Inherits="CommunityHubb.Private.UserCommunity" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <div class="container p-3">
            <asp:Label ID="successlabel" class="alert alert-success alert-dismissible fade show" Text="Login successful" runat="server" Visible="false" />
            <asp:Label ID="FailLabel" class="alert alert-danger alert-dismissible fade show" Text="Error" runat="server" Visible="false" />




            <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                SelectCommand="SELECT Id, * FROM Post WHERE (CommunityId IN (SELECT CommunityId FROM CommunityUser WHERE (UserId=@Id))) ORDER BY Date DESC"
                ID="publicPosts">
                <SelectParameters>
                    <asp:SessionParameter SessionField="UserId" Name="Id"></asp:SessionParameter>
                </SelectParameters>
            </asp:SqlDataSource>
            <div class="fw-bolder display-6 mb-3">Your Followed Posts</div>
            <table class="table-borderless w-100">
                <asp:Repeater runat="server" DataSourceID="publicPosts">
                    <ItemTemplate>
                        <tbody class="table">
                            <tr>
                                <td>
                                    <div onclick="window.location='../ManagePost/PostHome.aspx?id=<%#Eval("Id") %>'" class="btn btn-outline-dark container p-3 rounded-4 m-1">
                                        <div class="d-flex justify-content-between">
                                            <div style='font-weight: 600'>In:<a href="ManagePost" class="text-decoration-none"><span class="fw-bolder"> <%# Eval("CommunityName") %></span> </a>, By<span class="fw-bolder"> <%# Eval("Author") %></span></div>
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


    </main>

</asp:Content>
