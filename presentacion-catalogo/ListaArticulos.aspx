<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="ListaArticulos.aspx.cs" Inherits="presentacion_catalogo.ListaArticulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .validacion {
            color: red;
            font-size: 14px;
        }

        .requerido {
            color: red;
            font-size: 16px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>BIENVENIDO</h1>

    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <asp:Label Text="Filtrar" ID="lblFiltro" runat="server" />
                <asp:TextBox runat="server" ID="txtFiltro" CssClass="form-control" OnTextChanged="txtFiltro_TextChanged"
                    AutoPostBack="true" />
            </div>
        </div>
        <div class="col-6" style="display: flex; flex-direction: column; justify-content: flex-end;">
            <div class="mb-3">
                <asp:CheckBox Text="Filtro Avanzado" runat="server" CssClass=""
                    ID="chkAvanzado" AutoPostBack="true"
                    OnCheckedChanged="chkAvanzado_CheckedChanged" />
            </div>
        </div>
        <%if (chkAvanzado.Checked)
            { %>
            <div class="row">
                <div class="col-3">
                    <div class="mb-3">
                        <asp:Label Text="Campo" CssClass="form-label" runat="server" /><span class="requerido"> *</span>
                        <asp:DropDownList runat="server" AutoPostBack="true" CssClass="form-select" ID="ddlCampo"
                            OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged">
                            <asp:ListItem Text="Marca" />
                            <asp:ListItem Text="Categoria" />
                            <asp:ListItem Text="Precio" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-3">
                    <div class="mb-3">
                        <asp:Label Text="Criterio" CssClass="form-label" runat="server" /><span class="requerido"> *</span>
                        <asp:DropDownList runat="server" ID="ddlCriterio" CssClass="form-select"></asp:DropDownList>

                    </div>
                </div>
                <div class="col-3">
                    <div class="mb-3">
                        <asp:Label Text="Filtro" CssClass="form-label" runat="server" /><span class="requerido"> *</span>
                        <asp:TextBox runat="server" ID="txtFiltroAvanzado" CssClass="form-control" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-3">
                        <div class="mb-3">
                            <asp:Button Text="Buscar" runat="server" CssClass="btn btn-primary" ID="btnBuscar" OnClick="btnBuscar_Click" />
                            <asp:Button Text="Limpiar" ID="btnLimpiar" runat="server" CssClass="btn btn-primary" OnClick="btnLimpiar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        <% } %>
    </div>

    <asp:GridView runat="server" ID="dgvArticulos" CssClass="table table-bordered" AutoGenerateColumns="false"
        DataKeyNames="Id" OnSelectedIndexChanged="dgvArticulos_SelectedIndexChanged"
        OnPageIndexChanging="dgvArticulos_PageIndexChanging"
        AllowPaging="true" PageSize="4">
        <Columns>
            <asp:BoundField HeaderText="Codigo" DataField="Codigo" />
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" />
            <asp:BoundField HeaderText="Url Imagen" DataField="ImagenUrl" />
            <asp:BoundField HeaderText="Marca" DataField="Marca.Descripcion" />
            <asp:BoundField HeaderText="Categoria" DataField="Categoria.Descripcion" />
            <asp:BoundField HeaderText="Precio" DataField="Precio" />
            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="seleccionar" />
        </Columns>
    </asp:GridView>
    <a class="btn btn-primary" href="ArticuloABM.aspx">Agregar</a>
</asp:Content>
