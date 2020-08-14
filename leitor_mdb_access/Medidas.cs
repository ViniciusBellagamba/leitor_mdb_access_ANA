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
            List<DateTime> temp = new List<DateTime>();
            int contador = 1;
            foreach (var x in lista.Skip(1))
            {
                if ((x.dt - lista[contador - 1].dt).TotalDays > 1)
                {
                    var datas = Enumerable.Range(1, -1 + x.dt.Subtract(lista[contador - 1].dt).Days)
                        .Select(offset => lista[contador - 1].dt.AddDays(offset))
                        .ToArray();

                    foreach (var data in datas)
                    {
                        temp.Add(data);
                    }

                }
                contador++;
            }

            foreach (var x in temp)
            {
                lista.Add(new Medidas(x, "", ""));
            }

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
            try
            {
                OleDbConnection myConnection = new OleDbConnection();
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
                    for (int i = 1; i <= 31; i++)
                    {
                        string index = variavel + i.ToString("00");
                        if (reader[index] != DBNull.Value)
                        {
                            DateTime dt = DateTime.ParseExact(reader["Data"].ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                            dt = dt.AddDays(i - 1);
                            lista.Add(new Medidas(dt, reader["NivelConsistencia"].ToString(), reader[index].ToString()));
                        }
                    }
                }
                myConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("OLEDB Connection FAILED: " + ex.Message);
            }
            lista.Sort((x, y) => DateTime.Compare(x.dt, y.dt));
            return lista;
        }
    }
}
