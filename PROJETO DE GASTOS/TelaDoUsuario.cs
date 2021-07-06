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
using System.Data.Sql;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;


namespace PROJETO_DE_GASTOS
{
    public partial class TelaDoUsuario : Form
    {
        int User;
        public TelaDoUsuario(int UsuarioAtual, string NomeAtual)
        {
            InitializeComponent();
            label3.Text = UsuarioAtual.ToString();
            label5.Text = "Seja Bem Vindo, " + NomeAtual.ToString() + ".";
            User = UsuarioAtual;
            CarregarVoid();
            DadosDoUsuario();
            CarregarComboBox();
            grafico();
            ObterEntradaSaida(User);
        }
        private void label1_Click(object sender, EventArgs e)
        {
        }
        private void TelaDoUsuario_Load(object sender, EventArgs e)
        {
            //COR DO FUNDO
            this.BackColor = Color.White;

            //BOTOES
            button2.BackColor = Color.FromArgb(0, 128, 128);

            //COMBOBOX
            comboBox1.BackColor = Color.FromArgb(0, 128, 128);

            //LIST VIEW
            listView1.BackColor = Color.White;

            //LABEL
            label1.BackColor = Color.FromArgb(0, 128, 128);
            label2.BackColor = Color.FromArgb(0, 128, 128);
            label3.BackColor = Color.FromArgb(0, 128, 128);
            label4.BackColor = Color.FromArgb(0, 128, 128);
            label5.BackColor = Color.FromArgb(0, 128, 128);
            label6.BackColor = Color.FromArgb(0, 128, 128);
            label7.BackColor = Color.FromArgb(0, 128, 128);

            label8.BackColor = Color.White;

            label9.BackColor = Color.FromArgb(0, 128, 128);
            label10.BackColor = Color.FromArgb(0, 128, 128);
            label11.BackColor = Color.FromArgb(0, 128, 128);
            label12.BackColor = Color.FromArgb(0, 128, 128);
            label13.BackColor = Color.FromArgb(0, 128, 128);
            label14.BackColor = Color.FromArgb(0, 128, 128);

            //PICTURE BOX
            pictureBox1.BackColor = Color.FromArgb(0, 128, 128);

        }
        private void CarregarVoid()
        {
            listView1.Clear();

            listView1.Columns.Add("CATEGORIA", 125);
            listView1.Columns.Add("OBSERVAÇÕES", 200);
            listView1.Columns.Add("SALDO R$", 100);
            listView1.Columns.Add("ENTRADA R$", 100);
            listView1.Columns.Add("SAIDA R$", 100);

            listView1.View = View.Details;

            listView1.LabelEdit = false;

            listView1.AllowColumnReorder = false;
            listView1.CheckBoxes = false;
            listView1.FullRowSelect = true;
            listView1.GridLines = false;
            //listView1.Sorting = System.Windows.Forms.SortOrder.Ascending;
        }

