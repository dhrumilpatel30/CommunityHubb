<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CommunityHome.aspx.cs" Inherits="CommunityHubb.ManageCommunity.CommunityHome" %>
<%@ Register Src="~/postlist.ascx" TagName="posts" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>

        <div class="container p-3">

            <div class="row">
                <div class="col-8">
                    <div class="card m-2">
                        <div class="card-body">
                            <div class="border-bottom border-dark">
                                <asp:Label class="fw-bolder fs-1 mb-2" runat="server" Text="Community Name" ID="titlebox"></asp:Label>
                                <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Post] WHERE ([CommunityId] = @CommunityId)" ID="postslistcomm">
                                    <SelectParameters>
                                        <asp:QueryStringParameter QueryStringField="id" Name="CommunityId" Type="Int32"></asp:QueryStringParameter>
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                            <div class="m-2 ms-3 fs-5">
                                <div class="fw-bold">Description:</div>
                                <asp:Label ID="commdesc" runat="server" Text="This is description" CssClass="m-2 fs-5"></asp:Label>
                            </div>

                        </div>
                    </div>
                    <div class="card m-2">
                        <div class="card-body">
                            <div class="border-bottom border-dark mb-2">
                                <asp:Label class="fw-bolder fs-3" runat="server" Text="Posts" ID="Label2"></asp:Label>
                            </div>
                            
            <uc:posts runat="server" ID="posts" DataSourceID="postslistcomm" />
                        </div>
                    </div>
                </div>
                <div class="col-4">
                    <div class="card m-2">
                        <div class="card-body">
                            <div class="border-bottom border-dark mt-3">
                                <asp:Label class="fw-bolder fs-3 mb-3" runat="server" Text="Members" ID="Label1"></asp:Label>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
