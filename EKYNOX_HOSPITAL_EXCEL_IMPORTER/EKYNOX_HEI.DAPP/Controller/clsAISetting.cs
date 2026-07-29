using AutoMapper;
using EKYNOX_HEI.CORE.Enums;
using EKYNOX_HEI.CORE.Helpers;
using EKYNOX_HEI.CORE.Models.AISetting;
using EKYNOX_HEI.DATA.Database;
using EKYNOX_HEI.DATA.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKYNOX_HEI.DAPP.Controller
{
    public class clsAISetting
    {

        private readonly DatabaseContext context;
        
        public clsAISetting(DatabaseContext _context) 
        {        
            this.context = _context;
        }

        public ReturnData<List<AISettingListViewModel>> GetAISettingList() 
        {
            var result = new ReturnData<List<AISettingListViewModel>>();

            try
            {
                var data = context.AISetting.Select(c => new AISettingListViewModel 
                {
                    Ai = c.AI,
                    ApiKey = c.APIKEY,
                    LogicalRef = c.LOGICALREF,
                    MethodName = c.METHODNAME,
                    UsingStatus = c.USINGSTATUS
                }).ToList();

                result.Data = data;
                result.Status = StatusEnum.Success;
            }
            catch (Exception ex)
            {
                result.Status = CORE.Enums.StatusEnum.Error;
                result.Message = ex.Message;
            }

            return result;
        }

        public ReturnData<AISettingModel> GetAISetting(int logicalRef) 
        {
            var result = new ReturnData<AISettingModel>();

            try
            {              
                var control = context.AISetting.Find(logicalRef);
                if (control is null)
                {
                    result.Status = StatusEnum.Warning;
                    result.Message = "Veri bulunamadı.";
                    return result;
                }

                var detail = context.AISettingDetail.Where(c => c.AISETTINGREF == logicalRef).Select(c => new AISettingListModel 
                {
                    AiModelDesc = c.AIMODELDESC,
                    AiModelName = c.AIMODELNAME,
                    AISettingRef = c.AISETTINGREF,
                    LogicalRef = c.LOGICALREF,
                    LineNr = c.LINENR
                }).ToList();

                var mainData = new Mapper(new MapperConfiguration(c => { c.CreateMap<AISetting, AISettingModel>(); }, NullLoggerFactory.Instance)).Map<AISettingModel>(control);
                mainData.Detail = detail;

                result.Data = mainData;
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
                var control = context.AISetting.Find(id);
                if (control is null)
                {
                    result.Status = StatusEnum.Warning;
                    result.Message = "Veri bulunamadı.";
                    return result;
                }

                using (var transaction = context.Database.BeginTransaction())
                {
                    context.AISetting.Remove(control);
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
            }

            return result;
        }

        public ReturnData<string> CreateAINo()
        {
            var result = new ReturnData<string>();

            try
            {
                var lastAISetting = context.AISetting.OrderByDescending(i => i.LOGICALREF).FirstOrDefault();
                var template = $"AI{DateTime.Now.Year}.{(int.Parse((lastAISetting?.AINO.Split('.').LastOrDefault() ?? "0")) + 1).ToString().PadLeft(6, '0')}";
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

        public ReturnData<bool> Save(AISettingModel data) 
        {
            var result = new ReturnData<bool>();

            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var cfg = new MapperConfiguration(c => c.CreateMap<AISettingModel, AISetting>(), NullLoggerFactory.Instance);

                    var main = new Mapper(cfg).Map<AISetting>(data);
                    context.AISetting.Add(main);
                    context.SaveChanges();

                    data.Detail.ForEach(c => c.AISettingRef = main.LOGICALREF);
                    var detail = new Mapper(new MapperConfiguration(c => c.CreateMap<AISettingListModel, AISettingDetail>(), NullLoggerFactory.Instance)).Map<List<AISettingDetail>>(data.Detail);
                    context.AISettingDetail.AddRange(detail);
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

        public ReturnData<bool> Update(AISettingModel data)
        {
            var result = new ReturnData<bool>();

            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var mainData = context.AISetting.Find(data.LogicalRef);
                    if (mainData is null)
                    {
                        result.Status = StatusEnum.Warning;
                        result.Message = "Veri bulunamadı";
                        return result;
                    }                   

                    var main = new Mapper(new MapperConfiguration(c => c.CreateMap<AISettingModel, AISetting>(), NullLoggerFactory.Instance)).Map(data, mainData);
                    context.AISetting.Update(main);
                    context.Entry(main).Property(x => x.CREATEDUSER).IsModified = false;
                    context.Entry(main).Property(x => x.CREATEDATE).IsModified = false;
                    context.SaveChanges();

                    data.Detail.ForEach(c => c.AISettingRef = main.LOGICALREF);

                    context.AISettingDetail.RemoveRange(context.AISettingDetail.Where(c => c.AISETTINGREF == main.LOGICALREF));
                    context.SaveChanges();

                    var detailMap = new Mapper(new MapperConfiguration(c => c.CreateMap<AISettingListModel, AISettingDetail>(), NullLoggerFactory.Instance)).Map<List<AISettingDetail>>(data.Detail);
                    context.AISettingDetail.AddRange(detailMap.Where(c => c.LOGICALREF > 0));
                    context.SaveChanges();


                    context.AISettingDetail.AddRange(detailMap.Where(c => c.LOGICALREF <= 0));
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