        private void DadosDoUsuario()
        {
            SqlCommand comm = new SqlCommand();
            comm.Connection = ConexaoBanco.Abrir();
            comm.CommandText = string.Format("SELECT Financas.Categoria,Financas.Observações,Financas.Entradas, Financas.Saidas, Financas.Saldo "
                                    + "FROM Usuarios INNER JOIN Financas "

                                    + "ON Usuarios.id = '{0}' and Financas.id = '{0}' "
                                    + "order by Financas.qnt asc ", User);

            SqlDataReader dr = comm.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    try
                    {

                        //===========================================================================
                        //ListViewItem lvItem = new ListViewItem(
                        //dr.GetInt32(0).ToString()); //ID -- CATEGORIA
                        //=========================================================
                        string value3;
                        if(dr.GetSqlString(0).IsNull == true)
                        {
                            value3 = "GERAL (N/C)";
                            //MessageBox.Show("Assim funfa");
                        }
                        else
                        {
                            value3 = (dr.GetString(0).ToString());
                            //MessageBox.Show("deu ruim");
                        }
                        ListViewItem lvItem = new ListViewItem(value3);
                        lvItem.BackColor = Color.Aqua;
                        //=========================================================
                        lvItem.UseItemStyleForSubItems = false;

                        //===========================================================================
                        //lvItem.SubItems.Add(dr.GetString(1).ToString()); //NOME -- OBSERVAÇÃO

                        string value4;
                        if(dr.GetSqlString(1).IsNull == true)
                        {
                            value4 = "SEM OBS.";
                        }
                        else
                        {
                            value4 = dr.GetString(1).ToString();
                        }
                        lvItem.SubItems.Add(value4.ToString()); //NOME -- OBSERVAÇÃO
                        //===========================================================================
                        //SALDO
                        double value;

                        if (dr.GetSqlDouble(4).IsNull == true)
                        {
                            value = 0;
                        }
                        else
                        {
                            value = Convert.ToDouble(dr.GetSqlDouble(4).ToString());
                        }
                        lvItem.SubItems.Add(value.ToString("C2")); //saldo
                        if (value < 0)
                        {
                            lvItem.SubItems[2].BackColor = Color.FromArgb(255, 0, 0);
                        }
                        else if (value == 0 || dr.GetSqlDouble(4).ToString() == "null")
                        {
                            lvItem.SubItems[2].BackColor = Color.FromArgb(255, 215, 0);
                        }
                        else
                        {
                            lvItem.SubItems[2].BackColor = Color.FromArgb(50, 205, 50);
                        }
                        //===========================================================================
                        //ENTRADA
                        double value1;

                        if (dr.GetSqlDouble(2).ToString() == "null")
                        {
                            value1 = 0;
                        }
                        else
                        {
                            value1 = dr.GetDouble(2);
                        }
                        lvItem.SubItems.Add(value1.ToString("C2")); //entrada
                        //lvItem.SubItems[3].BackColor = Color.FromArgb(50, 205, 50);
                        lvItem.SubItems[3].ForeColor = Color.FromArgb(0, 100, 0);
                        //===========================================================================
                        //SAIDA
                        double value2;

                        if (dr.GetSqlDouble(3).ToString() == "null")
                        {
                            value2 = 0;
                        }
                        else
                        {
                            value2 = dr.GetDouble(3);
                        }
                        lvItem.SubItems.Add(value2.ToString("C2")); //saida
                        //lvItem.SubItems[4].BackColor = Color.FromArgb(255, 0, 0);
                        lvItem.SubItems[4].ForeColor = Color.FromArgb(255, 0, 0);
                        //===========================================================================
                        listView1.Items.Add(lvItem);
                        {
                            /*/  listView1.Items.Clear();
                              string[] coluna =
                              {
                          //ID
                          (dr.GetInt32(0)).ToString(),
                         //NOME
                          //dr.GetString(1).ToString(),
                          //saldo
                          dr.GetString(1).ToString(),
                          //entrada
                          (dr.GetDecimal(2).ToString()),
                           //saida
                          (dr.GetDecimal(3).ToString()),
                          };
                              var Lista = new ListViewItem(coluna);
                              listView1.Items.Add(Lista);
                              */
                        }
                    }
                    catch (System.Data.SqlTypes.SqlNullValueException)
                    {
                        MessageBox.Show("ALGUM DO VALOR DO BANCO DE DADOS ESTA COM CEDULAS EM BRANCO, DESEJA BUSCAR E EXCLUIR ?",
                                            "ERRO NO BANCO DE DADOS",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
           }
            else
            {
                MessageBox.Show("HMMM... PARECE QUE SEU BANCO DE DADOS ESTA VAZIO, SEJA BEM VINDO ENTAÃO !", "NOVO USUARIO", 
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string valor = textBox2.Text;
            if (valor == "" || string.IsNullOrWhiteSpace(valor))
            {
                MessageBox.Show("Valores em branco", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (comboBox1.Text == null)
                {
                    MessageBox.Show("ESCOLHA SE O VALOR É ENTRADA OU SAIDA");
                }
                else if (comboBox1.Text == "ENTRADA")
                {
                    string um, dois;
                    //ENTRADA
                    string val = textBox2.Text;
                    //CATEGORIA
                    if (comboBox2.Text == "")
                    {
                        um = "GERAL (N/C)";
                    }
                    else
                    {
                        um = comboBox2.Text;
                    }

                    //OBS
                    if (textBox1.Text == "" || string.IsNullOrWhiteSpace(textBox1.Text))
                    {
                        dois = "SEM OBS";
                    }
                    else
                    {
                        dois = textBox1.Text;
                    }
                    //MessageBox.Show(val.ToString());
                    AdicionarValores entrada = new AdicionarValores();
                    entrada.AdicionarEntrada(User, Convert.ToString(val),um, dois);
                    listView1.Items.Clear();
                    DadosDoUsuario();
                    grafico();
                    ObterEntradaSaida(User);
                }
                else
                {
                    string um, dois;
                    //SAIDA
                    string val = textBox2.Text;
                    //CATEGORIA
                    if (comboBox2.Text == "")
                    {
                        um = "GERAL (N/C)";
                    }
                    else
                    {
                    um = comboBox2.Text;
                    }

                    //OBS
                    if(textBox1.Text == "" || string.IsNullOrWhiteSpace(textBox1.Text))
                    {
                        dois = "SEM OBS";
                    }
                    else
                    {
                        dois = textBox1.Text;
                    }
                    //MessageBox.Show(val.ToString());
                    AdicionarValores entrada = new AdicionarValores();
                    entrada.AdicionarSaida(User, Convert.ToString(val), um, dois);
                    listView1.Items.Clear();
                    DadosDoUsuario();
                    grafico();
                    ObterEntradaSaida(User);

                }
                textBox1.Text = "";
                textBox2.Text = "";

            }
        }
        private void button2_KeyPress(object sender, KeyPressEventArgs e)
        {
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '.' || e.KeyChar == ',')
            {
                //|| e.KeyChar ==
            }
            else
            {
                //   e.Handled = true;
            }
        }
        private void CarregarComboBox()
        {
            comboBox1.Items.Add("ENTRADA");
            comboBox1.Items.Add("SAIDA");
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
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
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void grafico()
        {

        }
        private void chart1_Click(object sender, EventArgs e)
        {

        }
        public void ObterEntradaSaida(int id)
        {
            id = User;
            //CRIA E LIMPA O GRAFICO
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            chart1.Series[0].LabelBackColor = Color.Transparent;

            chart1.Legends.Clear();
            chart1.Series["Series1"].Points.Clear();

            ReceberValores entrada = new ReceberValores();
            string te = entrada.TotalEntrada(id);
            string ts = entrada.TotalSaida(id);

            double tee;
            double tss;

            if(te == "" ||ts == "")
            {
            tee = 0;
            tss = 0;
            }
            else
            {
            tee = Convert.ToDouble(te);
            tss = Convert.ToDouble(ts);
            }

            label1.Text = tee.ToString("C2");
            label1.ForeColor = Color.FromArgb(0, 100, 0);

            label4.Text = tss.ToString("C2");
            label4.ForeColor = Color.FromArgb(220, 20, 60);

            label8.Text = (tee - tss).ToString("C2");
            if(tee - tss > 0)
            {
                
                label8.ForeColor = Color.Green;
            }
            else
            {
                label8.ForeColor = Color.Red;
            }

            chart1.Series["Series1"].Points.AddXY("SAIDA", (tss));
            chart1.Series["Series1"].Points.AddXY("ENTRADA", tee);

            //CORES DAS COLUNAS
            //saida
            chart1.Series["Series1"].Points[1].Color = Color.Green;

            //entrada
            chart1.Series["Series1"].Points[0].Color = Color.Red;

            //HABILITA LEGENDA ALTOMATICA
            chart1.Legends.Add("Legend1");
            chart1.Legends["Legend1"].BackColor = Color.Transparent;

            //MessageBox.Show("Saida: "+ ts +"entrada: "+ te);

           double total = tee + tss;
           label13.Text = (((tee / tee) * 100).ToString("F") + "%");
           label14.Text = (((tss / tee) * 100).ToString("F") + "%");

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "ENTRADA")
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("SALARIO");
                comboBox2.Items.Add("TRANSFERENCIA");
                comboBox2.Items.Add("GANHOS GERAIS");
                comboBox2.Items.Add("FREELANCER");
                comboBox2.Items.Add("VENDAS");
                comboBox2.Items.Add("INVESTIMENTOS");
            }
            else if (comboBox1.Text == "SAIDA")
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("DESPESAS");
                comboBox2.Items.Add("ALIMENTAÇÃO");
                comboBox2.Items.Add("LAZER");
                comboBox2.Items.Add("EMERGENCIA");
                comboBox2.Items.Add("COMBUSTIVEL");
                comboBox2.Items.Add("MANUTENÇÃO");
                comboBox2.Items.Add("VESTUARIO");
                comboBox2.Items.Add("VIAGENS");
                comboBox2.Items.Add("MIMOS");
            }
            else
            {
                comboBox2.Items.Clear();
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
