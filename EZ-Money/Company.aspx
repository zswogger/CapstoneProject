<%@ Page Title="Company" Language="C#" MasterPageFile="~/LoggedIn.Master" AutoEventWireup="true" CodeBehind="Company.aspx.cs" Inherits="EZ_Money.Company" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="NoCompany" runat="server" Visible="false">
        <div class="centered-div">
            <div class="boxed-form">
                <div class="form-group">
                    <div class="col">
                        <span style="color: var(--accent);">Company Name</span><br />
                        <asp:TextBox ID="CompanyName" Enabled="true" class="form-control searchBox my-2" runat="server" placeholder="Name" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="cname" runat="server" ControlToValidate="CompanyName" ErrorMessage="Please enter a company name" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col">
                        <span style="color: var(--accent);">Company URL</span>
                        <asp:TextBox ID="CompanyURL" Enabled="true" class="form-control searchBox my-2" runat="server" placeholder="URL" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="cUrl" runat="server" ControlToValidate="CompanyURL" ErrorMessage="Please enter a website URL" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col">
                        <span style="color: var(--accent);">Logo URL</span>
                        <asp:TextBox ID="LogoUrl" Enabled="true" class="form-control searchBox my-2" runat="server" placeholder="Logo Image URL" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="LogoUrlValidator" runat="server" ControlToValidate="LogoUrl" ErrorMessage="Please enter a URL for your company logo" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col">
                        <span style="color: var(--accent);">Address 1</span>
                        <asp:TextBox ID="Address1" Enabled="true" class="form-control searchBox my-2" runat="server" placeholder="Address 1" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="Address1Validator" runat="server" ControlToValidate="Address1" ErrorMessage="Please enter an Address" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col">
                        <span style="color: var(--accent);">Address 2</span>
                        <asp:TextBox ID="Address2" Enabled="true" class="form-control searchBox my-2" runat="server" placeholder="Address 2" MaxLength="50"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col">
                        <span style="color: var(--accent);">City</span>
                        <asp:TextBox ID="City" Enabled="true" class="form-control searchBox my-2" runat="server" placeholder="City" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="CityValidator" runat="server" ControlToValidate="City" ErrorMessage="Please enter a city" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col">
                        <span style="color: var(--accent);">State</span>
                        <asp:TextBox ID="State" Enabled="true" class="form-control searchBox my-2" runat="server" placeholder="State" MaxLength="20" autocomplete="new-password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="StateValidator" runat="server" ControlToValidate="State" ErrorMessage="Please enter a state" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col">
                        <span style="color: var(--accent);">Zip</span>
                        <asp:TextBox ID="Zip" Enabled="true" class="form-control searchBox my-2" runat="server" placeholder="Zip Code" MaxLength="20" autocomplete="new-password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ZipValidator" runat="server" ControlToValidate="Zip" ErrorMessage="Please enter a zip code" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col">
                        <span style="color: var(--accent);">EIN</span>
                        <asp:TextBox ID="EIN" Enabled="true" class="form-control searchBox my-2" runat="server" placeholder="Employer Identification Number" MaxLength="9" autocomplete="new-password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="EINValidator" runat="server" ControlToValidate="EIN" ErrorMessage="Please enter an employer identification number" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <button id="register" type="button" class="btn btn-primary full-button mt-2" data-dismiss="modal">Register</button>
            </div>
        </div>
    </asp:Panel>

    <!-- User has company -->
    <asp:Panel ID="YesCompany" runat="server" Visible="false">
        <asp:Literal ID="CompanyLogo" runat="server"></asp:Literal><br />
        <asp:TextBox ID="DisplayCompanyName" Enabled="false" class="form-control searchBox my-2" runat="server"></asp:TextBox><br />
        <asp:TextBox ID="DisplayCompanySite" Enabled="false" class="form-control searchBox my-2" runat="server"></asp:TextBox><br />
        <asp:TextBox ID="DisplayAdd1" Enabled="false" class="form-control searchBox my-2" runat="server"></asp:TextBox><br />
        <asp:TextBox ID="DisplayAdd2" Enabled="false" class="form-control searchBox my-2" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="DisplayCity" Enabled="false" class="form-control searchBox my-2" runat="server"></asp:TextBox><br />
        <asp:TextBox ID="DisplayState" Enabled="false" class="form-control searchBox my-2" runat="server"></asp:TextBox><br />
        <asp:TextBox ID="DisplayZip" Enabled="false" class="form-control searchBox my-2" runat="server"></asp:TextBox><br />
        <asp:TextBox ID="DisplayEIN" Enabled="false" class="form-control searchBox my-2" runat="server"></asp:TextBox>
    </asp:Panel>

    <!-- Confirm Modal -->
    <div class="modal centered-div" id="confirmCompany" tabindex="-1" role="dialog" style="display: none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h3 class="modal-title">Are you sure?</h3>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <h3 style="color: red;">Warning:</h3>
                    <h4>You are about to create a company. From this moment forward, you will be charged a fee for all transactions until you delete the company.</h4>
                    <hr />
                    <h4>Please double check that the following information is correct:</h4>
                    <span style="color: var(--accent);">Name: </span><span id="checkName"></span>
                    <br />
                    <span style="color: var(--accent);">Website: </span><span id="checkSite"></span>
                    <br />
                    <span style="color: var(--accent);">Logo URL:  </span><span id="checkLogo"></span>
                    <br />
                    <span style="color: var(--accent);">Address 1: </span><span id="checkAdd1"></span>
                    <br />
                    <span style="color: var(--accent);">Address 2: </span><span id="checkAdd2"></span>
                    <br />
                    <span style="color: var(--accent);">City:  </span><span id="checkCity"></span>
                    <br />
                    <span style="color: var(--accent);">State: </span><span id="checkState"></span>
                    <br />
                    <span style="color: var(--accent);">Zip: </span><span id="checkZip"></span>
                    <br />
                    <span style="color: var(--accent);">EIN:  </span><span id="checkEIN"></span>
                </div>
                <div class="modal-footer">
                    <asp:Button class="btn btn-primary mt-2" CssClass="btn btn-primary" runat="server" type="submit" ID="Confirm" OnClick="registerCompany" Text="Confirm" />
                    <button id="cancel" type="button" class="btn btn-danger" data-dismiss="modal">Cancel</button>
                </div>
            </div>
        </div>
    </div>

    <script>
        $('#register').on('click', function () {
            setCompanyValues();
            $('#confirmCompany').show();
        });

        $('#cancel').on('click', function () {
            $('#confirmCompany').hide();
        });

        function setCompanyValues() {
            $('#checkName').text($('#MainContent_CompanyName').val());
            $('#checkSite').text($('#MainContent_CompanyURL').val());
            $('#checkLogo').text($('#MainContent_LogoUrl').val());
            $('#checkAdd1').text($('#MainContent_Address1').val());
            $('#checkAdd2').text($('#MainContent_Address2').val());
            $('#checkCity').text($('#MainContent_City').val());
            $('#checkState').text($('#MainContent_State').val());
            $('#checkZip').text($('#MainContent_Zip').val());
            $('#checkEIN').text($('#MainContent_EIN').val());
        }
    </script>

    <style>
        .grid-container {
            display: grid;
        }

        img {
            height: 100px;
            width: 100px;
            border-radius: 8px;
            background-color: var(--dark-background-color);
        }
    </style>
</asp:Content>
