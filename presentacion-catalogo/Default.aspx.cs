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
    public partial class Default : System.Web.UI.Page
    {
        public bool Filtro { get; set; }
        public bool Precio { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Filtro = chkFiltrar.Checked;
            
            if (!IsPostBack)
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                
                ddlCampo.Items.Insert(0, new ListItem("Seleccione una opción"));

                Session.Add("listaArticulos", negocio.listarConSp());
                repRepetidor.DataSource = Session["listaArticulos"];
                repRepetidor.DataBind();

            }
        }

        protected void btnVerDetalle_Click(object sender, EventArgs e)
        {
            string id = ((Button)sender).CommandArgument;
            Response.Redirect("VerDetalle.aspx?id=" + id);

        }

        protected void btnAgregarFavorito_Click(object sender, EventArgs e)
        {
            if (Seguridad.sesionActiva(Session["usuarioActivo"]))
            {
                Usuario usuario = (Usuario)Session["usuarioActivo"];
                UsuarioNegocio negocio = new UsuarioNegocio();

                int idArticulo = int.Parse(((Button)sender).CommandArgument);
                negocio.insertarFavorito(usuario.Id, idArticulo);
                Response.Redirect("Favoritos.aspx");
            }
            else
            {
                Response.Redirect("Login.aspx");
            }

        }

        protected void chkFiltrar_CheckedChanged(object sender, EventArgs e)
        {
            Filtro = chkFiltrar.Checked;
        }

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriterio.Items.Clear();
            if (ddlCampo.SelectedItem.ToString() == "Precio")
            {
                ddlCriterio.Items.Add("Mayor a");
                ddlCriterio.Items.Add("Menor a");
            }
            else
            {
                ddlCriterio.Items.Add("Comienza con");
                ddlCriterio.Items.Add("Termina con");
                ddlCriterio.Items.Add("Contiene a");
            }
            if (ddlCampo.SelectedItem.ToString() == "Seleccione una opción")
                ddlCriterio.Items.Clear();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();
                if (!Page.IsValid)
                    return;

                if(ddlCampo.SelectedItem.ToString()=="Seleccione una opción")
                {
                    Session.Add("error","Debe seleccionar una opción en: Campo");
                    Response.Redirect("Error.aspx");
                }

                if (Validacion.textoVacio(txtFiltro))
                {
                    Session.Add("error","El campo: Filtro es requerido");
                    Response.Redirect("Error.aspx");
                }

                if (ddlCampo.SelectedItem.ToString() == "Precio")
                {
                    if (!(Validacion.soloNros(txtFiltro.Text)))
                    {
                        Session.Add("error", "Ingrese sólo nros si desea filtrar por Precio...");
                        Response.Redirect("Error.aspx");
                    }
                }

                ArticuloNegocio negocio = new ArticuloNegocio();
                repRepetidor.DataSource = negocio.filtroUser(ddlCampo.SelectedItem.ToString(), 
                    ddlCriterio.SelectedItem.ToString(), txtFiltro.Text);
                repRepetidor.DataBind();
            }
            catch(System.Threading.ThreadAbortException ex) { }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            ddlCampo.SelectedIndex = 0;

            repRepetidor.DataSource = Session["listaArticulos"];
            repRepetidor.DataBind();
            ddlCriterio.Items.Clear();
            txtFiltro.Text = "";
        }
    }
}