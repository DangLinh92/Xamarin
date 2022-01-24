using log4net.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SmartGasAPI.Common;
using SmartGasAPI.Data;
using SmartGasAPI.Models;
using SmartGasAPI.Models.SPAREPART;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGasAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class InStockController : BaseApiController<InStockController>
    {
        private SmartGas_MRO_DBcontext _mroDBContext;
        private SmartGas_SP_DBContext _spDBContext;
        public InStockController(SmartGas_MRO_DBcontext mroContext, SmartGas_SP_DBContext spContext)
        {
            _mroDBContext = mroContext;
            _spDBContext = spContext;
        }

        [HttpPost]
        public IActionResult Post([FromBody] pInOutInfo pInOutModel)
        {
            _log4net.Info("PARAM: " + pInOutModel.spare_part_code);
            var context = InstanceDB.context(pInOutModel.department, _spDBContext, _mroDBContext) as SmartGas_MRO_DBcontext;

            ResultDB resultDB = null;
            if (context == null) /*Spare part*/
            {
                pInOutModel.stock_code = Constants.GetStockCode(pInOutModel.department);
                resultDB = _spDBContext.PutStock(pInOutModel);

                _log4net.Error("spare part: " + resultDB.V_RETURN);
            }
            else
            {
                if (pInOutModel.in_out == "IN")
                {
                    resultDB = _mroDBContext.PutInStock(pInOutModel);

                    _log4net.Error("mro in: " + resultDB.V_RETURN);
                }
                else
                {
                    resultDB = _mroDBContext.PutOutStock(pInOutModel);

                    _log4net.Error("mro out: " + resultDB.V_RETURN);
                }
            }

            if (resultDB.N_RETURN == 0)
            {
                return StatusCode(StatusCodes.Status200OK);
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Unit>> GetUnits(string department)
        {
            try
            {
                var context = InstanceDB.context(department, _spDBContext, _mroDBContext) as SmartGas_MRO_DBcontext;
                if (context == null) /*Spare part*/
                {
                    return _spDBContext.Units.Select(x => x).ToList();
                }

                return new List<Unit>();
            }
            catch (Exception)
            {
                return new List<Unit>();
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Unit>> GetUnitBySparepart(string department, string code)
        {
            try
            {
                _log4net.Info("GetUnitBySparepart: " + department + ":" + code);
                var context = InstanceDB.context(department, _spDBContext, _mroDBContext) as SmartGas_MRO_DBcontext;
                if (context == null) /*Spare part*/
                {
                    ResultDB resultDB = _spDBContext.GetUnitBySparepart(department, code);

                    if (resultDB.N_RETURN == 0)
                    {
                        return Helper.ConvertDataTable<Unit>((DataTable)resultDB.Data);
                    }
                }

                return new List<Unit>();
            }
            catch (Exception)
            {
                return new List<Unit>();
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<PartInfo>> GetNameBySparepart(string department, string code)
        {
            try
            {
                _log4net.Info("GetNameBySparepart: " + department + ":" + code);
                var context = InstanceDB.context(department, _spDBContext, _mroDBContext) as SmartGas_MRO_DBcontext;
                if (context == null) /*Spare part*/
                {
                    ResultDB resultDB = _spDBContext.GetNameBySparepart(department, code);

                    if (resultDB.N_RETURN == 0)
                    {
                        return Helper.ConvertDataTable<PartInfo>((DataTable)resultDB.Data);
                    }
                }

                return new List<PartInfo>();
            }
            catch (Exception)
            {
                return new List<PartInfo>();
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> GetSparepartByEncript(string department, string code)
        {
            try
            {
                _log4net.Info("GetSparepartByEncript: " + department + ":" + code);
                var context = InstanceDB.context(department, _spDBContext, _mroDBContext) as SmartGas_MRO_DBcontext;
                if (context == null) /*Spare part*/
                {
                    ResultDB resultDB = _spDBContext.GetSparepartCodeByEnCript(department, code);

                    if (resultDB.N_RETURN == 0)
                    {
                        return Helper.ConvertDataTable<string>((DataTable)resultDB.Data);
                    }
                }

                return new List<string>();
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<PutInHistory>> GetPutInHistory(string department)
        {
            try
            {
                _log4net.Info("GetPutInHistory: " + department);
                var context = InstanceDB.context(department, _spDBContext, _mroDBContext) as SmartGas_MRO_DBcontext;
                if (context == null) /*Spare part*/
                {
                    ResultDB resultDB = _spDBContext.GetPutInHistory(department);

                    if (resultDB.N_RETURN == 0)
                    {
                        foreach (DataColumn col in ((DataTable)resultDB.Data).Columns)
                        {
                            _log4net.Info("GetPutInHistory: " + col.ColumnName + ":" + col.DataType.Name);
                        }
                        foreach (DataRow row in ((DataTable)resultDB.Data).Rows)
                        {
                            _log4net.Info("GetPutInHistory: " + row["SPARE_PART_CODE"]);
                            _log4net.Info("GetPutInHistory: " + row["NAME"]);
                            _log4net.Info("GetPutInHistory: " + row["QUANTITY"]);
                            _log4net.Info("GetPutInHistory: " + row["UNIT"]);
                            _log4net.Info("GetPutInHistory: " + row["CREATE_DATE"]);
                        }

                        List<PutInHistory> lst = Helper.ConvertDataTable<PutInHistory>((DataTable)resultDB.Data);
                        foreach (var item in lst)
                        {
                            _log4net.Info("GetPutInHistory: " + item.SPARE_PART_CODE);
                            _log4net.Info("GetPutInHistory: " + item.NAME);
                            _log4net.Info("GetPutInHistory: " + item.QUANTITY);
                            _log4net.Info("GetPutInHistory: " + item.UNIT);
                            _log4net.Info("GetPutInHistory: " + item.CREATE_DATE);
                        }
                        return lst;
                    }
                }
                else
                {
                    ResultDB resultDB = _mroDBContext.GetPutInHistory(department);

                    if (resultDB.N_RETURN == 0)
                    {
                        return Helper.ConvertDataTable<PutInHistory>((DataTable)resultDB.Data);
                    }
                }

                return new List<PutInHistory>();
            }
            catch (Exception)
            {
                return new List<PutInHistory>();
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<PutOutHistory>> GetPutOutHistory(string department)
        {
            try
            {
                _log4net.Info("GetPutOutHistory: " + department);
                var context = InstanceDB.context(department, _spDBContext, _mroDBContext) as SmartGas_MRO_DBcontext;
                if (context == null) /*Spare part*/
                {
                    ResultDB resultDB = _spDBContext.GetPutOutHistory(department);

                    if (resultDB.N_RETURN == 0)
                    {
                        List<PutOutHistory> lst = Helper.ConvertDataTable<PutOutHistory>((DataTable)resultDB.Data);
                        return lst;
                    }
                }
                else
                {
                    ResultDB resultDB = _mroDBContext.GetPutOutHistory(department);

                    if (resultDB.N_RETURN == 0)
                    {
                        return Helper.ConvertDataTable<PutOutHistory>((DataTable)resultDB.Data);
                    }
                }

                return new List<PutOutHistory>();
            }
            catch (Exception)
            {
                return new List<PutOutHistory>();
            }
        }

        [HttpGet]
        public ActionResult<string> GetGasInforInSparepart(string department, string gasId)
        {
            try
            {
                _log4net.Info("GetGasInforInSparepart: " + department);
                var context = InstanceDB.context(department, _spDBContext, _mroDBContext) as SmartGas_MRO_DBcontext;

                if (context == null) /*Spare part*/
                {
                    ResultDB resultDB = _spDBContext.GetGasInforInSparepart(department, gasId);

                    if (resultDB.N_RETURN == 0)
                    {
                        string result = ((DataTable)resultDB.Data).Rows[0][0] + "";
                        return result;
                    }
                }
                else
                {
                    ResultDB resultDB = _mroDBContext.GetGasInforInMro(department, gasId);

                    if (resultDB.N_RETURN == 0)
                    {
                        return ((DataTable)resultDB.Data).Rows[0][0] + "";
                    }
                }

                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<pReturnGasModel>> GetGasInforReturn(string department)
        {
            try
            {
                _log4net.Info("GetGasInforReturn: " + department);
                var context = InstanceDB.context(department, _spDBContext, _mroDBContext) as SmartGas_MRO_DBcontext;

                if (context == null) /*Spare part*/
                {
                    ResultDB resultDB = _spDBContext.GetGasInforReturn(department);

                    if (resultDB.N_RETURN == 0)
                    {
                        return Helper.ConvertDataTable<pReturnGasModel>((DataTable)resultDB.Data);
                    }
                }
                else
                {
                    ResultDB resultDB = _mroDBContext.GetGasInforReturn(department);

                    if (resultDB.N_RETURN == 0)
                    {
                        return Helper.ConvertDataTable<pReturnGasModel>((DataTable)resultDB.Data);
                    }
                }

                return new List<pReturnGasModel>();
            }
            catch (Exception)
            {
                return new List<pReturnGasModel>();
            }
        }

        [HttpPost]
        public IActionResult PutGasInforReturn([FromBody] pReturnGasModel pReturnGasModel)
        {
            _log4net.Info("PARAM: " + pReturnGasModel.gas_id);

            var context = InstanceDB.context(pReturnGasModel.dept_code, _spDBContext, _mroDBContext) as SmartGas_MRO_DBcontext;

            ResultDB resultDB = null;

            if (context == null) /*Spare part*/
            {
                resultDB = _spDBContext.PutReturnGas(pReturnGasModel);
                _log4net.Error("PutGasInforReturn: " + resultDB.V_RETURN);
            }
            else
            {
                resultDB = _mroDBContext.PutReturnGas(pReturnGasModel);
                _log4net.Error("mro PutGasInforReturn: " + resultDB.V_RETURN);
            }

            if (resultDB.N_RETURN == 0)
            {
                return StatusCode(StatusCodes.Status200OK);
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet]
        public ActionResult<IEnumerable<InventoryGasModel>> GetGasInventory(string department)
        {
            try
            {
                _log4net.Info("GetGasInforReturn: " + department);
                var context = InstanceDB.context(department, _spDBContext, _mroDBContext) as SmartGas_MRO_DBcontext;

                if (context == null) /*Spare part*/
                {
                    ResultDB resultDB = _spDBContext.GetGasInventory(department);

                    _log4net.Info("GetGasInforReturn: N_RETURN" + resultDB.N_RETURN);
                    if (resultDB.N_RETURN == 0)
                    {
                        _log4net.Info("GetGasInforReturn: " + ((DataTable)resultDB.Data).Rows[0][0]);
                        List<InventoryGasModel> lstGas = Helper.ConvertDataTable<InventoryGasModel>((DataTable)resultDB.Data);
                        _log4net.Info("GetGasInforReturn: " + lstGas.Count);
                        return lstGas;
                    }
                }
                else
                {
                    ResultDB resultDB = _mroDBContext.GetGasInventory(department);

                    if (resultDB.N_RETURN == 0)
                    {
                        return Helper.ConvertDataTable<InventoryGasModel>((DataTable)resultDB.Data);
                    }
                }

                return new List<InventoryGasModel>();
            }
            catch (Exception ex)
            {
                _log4net.Info("GetGasInforReturn: ex" + ex.Message);
                return new List<InventoryGasModel>();
            }
        }

    }
}
