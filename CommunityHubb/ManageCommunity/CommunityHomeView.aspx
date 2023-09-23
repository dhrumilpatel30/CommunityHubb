<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CommunityHomeView.aspx.cs" Inherits="CommunityHubb.ManageCommunity.CommunityHomeView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="container-fluid p-3">
            <div class="fw-bolder display-6 mb-3">Communities</div>
        </div>
        <div class="row">
            <div class="col-lg-3 col-md-5 col-sm-12 border border-1 rounded-2 p-4 h-100 bg-light" style="height: fit-content; top: 75px; position: sticky;">
                <h2 class="fw-bold fs-4">Filter Communities</h2>
                <div class="text-danger mb-3 ms-1">
                    <asp:Literal ID="loginError" runat="server" />
                </div>
                <div class="form-group">
                    <label class="fs-5 fw-bold">Community Type:</label>
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
                    <label class="fs-5 fw-bold">View Communities:</label>
                    <div class="form-check ps-2 p-1 fw-bold">
                        <asp:RadioButton ID="followedRadio" runat="server" Text=" Followed Communities" GroupName="viewPosts" />
                    </div>
                    <div class="form-check ps-2 p-1 fw-bold">
                        <asp:RadioButton ID="unfollowedRadio" runat="server" Text=" Unfollowed Communities" GroupName="viewPosts" />
                    </div>
                    <div class="form-check ps-2 p-1 fw-bold">
                        <asp:RadioButton ID="bothViewRadio" runat="server" Text=" Both" GroupName="viewPosts" Checked="True" />
                    </div>
                </div>

                <asp:Button ID="applyFiltersButton" runat="server" Text="Apply Filters" CssClass="btn btn-dark mt-3" OnClick="applyFiltersButton_Click" />
            </div>

            <div class="col-8">

                <table class="table-borderless w-100">
                    <asp:Repeater runat="server" ID="communityListView">
                        <ItemTemplate>
                            <tr>
                                <div class="col-12 m-2">
                                    <div onclick='window.location=`<%#Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/" %>ManageCommunity/CommunityHome.aspx?id=<%#Eval("Id") %>`'
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
        </div>


    </main>
</asp:Content>
