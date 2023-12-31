﻿<%@ Page Title="Post Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="CommunityHubb.ManagePost.Home" EnableEventValidation="false" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="container p-3">
            <div class="row">
                <div class="col-8">
                    <div class="card m-2 bg-light">
                        <div class="card-body">
                            <div class="border-bottom border-dark">
                                <asp:Label class="fw-bolder fs-1 mb-2" runat="server" ID="titlebox"></asp:Label>
                            </div>
                            <div class="m-2 ms-3 fs-5">
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
                    <div class="card m-2 bg-light">
                        <div class="card-body">
                            <div class="fw-bold fs-4 ms-2 mt-2">Community</div>
                            <a runat="server" id="communityLink" class="text-decoration-none">
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
                        <div class="card-body d-">
                            <div class="border-bottom border-dark mb-3 d-flex justify-content-between">
                                <div class="fw-bolder fs-2 mb-2">Replies </div>
                                <asp:Button class="btn btn-dark" Style="height: fit-content;" runat="server" ID="AddReplyBtn" OnClick="AddReplyBtn_Click" Text="Add Reply" />
                            </div>
                            <div runat="server" id="replyBox">
                                <div class="fs-6 border border-1 p-3 rounded-4 bg-light">
                                    <div class="row">
                                        <div class="col-8">
                                            <h5 class="fw-bold">Your Reply</h5>
                                            <h4 class="fw-bold pt-1 d-flex p-1">
                                                <asp:TextBox runat="server" CssClass="w-100 p-3 fw-bold" ID="replyData" TextMode="MultiLine"></asp:TextBox></h4>
                                        </div>
                                        <div class="col-4 align-self-center">

                                            <asp:Button runat="server" ID="SaveReply" OnClick="SaveReply_Click" CssClass='btn btn-dark' Text="Save" />
                                            <asp:Button runat="server" ID="Cancel" OnClick="Cancel_Click" CssClass='btn btn-outline-dark' Text="Cancel" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:Repeater runat="server" ID="repliesListView">
                                <ItemTemplate>

                                    <div class="fs-6 bg-light border border-1 p-3 rounded-4 mt-2">
                                        <div class="row">
                                            <div class="col-8">

                                                <div class="d-flex justify-content-between">
                                                </div>
                                                <h4 class="fw-bold pt-1 d-flex"><%#Eval("Content") %></h4>
                                                <div style='font-weight: 600; font-size: 0.9rem;'>
                                                    By: <a href='../Account/Home.aspx?id=<%#Eval("User.Id") %>' class="text-decoration-none"><span class="fw-bolder"><%#Eval("User.Name") %></span></a>, On: <%#Eval("CreatedAt") %>
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
