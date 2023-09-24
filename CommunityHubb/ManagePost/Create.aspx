<%@ Page Title="Create your post" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="CommunityHubb.ManagePost.Create" validateRequest = false %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>

            <div class="container-fluid p-3">
                <div class="fw-bolder display-6 mb-3">Post Your Thought</div>
                <div class='w-100 mx-auto'>
                    <div class="mb-3 col-4 w-100">
                        <label for="title" class="form-label h4 fw-bold">Title</label>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="titleinput" ErrorMessage=" (Title is required)" CssClass="text-danger" />
                        <asp:TextBox runat="server" id="titleinput" CssClass="form-control w-75 mb-2" Font-Size="X-Large" Font-Bold="true"/>
                        <label for="postcontent" class="form-label h4 fw-bold">Content</label>
                         <asp:TextBox TextMode="MultiLine"  runat="server" id="postcontent" CausesValidation="false"></asp:TextBox>
                    </div>
                </div>
                
                <div class="row d-flex justify-content-end">
                    <div class=" col-4">
                        <label for="communityname" class="form-label h5 fw-bold">Community Name</label>
                        <asp:TextBox CssClass="form-control" ID="communityname" runat="server" ReadOnly="true" />
                    </div>
                    <div class="col-4">
                        <label for="authorname" class="form-label h5 fw-bold">Author Name</label>
                        <asp:TextBox CssClass="form-control" ID="authorname" runat="server" ReadOnly="true" />
                    </div>
                    <div class="col-4">
                        <asp:Button ID="CreateButton" runat="server" Text="Post Now" OnClick="CreatePost" CssClass="btn btn-dark w-100 h-100 fs-4 fw-bolder" />
                    </div>
                </div>
            </div>
    </main>
    <script src="../Scripts/tinymce/tinymce.min.js" type="text/javascript"></script>
    <script>
        tinymce.init({
            selector: 'textarea',
        });
    </script>
</asp:Content>
