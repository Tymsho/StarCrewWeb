<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tripulantes.aspx.cs" Inherits="StarCrewWeb.Tripulantes" %>

<%-- Este es el contenido de la página --%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <h1>Gestión de Tripulantes</h1>
    
    <div style="border: 1px solid #ccc; padding: 10px; margin-bottom: 20px; max-width: 500px;">
        <h3>Crear o Actualizar Tripulante</h3>
        
        <asp:TextBox ID="txtNombre" runat="server" Width="300px"></asp:TextBox>
        
        <asp:DropDownList ID="cmbRoles" runat="server"></asp:DropDownList>
        
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
        
        <br /><br />
        
        <asp:Label ID="lblAgregar" runat="server" Text="" ForeColor="Green"></asp:Label>
    </div>

<h2>Tripulantes Actuales</h2>

<table class="grid-table" cellpadding="4" style="width: 100%; border-collapse: collapse; border: 1px solid #333;">
    <thead>
        <tr style="background-color: #333; color: limegreen;">
            <th>ID</th>
            <th>Nombre</th>
            <th>Rol</th>
            <th>Nivel</th>
            <th>Editar</th>
            <th>Eliminar</th>
        </tr>
    </thead>
    <tbody>
        <%--Repeater carga la lista de tripulantes --%>
        <asp:Repeater ID="rptTripulantes" runat="server" 
            OnItemCommand="rptTripulantes_ItemCommand"> 
            
            <%-- El HTML que sigue se va a generar una vez por cada
              tripulante que tengamos en la lista. --%>
            <ItemTemplate>
                <tr style="border-bottom: 1px solid #ccc;">
                    <td><%# Eval("Id") %></td>
                    <td><%# Eval("Nombre") %></td>
                    <td><%# Eval("Rol") %></td>
                    <td><%# Eval("Nivel") %></td>
                    
                    <%-- Botón de Editar --%>
                    <td>
                        <asp:Button ID="btnEditar" runat="server" 
                            Text="Editar" 
                            CommandName="Editar" 
                            CommandArgument='<%# Eval("Id") %>' />
                    </td>
                    
                    <%-- Botón de Eliminar --%>
                    <td>
                        <asp:Button ID="btnEliminar" runat="server" 
                            Text="Eliminar" 
                            CommandName="Eliminar" 
                            CommandArgument='<%# Eval("Id") %>' />
                    </td>
                </tr>
            </ItemTemplate>

        </asp:Repeater>
        <%-- Aquí termina el repeater --%>
    </tbody>
</table>

</asp:Content>

 