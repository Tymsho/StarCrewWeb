<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="StarCrewWeb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
       <asp:Image ID="MiImagen" runat="server" 
        ImageUrl="~/Imagenes/fondo.jpg" 
        AlternateText="Logo de StarCrew" 
        CssClass="imagen-presentacion"/>
    </main>

</asp:Content>
