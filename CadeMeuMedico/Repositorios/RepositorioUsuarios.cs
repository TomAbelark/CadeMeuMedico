using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CadeMeuMedico.Models;
using System.Web.Security;

namespace CadeMeuMedico.Repositorios
{
    public class RepositorioUsuarios
    {

        public static bool AutenticarUsuario(string Login, string Senha)
        {
            var SenhaCriptografada = FormsAuthentication.HashPasswordForStoringInConfigFile(Senha, "sha1");
            try
            {
                using (CadeMeuMedicoBDEntities1 db = new CadeMeuMedicoBDEntities1())
                {
                    var QueryAutenticaUsuarios = db.Usuarios.Where(x => x.Login == Login && x.Senha == Senha).SingleOrDefault();
                    if (QueryAutenticaUsuarios == null)
                    {
                        return false;
                    }
                    else
                    {
                        RepositorioCookies.RegistraCookieAutenticacao(QueryAutenticaUsuarios.IDUsuario);
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        // Listagem 7.7 - Métodos de apoio para verificação do status do usuário:
        public static Usuarios RecuperaUsuarioPorID(long IDUsuario)
        {
            try
            {
                using (CadeMeuMedicoBDEntities1 db = new CadeMeuMedicoBDEntities1())
                {
                    var Usuario = db.Usuarios.Where(u => u.IDUsuario == IDUsuario).SingleOrDefault();
                    return Usuario;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static Usuarios VerificaSeOUsuarioEstaLogado()
        {
            var Usuario = HttpContext.Current.Request.Cookies["UserCookieAuthentication"];
            if (Usuario == null)
            {
                return null;
            }
            else
            {
                long IDUsuario = Convert.ToInt64(RepositorioCriptografia.Descriptografar(Usuario.Values["IDUsuario"]));
                var UsuarioRetornado = RecuperaUsuarioPorID(IDUsuario);
                return UsuarioRetornado;
            }
        }
    }
        
}