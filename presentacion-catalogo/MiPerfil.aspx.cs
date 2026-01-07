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
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Seguridad.sesionActiva(Session["usuarioActivo"]))
                    {
                        Usuario usuario = (Usuario)Session["usuarioActivo"];
                        txtEmail.Text = usuario.Email;
                        txtEmail.ReadOnly = true;
                        txtNombre.Text = usuario.Nombre;
                        txtApellido.Text = usuario.Apellido;
                        if (!string.IsNullOrEmpty(usuario.ImagenPerfil))
                            imgNuevoPerfil.ImageUrl = "~/Images/" + usuario.ImagenPerfil + "?v=" + DateTime.Now.Ticks.ToString();
                        else
                            imgNuevoPerfil.ImageUrl = "https://simg.nicepng.com/png/small/202-2022264_usuario-annimo-usuario-annimo-user-icon-png-transparent.png";
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = (Usuario)Session["usuarioActivo"];
                UsuarioNegocio negocio = new UsuarioNegocio();

                //para guardar imagen
                if (txtImagen.PostedFile.FileName != "")
                {
                    string ruta = Server.MapPath("./Images/");
                    txtImagen.PostedFile.SaveAs(ruta + "perfil-" + usuario.Id + ".jpg");
                    usuario.ImagenPerfil = "perfil-" + usuario.Id + ".jpg";
                }

                usuario.Nombre = txtNombre.Text;
                usuario.Apellido = txtApellido.Text;

                negocio.actualizar(usuario);
                //Leer la imagen
                Image img = (Image)Master.FindControl("imgAvatar");
                img.ImageUrl = "~/Images/" + usuario.ImagenPerfil;
                imgNuevoPerfil.ImageUrl = "~/Images/" + usuario.ImagenPerfil + "?v=" + DateTime.Now.Ticks.ToString();

                Response.Redirect("Default.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}