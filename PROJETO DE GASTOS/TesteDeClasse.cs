using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJETO_DE_GASTOS
{
    class TesteDeClasse
    {
        //PROPRIEDADE
        private double lado;

        //CONSTRUTOR -> MESMO NOME QUE A CLASSE E ATRIBUI OS VALORES
        public TesteDeClasse()
        {
            lado = 8;
        }
        public TesteDeClasse(double qualquervalor)
        {
            lado = qualquervalor;
        }
        //METODO
        //================================
        public double volume()
        {
            return lado * lado * lado;
        }
        //================================
        //METODOS COM PUBLIC STATIC NAO PRECISAM 
        //ESTANCIAR (classe vari, vari = new classe())
    }
}
    