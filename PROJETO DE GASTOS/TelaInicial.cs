using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;



namespace PROJETO_DE_GASTOS
{
    public partial class TelaInicial : Form
    {
        public TelaInicial()
        {
            InitializeComponent();
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void button5_Click(object sender, EventArgs e)
        {
            //ENTRADA VARIAVEL DO CONSTRUTOR SOBRECARREGADO
            //=====================================================
            Double valor = Convert.ToDouble(textBox1.Text);

            TesteDeClasse VAL;
            VAL = new TesteDeClasse(valor);
            //=====================================================
            double a = VAL.volume();
            MessageBox.Show(a.ToString());
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            VerificarExistencia();
            //TelaInicial.StartPosition = FormStartPosition.CenterScreen;
            //aculta a barra de opções da janela
            this.ControlBox = false;
            //impede redimensionamento
            //this.FormBorderStyle = String("FixedSingle");

            //COR DO FUNDO
            this.BackColor = Color.FromArgb(0, 206, 209);

            //BOTOES
            button1.BackColor = Color.FromArgb(0, 128, 128);
            button2.BackColor = Color.FromArgb(0, 128, 128);
            button3.BackColor = Color.FromArgb(0, 128, 128);
            button4.BackColor = Color.FromArgb(0, 128, 128);
            button6.BackColor = Color.FromArgb(220, 20, 60);

            //PICTURE BOX
            pictureBox1.BackColor = Color.FromArgb(0, 128, 128);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //oculta o form atual
            this.Hide();
            //cria uma variavel do form
            var form2 = new Cadastro();
            //Quando o form atual for fechado executa a função form2
            form2.Closed += (s, args) => this.Close();
            //Mostrar o formulario de Cadastro
            form2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand comm = new SqlCommand();
            comm.Connection = ConexaoBanco.Abrir();

            //variaveis
            string Nome = textBox3.Text;
            string Senha = textBox2.Text;
            if (textBox3.Text != "")
            {
               // try
               // {
                    //SEM CONCATENAR   
                    comm.CommandText = string.Format("SELECT nome FROM Usuarios Where nome='{0}'", Nome);
                    //COM CONCATENAÇÃO
                    //comm.CommandText = "SELECT nome FROM Usuarios WHERE nome = '" + Nome + "'";
                    comm.ExecuteNonQuery();
                    if(string.IsNullOrWhiteSpace(Nome))
                    {
                        MessageBox.Show("A BUSCA ESTA EM BRANCO, POR ISSO NA RETORNOU VALORES !");
                        textBox3.Text = "";
                    }
                    else
                    {                     
                     if (comm.ExecuteScalar()==null)
                        {
                            MessageBox.Show("A BUSCA NÃO ENCONTROU NENHUM LOGIN");
                            textBox3.Text = "";
                        }
                     else
                        {
                            //MessageBox.Show("A BUSCA ENCONTROU O USUARIO: " + Nome + " .");
                            if(string.IsNullOrWhiteSpace(Senha) || Senha == "")
                            {
                                MessageBox.Show("Digite a senha para prosseguir");
                            }
                            else
                            {
                                comm.CommandText = string.Format("SELECT nome FROM Usuarios Where senha='{0}' and nome='{1}'", Senha,Nome);
                                if(comm.ExecuteScalar() == null)
                                {
                                    MessageBox.Show("LOGIN OU SENHA INCORRETOS");
                                    textBox2.Text = "";
                                    textBox3.Text = "";
                                }
                                else
                                {                             
                                    comm.CommandText = string.Format("SELECT id FROM Usuarios Where nome='{0}'",Nome);
                                    comm.ExecuteNonQuery();

                                    int chave = Convert.ToInt32(comm.ExecuteScalar());
                                    //MessageBox.Show("entrou, bem vindo !" + Nome + " seu ID é: " + chave + ",");
                                    comm.Connection = ConexaoBanco.Fechar();

                                    //oculta o form atual
                                    this.Hide();
                                    //cria uma variavel do form
                                    var formulario = new TelaDoUsuario(chave,Nome);
                                    //Quando o form atual for fechado executa a função form2
                                    formulario.Closed += (s, args) => this.Close();
                                    //Mostrar o formulario de Cadastro
                                    formulario.Show();
                                }
                            }
                        }
                    }
               // }
               // catch (Exception erro)
               // {
                //    MessageBox.Show("NENHUM USUARIO ENCONTRADO\n\n" + erro.Message + "", "USUARIO NÃO CADASTRADO",
                 //   MessageBoxButtons.OK, MessageBoxIcon.Error);
               // }
            }
            else
            {
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private bool VerificarExistencia()
        {
            bool retorno = false;

            try
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = ConexaoBanco.Abrir();             

                    {
                        comm.CommandText = "SELECT 1 FROM SYS.DATABASES WHERE NAME LIKE 'PROG_GASTOS'";
                        var valor = comm.ExecuteScalar();

                        if (valor != null && valor != DBNull.Value && Convert.ToInt32(valor).Equals(1))
                        {
                            retorno = true;
                        }
                    }             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return retorno;
        }

        private void DescobrirDiretoriosPadrao(out string diretorioDados, out string diretorioLog, out string diretorioBackup)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(@"Server=.\SQLEXPRESS;Database=master;Trusted_Connection=True;"))
            {
                var serverConnection = new Microsoft.SqlServer.Management.Common.ServerConnection(connection);
                var server = new Microsoft.SqlServer.Management.Smo.Server(serverConnection);
                diretorioDados = !string.IsNullOrWhiteSpace(server.Settings.DefaultFile) ? server.Settings.DefaultFile : (!string.IsNullOrWhiteSpace(server.DefaultFile) ? server.DefaultFile : server.MasterDBPath);
                diretorioLog = !string.IsNullOrWhiteSpace(server.Settings.DefaultLog) ? server.Settings.DefaultLog : (!string.IsNullOrWhiteSpace(server.DefaultLog) ? server.DefaultLog : server.MasterDBLogPath);
                diretorioBackup = !string.IsNullOrWhiteSpace(server.Settings.BackupDirectory) ? server.Settings.BackupDirectory : server.BackupDirectory;
            }
        }
        private void RestaurarDBPadrao()
        {
            try
            {
                string diretorioDados, diretorioLog, diretorioBackup;
                DescobrirDiretoriosPadrao(out diretorioDados, out diretorioLog, out diretorioBackup);

                using (var conn = new System.Data.SqlClient.SqlConnection(@"Server=.\SQLEXPRESS;Database=master;Trusted_Connection=True;"))
                {
                    conn.Open();
                    using (var comm = conn.CreateCommand())
                    {
                        var caminhoCompletoBackup = System.IO.Path.Combine(diretorioBackup, "DBTemplate.bak");
                        var caminhoCompletoDados = System.IO.Path.Combine(diretorioDados, "ExemploInstalador.mdf");
                        var caminhoCompletoLog = System.IO.Path.Combine(diretorioLog, "ExemploInstalador_Log.ldf");
                        System.IO.File.Copy("DBTemplate.bak", caminhoCompletoBackup, true);
                        comm.CommandText =
                            @"RESTORE DATABASE ExemploInstalador " +
                            @"FROM DISK = N'" + caminhoCompletoBackup + "' " +
                            @"WITH FILE = 1, " +
                            @"MOVE N'ExemploInstalador' TO N'" + caminhoCompletoDados + "', " +
                            @"MOVE N'ExemploInstalador_LOG' TO N'" + caminhoCompletoLog + "', " +
                            @"NOUNLOAD, REPLACE, STATS = 10";
                        comm.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RestaurarDBPadraoCasoNaoExista()
        {
            try
            {
                var bancoExiste = VerificarExistencia();

                if (!bancoExiste)
                {
                    RestaurarDBPadrao();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
    