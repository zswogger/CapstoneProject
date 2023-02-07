<%@ Page Title="Admin Dashboard" Language="C#" MasterPageFile="~/LoggedIn.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="EZMoney.Admin" %>

<asp:Content ID="AdminContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="centered-div">
        <div class="boxed-form" style="width: auto;">
            <div class="tab">
                <button type="button" class="tablinks active" id="UsersTab">Users</button>
                <button type="button" class="tablinks" id="TransactionsTab">Transactions</button>
                <button type="button" class="tablinks" id="ProfitTab">Profits</button>
            </div>
            <div id="Users" class="tabcontent usertable" style="display: block;">
                <asp:TextBox ID="SearchUsername" Enabled="true" class="searchBox" runat="server" placeholder="Username" MaxLength="50" ></asp:TextBox>
                <asp:Button class="btn btn-primary mt-2" CssClass="btn btn-primary" runat="server" type="submit" OnClick="searchUserName" ID="uNameSearch" Text="Search" /><br />
                <asp:Button class="btn btn-primary mt-2" CssClass="btn btn-light-dashboard" runat="server" type="submit" OnClick="prevPage" ID="prevUser" Text="Previous" />
                <asp:Button class="btn btn-primary mt-2" CssClass="btn btn-primary-dashboard" runat="server" type="submit" OnClick="nextPage" ID="nextUser" Text="Next" />
                <asp:Table ID="UsersTable" runat="server"></asp:Table>
            </div>

            <div id="Transactions" class="tabcontent">
                <asp:TextBox ID="SearchTxUsername" Enabled="true" class="searchBox" runat="server" placeholder="Username" MaxLength="50" ></asp:TextBox>
                <asp:Button class="btn btn-primary mt-2" CssClass="btn btn-primary" runat="server" type="submit" OnClick="searchTxUsername" ID="Button1" Text="Search" /><br />
                <asp:Button class="btn btn-primary mt-2" CssClass="btn btn-light-dashboard" runat="server" type="submit" OnClick="prevPage" ID="prevTransaction" Text="Previous" />
                <asp:Button class="btn btn-primary mt-2" CssClass="btn btn-primary-dashboard" runat="server" type="submit" OnClick="nextPage" ID="nextTransaction" Text="Next" />
                <asp:Table ID="TransactionsTable" runat="server"></asp:Table>
            </div>

            <div id="Profits" class="tabcontent">
                <asp:TextBox ID="TotalProfit" Enabled="false" class="searchBox" runat="server" placeholder="Username"></asp:TextBox>
                <asp:Button class="btn btn-primary mt-2" CssClass="btn btn-light-dashboard" runat="server" type="submit" OnClick="prevPage" ID="prevProfit" Text="Previous" />
                <asp:Button class="btn btn-primary mt-2" CssClass="btn btn-primary-dashboard" runat="server" type="submit" OnClick="nextPage" ID="nextProfit" Text="Next" />
                <asp:Table ID="ProfitsTable" runat="server"></asp:Table>
            </div>
        </div>
        <asp:HiddenField ID="tabSelector" runat="server" Value="0"/>
    </div>


    <script type="text/javascript">

        $(document).ready(function () {
            var tab = document.getElementById('<%=tabSelector.ClientID %>').value;

            if (tab == "0") {
                $('#UsersTab').trigger('click');
            }

            if (tab == "1") {
                $('#TransactionsTab').trigger('click');
            }

            if (tab == "2") {
                $('#ProfitTab').trigger('click');
            }
        });

        $('#UsersTab').on('click', function () {
            hideAll();
            $('#UsersTab').addClass('active');
            $('#Users').show();
        })

        $('#TransactionsTab').on('click', function () {
            hideAll();
            $('#TransactionsTab').addClass('active');
            $('#Transactions').show();
        })

        $('#ProfitTab').on('click', function () {
            hideAll();
            $('#ProfitTab').addClass('active');
            $('#Profits').show();
        })

        function hideAll() {
            $('#UsersTab').removeClass('active');
            $('#Users').hide();

            $('#TransactionsTab').removeClass('active');
            $('#Transactions').hide();

            $('#ProfitTab').removeClass('active');
            $('#Profits').hide();
        }
    </script>
</asp:Content>
