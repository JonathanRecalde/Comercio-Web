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
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();
                if (!Page.IsValid)
                    return;

                if (Validacion.textoVacio(txtEmail) || Validacion.textoVacio(txtPassword))
                {
                    Session.Add("error","Debe completar todos los campos...");
                    Response.Redirect("Error.aspx");
                }
                Usuario usuario = new Usuario();
                UsuarioNegocio negocio = new UsuarioNegocio();
                EmailService emailService = new EmailService();

                usuario.Email = txtEmail.Text;
                usuario.Pass = txtPassword.Text;

                usuario.Id = negocio.insertarNuevo(usuario);
                Session.Add("usuarioActivo", usuario);

                //emailService.armarCorreo(usuario.Email, "Bienvenido", "Tu usuario ha sido generado exitosamente, bienvenido...");
                //emailService.enviarEmail();

                Response.Redirect("Default.aspx", false);
            }
            catch(System.Threading.ThreadAbortException ex) { }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}