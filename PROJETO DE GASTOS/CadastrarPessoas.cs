//using Microsoft.Analytics.Interfaces;
//using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace PROJETO_DE_GASTOS
{
    //NOME DA CLASSE
    //PUBLIC, PRIVATE,
    public class CadastrarPessoas
    {
        //PROPRIEDADE

        //VARIAVEIS DA CLASSE
        private string _nome;
        private string _senha;

       //CONSTRUTOR
        public string Nome
        {
            get {return this._nome;}
            set { this._nome = value;}
        }
        public string Senha
        {
            get {return this._senha;}
            set {this._senha = value;}
        }

        //METODO
        public void cadastrar(string _nome, string _senha)
        {
            SqlCommand comm = new SqlCommand();
            comm.Connection = ConexaoBanco.Abrir();
            comm.CommandText = "INSERT INTO Usuarios (nome, senha) VALUES ('" + Nome+  "', '" + Senha + "');";
            comm.ExecuteNonQuery();
            comm.CommandText = "SELECT id FROM Usuarios WHERE nome= '" +  Nome + "';";
            string dado = comm.ExecuteScalar().ToString();
            comm.Connection = ConexaoBanco.Fechar();
        }
    }
}