<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="Misiones.aspx.cs" 
    Inherits="StarCrewWeb.Misiones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Asignar Tripulantes a Misión</h1>

    <div style="display: flex; gap: 40px;">
        
        <div>
            <h3>1. Selecciona la Misión</h3>
            <asp:DropDownList ID="ddlMisiones" runat="server" Width="300px" 
                AutoPostBack="True" 
                OnSelectedIndexChanged="ddlMisiones_SelectedIndexChanged">
            </asp:DropDownList>

            <asp:Panel ID="pnlMisionDetails" runat="server" 
                Visible="false" 
                style="border: 1px solid #555; padding: 10px; margin-top: 10px; background-color: #000;">
                
                <h4>Detalles de la Misión:</h4>
                <asp:Label ID="lblDificultad" runat="server" ></asp:Label>
                <br />
                <asp:Label ID="lblRequisitos" runat="server"></asp:Label>
                
            </asp:Panel>
            </div>

        <div>
            <h3>2. Selecciona los Tripulantes</h3>
            <p>(Usa Ctrl+Click o Shift+Click para seleccionar varios)</p>
            
            <asp:ListBox ID="lstDisponibles" runat="server" 
                         SelectionMode="Multiple" 
                         Width="300px" 
                         Rows="10">
            </asp:ListBox>
        </div>
    </div>

    <hr />

    <h3>3. Confirma la Asignación</h3>
    <asp:Button ID="btnAsignar" runat="server" Text="Asignar Tripulantes" OnClick="btnAsignar_Click" />
    <br />
    <asp:Label ID="lblResultado" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label>

</asp:Content>