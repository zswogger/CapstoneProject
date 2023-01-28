<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/LoggedIn.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="EZMoney.Dashboard" %>

<asp:Content ID="DashboardContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="">
        <h1 id="welcome" runat="server" style="color: var(--accent); display: inline;"></h1>
        <h1 id="usersName" runat="server" style="color: var(--text-color); display: inline;"></h1>
    </div>
    <div>
        <h3 id="walletBalance" runat="server" style="color: var(--text-color);"></h3>
    </div>
    <div class="centered-div" style="margin-top: 20px;">
        <div class="boxed-form" style="padding-top: 20px !important;">
            <div class="centered-div">
                <h3 style="color: var(--accent); padding-top: 0px; margin-top: 0px;">Transaction History</h3>
            </div>
            <button id="sendMoney" type="button" class="btn btn-primary-dashboard full-button" >Send Money</button>
            <button id="requestMoney" type="button" class="btn btn-light-dashboard full-button" >Request Money</button>
            <asp:Table ID="TransactionsTable" runat="server"></asp:Table>
        </div>
    </div>


<script type="text/javascript">
    $('#sendMoney').on('click', function () {
        location.href = '/SendMoney';
    });

    $('#requestMoney').on('click', function () {
        location.href = '/RequestMoney';
    })

</script>
</asp:Content>
