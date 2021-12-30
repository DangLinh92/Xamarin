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
    }
}
