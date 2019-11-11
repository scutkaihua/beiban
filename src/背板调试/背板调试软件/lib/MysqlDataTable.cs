using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace LD.lib
{
    public class MySqlDataTable
    {
        static string source = "server=127.0.0.1;port=3306;user=root;password=kaihua;database=laohua;CharSet=utf8";

        static List<KeyValue<string, string>> values = new List<KeyValue<string, string>>();

       MySqlConnection connection=new MySqlConnection(source);



        public DataTable Query(string query)
        {

            try
            {
                connection.Open();
                DataTable dt = QueryDataTable(query);
                ReplaceHeader(dt);
                connection.Close();
                return dt;
            }catch(Exception e)
            {
                connection.Close();
                return null;
            }
        }

        /// <summary>
        /// 查询数据表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public DataTable QueryDataTable(string query)
        {
            MySqlCommand command;
            MySqlDataAdapter adapter;
            command = new MySqlCommand(query, connection);
            command.CommandType = CommandType.Text;
            DataTable dataTable = new DataTable();
            adapter = new MySqlDataAdapter(command);
            adapter.Fill(dataTable);
            return dataTable;
        }



        public List<KeyValue<string,string>> FieldComment()
        {

            if (values.Count != 0) return values;
            string[] tables = new string[] { "task","channels","details"};
            foreach(string t in tables)
            {
                string q = "show full columns from " + t;
                DataTable dt = QueryDataTable(q);
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    string k = (string)dt.Rows[i][0];
                    string v = (string)dt.Rows[i][8];
                    KeyValue<string, string> kv = new KeyValue<string, string>(k,v);
                    values.Add(kv);
                }
            }
            return values;
        }

        public void ReplaceHeader(DataTable dt)
        {
            List<KeyValue<string, string>> fc = FieldComment();
            foreach (DataColumn dc in dt.Columns)
            {
                dc.ColumnName = ReplaceAHeader(dc.ColumnName,fc);
            }
        }

        public string ReplaceAHeader(string src,List<KeyValue<string,string>> headers)
        {
            foreach(KeyValue<string,string> kv in headers)
            {
                if (kv.key == src) return kv.val;
            }
            return src;
        }


    }
}
