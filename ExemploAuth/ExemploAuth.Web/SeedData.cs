using ExemploAuth.Web.Data.Contextos;
using ExemploAuth.Web.Dominio.Entidades;

namespace ExemploAuth.Web
{
    public static class SeedData
    {
        public static void AddSeedData(WebApplication app)
        {
            var scope = app.Services.CreateScope();
            using (var context = scope.ServiceProvider.GetRequiredService<AuthContexto>())
            {
                var usuarioJaIserido = context.Usuario.FirstOrDefault(x => x.Id == 1);

                if (usuarioJaIserido == null)
                {
                    #region Usuário

                    var usuario = new UsuarioEntidade(id: 1, nome: "Rodrigo", senha: "123");
                    context.Usuario.Add(usuario);
                    context.SaveChanges();

                    #endregion
                }
            }
        }
    }
}
