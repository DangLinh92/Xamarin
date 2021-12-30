using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SmartGasAPI.Models;
using SmartGasAPI.Models.MRO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGasAPI.Data
{

    public class SmartGas_MRO_DBcontext : SmartGasContext
    {
        public SmartGas_MRO_DBcontext(DbContextOptions<SmartGas_MRO_DBcontext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<InOutInfo> InOutInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(new string[] { "PLANT", "DEPARTMENT", "USER_ID" });
            modelBuilder.Entity<InOutInfo>().HasKey(new string[] { "ID"});
        }

        public override DbSet<User> GetUser()
        {
            return Users;
        }

        #region Nhap Xuat Kho

        public ResultDB PutInStock(pInOutInfo pInOutModel)
        {
            try
            {
                SqlParameter sparepartCode = new SqlParameter("A_CODE", pInOutModel.spare_part_code);
                SqlParameter qty = new SqlParameter("A_LOT_QUANTITY", pInOutModel.quantity);
                SqlParameter qtyPerStock = new SqlParameter("A_QUANTITY_PER_STOCK", pInOutModel.quantity_per_stock);
                SqlParameter exprired_date = new SqlParameter("A_EXP_DATE", pInOutModel.exprired_date);
                SqlParameter user_create = new SqlParameter("A_TRAN_USER", pInOutModel.user_create);
                SqlParameter department = new SqlParameter("A_DEPARTMENT", pInOutModel.department);

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
                    Size=4000
                };

                string sql = "EXEC PKG_BUSINESS_GOODS_RECEIPT_ISSUE_TYPE_DATA_TABLE_PUT_MOBILE@PUT_IN @A_CODE,@A_LOT_QUANTITY,@A_QUANTITY_PER_STOCK,@A_EXP_DATE,@A_TRAN_USER,@A_DEPARTMENT,@N_RETURN OUT,@V_RETURN OUT";
                var result = this.Database.ExecuteSqlRaw(sql, sparepartCode, qty, qtyPerStock,exprired_date,user_create,department, nReturn, vReturn);
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

        public ResultDB PutOutStock(pInOutInfo pInOutModel)
        {
            try
            {
                SqlParameter lotNo = new SqlParameter("A_LOTNO", pInOutModel.lot_no);
                SqlParameter dateIn = new SqlParameter("A_DATE_IN", pInOutModel.create_date);
                SqlParameter code = new SqlParameter("A_CODE", pInOutModel.spare_part_code);
                SqlParameter qty = new SqlParameter("A_QUANTITY", pInOutModel.quantity);
                SqlParameter tran_user = new SqlParameter("A_TRAN_USER", pInOutModel.tran_user);
                SqlParameter user_create = new SqlParameter("A_NGUOI_THAO_TAC", pInOutModel.nguoi_thao_tac);
                SqlParameter department = new SqlParameter("A_DEPARTMENT", pInOutModel.department);

                var nReturn = new SqlParameter
                {
                    ParameterName = "N_RETURN",
                    DbType = System.Data.DbType.Int64,
                    Direction = System.Data.ParameterDirection.Output
                };
                var vReturn = new SqlParameter
                {
                    ParameterName = "V_RETURN",
                    DbType = System.Data.DbType.String,
                    Direction = System.Data.ParameterDirection.Output
                };

                string sql = "EXEC PKG_BUSINESS_GOODS_RECEIPT_ISSUE_TYPE_DATA_TABLE_PUT_MOBILE@PUT_OUT @A_LOTNO,@A_DATE_IN,@A_CODE,@A_QUANTITY,@A_TRAN_USER,@A_DEPARTMENT,@A_NGUOI_THAO_TAC,@N_RETURN OUT,@V_RETURN OUT";
                var result = this.Database.ExecuteSqlRaw(sql, lotNo,dateIn,code,qty,tran_user,department,user_create, nReturn, vReturn);
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

                    _log4net.Info("GetPutInHistory DB MRO context: row count " + table.Rows.Count);

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
                _log4net.Info("GetPutInHistory DB MRO context: " + ex.Message);
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
