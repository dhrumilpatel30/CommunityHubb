<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PostHome.aspx.cs" Inherits="CommunityHubb.ManagePost.PostHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>

        <div class="container p-3">
            <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Post] WHERE ([Id] = @Id)" ID="Post">
                <SelectParameters>
                    <asp:QueryStringParameter QueryStringField="id" Name="Id" Type="Int32"></asp:QueryStringParameter>
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:Repeater runat="server" DataSourceID="Post">
                <ItemTemplate>


                    <div class="row">
                        <div class="col-8">
                            <div class="card m-2">
                                <div class="card-body">
                                    <div class="border-bottom border-dark">
                                        <asp:Label class="fw-bolder fs-1 mb-2" runat="server" Text='<%#Eval("Title") %>' ID="titlebox"></asp:Label>
                                    </div>
                                    <div class="m-2 ms-3 fs-4">
                                        <asp:Literal runat="server" ID="postcontent" EnableViewState="true" Text='<%#Eval("Content") %>'></asp:Literal>
                                    </div>
                                    <div class="mt-5">
                                        <asp:Label class="fw-bolder" runat="server" Text='<%#Eval("Date") %>' ID="dateofpost"></asp:Label>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="card m-2">
                                <div class="card-body">
                                    <div class="fw-bold fs-4 ms-2 mt-2">Community</div>
                                    <a href="/ManageCommunity/CommunityHome.aspx?id=<%# Eval("CommunityId") %>" class="text-decoration-none">
                                        <asp:Label runat="server" ID="communityname" Text='<%#Eval("CommunityName")%>' CssClass="fw-bolder fs-4 ms-3"></asp:Label>
                                    </a>
                                    <div class="fw-bold fs-4 ms-2 mt-2">Author</div>
                                    <a href="/UserAccount/UserHome.aspx?id=<%# Eval("AutorId") %>" class="text-decoration-none">
                                        <asp:Label runat="server" ID="Label1" Text='<%#Eval("Author") %>' CssClass="fw-bolder fs-4 ms-3"></asp:Label>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </main>
</asp:Content>
