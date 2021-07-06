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
    public class TabelaDeValores
    {
        //VARIAVEIS DA CLASSE
        public int id;

        //CONSTRUTOR
        public TabelaDeValores(int UserAtual)
        {
            id = UserAtual;
        }
    }
}
