using EKYNOX_HEI.DATA.DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKYNOX_HEI.DATA.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {

        }

        public DbSet<Institutions> Institutions { get; set; }
        public DbSet<EducationAttendance> EducationAttendance { get; set; }
        public DbSet<Institutions> Users { get; set; }

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
                entity.Property(e => e.EDUCATIONFULLNAME).HasMaxLength(150);
                entity.Property(e => e.FILENAME).HasMaxLength(150);
                entity.Property(e => e.FILEPATH).HasMaxLength(300);
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
        }
    }
}
