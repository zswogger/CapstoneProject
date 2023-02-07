<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/LoggedIn.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="EZMoney.Dashboard" %>

<asp:Content ID="DashboardContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="">
        <h1 id="welcome" runat="server" style="color: var(--accent); display: inline;"></h1>
        <h1 id="usersName" runat="server" style="color: var(--text-color); display: inline;"></h1>
        <div class="lock-right">
            <i class="fa-regular fa-envelope fa-2xl lock-right" id="closedEnvelope" style="display: none;"></i>
            <i class="fa-regular fa-envelope-open fa-2xl lock-right" id="openEnvelope"></i>
            <span class='badge badge-warning lock-right' id='requestCount' style="display: none;">5</span>
        </div>
        <asp:HiddenField ID="PendingTransactionsBadge" runat="server" Value="0"/>
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

    <div class="modal centered-div" id="verifyModal" tabindex="-1" role="dialog" style="display:none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h3 class="modal-title">Pending Transactions</h3>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body" style="max-height: calc(100vh - 210px); overflow-y: auto;">
                    <asp:Panel runat="server" ID="pendingTxData">
                    </asp:Panel>
                </div>
                <div class="modal-footer">
                    <button id="cancel" type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
        <div class="toast" id="toast">
      <div class="toast-header">
        Toast Header
      </div>
      <div class="toast-body">
        Some text inside the toast body
      </div>
    </div>
    </div>
<script type="text/javascript">

    $(document).ready(function () {
        var pendingTransactions = document.getElementById('<%=PendingTransactionsBadge.ClientID %>').value;

        if (pendingTransactions != '0') {
            $('#openEnvelope').hide();
            $('#closedEnvelope').show();
            $('#requestCount').text(pendingTransactions);
            $('#requestCount').show();
        }
    });

    $('#sendMoney').on('click', function () {
        location.href = '/SendMoney';
    });

    $('#requestMoney').on('click', function () {
        location.href = '/RequestMoney';
    });

    $('#closedEnvelope').on('click', function () {
        $('#verifyModal').show();
    });

    $('#cancel').on('click', function () {
        $('#verifyModal').hide();
    });

    $('.confirmTransaction').on('click', function () {
        var id = $(this).data("id");
        var data = {
            id: id
        };
        var d = $.ajax({
            type: "POST",
            url: '<%= ResolveUrl("Dashboard.aspx/completeTransaction") %>',
            data: JSON.stringify(data),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json'
        });
        window.location.reload();
    });

    $('.denyTransaction').on('click', function () {
        var id = $(this).data("id");
        var data = {
            id: id
        };
        var d = $.ajax({
            type: "POST",
            url: '<%= ResolveUrl("Dashboard.aspx/denyTransaction") %>',
            data: JSON.stringify(data),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json'
        });
        window.location.reload();
    });

</script>
</asp:Content>
