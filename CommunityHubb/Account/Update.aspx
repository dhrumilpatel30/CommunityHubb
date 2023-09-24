<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Update.aspx.cs" Inherits="CommunityHubb.Account.Update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="container-fluid p-3">
            <div class="fw-bolder display-6 mb-3">Update your info</div>
            <div class="row">
                <div class="col-md-10">
                    <div class="card mb-3">
                        <div class="card-body bg-light">
                            <div class="fw-bolder fs-3 mb-1">Details</div>
                            <div>Other things like Name, Email and password are not possible to update. :(</div>
                            <div class="mt-2 mb-4">

                                <label class="form-label fw-bold fs-6">About you</label>
                                <asp:TextBox ID="aboutbox" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="About (in 150 chars)"></asp:TextBox>
                            </div>
                            <div class="mb-4">
                                <label class="form-label fw-bold fs-6">Mobile Number</label>
                                <asp:TextBox ID="numberbox" runat="server" CssClass="form-control" TextMode="Number" placeholder="Phone Number"></asp:TextBox>
                            </div>
                            <div class="mb-4">
                                <label for="dobbox" class="form-label fs-6 fw-bold">Date of Birth</label>
                                <asp:TextBox ID="dobbox" runat="server" CssClass="form-control" TextMode="Date" placeholder=""></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="d-flex justify-content-around">

                    <asp:Button runat="server" CssClass="btn btn-dark fs-4 w-25 fw-bold" Text="Update Details" OnClick="UpdateDetails" />
                    <a class="btn btn-outline-dark fw-semibold fs-5 w-25" type="button" href="/" >Cancel Update</a>
                </div>
            </div>
        </div>

    </main>
</asp:Content>
