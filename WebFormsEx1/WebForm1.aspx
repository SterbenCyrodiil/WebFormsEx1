<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebFormsEx1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

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
            <asp:Button ID="submitBTN" runat="server" Text="SUBMIT" OnClick="SubmitArticle" />
            <asp:Button ID="updateBTN" runat="server" Text="UPDATE" OnClick="UpdateArticle" />
            <asp:Button ID="searchButton" runat="server" Text="SEARCH" OnClick="SearchArticle" />
            <asp:Button ID="deleteButton" runat="server" Text="DELETE" OnClick="DeleteArticle" />
        </div>
        <asp:GridView ID="GridView1" 
            runat="server" CssClass="table" ItemType="WebFormsEx1.Entities.Article" 
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <%#:Item.Name %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price">
                    <ItemTemplate>
                        <%#:Item.Price %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
