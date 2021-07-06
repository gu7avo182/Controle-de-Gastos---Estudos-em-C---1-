using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;


namespace PROJETO_DE_GASTOS
{
    class AdicionarValores
    {

        //VARIAVEIS
        private decimal _valor;
        private int _id;
        private string _categoria;
        private string _obs;
        public string error;

        //ACESSO A VARIAVEL
        public decimal Valor
        {
            get { return this._valor; }
            set { this._valor = value; }
        }
        public int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }
        public string Categoria    
        {
            get { return this._categoria; }
            set { this._categoria = value; }
        }
        public string Obs
        {
            get { return this._obs; }
            set { this._obs = value; }
        }
        public AdicionarValores()
        {

        }
        public void AdicionarEntrada(int Id, string Valor, string Categoria, string Obs)
        {
           try
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = ConexaoBanco.Abrir();
            
           comm.CommandText = string.Format("INSERT INTO Financas (id, Entradas,Saidas, Saldo, Categoria, Observações) "+
                                "VALUES( "+
                                "'{0}', "+
                                "'{1}', "+
                                "'0', "+
                                "((((SELECT SUM(Entradas) AS Saldo FROM Financas WHERE id = '{0}'))" +
                                "-(SELECT SUM(Saidas) FROM Financas WHERE id = '{0}')))," +
                                "'{2}', " +
                                "'{3}') "
                                , Id, Valor, Categoria, Obs);

                comm.ExecuteNonQuery();
                comm.Connection = ConexaoBanco.Fechar();
           }
            catch(Exception erro)
           {
           error =  erro.Message;
           }
        }
        public void AdicionarSaida(int Id, string Valor,string Categoria, string Obs)
        {
            try
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = ConexaoBanco.Abrir();

                comm.CommandText = string.Format("INSERT INTO Financas (id, Entradas,Saidas, Saldo, Categoria, Observações) " +
                     "VALUES( " +
                     "'{0}', " +
                     "'0', " +
                     "'{1}', " +
                     "((((SELECT SUM(Entradas) AS Saldo FROM Financas WHERE id = '{0}'))" +
                     "-(SELECT SUM(Saidas) FROM Financas WHERE id = '{0}')))," +
                     "'{2}', " +
                     "'{3}') "
                     , Id, Valor, Categoria, Obs);

                comm.ExecuteNonQuery();
                comm.Connection = ConexaoBanco.Fechar();
            }
            catch (Exception erro)
            {
                error = erro.Message;
            }
        }
    }
}
