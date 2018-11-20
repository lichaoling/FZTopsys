using JXGIS.FZToponymy.Models.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using JXGIS.FZToponymy.Utils.ReturnObj;

namespace JXGIS.FZToponymy.Utils
{
    public class SimpleController
    {
        public string Controller { get; set; }
        public string ControllerName { get; set; }

        public List<SimpleAction> Actions { get; set; }
    }

    public class SimpleAction
    {
        public string ID { get; set; }
        public string Action { get; set; }

        public string ActionName { get; set; }

        public bool NeedAuthorize { get; set; }

        public string Type { get; set; }

        public bool UserHasPrivilige { get; set; } = false;
    }

    public class Department
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ParentId { get; set; }

        public List<Department> SubDepartments { get; set; }

        public bool UserIn { get; set; } = false;

        public List<User> Users { get; set; }

    }
    public class District
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string ParentId { get; set; }

        public List<District> SubDistrict { get; set; }

        public bool UserIn { get; set; } = false;

        public List<User> Users { get; set; }

    }

    public class User
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public DateTime? Birthday { get; set; }

        public string Sex { get; set; }

        public string Email { get; set; }

        public string AliasName { get; set; }

        public string Password { get; set; }

        public string Telephone { get; set; }

    }

    public class Role
    {
        public string roleId { get; set; }
        public string roleName { get; set; }
        public string roleDescription { get; set; }

        public bool userIn { get; set; }
    }

    public class PriviligeUtils
    {
        public static class ActionResultType
        {
            public const string View = "view";
            public const string Data = "data";
        }

        public enum NeedAuthorize
        {
            Yes,
            No,
            All
        }

        /// <summary>
        /// 获取权限树，根据给定的系统权限列表，根据是否需要权限验证，获取新的权限树形结构（两级，Controller+Action）
        /// </summary>
        /// <param name="priviliges"></param>
        /// <param name="needAuthorize"></param>
        /// <returns></returns>
        public static List<SimpleController> GetPriviligeTree(List<SYSPRIVILIGE> priviliges, NeedAuthorize needAuthorize = NeedAuthorize.All)
        {
            if (needAuthorize == NeedAuthorize.All)
            {
                return (from p in priviliges
                        group p by new { p.CONTROLLER, p.CONTROLLERNAME } into g
                        select new SimpleController
                        {
                            Controller = g.Key.CONTROLLER,
                            ControllerName = g.Key.CONTROLLERNAME,
                            Actions = (from g0 in g orderby g0.TYPE, g0.ACTION select new SimpleAction { ID = g0.PRIVILIGEID, Action = g0.ACTION, ActionName = g0.ACTIONNAME, NeedAuthorize = g0.NEEDAUTHORIZE, Type = g0.TYPE }).ToList()
                        }).ToList();
            }
            else if (needAuthorize == NeedAuthorize.Yes)
            {
                return (from p in priviliges
                        where p.NEEDAUTHORIZE == true
                        group p by new { p.CONTROLLER, p.CONTROLLERNAME } into g
                        select new SimpleController
                        {
                            Controller = g.Key.CONTROLLER,
                            ControllerName = g.Key.CONTROLLERNAME,
                            Actions = (from g0 in g orderby g0.TYPE, g0.ACTION select new SimpleAction { ID = g0.PRIVILIGEID, Action = g0.ACTION, ActionName = g0.ACTIONNAME, NeedAuthorize = g0.NEEDAUTHORIZE, Type = g0.TYPE }).ToList()
                        }).ToList();
            }
            else
            {
                return (from p in priviliges
                        where p.NEEDAUTHORIZE == false
                        group p by new { p.CONTROLLER, p.CONTROLLERNAME } into g
                        select new SimpleController
                        {
                            Controller = g.Key.CONTROLLER,
                            ControllerName = g.Key.CONTROLLERNAME,
                            Actions = (from g0 in g orderby g0.TYPE, g0.ACTION select new SimpleAction { ID = g0.PRIVILIGEID, Action = g0.ACTION, ActionName = g0.ACTIONNAME, NeedAuthorize = g0.NEEDAUTHORIZE, Type = g0.TYPE }).ToList()
                        }).ToList();
            }
        }

        /// <summary>
        /// 数据库中的权限配置
        /// </summary>
        public static List<SYSPRIVILIGE> PriviligesInDatabase
        {
            get
            {
                return PriviligeUtils.GetPriviligesInDatabase();
            }
        }

        /// <summary>
        /// DLL中的权限
        /// </summary>
        public static List<SYSPRIVILIGE> PriviligesInWebDLL
        {
            get
            {
                return PriviligeUtils.GetPriviligesInDLL(HttpContext.Current.Server.MapPath("~/bin/JXGIS.TianDiTuPinghu.Web.dll"));
            }
        }

        /// <summary>
        /// 获取数据库中的权限配置
        /// </summary>
        /// <returns></returns>
        public static List<SYSPRIVILIGE> GetPriviligesInDatabase()
        {
            List<SYSPRIVILIGE> priviliges = null;
            using (var db = SystemUtils.NewEFDbContext)
            {
                priviliges = db.SYSPRIVILIGE.ToList();
            }
            return priviliges;
        }
        /// <summary>
        /// 获取程序集中所有控制器的权限
        /// </summary>
        /// <returns></returns>
        public static List<SYSPRIVILIGE> GetPriviligesInDLL(string dllPath)
        {
            if (!File.Exists(dllPath)) throw new Error("未找到指定DLL，请确定文件路径是否正确！");

            var assembly = Assembly.LoadFrom(dllPath);
            var controllerType = typeof(Controller);
            var actionResultType = typeof(ActionResult);
            var descriptionAttributeType = typeof(DescriptionAttribute);
            var priviligeAttributeType = typeof(PriviligeAttribute);
            var viewResultBaseType = typeof(ViewResultBase);
            var types = assembly.GetTypes().Where(t => t.IsSubclassOf(controllerType)).ToList();

            var priviliges = new List<SYSPRIVILIGE>();
            foreach (var t in types)
            {
                var methods = t.GetMethods().Where(m => m.ReturnType.IsSubclassOf(actionResultType) || m.ReturnType == actionResultType).ToList();  //获取所有返回类型是actionresult或其子类的一些action
                var attrController = t.GetCustomAttribute(descriptionAttributeType) as DescriptionAttribute;
                var name = t.Name.Substring(0, t.Name.IndexOf("Controller"));

                if (attrController != null)
                {
                    var controllerName = attrController.Name;
                    foreach (var m in methods)        //遍历每一个action
                    {
                        var attrAction = m.GetCustomAttribute(priviligeAttributeType) as PriviligeAttribute;     //action的属性
                        string actionName = null;
                        string description = null;
                        string type = null;

                        if (m.ReturnType.IsSubclassOf(viewResultBaseType)) type = ActionResultType.View;       //返回的是view
                        else if (m.ReturnType.Equals(actionResultType)) type = null;
                        else type = ActionResultType.Data;          //返回的是data

                        if (attrAction != null)
                        {
                            actionName = attrAction.Name;
                            description = attrAction.Description;
                            priviliges.Add(new SYSPRIVILIGE() { CONTROLLER = name, CONTROLLERNAME = controllerName, ACTION = m.Name, ACTIONNAME = actionName, DESCIRPTION = description, TYPE = type });
                        }
                    }
                }
            }
            return priviliges;
        }

        /// <summary>
        /// 将DLL中的权限更新到数据库中，并不删除DLL中去掉的权限
        /// </summary>
        /// <returns></returns>
        public static List<SYSPRIVILIGE> UpdatePriviliges()
        {
            using (var db = SystemUtils.NewEFDbContext)
            {
                var pgsInDb = db.SYSPRIVILIGE.ToList();
                var pgsInDLL = PriviligesInWebDLL;

                foreach (var pg in pgsInDLL)
                {
                    // dll中有的，数据库中没有的，新增到数据库；数据库中有的，则更新信息
                    if (!pgsInDb.Any(p => p.CONTROLLER == pg.CONTROLLER && p.ACTION == pg.ACTION))
                    {
                        pg.PRIVILIGEID = Guid.NewGuid().ToString();
                        db.SYSPRIVILIGE.Add(pg);
                        pg.CREATETIME = DateTime.Now;
                        pg.LASTMODIFYTIME = DateTime.Now;
                    }
                    else
                    {
                        var pgDb = pgsInDb.Where(p => p.CONTROLLER == pg.CONTROLLER && p.ACTION == pg.ACTION).FirstOrDefault();
                        if (pgDb != null &&
                            (pgDb.ACTIONNAME == pg.ACTIONNAME ||
                            pgDb.CONTROLLERNAME == pg.CONTROLLERNAME ||
                            pgDb.DESCIRPTION == pg.DESCIRPTION ||
                            pgDb.TYPE == pg.TYPE))
                        {
                            pgDb.ACTIONNAME = pg.ACTIONNAME;
                            pgDb.CONTROLLERNAME = pg.CONTROLLERNAME;
                            pgDb.DESCIRPTION = pg.DESCIRPTION;
                            pgDb.TYPE = pg.TYPE;
                            pgDb.LASTMODIFYTIME = DateTime.Now;
                        }
                    }
                }
                var count = db.SaveChanges();
                return db.SYSPRIVILIGE.ToList();
            }
        }


        /// <summary>
        /// 根据UserId获取部门树，部门树中的UseIn标识了该用户是否位于该部门
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<Department> GetDepartmentTree(string userId)
        {
            using (var db = SystemUtils.NewEFDbContext)
            {
                var dpts = db.SYSDEPARTMENT.Where(d => d.PARENTID == null).ToList();
                SYSUSER user = null;
                List<SYSDEPARTMENT> userDpts = null;
                if (!string.IsNullOrEmpty(userId))
                {
                    user = db.SYSUSER.Where(u => u.USERID == userId).FirstOrDefault();
                    if (user != null)
                    {
                        userDpts = user.SYSDEPARTMENTs;
                    }
                }

                return GetDepartmentTree(dpts, userDpts);
            }
        }
        /// <summary>
        /// 根据UserId获取行政区划树，行政区划树中的UseIn标识了该用户是否位于该行政区划
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<District> GetDistrictTree(string userId)
        {
            using (var db = SystemUtils.NewEFDbContext)
            {
                var dpts = db.DISTRICT.Where(d => d.PARENTID == null).ToList();
                SYSUSER user = null;
                List<DISTRICT> userDists = null;
                if (!string.IsNullOrEmpty(userId))
                {
                    user = db.SYSUSER.Where(u => u.USERID == userId).FirstOrDefault();
                    if (user != null)
                    {
                        userDists = user.DISTRICTs;
                    }
                }

                return GetDistrictTree(dpts, userDists);
            }
        }


        /// <summary>
        /// 获取部门树，根据系统部门以及用户所处部门获取部门树，并在部门树中通过标识UserIn属性指定用户是否属于该部门
        /// </summary>
        /// <param name="sysDpts"></param>
        /// <param name="userDpts"></param>
        /// <returns></returns>
        public static List<Department> GetDepartmentTree(List<SYSDEPARTMENT> sysDpts, List<SYSDEPARTMENT> userDpts)
        {
            var dptList = new List<Department>();
            foreach (var sysDpt in sysDpts)
            {
                var dpt = new Department()
                {
                    ID = sysDpt.DEPARTMENTID,
                    Name = sysDpt.NAME,
                    SubDepartments = GetDepartmentTree(sysDpt.SubDepartments, userDpts),
                    UserIn = userDpts != null ? userDpts.Any(d => d.DEPARTMENTID == sysDpt.DEPARTMENTID) : false
                };
                dptList.Add(dpt);
            }
            return dptList;
        }
        /// <summary>
        /// 获取行政区划树，根据行政区划以及用户所处行政区划获取行政区划树，并在行政区划树中通过标识UserIn属性指定用户是否属于该行政区划
        /// </summary>
        /// <param name="sysDists"></param>
        /// <param name="userDists"></param>
        /// <returns></returns>
        public static List<District> GetDistrictTree(List<DISTRICT> sysDists, List<DISTRICT> userDists)
        {
            var dstList = new List<District>();
            foreach (var sysDst in sysDists)
            {
                var dst = new District()
                {
                    ID = sysDst.DISTRICTID,
                    Name = sysDst.NAME,
                    Code = sysDst.CODE,
                    SubDistrict = GetDistrictTree(sysDst.SubDistrict, userDists),
                    UserIn = userDists != null ? userDists.Any(d => d.DISTRICTID == sysDst.DISTRICTID || sysDst.DISTRICTID.IndexOf(d.DISTRICTID + ".") == 0) : false
                };
                dstList.Add(dst);
            }
            return dstList;
        }



        /// <summary>
        /// 根据部门获取部门树，给定部门及以下的部门不显示
        /// </summary>
        /// <param name="dpt"></param>
        /// <returns></returns>
        public static List<Department> GetDepartmentTree(Department dpt)
        {
            using (var db = SystemUtils.NewEFDbContext)
            {
                var dpts = db.SYSDEPARTMENT.Where(d => d.PARENTID == null).ToList();
                return GetDepartmentTree(dpts, dpt);
            }
        }
        /// <summary>
        /// 根据行政区划获取行政区划，给定行政区划及以下的行政区划不显示
        /// </summary>
        /// <param name="dst"></param>
        /// <returns></returns>
        public static List<District> GetDistrictTree(District dst)
        {
            using (var db = SystemUtils.NewEFDbContext)
            {
                var dsts = db.DISTRICT.Where(d => d.PARENTID == null).ToList();
                return GetDistrictTree(dsts, dst);
            }
        }



        /// <summary>
        /// 根据系统部门以及给定的部门获取部门树，给定部门及以下的部门不显示
        /// </summary>
        /// <param name="dpt"></param>
        /// <returns></returns>
        public static List<Department> GetDepartmentTree(List<SYSDEPARTMENT> sysDpts, Department department)
        {
            var dptList = new List<Department>();
            foreach (var sysDpt in sysDpts)
            {
                if (department == null || sysDpt.DEPARTMENTID != department.ID)
                {
                    var dpt = new Department()
                    {
                        ID = sysDpt.DEPARTMENTID,
                        Name = sysDpt.NAME,
                        Description = sysDpt.DESCRIPTION,
                        SubDepartments = GetDepartmentTree(sysDpt.SubDepartments, department)
                    };
                    dptList.Add(dpt);
                }
            }
            return dptList;
        }
        /// <summary>
        /// 根据行政区划获取行政区划，给定行政区划及以下的行政区划不显示
        /// </summary>
        /// <param name="sysDsts"></param>
        /// <param name="district"></param>
        /// <returns></returns>
        public static List<District> GetDistrictTree(List<DISTRICT> sysDsts, District district)
        {
            var dstList = new List<District>();
            foreach (var sysDst in sysDsts)
            {
                if (district != null && (sysDst.DISTRICTID == district.ID || sysDst.DISTRICTID.IndexOf(district.ID + ".") == 0 || district.ID.IndexOf(sysDst.DISTRICTID) == 0))
                {
                    var dst = new District()
                    {
                        ID = sysDst.DISTRICTID,
                        Name = sysDst.NAME,
                        Code = sysDst.CODE,
                        SubDistrict = GetDistrictTree(sysDst.SubDistrict, district)
                    };
                    dstList.Add(dst);
                }
            }
            return dstList;
        }


        /// <summary>
        /// 获取已部门的用户的部门-用户树，和未分部门的用户
        /// </summary>
        /// <param name="sysDpts"></param>
        /// <returns></returns>
        public static List<Department> GetDepartmentUserTree()
        {
            var dptsNew = new List<Department>();
            using (var db = SystemUtils.NewEFDbContext)
            {
                var dpts = db.SYSDEPARTMENT.Where(d => d.PARENTID == null).ToList();
                var dptsTree = GetDepartmentUserTree(dpts);

                dptsNew.Add(new Department()
                {
                    ID = "分类",
                    Name = "分类",
                    SubDepartments = dptsTree
                });

                var users = (from u in db.SYSUSER
                             where u.SYSDEPARTMENTs.Count == 0
                             select new User { UserId = u.USERID, UserName = u.USERNAME, AliasName = u.NAME }
                             ).ToList();
                dptsNew.Add(new Department()
                {
                    ID = "未分类",
                    Name = "未分类",
                    Users = users
                });
                return dptsNew;
            }
        }
        public static List<District> GetDistrictUserTree()
        {
            var dstsNew = new List<District>();
            using (var db = SystemUtils.NewEFDbContext)
            {
                var dsts = db.DISTRICT.Where(d => d.PARENTID == null).ToList();
                var dstsTree = GetDistrictUserTree(dsts);
                dstsNew.Add(new District()
                {
                    ID = "分类",
                    Name = "分类",
                    SubDistrict = dstsTree
                });
                var users = (from u in db.SYSUSER
                             where u.DISTRICTs.Count == 0
                             select new User { UserId = u.USERID, UserName = u.USERNAME, AliasName = u.NAME }
                             ).ToList();
                dstsNew.Add(new District()
                {
                    ID = "未分类",
                    Name = "未分类",
                    Users = users
                });
                return dstsNew;
            }
        }


        /// <summary>
        /// 获取部门-用户树，根据系统部门列表，获取包含子部门、部门下用户的树形结构
        /// </summary>
        /// <param name="sysDpts"></param>
        /// <returns></returns>
        public static List<Department> GetDepartmentUserTree(List<SYSDEPARTMENT> sysDpts)
        {
            var dptList = new List<Department>();
            foreach (var sysDpt in sysDpts)
            {
                var users = (from u in sysDpt.SYSUSERs select new User { UserId = u.USERID, UserName = u.USERNAME, Name = u.NAME, AliasName = u.NAME }).ToList();
                var subdeparts = GetDepartmentUserTree(sysDpt.SubDepartments);

                var dpt = new Department()
                {
                    ID = sysDpt.DEPARTMENTID,
                    Name = sysDpt.NAME,
                    SubDepartments = subdeparts,
                    Users = users
                };
                dptList.Add(dpt);

            }
            return dptList;
        }
        public static List<District> GetDistrictUserTree(List<DISTRICT> sysDsts)
        {
            var dstList = new List<District>();
            foreach (var sysDst in sysDsts)
            {
                var users = (from u in sysDst.SYSUSERs select new User { UserId = u.USERID, UserName = u.USERNAME, Name = u.NAME, AliasName = u.NAME }).ToList();

                List<District> subdists = null;
                if (users.Count() > 0)
                {
                    List<DISTRICT> newSubs = new List<DISTRICT>();
                    foreach (var sub in sysDst.SubDistrict)
                    {
                        sub.SYSUSERs = sysDst.SYSUSERs;
                        newSubs.Add(sub);
                    }
                    subdists = GetDistrictUserTree(newSubs);
                }
                subdists = GetDistrictUserTree(sysDst.SubDistrict);
                var dpt = new District()
                {
                    ID = sysDst.DISTRICTID,
                    Name = sysDst.NAME,
                    Code = sysDst.CODE,
                    SubDistrict = subdists,
                    Users = users
                };
                dstList.Add(dpt);

            }
            return dstList;
        }


        /// <summary>
        /// 获取角色列表，根据UserId标识列表中的UserIn属性来标识用户是否属于该角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<Role> GetRoleList(string userId = null)
        {
            using (var db = SystemUtils.NewEFDbContext)
            {
                var rs = (from r in db.SYSROLE
                          orderby r.LASTMODIFYTIME descending
                          select new
                          {
                              roleId = r.ROLEID,
                              roleName = r.ROLENAME,
                              roleDescription = r.ROLEDESCRIPTION,
                              users = r.SYSUSERs.ToList()
                          }).ToList();
                var roles = (from r in rs
                             select new Role
                             {
                                 roleId = r.roleId,
                                 roleName = r.roleName,
                                 roleDescription = r.roleDescription,
                                 userIn = string.IsNullOrEmpty(userId) ? false : r.users.Any(p => p.USERID == userId)
                             }).ToList();

                return roles;
            }
        }
    }
}