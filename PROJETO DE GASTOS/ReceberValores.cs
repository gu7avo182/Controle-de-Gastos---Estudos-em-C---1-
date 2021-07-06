using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;


namespace PROJETO_DE_GASTOS
{
    class ReceberValores
    {
        public string TotalEntrada(int Id)
        {
            SqlCommand comm = new SqlCommand();
            comm.Connection = ConexaoBanco.Abrir();
            comm.CommandText = string.Format("SELECT SUM(Entradas)FROM Financas WHERE id='{0}'", Id);
            comm.ExecuteNonQuery();
            string entrada;
            return entrada = Convert.ToString(comm.ExecuteScalar());
        }

        public string TotalSaida(int Id)
        {
            SqlCommand comm = new SqlCommand();
            comm.Connection = ConexaoBanco.Abrir();
            comm.CommandText = string.Format("SELECT SUM(Saidas)FROM Financas WHERE id='{0}'", Id);
            comm.ExecuteNonQuery();
            string saida;
            return saida = Convert.ToString(comm.ExecuteScalar());
            
        }
    }
}
