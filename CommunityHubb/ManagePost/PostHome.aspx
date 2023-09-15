<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PostHome.aspx.cs" Inherits="CommunityHubb.ManagePost.PostHome" EnableEventValidation="false" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>

        <div class="container p-3">
            <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Post] WHERE ([Id] = @Id)" ID="Post">
                <SelectParameters>
                    <asp:QueryStringParameter QueryStringField="id" Name="Id" Type="Int32"></asp:QueryStringParameter>
                </SelectParameters>
            </asp:SqlDataSource>

            <div class="row">
                <div class="col-8">
                    <div class="card m-2">
                        <div class="card-body">
                            <div class="border-bottom border-dark">
                                <asp:Label class="fw-bolder fs-1 mb-2" runat="server" ID="titlebox"></asp:Label>
                            </div>
                            <div class="m-2 ms-3 fs-4">
                                <asp:Literal runat="server" ID="postcontent" EnableViewState="true"></asp:Literal>
                            </div>
                            <div class="row">

                                <div class="col-9 align-self-end">
                                    <asp:Literal runat="server" ID="postLikeCount"></asp:Literal>
                                    Likes
    
                                    <asp:Button runat="server" ID="PostLike" OnClick="PostLike_Click" Text="Like" />
                                    <asp:Literal ID="postDislikeCount" runat="server"></asp:Literal>
                                    Dislikes
                                    <asp:Button runat="server" ID="PostDislike" OnClick="PostDislike_Click" Text="Dislike" />
                                </div>
                                <div class="col-3 mt-5">
                                    <asp:Label class="fw-bolder" runat="server" ID="dateofpost"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-4">
                    <div class="card m-2">
                        <div class="card-body">
                            <div class="fw-bold fs-4 ms-2 mt-2">Community</div>
                            <a runat="server"  id="communityLink" class="text-decoration-none">
                                <asp:Label runat="server" ID="communityname" CssClass="fw-bolder fs-4 ms-3"></asp:Label>
                            </a>
                            <div class="fw-bold fs-4 ms-2 mt-2">Author</div>
                            <a runat="server" id="authorLink" class="text-decoration-none">
                                <asp:Label runat="server" ID="authorName" CssClass="fw-bolder fs-4 ms-3"></asp:Label>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="container row">
                <div class="col-10">
                    <div class="card m-2 border-0">
                        <div class="card-body">
                            <div class="border-bottom border-dark mb-3">
                                <div class="fw-bolder fs-2 mb-2">Replies </div>
                            </div>
                            <asp:Repeater runat="server" ID="repliesListView">
                                <ItemTemplate>

                                    <div class="fs-6 border border-1 p-3 rounded-4">
                                        <div class="row">
                                            <div class="col-8">

                                                <div class="d-flex justify-content-between">
                                                </div>
                                                <h4 class="fw-bold pt-1 d-flex"><%#Eval("Content") %></h4>
                                                <div style='font-weight: 600; font-size: 0.9rem;'>
                                                    By
         <a href='../UserAccount/UserHome.aspx?id=<%#Eval("UserId") %>' class="text-decoration-none"><span class="fw-bolder"><%#Eval("authorName") %></span></a>, On: <%#Eval("Created") %>
                                                </div>
                                            </div>
                                            <div class="col-4 align-self-center">
                                                <%#Eval("likeCount") %> Likes
                                                
                                                <asp:Button runat="server" ID="LikeClick" OnClick="LikeClick_Click" CssClass='<%#Eval("likeCss") %>' CommandArgument='<%#Eval("Id") %>' Text="Like" />
                                                <%#Eval("disLikeCount") %> Dislikes
                                                <asp:Button runat="server" ID="Dislike" OnClick="Dislike_Click" CssClass='<%#Eval("dislikeCss") %>' CommandArgument='<%#Eval("Id") %>' Text="Dislike" />
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
