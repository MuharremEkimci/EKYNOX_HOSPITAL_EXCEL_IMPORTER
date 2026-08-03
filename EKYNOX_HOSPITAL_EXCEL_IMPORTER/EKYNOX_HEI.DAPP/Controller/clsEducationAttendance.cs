using AutoMapper;
using EKYNOX_HEI.CORE.Enums;
using EKYNOX_HEI.CORE.Helpers;
using EKYNOX_HEI.CORE.Models.AISetting;
using EKYNOX_HEI.CORE.Models.EducationAttendance;
using EKYNOX_HEI.CORE.Models.Institutions;
using EKYNOX_HEI.DATA.Database;
using EKYNOX_HEI.DATA.DataModel;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Management;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace EKYNOX_HEI.DAPP.Controller
{
    public class clsEducationAttendance
    {
        private readonly DatabaseContext context;

        public clsEducationAttendance(DatabaseContext _context)
        {
            context = _context;
        }

        public ReturnData<List<EducationAttendanceListViewModel>> GetEducationAttendanceList() 
        {
            var result = new ReturnData<List<EducationAttendanceListViewModel>>();

            try
            {
                var educationAttendace = context.EducationAttendance.ToList();
                var users = context.Users.ToList();
                var institutions = context.Institutions.ToList();

                var joinData = educationAttendace.Join
                               (
                                   users,
                                   educAtt => educAtt.EDUCATORREF,
                                   user => user.LOGICALREF,
                                   (educAtt, user) => new { educAtt, user }
                               )
                               .Join
                               (
                                   institutions,
                                   educInf => educInf.educAtt.INSTUTIONREF,
                                   institution => institution.LOGICALREF,
                                   (educInf, institution) => new { educInf, institution }
                               ).Select(c => new EducationAttendanceListViewModel 
                               {
                                    LogicalRef = c.educInf.educAtt.LOGICALREF,
                                    CreatedDate = c.educInf.educAtt.CREATEDATE,
                                    DocNo = c.educInf.educAtt.DOCNO,
                                    Educator = $@"{c.educInf.user.NAME} {c.educInf.user.SURNAME}",
                                    InstitutionCode = c.institution.CODE,
                                    InstitutionName = c.institution.NAME,
                                    ModifiedDate = c.educInf.educAtt.MODIFIEDDATE
                               }).ToList();

                result.Data = joinData;
                result.Status = StatusEnum.Success;

            }
            catch (Exception ex)
            {
                result.Status = StatusEnum.Error;
                result.Message = ex.Message;
            }

            return result;
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

        public async Task<ReturnData<List<EducationAttendanceDetailModel>>> GetImageReadAI(byte[] imageData, string fileMimeType) 
        {
            var result = new ReturnData<List<EducationAttendanceDetailModel>>();

            try
            {
                var aiList = context.AISetting.Where(c => c.USINGSTATUS == AIEnumUsingStatus.Using).ToList();
                if (!aiList.Any())
                {
                    result.Message = "Yapay zeka tanımı yapılmalıdır.";
                    result.Status = StatusEnum.Warning;
                    return result;
                }

                var aiDetail = context.AISettingDetail.ToList();

                var aiListDetail = aiList.ToList()
                                   .GroupJoin
                                   (
                                     aiDetail,
                                     ai => ai.LOGICALREF,
                                     aiDetail => aiDetail.AISETTINGREF,
                                     (ai, aiDetail) => new 
                                     {
                                         AI = ai, 
                                         AIDETAIL = new Mapper(new MapperConfiguration(c => c.CreateMap<AISettingDetail, AISettingListModel>(), NullLoggerFactory.Instance)).Map<List<AISettingListModel>>(aiDetail.DefaultIfEmpty().ToList()) 
                                     }
                                   );

                if (aiListDetail.Any(c => !c.AIDETAIL.Any()))
                {
                    result.Message = "Yapay zeka tanımlarında model girişleri yapılmalıdır.";
                    result.Status = StatusEnum.Warning;
                    return result;
                }

                string prompt = @"Sen profesyonel bir optik karakter tanıma (OCR) asistanısın. 
                                  Görseldeki el yazısı katılım listesini incele ve katılan kişilerin ad ve soyadlarını ayıkla.
                                  
                                  GÖREVLER VE KURALLAR:
                                  1. Görseldeki el yazılarını azami dikkatle oku ve doğru tahmin et.
                                  2. SADECE kişilerin İSİM ve SOYİSİMLERİNİ al. Tablodaki Birim (Hostes, Vezne vb.), Tarih, Döküman No ve İmza alanlarını KESİNLİKLE dahil etme.
                                  3. İsim ve soyisimleri Türkçe karakter kurallarına uygun olarak TÜMÜ BÜYÜK HARFLERLE yaz (Örn: İSMEK -> İSMEK, ı -> I).
                                  4. İsim ve soyisimi ayrıştırarak şablona yerleştir.
                                  
                                  HEDEF JSON ŞEMASI:
                                  {
                                    ""participants"": [
                                      {
                                        ""class_no"": 1,
                                        ""name"": ""MUSA"",
                                        ""surname"": ""TUNÇ""
                                      }
                                    ]
                                  }";

                var classType = typeof(AIHelper);
                var aihelper = new AIHelper();

                foreach (var aiInfo in aiListDetail)
                {
                    var detail = aiInfo.AIDETAIL.Where(c => c.UseInTheMethod).OrderBy(c => c.LineNr).ToList();

                    var data = new AiRequestData
                    {
                        AiModelNames = detail,
                        ImageBytes = imageData,
                        ImageMimeType = fileMimeType,
                        ApiKey = aiInfo.AI.APIKEY,
                        Prompt = prompt,
                        Endpoint = aiInfo.AI.ENDPOINT
                    };

                    var method = classType.GetMethod(aiInfo.AI.METHODNAME, BindingFlags.Instance | BindingFlags.Public);
                    if (method != null)
                    {                        
                        var res = method.Invoke(aihelper, new object[] { data, true, true} );
                        if (res is Task task)
                        {
                            await task;

                            var resultProperty = task.GetType().GetProperty("Result");
                            var ress = (ReturnData<string>)resultProperty?.GetValue(task);
                            if (ress.Status == StatusEnum.Error)
                                continue;

                            if (ress.Status == StatusEnum.Success)
                            {
                                var jsonContent = ress.Data;
                                if (aiInfo.AI.AI == AIEnum.Groq)
                                {
                                    string cleaned = Regex.Replace(ress.Data, @"<think>[\s\S]*?</think>", "").Trim().Replace("```json", "").Replace("```", "");
                                    jsonContent = cleaned;
                                }

                                var respData = JsonConvert.DeserializeObject<ImageAIResponseModel>(jsonContent);

                                result.Data = respData.participants.Select(c => new EducationAttendanceDetailModel
                                {
                                    ClassNo = c.class_no,
                                    Name = c.name,
                                    Surname = c.surname
                                }).ToList();
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Status = CORE.Enums.StatusEnum.Error;
                result.Message = ex.Message;
            }

            return result;
        }

        public ReturnData<EducationAttendanceModel> GetData(int id) 
        {
            var result = new ReturnData<EducationAttendanceModel>();

            try
            {
                var educationAttendance = context.EducationAttendance.Find(id);
                if (educationAttendance is null)
                {
                    result.Message = "Veri bulunamadı.";
                    result.Status = StatusEnum.Warning;
                    return result;
                }

                var educAtt = new Mapper(new MapperConfiguration(c => c.CreateMap<EducationAttendance, EducationAttendanceModel>(), NullLoggerFactory.Instance))
                              .Map<EducationAttendanceModel>(educationAttendance);

                var educationAttendanceDetail = new Mapper(new MapperConfiguration(c => c.CreateMap<EducationAttendanceDetail, EducationAttendanceListModel>(), NullLoggerFactory.Instance))
                                                .Map<List<EducationAttendanceListModel>>(context.EducationAttendanceDetail.Where(c => c.EDUCATIONATTENDANCEREF == id).ToList());

                var educationAttendanceFileRead = new Mapper(new MapperConfiguration(c => c.CreateMap<EducationAttendanceFileRead, EducationAttendanceDetailModel>(), NullLoggerFactory.Instance))
                                                  .Map<List<EducationAttendanceDetailModel>>(context.EducationAttendanceFileRead.Where(c => educationAttendanceDetail.Select(x => x.LogicalRef).Contains(c.EDUCATIONATTENDANCEDETAILREF)).ToList());

                var joinData = educationAttendanceDetail
                              .GroupJoin
                              (
                                  educationAttendanceFileRead, 
                                  educAtt => educAtt.LogicalRef,
                                  educAttFileRead => educAttFileRead.EducationAttendanceDetailRef,
                                  (educAtt, educAttFileRead) => 
                                  {
                                      educAtt.Detail = educAttFileRead.ToList();
                                      return educAtt;
                                  }
                              ).ToList();

                educAtt.ImagesDetailList = joinData;

            }
            catch (Exception ex)
            {
                result.Status = StatusEnum.Error;
                result.Message = ex.Message;
            }

            return result;
        }

        public ReturnData<List<EducationAttendanceExcelReadModel>> ReadExcel(ModuleTypeEnum moduleType, byte[] excelData)  
        {
            var result = new ReturnData<List<EducationAttendanceExcelReadModel>>();
            result.Data = new List<EducationAttendanceExcelReadModel>();
            try
            {
                if (excelData is not null)
                {
                    using (var ms = new MemoryStream(excelData))
                    using (var excelPackage = new ExcelPackage(ms))
                    {
                        var worksheet = excelPackage.Workbook.Worksheets[moduleType.GetHashCode()];

                        int rowCount = worksheet.Dimension.Rows;
                        int colCount = worksheet.Dimension.Columns;

                        for (int row = 1; row < rowCount; row++)
                        {
                            result.Data.Add(new EducationAttendanceExcelReadModel 
                            {
                                ClassNo = row,
                                Name = worksheet.Cells[row, 1].Value?.ToString()??"",
                                Surname = worksheet.Cells[row, 2].Value?.ToString() ?? "",
                            });           
                        }
                    }
                }                    
            }
            catch (Exception ex)
            {
                result.Status = StatusEnum.Error;
                result.Message = ex.Message;
            }

            return result;
        }

        public ReturnData<byte[]> WriteExcel(byte[] excelData, EducationAttendanceListModel fileReadInfo, string educator)
        {
            var result = new ReturnData<byte[]>();
            try
            {
                using (var ms = new MemoryStream(excelData))
                using (var excelPackage = new ExcelPackage(ms))
                {
                    var worksheet = excelPackage.Workbook.Worksheets[fileReadInfo.ModuleType.GetHashCode()];

                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    #region Kolon Kontrol
                    var targetDateCol = 0;
                    var targetEducatorCol = 0;
                    for (int i = 1; i <= colCount; i++)
                    {
                        string colName = fileReadInfo.EducationType == EducationTypeEnum.Education ? $"{i}. Eğitim Tarihi" : $"{i}. Simülasyon Tarihi";

                        if ((worksheet.Cells[1, i].Value?.ToString() ?? "").Contains(colName))
                        {
                            targetDateCol = i;
                            targetEducatorCol = i + 1;
                            break;
                        }
                    }

                    if (targetDateCol <= 0)
                    {
                        result.Status = StatusEnum.Warning;
                        result.Message = $"Excel listesi, {EnumHelper.GetDisplayName(fileReadInfo.ModuleType)} adlı modülde {fileReadInfo.EducationNumber} numaralı {(fileReadInfo.EducationType == EducationTypeEnum.Education ? "Eğitim" : "Simülasyon")} tarihi sütunu bulunamadı.";
                        result.Data = null;
                        return result;
                    }

                    #endregion

                    int writeRowCount = worksheet.Dimension.Rows + 1;
                    var excelInfo = new List<EducationAttendanceExcelReadModel>();

                    for (int row = 1; row <= rowCount; row++)
                    {
                        excelInfo.Add(new EducationAttendanceExcelReadModel
                        {
                            ClassNo = row,
                            Name = worksheet.Cells[row, 1].Value?.ToString() ?? "",
                            Surname = worksheet.Cells[row, 2].Value?.ToString() ?? "",
                        });
                    }

                    foreach (var detail in fileReadInfo.Detail)
                    {
                        var filter = excelInfo.FirstOrDefault(c => c.Name?.Trim() == detail.Name?.Trim() && c.Surname?.Trim() == detail.Surname?.Trim());

                        if (filter is not null)
                        {
                            worksheet.Cells[filter.ClassNo, targetDateCol].Value = fileReadInfo.EducationDate.ToString("dd.MM.yyyy");
                            if (fileReadInfo.EducationType == EducationTypeEnum.Education) 
                                worksheet.Cells[filter.ClassNo, targetEducatorCol].Value = educator;
                        }
                        else
                        {
                            worksheet.Cells[writeRowCount, 1].Value = detail.Name;
                            worksheet.Cells[writeRowCount, 2].Value = detail.Surname;
                            worksheet.Cells[writeRowCount, targetDateCol].Value = fileReadInfo.EducationDate.ToString("dd.MM.yyyy");
                            if (fileReadInfo.EducationType == EducationTypeEnum.Education)
                                worksheet.Cells[writeRowCount, targetEducatorCol].Value = educator;

                            writeRowCount++;
                        }
                    }

                    result.Data = excelPackage.GetAsByteArray();
                }
            }
            catch (Exception ex)
            {
                result.Status = StatusEnum.Error;
                result.Message = ex.Message;
            }

            return result;
        }

        public ReturnData<bool> Save(EducationAttendanceModel educationAttendance) 
        {
            var result = new ReturnData<bool>();

            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var educAtt = new Mapper(new MapperConfiguration(c => c.CreateMap<EducationAttendanceModel, EducationAttendance>(), NullLoggerFactory.Instance))
                                  .Map<EducationAttendance>(educationAttendance);

                    context.EducationAttendance.Add(educAtt);
                    context.SaveChanges();

                    foreach (var detail in educationAttendance.ImagesDetailList)
                    {
                        var educAttDetail = new Mapper(new MapperConfiguration(c => c.CreateMap<EducationAttendanceListModel, EducationAttendanceDetail>(), NullLoggerFactory.Instance))
                                             .Map<EducationAttendanceDetail>(detail);
                        educAttDetail.EDUCATIONATTENDANCEREF = educAtt.LOGICALREF;
                        context.EducationAttendanceDetail.Add(educAttDetail);
                        context.SaveChanges();

                        foreach (var fileRead in detail.Detail)
                        {
                            var educAttFileRead = new Mapper(new MapperConfiguration(c => c.CreateMap<EducationAttendanceDetailModel, EducationAttendanceFileRead>(), NullLoggerFactory.Instance))
                                                    .Map<EducationAttendanceFileRead>(fileRead);
                            educAttFileRead.EDUCATIONATTENDANCEDETAILREF = educAttDetail.LOGICALREF;
                            context.EducationAttendanceFileRead.Add(educAttFileRead);
                            context.SaveChanges();
                        }
                    }

                    result.Data = true;
                    result.Status = StatusEnum.Success;

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                result.Status = StatusEnum.Error;
                result.Message = ex.Message;
            }

            return result;
        }

        public ReturnData<bool> Update(EducationAttendanceModel educationAttendance) 
        {
            var result = new ReturnData<bool>();

            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var educAtt = context.EducationAttendance.Find(educationAttendance.LogicalRef);
                    if (educAtt == null)
                    {
                        result.Status = StatusEnum.Warning;
                        result.Message = "Veri bulunamadı.";
                        return result;
                    }

                    var educAttUpdate = new Mapper(new MapperConfiguration(c => c.CreateMap<EducationAttendanceModel, EducationAttendance>(), NullLoggerFactory.Instance))
                                         .Map(educationAttendance, educAtt);
                    context.Entry(educAttUpdate).Property(x => x.CREATEDUSER).IsModified = false;
                    context.Entry(educAttUpdate).Property(x => x.CREATEDATE).IsModified = false;
                    context.EducationAttendance.Update(educAttUpdate);
                    context.SaveChanges();

                    var educAttDetail = context.EducationAttendanceDetail.Where(c => c.EDUCATIONATTENDANCEREF == educAtt.LOGICALREF).ToList();
                    context.EducationAttendanceDetail.RemoveRange(educAttDetail);
                    context.SaveChanges();
                    context.EducationAttendanceFileRead.RemoveRange(context.EducationAttendanceFileRead.Where(c => educAttDetail.Select(c => c.LOGICALREF).Contains(c.EDUCATIONATTENDANCEDETAILREF)).ToList());
                    context.SaveChanges();

                    foreach (var detail in educationAttendance.ImagesDetailList)
                    {
                        var educAttDetailNew = new Mapper(new MapperConfiguration(c => c.CreateMap<EducationAttendanceListModel, EducationAttendanceDetail>(), NullLoggerFactory.Instance))
                                                .Map<EducationAttendanceDetail>(detail);
                        educAttDetailNew.EDUCATIONATTENDANCEREF = educAtt.LOGICALREF;
                        context.EducationAttendanceDetail.Add(educAttDetailNew);
                        context.SaveChanges();

                        foreach (var fileRead in detail.Detail)
                        {
                            var educAttFileReadNew = new Mapper(new MapperConfiguration(c => c.CreateMap<EducationAttendanceDetailModel, EducationAttendanceFileRead>(), NullLoggerFactory.Instance))
                                                        .Map<EducationAttendanceFileRead>(fileRead);
                            educAttFileReadNew.EDUCATIONATTENDANCEDETAILREF = educAttDetailNew.LOGICALREF;
                            context.EducationAttendanceFileRead.Add(educAttFileReadNew);
                            context.SaveChanges();
                        }
                    }

                    result.Data = true;
                    result.Status = StatusEnum.Success;

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                result.Status = StatusEnum.Error;
                result.Message = ex.Message;
            }

            return result;
        }

        public ReturnData<bool> Delete(int id) 
        {
            var result = new ReturnData<bool>();

            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var control = context.EducationAttendance.Find(id);
                    if (control is null)
                    {
                        result.Status = StatusEnum.Warning;
                        result.Message = "Veri bulunamadı.";
                        return result;
                    }

                    context.EducationAttendance.RemoveRange(control);
                    context.SaveChanges();

                    var educAttDetail = context.EducationAttendanceDetail.Where(c => c.EDUCATIONATTENDANCEREF == control.LOGICALREF).ToList();
                    context.EducationAttendanceDetail.RemoveRange(educAttDetail);
                    context.SaveChanges();
                    context.EducationAttendanceFileRead.RemoveRange(context.EducationAttendanceFileRead.Where(c => educAttDetail.Select(c => c.LOGICALREF).Contains(c.EDUCATIONATTENDANCEDETAILREF)).ToList());
                    context.SaveChanges();

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                result.Status = StatusEnum.Error;
                result.Message = ex.Message;
            }

            return result;
        }
    }
}
