using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartGasAPI.Models.SPAREPART;
using Microsoft.Data.SqlClient;
using SmartGasAPI.Models;
using System.Data;
using System.Data.Common;

namespace SmartGasAPI.Data
{

    public class SmartGas_SP_DBContext : SmartGasContext
    {
        public SmartGas_SP_DBContext(DbContextOptions<SmartGas_SP_DBContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<InOutInfo> InOutInfo { get; set; }
        public DbSet<Unit> Units { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(new string[] { "PLANT", "DEPARTMENT", "USER_ID" });
            modelBuilder.Entity<InOutInfo>().HasKey(new string[] { "ID" });
            modelBuilder.Entity<Unit>().HasKey(new string[] { "ID" });
        }

        public override DbSet<User> GetUser()
        {
            return Users;
        }

        public ResultDB GetNameBySparepart(string deptCode, string sparepart)
        {
            try
            {
                SqlParameter pDepartment = new SqlParameter("A_DEPT_CODE", deptCode);
                SqlParameter pCode = new SqlParameter("A_SPARE_PART_CODE", sparepart);


                var nReturn = new SqlParameter
                {
                    ParameterName = "N_RETURN",
                    DbType = DbType.Int32,
                    Direction = ParameterDirection.Output
                };
                var vReturn = new SqlParameter
                {
                    ParameterName = "V_RETURN",
                    DbType = DbType.String,
                    Direction = ParameterDirection.Output,
                    Size = 4000
                };

                string sql = "PKG_BUSINESS_GET_NAME_BY_CODE";

                using (var command = Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(new SqlParameter[] { pDepartment, pCode, nReturn, vReturn });
                    Database.OpenConnection();

                    DbDataAdapter adapter = DbProviderFactories.GetFactory(Database.GetDbConnection()).CreateDataAdapter();
                    adapter.SelectCommand = command;

                    // Fill the DataTable.
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    _log4net.Info("GetNameBySparepart DB context: row count " + table.Rows.Count);

                    return new ResultDB()
                    {
                        N_RETURN = (int)nReturn.Value,
                        V_RETURN = (string)vReturn.Value,
                        Data = table
                    };
                }
            }
            catch (Exception ex)
            {
                _log4net.Info("GetNameBySparepart DB context: " + ex.Message);
                return new ResultDB()
                {
                    N_RETURN = -999,
                    V_RETURN = ex.Message
                };
            }
        }

        public ResultDB GetUnitBySparepart(string deptCode, string sparepart)
        {
            try
            {
                SqlParameter pDepartment = new SqlParameter("A_DEPT_CODE", deptCode);
                SqlParameter pCode = new SqlParameter("A_SPARE_PART_CODE", sparepart);


                var nReturn = new SqlParameter
                {
                    ParameterName = "N_RETURN",
                    DbType = DbType.Int32,
                    Direction = ParameterDirection.Output
                };
                var vReturn = new SqlParameter
                {
                    ParameterName = "V_RETURN",
                    DbType = DbType.String,
                    Direction = ParameterDirection.Output,
                    Size = 4000
                };

                string sql = "PKG_BUSINESS_UNIT_SPAREPART@GET_UNIT_BY_SPAREPART_MOBILE";

                using (var command = Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(new SqlParameter[] { pDepartment, pCode, nReturn, vReturn });
                    Database.OpenConnection();

                    DbDataAdapter adapter = DbProviderFactories.GetFactory(Database.GetDbConnection()).CreateDataAdapter();
                    adapter.SelectCommand = command;

                    // Fill the DataTable.
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    _log4net.Info("GetUnitBySparepart DB context: row count " + table.Rows.Count);

                    return new ResultDB()
                    {
                        N_RETURN = (int)nReturn.Value,
                        V_RETURN = (string)vReturn.Value,
                        Data = table
                    };
                }
            }
            catch (Exception ex)
            {
                _log4net.Info("GetUnitBySparepart DB context: " + ex.Message);
                return new ResultDB()
                {
                    N_RETURN = -999,
                    V_RETURN = ex.Message
                };
            }
        }

        public ResultDB GetLocationBySparepart(string deptCode, string sparepart)
        {
            try
            {
                SqlParameter pDepartment = new SqlParameter("A_DEPT_CODE", deptCode);
                SqlParameter pCode = new SqlParameter("A_SPARE_PART_CODE", sparepart);


                var nReturn = new SqlParameter
                {
                    ParameterName = "N_RETURN",
                    DbType = DbType.Int32,
                    Direction = ParameterDirection.Output
                };
                var vReturn = new SqlParameter
                {
                    ParameterName = "V_RETURN",
                    DbType = DbType.String,
                    Direction = ParameterDirection.Output,
                    Size = 4000
                };

                string sql = "PKG_BUSINESS_UNIT_SPAREPART@GET_UNIT_BY_SPAREPART_MOBILE";

                using (var command = Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(new SqlParameter[] { pDepartment, pCode, nReturn, vReturn });
                    Database.OpenConnection();

                    DbDataAdapter adapter = DbProviderFactories.GetFactory(Database.GetDbConnection()).CreateDataAdapter();
                    adapter.SelectCommand = command;

                    // Fill the DataTable.
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    _log4net.Info("GetLocationBySparepart DB context: row count " + table.Rows.Count);

                    return new ResultDB()
                    {
                        N_RETURN = (int)nReturn.Value,
                        V_RETURN = (string)vReturn.Value,
                        Data = table
                    };
                }
            }
            catch (Exception ex)
            {
                _log4net.Info("GetLocationBySparepart DB context: " + ex.Message);
                return new ResultDB()
                {
                    N_RETURN = -999,
                    V_RETURN = ex.Message
                };
            }
        }

        public ResultDB GetSparepartCodeByEnCript(string deptCode, string sparepart)
        {
            try
            {
                SqlParameter pDepartment = new SqlParameter("A_DEPARTMENT", deptCode);
                SqlParameter pCode = new SqlParameter("A_SPARE_PART_CODE", sparepart);


                var nReturn = new SqlParameter
                {
                    ParameterName = "N_RETURN",
                    DbType = DbType.Int32,
                    Direction = ParameterDirection.Output
                };
                var vReturn = new SqlParameter
                {
                    ParameterName = "V_RETURN",
                    DbType = DbType.String,
                    Direction = ParameterDirection.Output,
                    Size = 4000
                };

                string sql = "PKG_BUSINESS_SPAREPART@ENCRIPT_CODE_BY_ID";

                using (var command = Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(new SqlParameter[] { pDepartment, pCode, nReturn, vReturn });
                    Database.OpenConnection();

                    DbDataAdapter adapter = DbProviderFactories.GetFactory(Database.GetDbConnection()).CreateDataAdapter();
                    adapter.SelectCommand = command;

                    // Fill the DataTable.
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    _log4net.Info("GetSparepartCodeByEnCript DB context: row count " + table.Rows.Count);

                    return new ResultDB()
                    {
                        N_RETURN = (int)nReturn.Value,
                        V_RETURN = (string)vReturn.Value,
                        Data = table
                    };
                }
            }
            catch (Exception ex)
            {
                _log4net.Info("GetSparepartCodeByEnCript DB context: " + ex.Message);
                return new ResultDB()
                {
                    N_RETURN = -999,
                    V_RETURN = ex.Message
                };
            }
        }

        #region Nhap Xuat Kho
        public ResultDB PutStock(pInOutInfo pInOutModel)
        {
            try
            {
                SqlParameter id = new SqlParameter("A_ID", "");
                SqlParameter sparepartCode = new SqlParameter("A_SPARE_PART", pInOutModel.spare_part_code);
                SqlParameter sparepartName = new SqlParameter("A_SPARE_PART_NAME", pInOutModel.name);
                SqlParameter qty = new SqlParameter("A_QUANTITY", pInOutModel.quantity);
                SqlParameter unit = new SqlParameter("A_UNIT", pInOutModel.unit);
                SqlParameter stockCode = new SqlParameter("A_STOCK_CODE", pInOutModel.stock_code);
                SqlParameter intOut = new SqlParameter("A_IN_OUT", pInOutModel.in_out);
                SqlParameter user = new SqlParameter("A_USER", pInOutModel.user_create);
                SqlParameter dept = new SqlParameter("A_DEPARTMENT", pInOutModel.department);

                SqlParameter location = new SqlParameter("A_LOCATION", pInOutModel.location);
                SqlParameter time_in = new SqlParameter("A_TIME_IN", pInOutModel.create_date);
                SqlParameter ex_time = new SqlParameter("A_TIME_EXPRICE", pInOutModel.exprired_date);

                var nReturn = new SqlParameter
                {
                    ParameterName = "N_RETURN",
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output
                };

                var vReturn = new SqlParameter
                {
                    ParameterName = "V_RETURN",
                    DbType = System.Data.DbType.String,
                    Direction = System.Data.ParameterDirection.Output,
                    Size = 4000
                };

                string sql = "EXEC PKG_BUSINESS_GOODS_RECEIPT_ISSUE_TYPE_DATA_TABLE_PUT_MOBILE@PUT @A_ID,@A_SPARE_PART,@A_SPARE_PART_NAME,@A_QUANTITY,@A_UNIT,@A_STOCK_CODE,@A_IN_OUT,@A_USER,@A_DEPARTMENT,@A_LOCATION,@A_TIME_IN,@A_TIME_EXPRICE,@N_RETURN OUT,@V_RETURN OUT";
                var result = this.Database.ExecuteSqlRaw(sql, id, sparepartCode, sparepartName, qty, unit, stockCode, intOut, user, dept, location, time_in, ex_time, nReturn, vReturn);
                return new ResultDB()
                {
                    N_RETURN = (int)nReturn.Value,
                    V_RETURN = (string)vReturn.Value
                };
            }
            catch (Exception ex)
            {
                return new ResultDB()
                {
                    N_RETURN = -999,
                    V_RETURN = ex.Message
                };
            }

        }

        public ResultDB GetPutInHistory(string deptCode)
        {
            try
            {
                SqlParameter pDepartment = new SqlParameter("A_DEPARTMENT", deptCode);

                var nReturn = new SqlParameter
                {
                    ParameterName = "N_RETURN",
                    DbType = DbType.Int32,
                    Direction = ParameterDirection.Output
                };
                var vReturn = new SqlParameter
                {
                    ParameterName = "V_RETURN",
                    DbType = DbType.String,
                    Direction = ParameterDirection.Output,
                    Size = 4000
                };

                string sql = "PKG_BUSINESS_DATA_PUT_MOBILE@INFO";

                using (var command = Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(new SqlParameter[] { pDepartment, nReturn, vReturn });
                    Database.OpenConnection();

                    DbDataAdapter adapter = DbProviderFactories.GetFactory(Database.GetDbConnection()).CreateDataAdapter();
                    adapter.SelectCommand = command;

                    // Fill the DataTable.
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    _log4net.Info("GetPutInHistory DB context: row count " + table.Rows.Count);

                    return new ResultDB()
                    {
                        N_RETURN = (int)nReturn.Value,
                        V_RETURN = (string)vReturn.Value,
                        Data = table
                    };
                }
            }
            catch (Exception ex)
            {
                _log4net.Info("GetPutInHistory DB context: " + ex.Message);
                return new ResultDB()
                {
                    N_RETURN = -999,
                    V_RETURN = ex.Message
                };
            }
        }

        public ResultDB GetPutOutHistory(string deptCode)
        {
            try
            {
                SqlParameter pDepartment = new SqlParameter("A_DEPARTMENT", deptCode);

                var nReturn = new SqlParameter
                {
                    ParameterName = "N_RETURN",
                    DbType = DbType.Int32,
                    Direction = ParameterDirection.Output
                };
                var vReturn = new SqlParameter
                {
                    ParameterName = "V_RETURN",
                    DbType = DbType.String,
                    Direction = ParameterDirection.Output,
                    Size = 4000
                };

                string sql = "PKG_BUSINESS_DATA_EXPORT_MOBILE@INFO";

                using (var command = Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(new SqlParameter[] { pDepartment, nReturn, vReturn });
                    Database.OpenConnection();

                    DbDataAdapter adapter = DbProviderFactories.GetFactory(Database.GetDbConnection()).CreateDataAdapter();
                    adapter.SelectCommand = command;

                    // Fill the DataTable.
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    _log4net.Info("GetPutOutHistory DB context: row count " + table.Rows.Count);

                    return new ResultDB()
                    {
                        N_RETURN = (int)nReturn.Value,
                        V_RETURN = (string)vReturn.Value,
                        Data = table
                    };
                }
            }
            catch (Exception ex)
            {
                _log4net.Info("GetPutOutHistory DB context: " + ex.Message);
                return new ResultDB()
                {
                    N_RETURN = -999,
                    V_RETURN = ex.Message
                };
            }
        }

        public ResultDB GetGasInforInSparepart(string deptCode, string gasId)
        {
            try
            {
                SqlParameter pGasId = new SqlParameter("A_GAS_ID", gasId);
                SqlParameter pDepartment = new SqlParameter("A_DEPARTMENT", deptCode);

                var nReturn = new SqlParameter
                {
                    ParameterName = "N_RETURN",
                    DbType = DbType.Int32,
                    Direction = ParameterDirection.Output
                };

                var vReturn = new SqlParameter
                {
                    ParameterName = "V_RETURN",
                    DbType = DbType.String,
                    Direction = ParameterDirection.Output,
                    Size = 4000
                };

                string sql = "PKG_BUSINESS_SPAREPART@GET_SPARE_INFOR_BY_GASID";

                using (var command = Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(new SqlParameter[] { pDepartment, pGasId, nReturn, vReturn });
                    Database.OpenConnection();

                    DbDataAdapter adapter = DbProviderFactories.GetFactory(Database.GetDbConnection()).CreateDataAdapter();
                    adapter.SelectCommand = command;

                    // Fill the DataTable.
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    _log4net.Info("GetGasInforInSparepart DB context: row count " + table.Rows.Count);

                    return new ResultDB()
                    {
                        N_RETURN = (int)nReturn.Value,
                        V_RETURN = (string)vReturn.Value,
                        Data = table
                    };
                }
            }
            catch (Exception ex)
            {
                _log4net.Info("GetGasInforInSparepart DB context: " + ex.Message);
                return new ResultDB()
                {
                    N_RETURN = -999,
                    V_RETURN = ex.Message
                };
            }
        }

        public ResultDB GetGasInforReturn(string deptCode)
        {
            try
            {
                SqlParameter pDepartment = new SqlParameter("A_DEPARTMENT", deptCode);

                var nReturn = new SqlParameter
                {
                    ParameterName = "N_RETURN",
                    DbType = DbType.Int32,
                    Direction = ParameterDirection.Output
                };

                var vReturn = new SqlParameter
                {
                    ParameterName = "V_RETURN",
                    DbType = DbType.String,
                    Direction = ParameterDirection.Output,
                    Size = 4000
                };

                string sql = "PKG_BUSINESS_GAS@GET_RETURN_GAS";

                using (var command = Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(new SqlParameter[] { pDepartment, nReturn, vReturn });
                    Database.OpenConnection();

                    DbDataAdapter adapter = DbProviderFactories.GetFactory(Database.GetDbConnection()).CreateDataAdapter();
                    adapter.SelectCommand = command;

                    // Fill the DataTable.
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    _log4net.Info("GetGasInforReturn DB context: row count " + table.Rows.Count);

                    return new ResultDB()
                    {
                        N_RETURN = (int)nReturn.Value,
                        V_RETURN = (string)vReturn.Value,
                        Data = table
                    };
                }
            }
            catch (Exception ex)
            {
                _log4net.Info("GetGasInforReturn DB context: " + ex.Message);
                return new ResultDB()
                {
                    N_RETURN = -999,
                    V_RETURN = ex.Message
                };
            }
        }

        public ResultDB PutReturnGas(pReturnGasModel pReturnGasModel)
        {
            try
            {
                SqlParameter gasId = new SqlParameter("A_GAS_ID", pReturnGasModel.gas_id);
                SqlParameter deptCode = new SqlParameter("A_DEPT_CODE", pReturnGasModel.dept_code);
                SqlParameter userReturn = new SqlParameter("A_USER", pReturnGasModel.user_return);

                var nReturn = new SqlParameter
                {
                    ParameterName = "N_RETURN",
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output
                };
                var vReturn = new SqlParameter
                {
                    ParameterName = "V_RETURN",
                    DbType = System.Data.DbType.String,
                    Direction = System.Data.ParameterDirection.Output,
                    Size = 4000
                };

                string sql = "EXEC PKG_BUSINESS@PUT_RETURN_GAS @A_GAS_ID,@A_DEPT_CODE,@A_USER,@N_RETURN OUT,@V_RETURN OUT";
                var result = this.Database.ExecuteSqlRaw(sql, gasId, deptCode, userReturn, nReturn, vReturn);

                return new ResultDB()
                {
                    N_RETURN = (int)nReturn.Value,
                    V_RETURN = (string)vReturn.Value
                };
            }
            catch (Exception ex)
            {
                return new ResultDB()
                {
                    N_RETURN = -999,
                    V_RETURN = ex.Message
                };
            }

        }

        public ResultDB GetGasInventory(string deptCode)
        {
            try
            {
                SqlParameter pDepartment = new SqlParameter("A_DEPT", deptCode);

                var nReturn = new SqlParameter
                {
                    ParameterName = "N_RETURN",
                    DbType = DbType.Int32,
                    Direction = ParameterDirection.Output
                };

                var vReturn = new SqlParameter
                {
                    ParameterName = "V_RETURN",
                    DbType = DbType.String,
                    Direction = ParameterDirection.Output,
                    Size = 4000
                };

                string sql = "PKG_BUSINESS@GET_TONKHO_BY_DEPT";

                using (var command = Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(new SqlParameter[] { pDepartment, nReturn, vReturn });
                    Database.OpenConnection();

                    DbDataAdapter adapter = DbProviderFactories.GetFactory(Database.GetDbConnection()).CreateDataAdapter();
                    adapter.SelectCommand = command;

                    // Fill the DataTable.
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    _log4net.Info("GetGasInventory DB context: row count " + table.Rows.Count);

                    return new ResultDB()
                    {
                        N_RETURN = (int)nReturn.Value,
                        V_RETURN = (string)vReturn.Value,
                        Data = table
                    };
                }
            }
            catch (Exception ex)
            {
                _log4net.Info("GetGasInventory DB context: " + ex.Message);
                return new ResultDB()
                {
                    N_RETURN = -999,
                    V_RETURN = ex.Message
                };
            }
        }
        #endregion
    }
}
