<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="EZMoney.Register" %>

<asp:Content ID="registerContent" ContentPlaceHolderID="MainContentNotLoggedIn" runat="server">
    <div class="centered-div">
        <div class="boxed-form">
            <div class="centered-div">
                <h5>Register</h5>
            </div>
            <div class="form-group">
                <div class="col">
                    <asp:TextBox ID="FirstName" Enabled="true" class="form-control my-2" runat="server" placeholder="First Name" MaxLength="20"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="fname" runat="server" ControlToValidate="FirstName" ErrorMessage="Please enter a first name" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <div class="col">
                    <asp:TextBox ID="LastName" Enabled="true" class="form-control my-2" runat="server" placeholder="Last Name" MaxLength="20"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="lname" runat="server" ControlToValidate="LastName" ErrorMessage="Please enter a last name" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <div class="col">
                    <asp:TextBox ID="PhoneNumber" Enabled="true" class="form-control my-2" runat="server" placeholder="Phone Number" MaxLength="20"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="phone" runat="server" ControlToValidate="PhoneNumber" ErrorMessage="Please enter a phone number" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <div class="col">
                    <asp:TextBox ID="Email" Enabled="true" class="form-control my-2" runat="server" placeholder="Email" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="emailvalidate" runat="server" ControlToValidate="Email" ErrorMessage="Please enter an email address" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <div class="col">
                    <asp:TextBox ID="UserName" Enabled="true" class="form-control my-2" runat="server" placeholder="Username" MaxLength="50"></asp:TextBox>
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
                <asp:Button class="btn btn-primary mt-2" CssClass="btn btn-primary full-button" runat="server" type="submit" OnClick="registerUser" ID="register" Text="Register" />
            </div>
        </div>
    </div>
</asp:Content>

