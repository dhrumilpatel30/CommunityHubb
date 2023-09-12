<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CommunityHomeView.aspx.cs" Inherits="CommunityHubb.ManageCommunity.CommunityHomeView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <main>
     <div class="container p-3">
         <asp:Label ID="successlabel" class="alert alert-success alert-dismissible fade show" Text="Login successful" runat="server" Visible="false" />
         <asp:Label ID="FailLabel" class="alert alert-danger alert-dismissible fade show" Text="Error" runat="server" Visible="false" />


         <div class="fw-bolder display-6 mb-3">Communities</div>
         <table class="table-borderless w-100">
             <asp:Repeater runat="server" ID="communityList">
                 <ItemTemplate>

                     <tbody class="table">
                         <tr>
                             <td>
                                 <a href='<%#Request.Url.Scheme + "://" + Request.Url.Authority + 
    Request.ApplicationPath.TrimEnd('/') + "/" %>ManageCommunity/CommunityHome.aspx?id=<%#Eval("Id") %>' class="text-decoration-none">
                                     <div class="btn btn-outline-dark container p-3 rounded-4 m-1">

                                         <div class="fw-bold pt-1 h3 d-flex"><%# Eval("Name") %></div>

                                         <div class="d-flex justify-content-md-start">
                                             <div style='font-weight: 600'>Description:<span class="fw-bolder"> <%# Eval("Description") %></span></div>
                                         </div>
                                     </div>
                                 </a>
                             </td>
                         </tr>
                     </tbody>
                 </ItemTemplate>
             </asp:Repeater>
         </table>
     </div>


 </main>
</asp:Content>
