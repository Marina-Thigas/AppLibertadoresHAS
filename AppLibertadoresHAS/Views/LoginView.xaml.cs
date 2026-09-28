using AppLibertadoresHAS.ViewModels.Usuarios;

namespace AppLibertadoresHAS.Views;

public partial class LoginView : ContentPage
{
	UsuarioViewModel viewModel;
	public LoginView()
	{
		viewModel = new UsuarioViewModel();
		InitializeComponent();
		BindingContext = viewModel;
	}
}