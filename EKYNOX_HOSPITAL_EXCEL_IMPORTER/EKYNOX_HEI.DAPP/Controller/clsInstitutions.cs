using EKYNOX_HEI.CORE.Enums;
using EKYNOX_HEI.CORE.Helpers;
using EKYNOX_HEI.CORE.Models.Institutions;
using EKYNOX_HEI.DATA.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKYNOX_HEI.DAPP.Controller
{
    public class clsInstitutions
    {
        private readonly DatabaseContext context;

        public clsInstitutions(DatabaseContext _context)
        {
            context = _context;
        }

        public ReturnData<string> CreateInstutionNo()
        {
            var result = new ReturnData<string>();

            try
            {
                var lastInstitution = context.Institutions.OrderByDescending(i => i.LOGICALREF).FirstOrDefault();
                var template = $"INS{DateTime.Now.Year}.{(int.Parse((lastInstitution?.CODE.Split('.').LastOrDefault() ?? "0")) + 1).ToString().PadLeft(6, '0')}";
                result.Status = StatusEnum.Success;
                result.Data = template;
            }
            catch (Exception ex)
            {
                result.Status = StatusEnum.Error;
                result.Message = ex.Message;
            }

            return result;
        }

        public ReturnData<List<InstitutionsViewModel>> GetAllInstitutions()
        {
            var result = new ReturnData<List<InstitutionsViewModel>>();
            try
            {
                var institutions = context.Institutions.Select(i => new InstitutionsViewModel
                {
                    LogicalRef = i.LOGICALREF,
                    Code = i.CODE,
                    Name = i.NAME,
                    City = i.CITY,
                    Town = i.TOWN,
                    District = i.DISTRICT,
                    Address = i.ADDRESS
                }).ToList();
                result.Status = StatusEnum.Success;
                result.Data = institutions;
            }
            catch (Exception ex)
            {
                result.Status = StatusEnum.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        public ReturnData<bool> AddInstitution(InstitutionsModel institution)
        {
            var result = new ReturnData<bool>();
            try
            {

                using (var transaction = context.Database.BeginTransaction())
                {
                    var newInstitution = new DATA.DataModel.Institutions
                    {
                        CODE = institution.Code,
                        NAME = institution.Name,
                        CITY = institution.City,
                        TOWN = institution.Town,
                        DISTRICT = institution.District,
                        ADDRESS = institution.Address
                    };
                    context.Institutions.Add(newInstitution);
                    context.SaveChanges();

                    transaction.Commit();
                }

                result.Status = StatusEnum.Success;
                result.Data = true;
            }
            catch (Exception ex)
            {
                result.Status = StatusEnum.Error;
                result.Message = ex.Message;
                result.Data = false;
            }
            return result;
        }

        public ReturnData<bool> UpdateInstitution(InstitutionsModel institution)
        {
            var result = new ReturnData<bool>();
            try
            {
                var existingInstitution = context.Institutions.Find(institution.LogicalRef);
                if (existingInstitution != null)
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        existingInstitution.CODE = institution.Code;
                        existingInstitution.NAME = institution.Name;
                        existingInstitution.CITY = institution.City;
                        existingInstitution.TOWN = institution.Town;
                        existingInstitution.DISTRICT = institution.District;
                        existingInstitution.ADDRESS = institution.Address;
                        context.SaveChanges();
                        result.Status = StatusEnum.Success;
                        result.Data = true;

                        transaction.Commit();
                    }
                }
                else
                {
                    result.Status = StatusEnum.Error;
                    result.Message = "Kurum Bulunamadı.";
                    result.Data = false;
                }
            }
            catch (Exception ex)
            {
                result.Status = StatusEnum.Error;
                result.Message = ex.Message;
                result.Data = false;
            }
            return result;
        }

        public ReturnData<bool> DeleteInstitution(int logicalRef)
        {
            var result = new ReturnData<bool>();
            try
            {
                var educationAttendanceRecords = context.EducationAttendance.Where(e => e.INSTUTIONREF == logicalRef).ToList();

                if (educationAttendanceRecords.Any())
                {
                    result.Status = StatusEnum.Warning;
                    result.Message = "Kurum silinemez. Bu kuruma ait eğitim katılım kayıtları bulunmaktadır.";
                    result.Data = false;
                    return result;
                }

                var existingInstitution = context.Institutions.Find(logicalRef);
                if (existingInstitution != null)
                {

                    using (var transaction = context.Database.BeginTransaction())
                    {
                        context.Institutions.Remove(existingInstitution);
                        context.SaveChanges();

                        transaction.Commit();
                    }

                    result.Status = StatusEnum.Success;
                    result.Data = true;
                }
                else
                {
                    result.Status = StatusEnum.Warning;
                    result.Message = "Kurum Bulunamadı.";
                    result.Data = false;
                }
            }
            catch (Exception ex)
            {
                result.Status = StatusEnum.Error;
                result.Message = ex.Message;
                result.Data = false;
            }
            return result;
        }
    }
}
