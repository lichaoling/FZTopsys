using JXGIS.FZToponymy.Models.Domain;
using JXGIS.FZToponymy.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace JXGIS.FZToponymy.Business.Common
{
    public class Paths
    {
        // 相对路径
        public string RelativePath { get; set; }
        // 绝对路径
        public string FullPath { get; set; }
        // 缩略图相对路径
        public string TRelativePath { get; set; }
        // 缩略图绝对路径
        public string TFullPath { get; set; }
        public string FileID { get; set; }
        public string FileName { get; set; }
    }

    public class UploadFileUtil
    {
        public static readonly string uploadBasePath = AppDomain.CurrentDomain.BaseDirectory;
        // 门牌的相对路径
        public static readonly string MPRelativePath = Path.Combine("UploadFiles", "门牌");

        // 地名的相对路径
        public static readonly string DMRelativePath = Path.Combine("UploadFiles", "地名");

        // 门牌编制申请文件上传的相对路径
        public static readonly string MPBZSQRelativePath = Path.Combine(MPRelativePath, Enums.UploadFileCategory.HouseBZ);
        // 门牌照片上传的相对路径
        public static readonly string MPPicRelativePath = Path.Combine(MPRelativePath, Enums.UploadFileCategory.MPPic);
        // 门牌二维码上传的相对路径
        public static readonly string MPQRCodeRelativePath = Path.Combine(MPRelativePath, Enums.UploadFileCategory.MPQRCode);

        // 道路（桥梁）照片上传的相对路径
        public static readonly string RoadRelativePath = Path.Combine(DMRelativePath, Enums.UploadFileCategory.RoadPic);
        // 小区（楼宇）照片上传的相对路径
        public static readonly string HouseRelativePath = Path.Combine(DMRelativePath, Enums.UploadFileCategory.HousePic);


        // 判断文件是否是图片
        public static bool IsPicture(string fileName)
        {
            bool isJPG = false;
            System.Drawing.Image img = System.Drawing.Image.FromFile(fileName);
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
                isJPG = true;
            return isJPG;
        }

        //获取完整的文件路径
        public static Paths GetUploadFilePath(string fileType, string ID, string fileID, string fileName)
        {
            string relativePath = string.Empty;
            var fileEx = new FileInfo(fileName).Extension;

            switch (fileType.ToUpper())
            {
                case Enums.UploadFileCategory.HouseBZ:
                    relativePath = MPBZSQRelativePath;
                    break;
                case Enums.UploadFileCategory.MPPic:
                    relativePath = MPPicRelativePath;
                    break;
                case Enums.UploadFileCategory.MPQRCode:
                    relativePath = MPQRCodeRelativePath;
                    break;
                case Enums.UploadFileCategory.RoadPic:
                    relativePath = RoadRelativePath;
                    break;
                case Enums.UploadFileCategory.HousePic:
                    relativePath = HouseRelativePath;
                    break;
                default:
                    throw new Exception("未知的文件目录");
            }
            relativePath = Path.Combine(relativePath, ID);
            string savePath = Path.Combine(uploadBasePath, relativePath);

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            string rPath = Path.Combine(relativePath, fileID + fileEx);
            string fPath = Path.Combine(savePath, fileID + fileEx);
            string trPath = IsPicture(fileName) ? Path.Combine(relativePath, "t-" + fileID + fileEx) : null;
            string tfPath = IsPicture(fileName) ? Path.Combine(savePath, "t-" + fileID + fileEx) : null;

            return new Paths()
            {
                FullPath = fPath,
                RelativePath = rPath,
                TFullPath = tfPath,
                TRelativePath = trPath,
                FileID = fileID,
                FileName = fileName,
            };
        }

        //上传文件
        public static void UploadFile(string ID, string fileType, string docType, HttpPostedFileBase file)
        {
            if (file != null)
            {
                var fileName = file.FileName;
                var fileID = System.Guid.NewGuid().ToString();
                var fileEx = new FileInfo(fileName).Extension;
                var paths = GetUploadFilePath(fileType, ID, fileID, fileName);

                // 保存文件
                file.SaveAs(paths.FullPath);

                if (IsPicture(fileName))//如果是图片，就保存缩略图
                {
                    // 保存缩略图片
                    Image image = Image.FromStream(file.InputStream);
                    image = PictureUtils.GetHvtThumbnail(image, 200);
                    image.Save(paths.TFullPath);
                }

                // 保存到数据库中
                // 文件ID，门牌记录的ID，图片相对路径，缩略图相对路径，文件名称等
                using (var dbContext = SystemUtils.NewEFDbContext)
                {
                    if (fileType == Enums.UploadFileCategory.HouseBZ)
                    {
                        HOUSEBZOFUPLOADFILES data = new HOUSEBZOFUPLOADFILES();
                        data.ID = fileID;
                        data.FILENAME = fileName;
                        data.TYPE = docType;
                        data.HOUSEBZID = ID;
                        data.FILEEX = fileEx;
                        data.STATE = Enums.State.Enable;
                        dbContext.HOUSEBZOFUPLOADFILES.Add(data);
                    }
                    if (fileType == Enums.UploadFileCategory.MPPic)
                    {
                        MPOFUPLOADFILES data = new MPOFUPLOADFILES();
                        data.ID = fileID;
                        data.FILENAME = fileName;
                        data.TYPE = Enums.UploadFileCategory.MPPic;
                        data.MPID = ID;
                        data.FILEEX = fileEx;
                        data.STATE = Enums.State.Enable;
                        dbContext.MPOFUPLOADFILES.Add(data);
                    }
                    if (fileType == Enums.UploadFileCategory.RoadPic)
                    {
                        DMOFUPLOADFILES data = new DMOFUPLOADFILES();
                        data.ID = fileID;
                        data.FILENAME = fileName;
                        data.TYPE = Enums.UploadFileCategory.RoadPic;
                        data.DMID = ID;
                        data.FILEEX = fileEx;
                        data.STATE = Enums.State.Enable;
                        dbContext.DMOFUPLOADFILES.Add(data);
                    }
                    if (fileType == Enums.UploadFileCategory.HousePic)
                    {
                        DMOFUPLOADFILES data = new DMOFUPLOADFILES();
                        data.ID = fileID;
                        data.FILENAME = fileName;
                        data.TYPE = Enums.UploadFileCategory.HousePic;
                        data.DMID = ID;
                        data.FILEEX = fileEx;
                        data.STATE = Enums.State.Enable;
                        dbContext.DMOFUPLOADFILES.Add(data);
                    }
                    dbContext.SaveChanges();
                }
            }
        }

        //获取一类文件的路径
        public static List<Paths> GetFileUrls(string ID, string fileType, string docType)
        {
            using (var dbContext = SystemUtils.NewEFDbContext)
            {
                List<Paths> paths = new List<Paths>();
                if (fileType == Enums.UploadFileCategory.HouseBZ)
                {
                    var files = dbContext.HOUSEBZOFUPLOADFILES.Where(t => t.STATE == Enums.State.Enable).Where(t => t.HOUSEBZID == ID).Where(t => t.TYPE == docType).ToList();
                    foreach (var f in files)
                    {
                        var p = GetUploadFilePath(fileType, ID, f.ID, f.FILENAME);
                        paths.Add(p);
                    }
                }
                else if (fileType == Enums.UploadFileCategory.MPPic) //docType传空
                {
                    var files = dbContext.MPOFUPLOADFILES.Where(t => t.STATE == Enums.State.Enable).Where(t => t.MPID == ID).Where(t => t.TYPE == Enums.UploadFileCategory.MPPic).ToList();
                    foreach (var f in files)
                    {
                        var p = GetUploadFilePath(fileType, ID, f.ID, f.FILENAME);
                        paths.Add(p);
                    }
                }
                else if (fileType == Enums.UploadFileCategory.MPQRCode) //docType传空
                {
                    var files = dbContext.MPOFUPLOADFILES.Where(t => t.STATE == Enums.State.Enable).Where(t => t.MPID == ID).Where(t => t.TYPE == Enums.UploadFileCategory.MPQRCode).ToList();
                    foreach (var f in files)
                    {
                        var p = GetUploadFilePath(fileType, ID, f.ID, f.FILENAME);
                        paths.Add(p);
                    }
                }
                else if (fileType == Enums.UploadFileCategory.RoadPic)  //docType传空
                {
                    var files = dbContext.DMOFUPLOADFILES.Where(t => t.STATE == Enums.State.Enable).Where(t => t.DMID == ID).Where(t => t.TYPE == Enums.UploadFileCategory.RoadPic).ToList();
                    foreach (var f in files)
                    {
                        var p = GetUploadFilePath(fileType, ID, f.ID, f.FILENAME);
                        paths.Add(p);
                    }
                }
                else if (fileType == Enums.UploadFileCategory.HousePic)  //docType传空
                {
                    var files = dbContext.DMOFUPLOADFILES.Where(t => t.STATE == Enums.State.Enable).Where(t => t.DMID == ID).Where(t => t.TYPE == Enums.UploadFileCategory.HousePic).ToList();
                    foreach (var f in files)
                    {
                        var p = GetUploadFilePath(fileType, ID, f.ID, f.FILENAME);
                        paths.Add(p);
                    }
                }
                else
                    throw new Exception("未知的文件类型");
                return paths;
            }
        }

        //删除一个文件
        public static void RemoveFile(string ID, string fileType)
        {
            using (var dbContext = SystemUtils.NewEFDbContext)
            {
                if (fileType == Enums.UploadFileCategory.HouseBZ)
                {
                    var query = dbContext.HOUSEBZOFUPLOADFILES.Where(t => t.STATE == Enums.State.Enable).Where(t => t.ID == ID).FirstOrDefault();
                    if (query == null)
                        throw new Exception("该文件已经被删除！");
                    query.STATE = Enums.State.Disable;
                }
                else if (fileType == Enums.UploadFileCategory.MPPic || fileType == Enums.UploadFileCategory.MPQRCode)
                {
                    var query = dbContext.MPOFUPLOADFILES.Where(t => t.STATE == Enums.State.Enable).Where(t => t.ID == ID).FirstOrDefault();
                    if (query == null)
                        throw new Exception("该文件已经被删除！");
                    query.STATE = Enums.State.Disable;
                }
                else if (fileType == Enums.UploadFileCategory.RoadPic || fileType == Enums.UploadFileCategory.HousePic)
                {
                    var query = dbContext.MPOFUPLOADFILES.Where(t => t.STATE == Enums.State.Enable).Where(t => t.ID == ID).FirstOrDefault();
                    if (query == null)
                        throw new Exception("该文件已经被删除！");
                    query.STATE = Enums.State.Disable;
                }
                else
                    throw new Exception("未知的图片类型");
                dbContext.SaveChanges();
            }
        }
    }
}