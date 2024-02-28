﻿using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio
{
    public interface IUsuarioRepositorio
    {
        UsuarioModel BuscarPorLogin(string login);

        List<UsuarioModel> BuscarTodos();
        
        UsuarioModel Adicionar(UsuarioModel usuario);

        UsuarioModel ListarPorId(int id);

        UsuarioModel Atualizar(UsuarioModel usuario);

        bool Apagar(int id);


    }
}
