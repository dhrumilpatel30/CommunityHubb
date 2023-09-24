<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="CommunityHubb.ManagePost.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>

        <div class="container-fluid p-3">
            <div class="fw-bolder display-6 mb-3">Posts</div>
        </div>
        <div class="row">
            <div class="col-lg-3 col-md-5 col-sm-12 border border-1 rounded-2 p-4 h-100 bg-light" style="height: fit-content; top: 75px; position: sticky;">
                <h2 class="fw-bold fs-4">Filter Posts</h2>
                <div class="text-danger mb-3 ms-1">
                    <asp:Literal ID="loginError" runat="server" />
                </div>
                <div class="form-group">
                    <label class="fs-5 fw-bold">Post Type:</label>
                    <div class="form-check ps-2 p-1 fw-bold">
                        <asp:RadioButton ID="publicRadio" runat="server" Text=" Public" GroupName="postType" />
                    </div>
                    <div class="form-check ps-2 p-1 fw-bold">
                        <asp:RadioButton ID="privateRadio" runat="server" Text=" Private" GroupName="postType" />
                    </div>
                    <div class="form-check ps-2 p-1 fw-bold">
                        <asp:RadioButton ID="bothRadio" runat="server" Text=" Both" GroupName="postType" Checked="True" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="fs-5 fw-bold">Sorting Method:</label>
                    <div class="form-check ps-2 p-1 fw-bold">
                        <asp:RadioButton ID="recentRadio" runat="server" Text=" Recent" GroupName="sortingMethod" Checked="True" />
                    </div>
                    <div class="form-check ps-2 p-1 fw-bold">
                        <asp:RadioButton ID="popularRadio" runat="server" Text=" Popular" GroupName="sortingMethod" />
                    </div>
                    <div class="form-check ps-2 p-1 fw-bold">
                        <asp:RadioButton ID="randomRadio" runat="server" Text=" Random" GroupName="sortingMethod" />
                    </div>
                    <div class="form-check ps-2 p-1 fw-bold">
                        <asp:RadioButton ID="alphabeticalRadio" runat="server" Text=" Alphabetical" GroupName="sortingMethod" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="fs-5 fw-bold">View Posts:</label>
                    <div class="form-check ps-2 p-1 fw-bold">
                        <asp:RadioButton ID="followedRadio" runat="server" Text=" Followed Posts" GroupName="viewPosts" />
                    </div>
                    <div class="form-check ps-2 p-1 fw-bold">
                        <asp:RadioButton ID="unfollowedRadio" runat="server" Text=" Unfollowed Posts" GroupName="viewPosts" />
                    </div>
                    <div class="form-check ps-2 p-1 fw-bold">
                        <asp:RadioButton ID="bothViewRadio" runat="server" Text=" Both" GroupName="viewPosts" Checked="True" />
                    </div>
                </div>
                <asp:Button ID="applyFiltersButton" runat="server" Text="Apply Filters" CssClass="btn btn-dark mt-3" OnClick="ApplyFilters" />
            </div>
            <div class="col-8">
                <table class="table-borderless w-100">
                    <asp:Repeater runat="server" ID="postlistview">
                        <ItemTemplate>
                            <tr>
                                <div class="col-12 m-2">
                                    <div onclick='window.location=`<%#Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/" %>ManagePost/Home.aspx?id=<%#Eval("Id") %>`'
                                        class="btn btn-light border border-1 p-3 rounded-4 w-100">
                                        <div class="d-flex justify-content-between">
                                            <div style='font-weight: 600'>
                                                In: <a href='../ManageCommunity/Home.aspx?id=<%#Eval("Community.Id")%>' class="text-decoration-none"><span class="fw-bolder"><%# Eval("Community.Name") %></span> </a>, 
                                                By: <a href='../Account/Home.aspx?id=<%#Eval("User.Id") %>' class="text-decoration-none"><span class="fw-bolder"><%# Eval("User.Name") %></span> </a>
                                            </div>

                                            <div style='font-weight: 500'>On: <%# Eval("CreatedAt") %> </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-6">
                                                <h3 class="fw-bold pt-1 d-flex"><%# Eval("Title") %></h3>
                                            </div>
                                            <div class="col-6 text-end fw-bold">
                                                <div><%#Eval("ReplyCount")%></div>
                                                <div><%#Eval("LikeCount")%> <%#Eval("DislikeCount")%></div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </main>
</asp:Content>
