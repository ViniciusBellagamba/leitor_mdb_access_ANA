using System;
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

        static string Dt_search(string valor,TextBox textBox, DataGridView dataGridView)
        {
            try
            {
                String searchValue = textBox.Text;
                int rowIndex = -1;
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.Cells[1].Value.ToString().Equals(searchValue))
                    {
                        rowIndex = row.Index;
                        break;
                    }
                }

                return dataGridView.Rows[rowIndex].Cells[valor].Value.ToString();
            }
            catch { return null; }
        }

        static void Data_max_min(string variavel, string file_path, string cod_estacao, DateTimePicker data_inicio, DateTimePicker data_fim)
        {
            string myConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;" +
                                       $@"Data Source={file_path};";
            OleDbConnection myConnection = new OleDbConnection();
            try
            {
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
                    data_inicio.CustomFormat = "dd/MM/yyyy";
                    data_inicio.Format = DateTimePickerFormat.Custom;
                    data_fim.CustomFormat = "dd/MM/yyyy";
                    data_fim.Format = DateTimePickerFormat.Custom;
                    data_inicio.Value = dt_inicio;
                    data_fim.Value = dt_fim;
                }
                myConnection.Close();
            }
            catch
            {
                myConnection.Close();
                data_inicio.CustomFormat = " ";
                data_inicio.Format = DateTimePickerFormat.Custom;
                data_fim.CustomFormat = " ";
                data_fim.Format = DateTimePickerFormat.Custom;
            }
        }
       
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            dtPicker_inicio.CustomFormat = " ";
            dtPicker_inicio.Format = DateTimePickerFormat.Custom;
            dtPicker_fim.CustomFormat = " ";
            dtPicker_fim.Format = DateTimePickerFormat.Custom;
            btn_gerar.Enabled = false;
            dataGridView_estacao.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView_estacao.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void Btn_buscar_Click(object sender, EventArgs e)
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
                OleDbConnection myConnection = new OleDbConnection();
                try
                {
                    myConnection.ConnectionString = myConnectionString;
                    myConnection.Open();

                    OleDbCommand cmd = myConnection.CreateCommand();
                    cmd.Connection = myConnection;
                    cmd.CommandText = $"select TipoEstacao, Codigo, Nome from Estacao";

                    DataTable tabela = new DataTable();
                    tabela.Clear();

                    OleDbDataAdapter ad = new OleDbDataAdapter() {
                        SelectCommand = cmd
                    };

                    ad.Fill(tabela);
                    dataGridView_estacao.DataSource = tabela;
                    btn_gerar.Enabled = true;
                    myConnection.Close();

                }
                catch
                {
                    myConnection.Close();
                    btn_gerar.Enabled = false;
                    dataGridView_estacao.DataSource = null;
                }
            }
        }

        private void Radio_Vazao_CheckedChanged(object sender, EventArgs e)
        {
            if (Radio_Vazao.Checked == true)
            {
                Data_max_min("Vazoes", textBox_mdb.Text, textBox_codEstacao.Text, dtPicker_inicio, dtPicker_fim);
                tabela = "Vazoes";
            }
        }

        private void Radio_Nivel_CheckedChanged(object sender, EventArgs e)
        {
            if (Radio_Nivel.Checked == true)
            {
                Data_max_min("Cotas", textBox_mdb.Text, textBox_codEstacao.Text, dtPicker_inicio, dtPicker_fim);
                tabela = "Cotas";
            }
        }

        private void Radio_Chuva_CheckedChanged(object sender, EventArgs e)
        {
            if (Radio_Chuva.Checked == true)
            {
                Data_max_min("Chuvas", textBox_mdb.Text, textBox_codEstacao.Text, dtPicker_inicio, dtPicker_fim);
                tabela = "Chuvas";
            }
        }

        private void Btn_gerar_Click(object sender, EventArgs e)
        {
            string estacao = Dt_search("Nome", textBox_codEstacao, dataGridView_estacao);
            string selected = comboBox1.GetItemText(this.comboBox1.SelectedItem);
            string option = selected;
            string dataInicio = dtPicker_inicio.Text;
            string dataFim = dtPicker_fim.Text;
            string codEstacao = textBox_codEstacao.Text;
            List<Medidas> lista_consistida;
            List<Medidas> lista_bruta;
            string fileName;

            if (option == "Ambos")
                fileName = $"{estacao}_{codEstacao}_{tabela}.csv";
            else
                fileName = $"{estacao}_{codEstacao}_{tabela}_{option}.csv";

            StreamWriter writer = new StreamWriter(fileName);
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

        private void TextBox_codEstacao_TextChanged(object sender, EventArgs e)
        {
            Data_max_min(tabela, textBox_mdb.Text, textBox_codEstacao.Text, dtPicker_inicio, dtPicker_fim);

            string tipo = Dt_search("TipoEstacao", textBox_codEstacao, dataGridView_estacao);

            switch(tipo)
            {
                case "1":
                    Radio_Nivel.Enabled = true;
                    Radio_Vazao.Enabled = true;
                    Radio_Chuva.Enabled = false;
                    Radio_Chuva.Checked = false;
                    if (Radio_Nivel.Checked == false && Radio_Vazao.Checked == false)
                        Radio_Nivel.Checked = true;
                    break;
                case "2":
                    Radio_Nivel.Enabled = false;
                    Radio_Vazao.Enabled = false;
                    Radio_Chuva.Enabled = true;
                    Radio_Nivel.Checked = false;
                    Radio_Vazao.Checked = false;
                    Radio_Chuva.Checked = true;
                    break;
            }
        }

        private void DataGridView_estacao_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView_estacao.SelectedRows)
            {
                textBox_codEstacao.Text = row.Cells["Codigo"].Value.ToString();
            }
        }
    }
}
