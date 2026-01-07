<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="presentacion_catalogo.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .validacion{
            color:red;
            font-size:14px;
        }
        .requerido{
            color:red;
            font-size:16px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
            <div class="col-6" style="display: flex; flex-direction: column; justify-content: flex-end;">
                <div class="mb-3">
                    <asp:CheckBox Text="Filtrar" runat="server" CssClass=""
                        ID="chkFiltrar" OnCheckedChanged="chkFiltrar_CheckedChanged" AutoPostBack="true" />
                </div>
            </div>
            <%if (chkFiltrar.Checked)
              { %>
                <div class="row">
                    <div class="col-3">
                        <div class="mb-3">
                            <label class="form-label">Campo<span class="requerido"> *</span></label>
                            <asp:DropDownList runat="server" ID="ddlCampo" CssClass="form-select" 
                                AutoPostBack="true" OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged" >
                                <asp:ListItem Text="Marca" />
                                <asp:ListItem Text="Categoria" />
                                <asp:ListItem Text="Precio" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator CssClass="validacion" ErrorMessage="Este campo es requerido" ControlToValidate="ddlCampo" runat="server" />
                        </div>
                    </div>
                    <div class="col-3">
                        <div class="mb-3">
                            <label class="form-label">Criterio<span class="requerido"> *</span></label>
                            <asp:DropDownList runat="server" ID="ddlCriterio" CssClass="form-select" ></asp:DropDownList>
                            <asp:RequiredFieldValidator CssClass="validacion" ErrorMessage="Este campo es requerido" ControlToValidate="ddlCriterio" runat="server" />
                        </div>
                    </div>
                    <div class="col-3">
                        <div class="mb-3">
                            <label class="form-label">Filtro<span class="requerido"> *</span></label>
                            <asp:TextBox runat="server" ID="txtFiltro" CssClass="form-control" />
                            <asp:RequiredFieldValidator CssClass="validacion" ErrorMessage="Este campo es requerido" ControlToValidate="txtFiltro" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-3">
                        <div class="mb-3">
                            <asp:Button Text="Buscar" ID="btnBuscar" OnClick="btnBuscar_Click" 
                                CssClass="btn btn-primary" runat="server" />
                            <asp:Button Text="Limpiar" ID="btnLimpiar" OnClick="btnLimpiar_Click" 
                                CssClass="btn btn-primary" runat="server" />
                        </div>
                    </div>
                </div>

           <% } %>


<%--     <%
         foreach (dominio.Articulo articulo in ListaArticulos)
         {

     %>
                <div class="col">
                    <div class="card">
                        <img src="<%:articulo.ImagenUrl %>" class="card-img-top" alt="...">
                        <div class="card-body">
                            <h5 class="card-title"><%:articulo.Nombre %></h5>
                            <p class="card-text"><%:articulo.Descripcion %></p>
                            <a href="#">Ver Detalle</a>
                        </div>
                    </div>
                </div>
    
      <% } %>--%>
    <div class="row row-cols-1 row-cols-md-3 g-4">
     <asp:Repeater ID="repRepetidor" runat="server">
            <ItemTemplate>
                   <div class="col">
                    <div class="card">
                        <img src="<%#Eval("ImagenUrl") %>" class="card-img-top" alt="...">
                        <div class="card-body">
                            <h5 class="card-title"><%#Eval("Nombre")%></h5>
                            <p class="card-text"><%#Eval("Descripcion")%></p>
                            <asp:Button runat="server" Text="Ver Detalle" CssClass="btn btn-primary" 
                                ID="btnVerDetalle" OnClick="btnVerDetalle_Click" CommandArgument='<%#Eval("Id") %>'
                                CommandName="ArticuloId"/>
                            <asp:Button Text="Agregar Favs" CssClass="btn btn-primary" ID="btnAgregarFavorito" 
                                OnClick="btnAgregarFavorito_Click" runat="server" 
                                CommandArgument='<%#Eval("Id") %>' CommandName="idArticulo" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

</asp:Content>
