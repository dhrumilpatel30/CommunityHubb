<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CommunityHubb.UserAccount.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
    <div class="container-fluid p-3">

        <div class="fw-bolder display-6 mb-3">Login your account</div>

        <div class="row">
            <div class="col-md-7">
                <div class="card mb-3">
                    <div class="card-body bg-light">
                        <div class="fw-bolder fs-3 mb-3">Account details</div>
                        <div class="mb-3">
                            <label class="form-label ml-1 fw-bold fs-5">Email</label>
                            <asp:TextBox ID="emailbox" runat="server" CssClass="form-control" placeholder="Enter your email"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="emailbox" ErrorMessage="Email is required" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="emailbox" ErrorMessage="Invalid email address" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </div>
                        <div class="mb-3">
                            <label class="form-label ml-1 fw-bold fs-5">Password</label>
                            <asp:TextBox ID="passwordbox" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter your password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="passwordbox" ErrorMessage="Password is required" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>
            
        </div>
        <div class="d-flex justify-content-around">
            
            <asp:Button runat="server" CssClass="btn btn-dark fs-4 w-25 fw-bold" Text="Log In" OnClick="LoginPost" />
            <button class="btn btn-outline-dark fw-semibold fs-5 w-25" type="button" onclick="location.href='Signup.aspx'">Not have one? (Signup)</button>
        </div>
    </div>
</main>
</asp:Content>
