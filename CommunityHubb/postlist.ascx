<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="postlist.ascx.cs" Inherits="CommunityHubb.postlist" %>

<table class="table-borderless w-100">
            <asp:Repeater runat="server" ID="postlistinchildlist">
                <ItemTemplate>
                    <tbody class="table">
                        <tr>
                            <td>
                                <div onclick='window.location=`<%#Request.Url.Scheme + "://" + Request.Url.Authority + 
    Request.ApplicationPath.TrimEnd('/') + "/" %>ManagePost/PostHome.aspx?id=<%#Eval("Id") %>`'
                                    class="btn btn-light border border-1 container p-3 rounded-4 m-1">
                                    <div class="d-flex justify-content-between">
                                        <div style='font-weight: 600'>
                                            In:<a href='../ManageCommunity/CommunityHome.aspx?id=<%#Eval("CommunityId")%>' class="text-decoration-none"><span class="fw-bolder"> <%# Eval("CommunityName") %></span> </a>, 
                                            By
                                            <a href='../UserAccount/UserHome.aspx?id=<%#Eval("AutorId") %>' class="text-decoration-none"><span class="fw-bolder"> <%# Eval("Author") %></span> </a>
                                        </div>
                                        <div style='font-weight: 500'>On: <%# Eval("Date") %> </div>
                                    </div>
                                    <h2 class="fw-bold pt-1 d-flex"><%# Eval("Title") %></h2>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </ItemTemplate>
            </asp:Repeater>
</table>