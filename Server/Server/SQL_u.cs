using System;
using System.Data;
using System.Data.SqlClient;

namespace Server
{
    class SQL_u
    {
        private SqlConnection sqlCon;
        private string server;
        private string database = "Chat";
        private string uid;
        private string password;

        /// <summary>
        /// 初始化 SQL Server 链接, 我用的是Windows 身份验证
        /// </summary>
        /// <param name="server"></param>
        /// <param name="uid"></param>
        /// <param name="password"></param>
        public void Initialize(string server, string database)
        {
            this.server = server;
            string connectionString = $"Server={server};Database={database};Integrated Security=True;";
            sqlCon = new SqlConnection(connectionString);
        }

        /// <summary>
        /// 打开数据库链接
        /// </summary>
        /// <returns></returns>
        public bool OpenConnection()
        {
            try
            {
                sqlCon.Open();
                return true;
            }
            catch (SqlException ex)
            {
                // 根据错误编号处理错误
                switch (ex.Number)
                {
                    case 0:
                        Console.Write("无法连接到服务器，请联系管理员");
                        break;

                    case 18456:
                        Console.Write("无效的用户名或密码，请重试");
                        break;

                    default:
                        Console.Write($"连接数据库失败：{ex.Message}");
                        break;
                }

                return false;
            }
        }

        /// <summary>
        /// 关闭数据库链接
        /// </summary>
        /// <returns></returns>
        public bool CloseConnection()
        {
            try
            {
                sqlCon.Close();
                return true;
            }
            catch (SqlException ex)
            {
                Console.Write($"关闭连接失败：{ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 返回 SqlDataAdapter
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public SqlDataAdapter GetAdapter(string SQL)
        {
            SqlDataAdapter da = new SqlDataAdapter(SQL, sqlCon);
            return da;
        }

        /// <summary>
        /// 构建 SQL 命令
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public SqlCommand CreateCmd(string SQL)
        {
            SqlCommand cmd = new SqlCommand(SQL, sqlCon);
            return cmd;
        }

        /// <summary>
        /// 根据 SQL 创建 DataTable
        /// </summary>
        /// <param name="SQL"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string SQL, string tableName)
        {
            SqlDataAdapter da = new SqlDataAdapter(SQL, sqlCon);
            DataTable dt = new DataTable(tableName);
            da.Fill(dt);
            return dt;
        }

        /// <summary>
        /// 根据 SQL 返回 SqlDataReader 对象
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public SqlDataReader GetReader(string SQL)
        {
            SqlCommand cmd = new SqlCommand(SQL, sqlCon);
            try
            {
                return cmd.ExecuteReader(CommandBehavior.Default);
            }
            catch
            {
                throw new Exception("读取数据失败：" + SQL);
            }
        }

        /// <summary>
        /// 根据 SQL 返回 DataSet 对象
        /// </summary>
        /// <param name="SQL"></param>
        /// <param name="ds"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataSet Get_DataSet(string SQL, DataSet ds, string tableName)
        {
            SqlDataAdapter da = new SqlDataAdapter(SQL, sqlCon);
            try
            {
                da.Fill(ds, tableName);
            }
            catch (Exception ex)
            {
                throw new Exception("获取DataSet失败：" + ex.Message);
            }

            return ds;
        }

        /// <summary>
        /// 根据 SQL 返回 DataSet 对象，将数据分页
        /// </summary>
        /// <param name="SQL"></param>
        /// <param name="ds"></param>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string SQL, DataSet ds, int startIndex, int pageSize, string tableName)
        {
            SqlDataAdapter da = new SqlDataAdapter(SQL, sqlCon);
            try
            {
                da.Fill(ds, startIndex, pageSize, tableName);
            }
            catch (Exception ex)
            {
                throw new Exception("分页获取DataSet失败：" + ex.Message);
            }

            return ds;
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="SQL"></param>
        public void GetInsert(string SQL)
        {
            SqlCommand cmd = new SqlCommand(SQL, sqlCon);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("插入数据失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="SQL"></param>
        public void GetUpdate(string SQL)
        {
            SqlCommand cmd = new SqlCommand(SQL, sqlCon);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("修改数据失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="SQL"></param>
        public void GetDel(string SQL)
        {
            SqlCommand cmd = new SqlCommand(SQL, sqlCon);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("删除数据失败：" + ex.Message);
            }
        }
    }
}
