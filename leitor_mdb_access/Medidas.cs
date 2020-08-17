using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;

namespace leitor_mdb_access
{
    public class Medidas
    {
        public string NivelConsistencia;
        public DateTime dt;
        public string Valor;

        public Medidas(DateTime dt, string NivelConsistencia, string Valor)
        {
            this.dt = dt;
            this.NivelConsistencia = NivelConsistencia;
            this.Valor = Valor;
        }

        public static List<Medidas> Adjust_List(List<Medidas> lista)
        {
            lista.Sort((x, y) => DateTime.Compare(x.dt, y.dt));
            return lista;
        }

        public static List<Medidas> Fusao_Listas(List<Medidas> lista_bruta, List<Medidas> lista_consistida)
        {
            List<Medidas> lista_final = new List<Medidas>();

            foreach (var medida in lista_bruta)
            {
                var item = lista_consistida.Find(x => x.dt == medida.dt);

                if (item == null)
                {
                    lista_final.Add(new Medidas(medida.dt, medida.NivelConsistencia, medida.Valor));
                }
                else
                {
                    lista_final.Add(new Medidas(item.dt, item.NivelConsistencia, item.Valor));
                }
            }

            return lista_final;
        }

        public static List<Medidas> Get_Oledb_Medidas(string tabela,string data_inicio, string data_fim ,string NivelConsistencia, string cod_estacao)
        {
            List<Medidas> lista = new List<Medidas>();

            string myConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;" +
                                       $@"Data Source={Form1.file_path};";
            OleDbConnection myConnection = new OleDbConnection();
            try
            {
                myConnection.ConnectionString = myConnectionString;
                myConnection.Open();
                OleDbCommand cmd = myConnection.CreateCommand();
                cmd.Connection = myConnection;
                cmd.CommandText = $"select * from {tabela} where Data between #{data_inicio}# AND #{data_fim}# AND NivelConsistencia = {NivelConsistencia} AND EstacaoCodigo = {cod_estacao}";

                string variavel = "";
                switch (tabela)
                {
                    case "Cotas":
                        variavel = "Cota";
                        cmd.CommandText = $"select * from {tabela} where Data between #{data_inicio}# AND #{data_fim}# AND NivelConsistencia = {NivelConsistencia} AND MediaDiaria = 1 AND EstacaoCodigo = {cod_estacao}";
                        break;
                    case "Chuvas":
                        variavel = "Chuva";
                        break;
                    case "Vazoes":
                        variavel = "Vazao";
                        cmd.CommandText = $"select * from {tabela} where Data between #{data_inicio}# AND #{data_fim}# AND NivelConsistencia = {NivelConsistencia} AND MediaDiaria = 1 AND EstacaoCodigo = {cod_estacao}";
                        break;
                }

                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DateTime dt = DateTime.ParseExact(reader["Data"].ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    int days = DateTime.DaysInMonth(dt.Year, dt.Month);
                    for (int i = 1; i <= days; i++)
                    {
                        string index = variavel + i.ToString("00");
                        dt = DateTime.ParseExact(reader["Data"].ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        dt = dt.AddDays(i - 1);
                        lista.Add(new Medidas(dt, reader["NivelConsistencia"].ToString(), reader[index].ToString()));
                    }
                }
                myConnection.Close();
            }
            catch
            {
                myConnection.Close();
            }
            lista.Sort((x, y) => DateTime.Compare(x.dt, y.dt));
            return lista;
        }
    }
}
