<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="CommunityHubb.UserAccount.Signup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="container-fluid p-3">

            <div class="fw-bolder display-6 mb-3">Start your journey</div>

            <div class="row">
                <div class="col-md-6">
                    <div class="card mb-3">
                        <div class="card-body">
                            <div class="fw-bolder fs-3 mb-3">Create an account</div>
                            <div class="mb-3">
                            <label class="form-label fw-bold fs-6">Email</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="emailbox" ErrorMessage=" (Email is required)" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="emailbox" ErrorMessage=" (Invalid email address)" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                <asp:TextBox ID="emailbox" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                            <label class="form-label fw-bold fs-6">Email</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="namebox" ErrorMessage=" (Name is required)" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="namebox" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                            <label class="form-label fw-bold fs-6">Email</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="passwordbox" ErrorMessage=" (Password is required)" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="passwordbox" runat="server" CssClass="form-control" TextMode="Password" placeholder="Password"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                            <label class="form-label fw-bold fs-6">Email</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="passwordbox2" ErrorMessage=" (Confirm Password is required)" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="passwordbox" ControlToValidate="passwordbox2" ErrorMessage=" (Passwords do not Match)" ForeColor="Red"></asp:CompareValidator>
                                <asp:TextBox ID="passwordbox2" runat="server" CssClass="form-control" TextMode="Password" placeholder="Confirm password"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="card mb-3">
                        <div class="card-body">
                            <div class="fw-bolder fs-3 mb-3">More info (optional)</div>


                            <div class="mb-4">
                                
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
            </div>
            <div class="d-flex justify-content-around">
                
                <asp:Button runat="server" CssClass="btn btn-dark fs-4 w-25 fw-bold" Text="Sign Up" OnClick="SignupUser" />
                <button class="btn btn-outline-dark fw-semibold fs-5 w-25" type="button" onclick="location.href='Login.aspx'">Already have one? (Login)</button>
            </div>
        </div>
    </main>
</asp:Content>
