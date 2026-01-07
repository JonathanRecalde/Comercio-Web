using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace presentacion_catalogo
{
    public partial class ListaArticulos : System.Web.UI.Page
    {
        public bool FiltroAvanzado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.esAdmin(Session["usuarioActivo"]))
            {
                Session.Add("error", "Requiere permisos de admin para ingresar...");
                Response.Redirect("Error.aspx", false);
            }
            
            FiltroAvanzado = chkAvanzado.Checked;
            if (!IsPostBack)
            {
                ddlCampo.Items.Insert(0, new ListItem("Seleccione una opción"));

                ArticuloNegocio negocio = new ArticuloNegocio();
                Session.Add("listaArticulos", negocio.listarConSp());
                dgvArticulos.DataSource = Session["listaArticulos"];
                dgvArticulos.DataBind();
            }
        }

        protected void dgvArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvArticulos.SelectedDataKey.Value.ToString();
            Response.Redirect("ArticuloABM.aspx?id=" + id);
        }

        protected void dgvArticulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (Session["listaArticulosFiltrados"] != null)
            {
                dgvArticulos.DataSource = Session["listaArticulosFiltrados"];
                dgvArticulos.PageIndex = e.NewPageIndex;
                dgvArticulos.DataBind();
            }
            else
            { 
                dgvArticulos.DataSource = Session["listaArticulos"];
                dgvArticulos.PageIndex = e.NewPageIndex;
                dgvArticulos.DataBind();
            }
            
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> lista = (List<Articulo>)Session["listaArticulos"];

            List<Articulo> listaFiltrada = lista.FindAll(x => x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()) || x.Marca.Descripcion.ToUpper().Contains(txtFiltro.Text.ToUpper()));
            dgvArticulos.DataSource = listaFiltrada;
            dgvArticulos.DataBind();
        }

        protected void chkAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            txtFiltro.Enabled = !chkAvanzado.Checked;
        }

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriterio.Items.Clear();
            if (ddlCampo.SelectedItem.ToString() == "Precio")
            {
                ddlCriterio.Items.Add("Igual a");
                ddlCriterio.Items.Add("Mayor a");
                ddlCriterio.Items.Add("Menor a");
            }
            else
            {
                ddlCriterio.Items.Add("Contiene");
                ddlCriterio.Items.Add("Comienza con");
                ddlCriterio.Items.Add("Termina con");
            }

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlCampo.SelectedItem.ToString() == "Seleccione una opción")
                {
                    Session.Add("error", "Debe seleccionar una opción en: Campo");
                    Response.Redirect("Error.aspx");
                }
                if (Validacion.textoVacio(txtFiltroAvanzado))
                {
                    Session.Add("error", "El campo: Filtro es requerido");
                    Response.Redirect("Error.aspx");
                }

                if (ddlCampo.SelectedItem.ToString() == "Precio")
                {
                    if (!Validacion.soloNros(txtFiltroAvanzado.Text))
                    {
                        Session.Add("error","Debe ingresar sólo nros si desea filtrar por Precio...");
                        Response.Redirect("Error.aspx");
                    }
                }
                
                ArticuloNegocio negocio = new ArticuloNegocio();
                Session.Add("listaArticulosFiltrados", negocio.filtroAdmin(ddlCampo.SelectedItem.ToString(),
                    ddlCriterio.SelectedItem.ToString(), txtFiltroAvanzado.Text));

                dgvArticulos.DataSource = Session["listaArticulosFiltrados"];
                dgvArticulos.DataBind();
            }
            catch(System.Threading.ThreadAbortException ex) { }
            catch (Exception ex)
            { 
                Session.Add("error", ex.ToString());
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            ddlCampo.SelectedIndex = 0;


            dgvArticulos.DataSource = Session["listaArticulos"];
            dgvArticulos.DataBind();
            ddlCriterio.Items.Clear();
            txtFiltroAvanzado.Text = "";
        }
    }
}