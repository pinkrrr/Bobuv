<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tovary.aspx.cs" Inherits="Bobuv.Pages.Admin.Tovary"
    MasterPageFile="~/Pages/Admin/Admin.Master" validateRequest="false" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">               
    <asp:ListView ID="ListView1" ItemType="Bobuv.Models.Shoe" SelectMethod="GetShoes"
        DataKeyNames="ShoeId" UpdateMethod="UpdateShoe" DeleteMethod="DeleteShoe"
        InsertMethod="InsertShoe" InsertItemPosition="LastItem" EnableViewState="false"
        runat="server">
        <LayoutTemplate>
            <div class="outerContainer">
                <table id="productsTable">
                    <tr>
                        <th>Название игры</th>
                        <th>Описание</th>
                        <th>Категория</th>
                        <th>Картинка</th>
                        <th>Цена</th>
                    </tr>
                    <tr runat="server" id="itemPlaceholder"></tr>
                </table>
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Item.Name %></td>
                <td class="description"><span><%# Item.Description %></span></td>
                <td><%# Item.Category %></td>
                <td><img style="float: left; max-height: 150px;" src="/img/<%# Item.Img %>" /></td>
                
                <td><%# Item.Price.ToString("c") %></td>
                <td>
                    <asp:Button ID="Button1" CommandName="Edit" Text="Изменить" runat="server" />
                    <asp:Button ID="Button2" CommandName="Delete" Text="Удалить" runat="server" />
                </td>
            </tr>
        </ItemTemplate>
        <EditItemTemplate>
            <tr>
                <td>
                    <input name="name" value="<%# Item.Name %>" />
                    <input type="hidden" name="ProductID" value="<%# Item.ShoeId %>" />
                </td>
                <td>
                    <input name="description" value="<%# Item.Description %>" /></td>
                <td>
                    <input name="category" value="<%# Item.Category %>" /></td>
                <td>
                      <input name="img" value="<%# Item.Img %>" /></td>    
                <td>
                    <input name="price" value="<%# Item.Price %>" /></td>
                <td>
                    <asp:Button ID="Button3" CommandName="Update" Text="Обновить" runat="server" />
                    <asp:Button ID="Button4" CommandName="Cancel" Text="Отмена" runat="server" />
                </td>
            </tr>
        </EditItemTemplate>
        <InsertItemTemplate>
            <tr>
                <td>
                    <input name="name" />
                    <input type="hidden" name="ProductID" value="0" />
                </td>
                <td>
                    <input name="description" /></td>
                <td>
                    <input name="category" /></td>
                <td>
                    <input name="img" /></td>
                <td>
                    <input name="price" /></td>      
                <td>
                    <asp:Button ID="Button5" CommandName="Insert" Text="Вставить" runat="server" />
                </td>
            </tr>
        </InsertItemTemplate>
    </asp:ListView>
                <asp:FileUpload id="FileUpload1"                 
           runat="server">
       </asp:FileUpload>

       <br /><br />

       <asp:Button id="UploadButton" 
           Text="Upload file"
           OnClick="UploadButton_Click"
           runat="server">
       </asp:Button>    

       <hr />

       <asp:Label id="UploadStatusLabel"
           runat="server">
       </asp:Label>
</asp:Content>
