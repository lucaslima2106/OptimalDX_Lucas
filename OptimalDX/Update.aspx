<%@ Page Title="Update" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Update.aspx.cs" Inherits="OptimalDX.Update" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="form-row">
        <div class="form-group col-md-4">
            <label for="txtFirstName">First Name</label>
            <asp:TextBox ID="txtFirstName" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="form-group col-md-4">
            <label for="txtLastName">Last Name</label>
            <asp:TextBox ID="txtLastName" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
    </div>

    <div class="form-row">
        <div class="form-group col-md-4">
            <label for="phone">Phone</label>
            <asp:TextBox ID="txtPhone" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="form-group col-md-4">
            <label for="email">Email</label>
            <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
    </div>

    <div class="form-row">
        <div class="form-group col-md-4">
            <label for="ddlGender">Gender</label>
            <asp:DropDownList ID="ddlGender" CssClass="form-control" runat="server">
                <asp:ListItem Text="Male" Value="M" />
                <asp:ListItem Text="Female" Value="F" />
                <asp:ListItem Text="Other" Value="O" />
            </asp:DropDownList>
        </div>
    </div>

    <div class="form-row">
        <div class="form-group col-md-12">
            <label for="txtNotes">Notes</label>
            <asp:TextBox ID="txtNotes" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
        </div>
    </div>

    <asp:Button ID="btnUpdatePatient" runat="server" Text="Update" OnClick="btnUpdatePatient_Click" />

</asp:Content>
