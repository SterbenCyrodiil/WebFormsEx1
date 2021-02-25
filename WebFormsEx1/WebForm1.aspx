<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebFormsEx1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>  
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
</head>
<body>
    <header>
        <h1>THIS IS IT</h1>
    </header>
    <form id="form1" runat="server">
        <div class="col-sm-4">
            <label>Name:</label>
             <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        </div>
        <div class="col-sm-4">
            <label>Price:</label>
            <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox>
        </div>
        <div class="col-sm-4">
            <asp:Button ID="submitBTN" runat="server" Text="SUBMIT" OnClick="SubmitArticle"/>
            <asp:Button ID="updateBTN" runat="server" Text="UPDATE" OnClick="UpdateArticle"/>
            <asp:Button ID="searchButton" runat="server" Text="SEARCH" OnClick="SearchArticle"/>

        </div>
    </form><br /> <br />
    <asp:UpdatePanel>
        <ContentTemplate>
            <asp:ListView ID="ProductListing">

            </asp:ListView>
        </ContentTemplate>
    </asp:UpdatePanel>

</body>
</html>
