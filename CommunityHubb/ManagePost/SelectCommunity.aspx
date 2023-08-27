<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SelectCommunity.aspx.cs" Inherits="CommunityHubb.ManagePost.SelectCommunity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="container p-3">
            <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                SelectCommand="SELECT * FROM Community WHERE (Id IN (SELECT CommunityId FROM CommunityUser WHERE (UserId = @Id)))"
                ID="userCommunities">
                <SelectParameters>
                    <asp:SessionParameter SessionField="UserId" Name="Id"></asp:SessionParameter>
                </SelectParameters>
            </asp:SqlDataSource>
            <div class="fw-bolder display-6 mb-3">Select community to post</div>
            <table class="table-borderless w-100">

                <asp:Repeater runat="server" DataSourceID="userCommunities">
                    <ItemTemplate>

                        <tbody class="table">
                            <tr>
                                <td>
                                    <a href="PostCreate.aspx?id=<%#Eval("Id") %>" class="text-decoration-none">
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
