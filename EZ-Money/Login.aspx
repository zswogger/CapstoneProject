<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EZMoney.Login" %>

<asp:Content ID="LoginContent" ContentPlaceHolderID="MainContentNotLoggedIn" runat="server">
    <div class="centered-div">
        <div class="boxed-form">
            <div class="centered-div">
                <h5>Login</h5>
            </div>
            <div class="form-group">
                <div class="col">
                    <asp:TextBox ID="UserName" Enabled="true" class="form-control my-2" runat="server" placeholder="Username" MaxLength="20"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="uname" runat="server" ControlToValidate="UserName" ErrorMessage="Please enter a user name" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <div class="col">
                    <asp:TextBox ID="Password" Type="password" Enabled="true" class="form-control my-2" runat="server" placeholder="Password" MaxLength="20"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="pass" runat="server" ControlToValidate="Password" ErrorMessage="Please enter a password" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="centered-div">
                <asp:Button class="btn btn-primary mt-2" CssClass="btn btn-primary full-button" runat="server" type="submit" OnClick="loginUser" ID="login" Text="Login" />
            </div>
        </div>
    </div>
</asp:Content>
