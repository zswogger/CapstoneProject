<%@ Page Title="Admin Dashboard" Language="C#" MasterPageFile="~/LoggedIn.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="EZMoney.Admin" %>

<asp:Content ID="AdminContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="centered-div">
        <div class="boxed-form" style="width: auto;">
            <div class="tab">
                <button type="button" class="tablinks" id="UsersTab">Users</button>
                <button type="button" class="tablinks" id="TransactionsTab">Transactions</button>
                <button type="button" class="tablinks" id="ProfitTab">Profits</button>
            </div>
            <div id="Users" class="tabcontent" style="display: block;">
                <asp:Table ID="UsersTable" runat="server"></asp:Table>
            </div>

            <div id="Transactions" class="tabcontent">
                <asp:Table ID="TransactionsTable" runat="server"></asp:Table>
            </div>

            <div id="Profits" class="tabcontent">
                <asp:Table ID="ProfitsTable" runat="server"></asp:Table>
            </div>
        </div>
    </div>


    <script type="text/javascript">
        $('#UsersTab').on('click', function () {
            hideAll();
            $('#Users').show();
        })

        $('#TransactionsTab').on('click', function () {
            hideAll();
            $('#Transactions').show();
        })

        $('#ProfitTab').on('click', function () {
            hideAll();
            $('#Profits').show();
        })

        function hideAll() {
            $('#Users').hide();
            $('#Transactions').hide();
            $('#Profits').hide();
        }
    </script>
</asp:Content>
