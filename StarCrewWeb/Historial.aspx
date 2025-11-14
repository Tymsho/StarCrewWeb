<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="Historial.aspx.cs" 
    Inherits="StarCrewWeb.Historial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Finalizar Misiones y Ver Historial</h1>

    <div style="border: 1px solid #ccc; padding: 10px; margin-bottom: 20px;">
        <h3>Finalizar Misión Activa</h3>
        <p>Selecciona una misión activa para procesar su resultado (Éxito o Fracaso).</p>
        
        <asp:DropDownList ID="ddlMisionesActivas" runat="server" Width="400px" 
            DataTextField="Titulo" DataValueField="Id" />
        
        <asp:Button ID="btnFinalizar" runat="server" Text="Finalizar Misión" OnClick="btnFinalizar_Click" />
        
        <br /><br />
        <asp:Label ID="lblResultadoFinal" runat="server" Font-Bold="true"></asp:Label>
    </div>

    <div>
        <h3>Historial de Misiones Completadas</h3>
        
        <asp:GridView ID="gvHistorial" runat="server"
            AutoGenerateColumns="False" 
            CellPadding="4"
            Width="100%">
            <Columns>
                <asp:BoundField DataField="FechaFinalizacion" HeaderText="Fecha" DataFormatString="{0:g}" />
                <asp:BoundField DataField="MisionId" HeaderText="Mision ID" />
                <asp:BoundField DataField="Resultado" HeaderText="Resultado" />
                <asp:BoundField DataField="Detalles" HeaderText="Detalles" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>