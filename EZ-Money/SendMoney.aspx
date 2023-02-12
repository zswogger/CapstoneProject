<%@ Page Title="Send Money" Language="C#" MasterPageFile="~/LoggedIn.Master" AutoEventWireup="true" CodeBehind="SendMoney.aspx.cs" Inherits="EZMoney.SendMoney" %>

<asp:Content ID="SendContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="centered-div">
        <div class="boxed-form">
            <h1 style="color: var(--accent); margin-top: 0px;">Send Money</h1>
            <div class="centered-div">
                <h3 id="walletBalance" style="margin-top: 0px; color: var(--text-color)" runat="server"></h3>
            </div>
            <div class="form-group">
                <div class="col">
                    <asp:TextBox ID="Username" Enabled="true" class="form-control my-2" runat="server" placeholder="Username" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="uname" runat="server" ControlToValidate="Username" ErrorMessage="Please enter a username" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <div class="col">
                    <asp:TextBox ID="Amount" Enabled="true" class="form-control my-2" runat="server" placeholder="$0.00" MaxLength="20" type="float"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <div class="col">
                    <asp:TextBox ID="Memo" Enabled="true" class="form-control my-2" CssClass="form-control memo-box" runat="server" placeholder="Memo (Optional)" MaxLength="150"></asp:TextBox>
                </div>
            </div>
            <div class="centered-div">
                <button id="send" value="Send" class="btn btn-primary mt-2 full-button" type="button">Send</button>
            </div>
        </div>
    </div>

    <!-- Start Modal -->
    <div class="modal centered-div" id="verifyModal" tabindex="-1" role="dialog" style="display:none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h3 class="modal-title">Are you sure?</h3>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <h4>Please double check that the following information is correct:</h4>
                    <span style="color: var(--accent);">Username: </span><span id="checkUserName"></span><br />
                    <span style="color: var(--accent);">Amount: </span><span id="checkAmount"></span><br />
                    <span style="color: var(--accent);">Memo: </span><span id="checkMemo"></span><br />
                </div>
                <div class="modal-footer">
                   <asp:Button class="btn btn-primary mt-2" CssClass="btn btn-primary" runat="server" type="submit" OnClick="sendTransaction" ID="sendConfirm" Text="Confirm" />
                    <button id="cancel" type="button" class="btn btn-danger" data-dismiss="modal">Cancel</button>
                </div>
            </div>
        </div>
    </div>

<!--  Start Scripts -->
<script>
    $('#send').on('click', function () {
        $('#checkUserName').text($('#MainContent_Username').val());
        $('#checkAmount').text('$' + $('#MainContent_Amount').val());
        $('#checkMemo').text($('#MainContent_Memo').val());
        $('#verifyModal').show();
    });

    $('#cancel').on('click', function () {
        $('#verifyModal').hide();
    })
</script>
</asp:Content>
