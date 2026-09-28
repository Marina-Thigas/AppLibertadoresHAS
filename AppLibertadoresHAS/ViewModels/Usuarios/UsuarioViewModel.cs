using AppLibertadoresHAS.Models;
using AppLibertadoresHAS.Services.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace AppLibertadoresHAS.ViewModels.Usuarios
{
    public class UsuarioViewModel : BaseViewModel
    {
        private UsuarioService uService;
        public ICommand AutenticarCommand { get; set; }

        public UsuarioViewModel()
        {
            uService = new UsuarioService();
            InicializarCommands();
        }

        public void InicializarCommands()
        {
            AutenticarCommand = new Command(async () => await AutenticarUsuario());
        }

        #region AtributosPropriedades

        private string login = string.Empty;
        public string Login
        {
                get {  return login; }
                set
                {
                    login = value;
                    OnPropertyChanged();
                }
        }

        private string senha = string.Empty;
        public string Senha
        {
            get { return senha; }
            set
            {
                senha = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public async Task AutenticarUsuario()
        {
            try
            {
                Usuario u = new Usuario();
                u.Username = Login;
                u.PasswordString = Senha;

                Usuario uAutenticado = await uService.PostAutenticarUsuarioAsync(u);

                if (!string.IsNullOrEmpty(uAutenticado.Token))
                {
                    string mensagem = $"Bem-vindo(a) {uAutenticado.Username}";

                    // Guardando dados do usuário para uso futuro
                    Preferences.Set("UsuarioId", uAutenticado.Id);
                    Preferences.Set("UsuarioUsername", uAutenticado.Username);
                    Preferences.Set("UsuarioPerfil", uAutenticado.Perfil);
                    Preferences.Set("UsuarioToken", uAutenticado.Token);

                    await Application.Current.MainPage.DisplayAlert("Informação", mensagem, "Ok");

                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Informação", "Dados incorretos :(", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }
    }
}
