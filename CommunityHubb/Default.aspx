<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CommunityHubb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>

        <div class="container p-3">
            <!--<asp:GridView runat="server" DataSourceID="publicPosts" ID="publicPostsView"></asp:GridView>-->


            <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                SelectCommand="SELECT Id, * FROM Post WHERE (CommunityId IN (SELECT Id FROM Community WHERE (isPrivate = 'False'))) ORDER BY Date DESC"
                ID="publicPosts"></asp:SqlDataSource>
            <div class="fw-bolder display-6 mb-3">Recent Posts</div>
            <table class="table-borderless w-100">
                <asp:Repeater runat="server" DataSourceID="publicPosts">
                    <ItemTemplate>
                        <tbody class="table">
                            <tr>
                                <td>
                                    <button class="btn btn-outline-dark container p-3 rounded-4 m-1">
                                        <div class="d-flex justify-content-between">
                                            <div style='font-weight: 600'>In:<span class="fw-bolder"> <%# Eval("CommunityName") %></span>, By<span class="fw-bolder"> <%# Eval("Author") %></span></div>
                                            <div style='font-weight: 500'>On: <%# Eval("Date") %> </div>
                                        </div>
                                        <h2 class="fw-bold pt-1 d-flex"><%# Eval("Title") %></h2>
                                    </button>
                                </td>
                            </tr>
                        </tbody>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>

    </main>

</asp:Content>
