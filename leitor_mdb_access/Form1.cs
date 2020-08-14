﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace leitor_mdb_access
{
    public partial class Form1 : Form
    {
        private string tabela;
        public static string file_path;

        public static string data_max_min(string variavel, string file_path, string cod_estacao)
        {
            string datas = " ; ";
            string myConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;" +
                                       $@"Data Source={file_path};";
            try
            {
                OleDbConnection myConnection = new OleDbConnection();
                myConnection.ConnectionString = myConnectionString;
                myConnection.Open();

                OleDbCommand cmd = myConnection.CreateCommand();
                cmd.Connection = myConnection;
                cmd.CommandText = $"SELECT MIN(Data), MAX(Data) from {variavel} where EstacaoCodigo = {cod_estacao}";
                OleDbDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    DateTime dt_inicio = DateTime.ParseExact(reader[0].ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    DateTime dt_fim = DateTime.ParseExact(reader[1].ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    datas = $"{dt_inicio.ToString("dd/MM/yyyy")};{dt_fim.ToString("dd/MM/yyyy")}";
                }
                myConnection.Close();
                return datas;
            }
            catch (Exception ex)
            {
                Console.WriteLine("OLEDB Connection FAILED: " + ex.Message);
                return datas;
            }
        }
       

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var FD = new OpenFileDialog();
            if (FD.ShowDialog() ==DialogResult.OK)
            {
                string fileToOpen = FD.FileName;

               FileInfo File = new FileInfo(FD.FileName);

                textBox_mdb.Text = File.ToString();
                file_path = textBox_mdb.Text;
                string myConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;" +
                                           $@"Data Source={file_path};";
                try
                {
                    OleDbConnection myConnection = new OleDbConnection();
                    myConnection.ConnectionString = myConnectionString;
                    myConnection.Open();

                    OleDbCommand cmd = myConnection.CreateCommand();
                    cmd.Connection = myConnection;
                    cmd.CommandText = $"select TipoEstacao, Codigo, Nome from Estacao";

                    myConnection.Close();

                    DataTable tabela = new DataTable();
                    tabela.Clear();

                    OleDbDataAdapter ad = new OleDbDataAdapter();
                    ad.SelectCommand = cmd;
                    ad.Fill(tabela);

                    dataGridView_estacao.DataSource = tabela;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("OLEDB Connection FAILED: " + ex.Message);
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (Radio_Vazao.Checked == true)
            {
                string datas = data_max_min("Vazoes", textBox_mdb.Text, textBox_codEstacao.Text);
                var data = datas.Split(';');
                textBox_dataInicio.Text = data[0];
                textBox_dataFim.Text = data[1];
                tabela = "Vazoes";
            }
        }

        private void Radio_Nivel_CheckedChanged(object sender, EventArgs e)
        {
            if (Radio_Nivel.Checked == true)
            {
                string datas = data_max_min("Cotas", textBox_mdb.Text, textBox_codEstacao.Text);
                var data = datas.Split(';');
                textBox_dataInicio.Text = data[0];
                textBox_dataFim.Text = data[1];
                tabela = "Cotas";
            }
        }

        private void Radio_Chuva_CheckedChanged(object sender, EventArgs e)
        {
            if (Radio_Chuva.Checked == true)
            {
                string datas = data_max_min("Chuvas", textBox_mdb.Text, textBox_codEstacao.Text);
                var data = datas.Split(';');
                textBox_dataInicio.Text = data[0];
                textBox_dataFim.Text = data[1];
                tabela = "Chuvas";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_gerar_Click(object sender, EventArgs e)
        {
            string estacao;
            try
            {
                String searchValue = textBox_codEstacao.Text;
                int rowIndex = -1;
                foreach (DataGridViewRow row in dataGridView_estacao.Rows)
                {
                    if (row.Cells[1].Value.ToString().Equals(searchValue))
                    {
                        rowIndex = row.Index;
                        break;
                    }
                }

                estacao = dataGridView_estacao.Rows[rowIndex].Cells[2].Value.ToString();
            }
            catch { estacao = ""; }

            string selected = comboBox1.GetItemText(this.comboBox1.SelectedItem);
            string option = selected;

            string dataInicio = textBox_dataInicio.Text;
            string dataFim = textBox_dataFim.Text;
            string codEstacao = textBox_codEstacao.Text;
            List<Medidas> lista_consistida;
            List<Medidas> lista_bruta;

            StreamWriter writer = new StreamWriter($"{estacao}_{codEstacao}_{tabela}.csv");
            writer.WriteLine("Data;{0};NC", tabela);
            switch (option)
            {
                case "Bruto":
                    lista_bruta = Medidas.Get_Oledb_Medidas(tabela, dataInicio, dataFim, "1", codEstacao);
                    lista_bruta = Medidas.Adjust_List(lista_bruta);

                    foreach (var medida in lista_bruta)
                        writer.WriteLine("{0};{1};{2}", medida.dt, medida.Valor, medida.NivelConsistencia);
                    break;
                case "Consistido":
                    lista_consistida = Medidas.Get_Oledb_Medidas(tabela, dataInicio, dataFim, "2", codEstacao);
                    lista_consistida = Medidas.Adjust_List(lista_consistida);

                    foreach (var medida in lista_consistida)
                        writer.WriteLine("{0};{1};{2}", medida.dt, medida.Valor, medida.NivelConsistencia);
                    break;
                case "Ambos":
                    lista_bruta = Medidas.Get_Oledb_Medidas(tabela, dataInicio, dataFim, "1", codEstacao);
                    lista_consistida = Medidas.Get_Oledb_Medidas(tabela, dataInicio, dataFim, "2", codEstacao);
                    List<Medidas> fusao = Medidas.Fusao_Listas(lista_bruta, lista_consistida);
                    fusao = Medidas.Adjust_List(fusao);

                    foreach (var medida in fusao)
                        writer.WriteLine("{0};{1};{2}", medida.dt, medida.Valor, medida.NivelConsistencia);
                    break;
            }
            writer.Close();
        }

        private void textBox_codEstacao_TextChanged(object sender, EventArgs e)
        {
            string datas = data_max_min(tabela, textBox_mdb.Text, textBox_codEstacao.Text);
            var data = datas.Split(';');
            textBox_dataInicio.Text = data[0];
            textBox_dataFim.Text = data[1];
        }

        private void dataGridView_estacao_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox_codEstacao.Text = dataGridView_estacao.CurrentCell.Value.ToString();
        }
    }
}