<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateCommunity.aspx.cs" Inherits="CommunityHubb.ManageCommunity.CreateCommunity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container p-3">

        <div class="fw-bolder display-6 mb-3">Create your community</div>
        <div class="row">
            <div class="col-md-8">
                <div class="card">

                    <div class="card-body">
                        <div class="fw-bolder fs-3 mb-3">Enter details</div>

                        <div class="mb-3">
                            <label class="form-label fw-bold fs-5">Community Name</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="namebox" ErrorMessage=" (Name is required)" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="namebox" runat="server" CssClass="form-control fs-5 fw-bold" placeholder="Community Name"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label class="form-label fw-bold fs-5">Visibility</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="RadioButtonList1" ErrorMessage=" (Visibility is required)" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Vertical" CssClass="ms-3 fw-bold fs-6">
                                <asp:ListItem Text="" Value="Public">
                                        <div class="ms-2 mb-2 fs-5 fw-bold d-grid">
                                            Public
                                           
<div class="fs-6 fw-normal">Anyone can see this community and its posts. <br /> Anyone can join this community and post in it.<br /> Members list are public. </div></div>

                                </asp:ListItem>
                                <asp:ListItem Text="" Value="Private">
                                        <div class="ms-2 mb-2 fs-5 fw-bold d-grid">
                                            Private
<div class="fs-6 fw-normal">Only approved members can see this community, its posts and add post in it. <br /> Only admins can add new members in this community. <br /> Whole community is private, so members list too. </div>
                                        </div>
                                </asp:ListItem>
                            </asp:RadioButtonList>

                        </div>
                    </div>
                </div>
            </div>
            <div class="col-4">
                <div class="card">
                    <div class="card-body">
                        <div class="mb-4">

                            <label class="form-label fw-bold fs-5">About Communtiy</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox1" ErrorMessage=" (About is required)" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="About (in 200 chars)" MaxLength="200"></asp:TextBox>
                        </div>
                        <asp:Button runat="server" CssClass="btn btn-dark fs-4 w-100 fw-bold mt-5" Text="Create Community" ID="createbutton" OnClick="CreateClick" />
                        <br />
                        <button class="btn btn-outline-dark fw-semibold fs-5 w-100 mt-3" type="button" onclick="location.href='Login.aspx'">Cancel</button>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
