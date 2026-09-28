using AppLibertadoresHAS.Models;
using AppLibertadoresHAS.Models.DTOs;
using AppLibertadoresHAS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppLibertadoresHAS.ViewModels
{
    public class ListagemJogadorViewModel : BaseViewModel
    {
        JogadorService _jogadorService;

        public ObservableCollection<Jogador> Jogadores { get; set; }

        public ListagemJogadorViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            _jogadorService = new JogadorService(token);

            Jogadores = new ObservableCollection<Jogador>();
            _ = ObterJogadores();
        }
        public async Task<ObservableCollection<Jogador>> ObterJogadores()
        {
            Jogadores = await _jogadorService.GetJogadoresAsync();
            OnPropertyChanged(nameof(Jogadores));
            return Jogadores;
        }
    }
}
