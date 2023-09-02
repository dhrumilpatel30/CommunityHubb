<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PostHomeView.aspx.cs" Inherits="CommunityHubb.ManagePost.PostHomeView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>

        <div class="container p-3">
            <div class="fw-bolder display-6 mb-3">Recent Posts</div>
        </div>
        <table class="table-borderless w-100">
            <asp:Repeater runat="server" ID="postlistview">
                <ItemTemplate>
                    <tbody class="table">
                        <tr>
                            <td>
                                <div onclick='window.location=`<%#Request.Url.Scheme + "://" + Request.Url.Authority + 
    Request.ApplicationPath.TrimEnd('/') + "/" %>ManagePost/PostHome.aspx?id=<%#Eval("Id") %>`'
                                    class="btn btn-outline-dark container p-3 rounded-4 m-1">
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
    </main>
</asp:Content>
