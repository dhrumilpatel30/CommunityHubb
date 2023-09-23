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
                        <tr>
                            <div class="col-12 m-2">
                                <div onclick='window.location=`<%#Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/" %>ManagePost/PostCreate.aspx?id=<%#Eval("Id") %>`'
                                    class="btn btn-light border border-1 p-3 rounded-4 w-100">

                                    <div class="fw-bold pt-1 h3 d-flex"><%# Eval("Name") %></div>

                                    <div class="d-flex justify-content-md-start">
                                        <div style='font-weight: 600'>Description:<span class="fw-bolder"> <%# Eval("Description") %></span></div>
                                    </div>
                                </div>
                            </div>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </main>
</asp:Content>
