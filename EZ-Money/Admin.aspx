<%@ Page Title="Admin Dashboard" Language="C#" MasterPageFile="~/LoggedIn.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="EZMoney.Admin" %>

<asp:Content ID="AdminContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="centered-div">
        <div class="boxed-form" style="width: auto;">
            <div class="tab">
                <button type="button" class="tablinks active" id="UsersTab">Users</button>
                <button type="button" class="tablinks" id="TransactionsTab">Transactions</button>
                <button type="button" class="tablinks" id="ProfitTab">Profits</button>
            </div>
            <div id="Users" class="tabcontent" style="display: block;">
                <asp:Button class="btn btn-primary mt-2" CssClass="btn btn-primary" runat="server" type="submit" OnClick="prevPage" ID="prevUser" Text="Previous Page" />
                <asp:Button class="btn btn-primary mt-2" CssClass="btn btn-primary" runat="server" type="submit" OnClick="nextPage" ID="nextUser" Text="Next Page" />
                <asp:Table ID="UsersTable" runat="server"></asp:Table>
            </div>

            <div id="Transactions" class="tabcontent">
                <asp:Button class="btn btn-primary mt-2" CssClass="btn btn-primary" runat="server" type="submit" OnClick="prevPage" ID="prevTransaction" Text="Previous Page" />
                <asp:Button class="btn btn-primary mt-2" CssClass="btn btn-primary" runat="server" type="submit" OnClick="nextPage" ID="nextTransaction" Text="Next Page" />
                <asp:Table ID="TransactionsTable" runat="server"></asp:Table>
            </div>

            <div id="Profits" class="tabcontent">
                <asp:Button class="btn btn-primary mt-2" CssClass="btn btn-primary" runat="server" type="submit" OnClick="prevPage" ID="prevProfit" Text="Previous Page" />
                <asp:Button class="btn btn-primary mt-2" CssClass="btn btn-primary" runat="server" type="submit" OnClick="nextPage" ID="nextProfit" Text="Next Page" />
                <asp:Table ID="ProfitsTable" runat="server"></asp:Table>
            </div>
        </div>
    </div>


    <script type="text/javascript">

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
