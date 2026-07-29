using EKYNOX_HEI.DATA.DataModel;
using EKYNOX_HEI.DATA.DataModel.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKYNOX_HEI.DATA.Database
{
    public class DatabaseContext : DbContext
    {
        private readonly UserInfoSet userInfo;

        public DatabaseContext(DbContextOptions<DatabaseContext> options, UserInfoSet _userInfo)
            : base(options)
        {
            userInfo = _userInfo;
        }

        public DbSet<Institutions> Institutions { get; set; }
        public DbSet<EducationAttendance> EducationAttendance { get; set; }
        public DbSet<EducationAttendanceDetail> EducationAttendanceDetail { get; set; }
        public DbSet<EducationAttendanceFileRead> EducationAttendanceFileRead { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<AISetting> AISetting { get; set; }
        public DbSet<AISettingDetail> AISettingDetail { get; set; }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added)
                {
                    
                }

                var type = entry.Entity.GetType();

                if (type == typeof(Institutions))
                {
                    if (((Institutions)entry.Entity).LOGICALREF <= 0)
                    {
                        ((Institutions)entry.Entity).CREATEDUSER = userInfo.LogicalRef;
                        ((Institutions)entry.Entity).CREATEDATE = DateTime.Now;
                    }
                    else
                    {
                        ((Institutions)entry.Entity).MODIFIEDUSER = userInfo.LogicalRef;
                        ((Institutions)entry.Entity).MODIFIEDDATE = DateTime.Now;
                    }
                }

                if (type == typeof(EducationAttendance))
                {
                    if (((EducationAttendance)entry.Entity).LOGICALREF <= 0)
                    {
                        ((EducationAttendance)entry.Entity).CREATEDUSER = userInfo.LogicalRef;
                        ((EducationAttendance)entry.Entity).CREATEDATE = DateTime.Now;
                    }
                    else
                    {
                        ((EducationAttendance)entry.Entity).MODIFIEDUSER = userInfo.LogicalRef;
                        ((EducationAttendance)entry.Entity).MODIFIEDDATE = DateTime.Now;
                    }
                }

                if (type == typeof(EducationAttendanceDetail))
                {
                    if (((EducationAttendanceDetail)entry.Entity).LOGICALREF <= 0)
                    {
                        ((EducationAttendanceDetail)entry.Entity).CREATEDUSER = userInfo.LogicalRef;
                        ((EducationAttendanceDetail)entry.Entity).CREATEDATE = DateTime.Now;
                    }
                    else
                    {
                        ((EducationAttendanceDetail)entry.Entity).MODIFIEDUSER = userInfo.LogicalRef;
                        ((EducationAttendanceDetail)entry.Entity).MODIFIEDDATE = DateTime.Now;
                    }
                }

                if (type == typeof(EducationAttendanceFileRead))
                {
                    if (((EducationAttendanceFileRead)entry.Entity).LOGICALREF <= 0)
                    {
                        ((EducationAttendanceFileRead)entry.Entity).CREATEDUSER = userInfo.LogicalRef;
                        ((EducationAttendanceFileRead)entry.Entity).CREATEDATE = DateTime.Now;
                    }
                    else
                    {
                        ((EducationAttendanceFileRead)entry.Entity).MODIFIEDUSER = userInfo.LogicalRef;
                        ((EducationAttendanceFileRead)entry.Entity).MODIFIEDDATE = DateTime.Now;
                    }
                }

                if (type == typeof(AISetting))
                {
                    if (((AISetting)entry.Entity).LOGICALREF <= 0)
                    {
                        ((AISetting)entry.Entity).CREATEDUSER = userInfo.LogicalRef;
                        ((AISetting)entry.Entity).CREATEDATE = DateTime.Now;
                    }
                    else
                    {
                        ((AISetting)entry.Entity).MODIFIEDUSER = userInfo.LogicalRef;
                        ((AISetting)entry.Entity).MODIFIEDDATE = DateTime.Now;
                    }
                }

                if (type == typeof(AISettingDetail))
                {
                    if (((AISettingDetail)entry.Entity).LOGICALREF <= 0)
                    {
                        ((AISettingDetail)entry.Entity).CREATEDUSER = userInfo.LogicalRef;
                        ((AISettingDetail)entry.Entity).CREATEDATE = DateTime.Now;
                    }
                    else
                    {
                        ((AISettingDetail)entry.Entity).MODIFIEDUSER = userInfo.LogicalRef;
                        ((AISettingDetail)entry.Entity).MODIFIEDDATE = DateTime.Now;
                    }
                }
            }
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Institutions
            modelBuilder.Entity<Institutions>
                        (entity =>
                        {
                            entity.HasKey(e => e.LOGICALREF);
                            entity.Property(e => e.CODE).HasMaxLength(51);
                            entity.Property(e => e.NAME).HasMaxLength(150);
                            entity.Property(e => e.CITY).HasMaxLength(100);
                            entity.Property(e => e.TOWN).HasMaxLength(100);
                            entity.Property(e => e.DISTRICT).HasMaxLength(100);
                            entity.Property(e => e.ADDRESS).HasMaxLength(700);
                        });
            #endregion

            #region EducationAttendance
            modelBuilder.Entity<EducationAttendance>
            (entity =>
            {
                entity.HasKey(e => e.LOGICALREF);
                entity.Property(e => e.DOCNO).HasMaxLength(100);
            });
            #endregion

            #region EducationAttendanceDetail
            modelBuilder.Entity<EducationAttendanceDetail>
            (entity =>
            {
                entity.HasKey(e => e.LOGICALREF);
                entity.Property(e => e.FILENAME).HasMaxLength(150);
                entity.Property(e => e.FILEPATH).HasMaxLength(300);
            });
            #endregion

            #region EducationAttendanceFileRead
            modelBuilder.Entity<EducationAttendanceFileRead>
            (entity =>
            {
                entity.HasKey(e => e.LOGICALREF);
                entity.Property(e => e.NAME).HasMaxLength(150);
                entity.Property(e => e.SURNAME).HasMaxLength(300);
            });
            #endregion

            #region Users
            modelBuilder.Entity<Users>
            (entity =>
            {
                entity.HasKey(e => e.LOGICALREF);
                entity.Property(e => e.NAME).HasMaxLength(150);
                entity.Property(e => e.USERNAME).HasMaxLength(150);
                entity.Property(e => e.SURNAME).HasMaxLength(150);
                entity.Property(e => e.PASSWORD).HasMaxLength(300);
                entity.Property(e => e.EMAIL).HasMaxLength(150);
                entity.Property(e => e.PHONE).HasMaxLength(20);
            });
            #endregion

            #region AISetting
            modelBuilder.Entity<AISetting>
            (entity =>
            {
                entity.HasKey(e => e.LOGICALREF);
                entity.Property(e => e.APIKEY).HasMaxLength(300);
                entity.Property(e => e.METHODNAME).HasMaxLength(150);
                entity.Property(e => e.ENDPOINT).HasMaxLength(150);
                entity.Property(e => e.AINO).HasMaxLength(51);
            });
            #endregion

            #region AISettingDetail
            modelBuilder.Entity<AISettingDetail>
            (entity =>
            {
                entity.HasKey(e => e.LOGICALREF);
                entity.Property(e => e.AIMODELNAME).HasMaxLength(150);
                entity.Property(e => e.AIMODELDESC).HasMaxLength(150);                
            });
            #endregion
        }
    }
}
