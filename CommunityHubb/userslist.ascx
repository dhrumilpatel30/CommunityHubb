<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="userslist.ascx.cs" Inherits="CommunityHubb.userslist" %>

<table class="table-borderless w-100">
    <asp:Repeater runat="server" ID="userlistinchild">
        <ItemTemplate>
            <tbody class="table">
                <tr>
                    <td>
                            <div onclick='window.location=`<%#Request.Url.Scheme + "://" + Request.Url.Authority + 
    Request.ApplicationPath.TrimEnd('/') + "/" %>UserAccount/UserHome.aspx?id=<%#Eval("Id") %>`' class="btn btn-light container rounded-4 m-2">
                                <h4 class="fw-bold d-flex"><%# Eval("Name") %></h4>
                            </div>
                    </td>
                </tr>
            </tbody>
        </ItemTemplate>
    </asp:Repeater>
</table>