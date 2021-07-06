using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.SqlTypes;

namespace PROJETO_DE_GASTOS
{
    //CLASSE
    public static class ConexaoBanco
    {
        //PROPRIEDADE
        public static string conexao;

        //METODO
        public static SqlConnection Abrir()
        {
            //string de conexao com o banco
            string str = @"Data Source=DESKTOP-K3QJTCD;Initial Catalog=PROG_GASTOS;Integrated Security=True";
            //Classe de conexao usando a string acima
            SqlConnection cn = new SqlConnection(str);
            //abre a conexao
            cn.Open();
            return cn;
        }
        public static SqlConnection Fechar()
        {
            //string de conexao com o banco
            string str = @"Data Source=DESKTOP-K3QJTCD;Initial Catalog=PROG_GASTOS;Integrated Security=True";
            //Classe de conexao usando a string acima
            SqlConnection cn = new SqlConnection(str);
            //fecha a conexao
            cn.Close();
            return cn;
        }

    }
}
