using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROJETO_DE_GASTOS
{
    public partial class Cadastro : Form
    {
        public Cadastro()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                try
                {
                    //CHAMAR E CRIAR UMA NOVA CLASSE
                    CadastrarPessoas cadastrar;
                    cadastrar = new CadastrarPessoas();

                    //VARIAVEIS  
                    // string txtnome;
                    //string txtsenha;

                    //ATRIBUIÇÃO A TEXTBOX
                    string txtnome = textBox1.Text;
                    string txtsenha = textBox2.Text;

                    //ATRIBUIÇÃO DOS VALORES A CLASSE
                    cadastrar.Nome = txtnome;
                    cadastrar.Senha = txtsenha;

                    //CHAMA O METODO QUE GRAVA NO BANCO DE DADOS
                    //ATRIBUINDO AS VARIAVESI PARA DAR ENTRADA NA AÇÃO DO SQL
                    cadastrar.cadastrar(txtnome, txtsenha);

                    MessageBox.Show("Cadastro efetuado com sucesso, não se esqueça de atualizar suas informações", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBox1.Text = null;
                    textBox2.Text = null;
                }
                catch(Exception elo)
                {
                    MessageBox.Show("Deu erro ai irmão\n\n" + elo.Message + "","Falha ao fazer a conexão com o banco de dados", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Nenhum dos valores podem ficar em branco","Nenhum valor inserido", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            //oculta o form atual
            this.Hide();
            //cria uma variavel do form
            var form2 = new TelaInicial();
            //Quando o form atual for fechado executa a função form2
            form2.Closed += (s, args) => this.Close();
            //Mostrar o formulario de Cadastro
            form2.Show();
        }

        private void Cadastro_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.BackColor = Color.FromArgb(0, 128, 128);
            button1.ForeColor = Color.White;
            button1.BackColor = Color.FromArgb(0, 128, 128);
        }
    }
}
