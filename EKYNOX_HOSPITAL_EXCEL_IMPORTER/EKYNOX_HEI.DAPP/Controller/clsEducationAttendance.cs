using EKYNOX_HEI.CORE.Helpers;
using EKYNOX_HEI.CORE.Models.Institutions;
using EKYNOX_HEI.DATA.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EKYNOX_HEI.DAPP.Controller
{
    public class clsEducationAttendance
    {
        private readonly DatabaseContext context;

        public clsEducationAttendance(DatabaseContext _context)
        {
            context = _context;
        }

        public ReturnData<List<InstitutionsModel>> GetInstutionsList() 
        {
            var result = new ReturnData<List<InstitutionsModel>>();

            try
            {
                var list = context.Institutions.Select(i => new InstitutionsModel
                {
                    LogicalRef = i.LOGICALREF,
                    Code = i.CODE,
                    Name = i.NAME,
                    City = i.CITY,
                    Town = i.TOWN,
                    District = i.DISTRICT,
                    Address = i.ADDRESS
                }).ToList();

                result.Data = list;
            }
            catch (Exception ex)
            {
                result.Status = CORE.Enums.StatusEnum.Error;
                result.Message = ex.Message;
            }

            return result;
        }

        public ReturnData<DataTable> GetUsersList()
        {
            var result = new ReturnData<DataTable>();

            try
            {
                var datatable = new DataTable();
                datatable.Columns.Add("LogicalRef", typeof(int));
                datatable.Columns.Add("UserName", typeof(string));
                datatable.Columns.Add("FullName", typeof(string));

                var list = context.Users.Select(u => new
                {
                    LogicalRef = u.LOGICALREF,
                    UserName = u.USERNAME,
                    FullName = $@"{u.NAME} {u.SURNAME}"
                }).ToList();

                foreach (var user in list)
                {
                    datatable.Rows.Add(user.LogicalRef, user.UserName, user.FullName);
                }

                result.Data = datatable;
            }
            catch (Exception ex)
            {
                result.Status = CORE.Enums.StatusEnum.Error;
                result.Message = ex.Message;
            }

            return result;
        }
    }
}
