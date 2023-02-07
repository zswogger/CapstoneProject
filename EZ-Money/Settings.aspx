<%@ Page Title="Settings" Language="C#" MasterPageFile="~/LoggedIn.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="EZ_Money.Settings" %>

<asp:Content ID="SettingsContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="centered-div">
        <div class="boxed-form">
            <div class="form-group">
                <div class="col">
                    <span style="color: var(--accent);">Username</span><br />
                    <asp:TextBox ID="UserName" Enabled="true" class="form-control searchBox my-2" runat="server" placeholder="Username" MaxLength="20" ></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <div class="col">
                    <span style="color: var(--accent);">First Name</span>
                    <asp:TextBox ID="FirstName" Enabled="true" class="form-control searchBox my-2" runat="server" placeholder="First Name" MaxLength="20"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="fname" runat="server" ControlToValidate="FirstName" ErrorMessage="Please enter a first name" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <div class="col">
                    <span style="color: var(--accent);">last Name</span>
                    <asp:TextBox ID="LastName" Enabled="true" class="form-control searchBox my-2" runat="server" placeholder="Last Name" MaxLength="20"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="lname" runat="server" ControlToValidate="LastName" ErrorMessage="Please enter a last name" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <div class="col">
                    <span style="color: var(--accent);">Phone Number</span>
                    <asp:TextBox ID="PhoneNumber" Enabled="true" class="form-control searchBox my-2" runat="server" placeholder="Phone Number" MaxLength="20"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="phone" runat="server" ControlToValidate="PhoneNumber" ErrorMessage="Please enter a phone number" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <div class="col">
                    <span style="color: var(--accent);">Email</span>
                    <asp:TextBox ID="Email" Enabled="true" class="form-control searchBox my-2" runat="server" placeholder="Email" MaxLength="20"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="emailvalidate" runat="server" ControlToValidate="Email" ErrorMessage="Please enter an email address" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
            <hr style="border-color: var(--accent);" />
            <span style="color: var(--text-color);">Change Password</span>
            <div class="form-group">
                <div class="col">
                    <span style="color: var(--accent);">Old Password</span>
                    <asp:TextBox ID="CurrentPass" Enabled="true" class="form-control searchBox my-2" runat="server" type="password" MaxLength="20" autocomplete="new-password"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <div class="col">
                    <span style="color: var(--accent);">New Password</span>
                    <asp:TextBox ID="NewPass1" Enabled="true" class="form-control searchBox my-2" runat="server" type="password" MaxLength="20" autocomplete="new-password"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <div class="col">
                    <span style="color: var(--accent);">Confirm Password</span>
                    <asp:TextBox ID="NewPass2" Enabled="true" class="form-control searchBox my-2" runat="server" type="password" MaxLength="20" autocomplete="new-password"></asp:TextBox>
                </div>
            </div>
            <asp:Button class="btn btn-primary mb-2" CssClass="btn btn-light-settings full-button" OnClick="saveUser" runat="server" type="submit" ID="Save" Text="Save" />
            <button id="Delete" type="button" class="btn btn-danger full-button mt-2" data-dismiss="modal">Delete</button>
        </div>
    </div>

    <!-- Start Modal -->
    <div class="modal centered-div" id="verifyModal" tabindex="-1" role="dialog" style="display: none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h3 class="modal-title">Are you sure?</h3>
                    <button id="closeModal" type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true" style="color: red;">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <h4 style="color: red;">You are about to delete your account! This cannot be undone and you cannot sign up with the same email! Only continue if you are 100% sure!</h4>
                    <span id="ChangedInfo" runat="server"></span>
                </div>
                <div class="modal-footer">
                    <button id="Cancel" type="button" class="btn btn-light-settings full-button" data-dismiss="modal">DO NOT DELETE MY ACCOUNT</button>
                    <asp:Button class="btn btn-primary mb-2" CssClass="btn btn-danger full-button" OnClick="deleteUser" runat="server" type="submit" ID="DeleteAccount" Text="DELETE" />
                </div>
            </div>
        </div>
    </div>

    <!-- Start Javascript -->
    <script type="text/javascript">
        $('#Delete').on('click', function () {
            $('#verifyModal').show();
        });

        $('#closeModal').on('click', function () {
            $('#verifyModal').hide();
        });

        $('#Cancel').on('click', function () {
            $('#verifyModal').hide();
        });
    </script>
</asp:Content>
