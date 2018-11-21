using JXGIS.FZToponymy.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using JXGIS.FZToponymy.Models.Domain;

namespace JXGIS.FZToponymy.Models.Context
{
    public class EFDbContext : DbContext
    {
        public EFDbContext() : base((string)SystemUtils.Config.DbConStr)
        {
            this.Database.Initialize(false);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            string connectionString = (string)SystemUtils.Config.DbConStr;
            int indexOf = connectionString.IndexOf("USER ID", StringComparison.OrdinalIgnoreCase);
            string str = connectionString.Substring(indexOf);
            int startIndexOf = str.IndexOf("=", StringComparison.OrdinalIgnoreCase);
            int lastIndexOf = str.IndexOf(";", StringComparison.OrdinalIgnoreCase);
            string uid = str.Substring(startIndexOf + 1, lastIndexOf - startIndexOf - 1).Trim().ToUpper();
            modelBuilder.HasDefaultSchema(uid);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // 添加用户和角色的映射关系
            modelBuilder.Entity<SYSUSER>().HasMany(t => t.SYSROLEs).WithMany(t => t.SYSUSERs).Map(m => { m.MapLeftKey("USERID"); m.MapRightKey("ROLEID"); m.ToTable("USER_ROLE"); });

            // 添加用户和权限的映射关系
            modelBuilder.Entity<SYSUSER>().HasMany(t => t.SYSPRIVILIGEs).WithMany(t => t.SYSUSERs).Map(m => { m.MapLeftKey("USERID"); m.MapRightKey("PRIVILIGEID"); m.ToTable("USER_PRIVILIGE"); });

            // 添加角色和权限的映射关系
            modelBuilder.Entity<SYSROLE>().HasMany(t => t.SYSPRIVILIGEs).WithMany(t => t.SYSROLEs).Map(m => { m.MapLeftKey("ROLEID"); m.MapRightKey("PRIVILIGEID"); m.ToTable("ROLE_PRIVILIGE"); });

            // 添加用户和部门的映射关系
            modelBuilder.Entity<SYSUSER>().HasMany(t => t.SYSDEPARTMENTs).WithMany(t => t.SYSUSERs).Map(m => { m.MapLeftKey("USERID"); m.MapRightKey("DEPARTMENTID"); m.ToTable("USER_DEPARTMENT"); });

            //添加用户和行政区划的映射关系
            modelBuilder.Entity<SYSUSER>().HasMany(t => t.DISTRICTs).WithMany(t => t.SYSUSERs).Map(m => { m.MapLeftKey("USERID"); m.MapRightKey("DISTRICTID"); m.ToTable("USER_DISTRICT"); });

            //添加住宅和道路的映射关系
            modelBuilder.Entity<HOUSE>().HasMany(t => t.ROADs).WithMany(t => t.HOUSEs).Map(m => { m.MapLeftKey("ID"); m.MapRightKey("ROAD"); m.ToTable("HOUSE_ROAD"); });
        }
        public DbSet<SYSUSER> SYSUSER { get; set; }
        public DbSet<SYSROLE> SYSROLE { get; set; }
        public DbSet<SYSPRIVILIGE> SYSPRIVILIGE { get; set; }
        public DbSet<SYSDEPARTMENT> SYSDEPARTMENT { get; set; }
        public DbSet<DISTRICT> DISTRICT { get; set; }
        public DbSet<HOUSEBZOFUPLOADFILES> HOUSEBZOFUPLOADFILES { get; set; }
        public DbSet<DMOFUPLOADFILES> DMOFUPLOADFILES { get; set; }
        public DbSet<MPOFUPLOADFILES> MPOFUPLOADFILES { get; set; }
    }
}