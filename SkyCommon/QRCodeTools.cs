using System;
using System.Web.Hosting;
using System.IO;
using System.Drawing;
using System.Text;
using ThoughtWorks.QRCode.Codec;

namespace SkyCommon
{
    public class QRCodeTools
    {
        #region 生成二维码
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string CreateQRCode(string rootpath, string folder, string QRCodeString)
        {
            System.Drawing.Bitmap bt;

            string enCodeString = QRCodeString;
            Random random = new Random();
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;//编码方式(注意：BYTE能支持中文，ALPHA_NUMERIC扫描出来的都是数字)
            qrCodeEncoder.QRCodeScale = 4;//大小(值越大生成的二维码图片像素越高)
            qrCodeEncoder.QRCodeVersion = 0;//版本(注意：设置为0主要是防止编码的字符串太长时发生错误)
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;//错误效验、错误更正(有4个等级)
            qrCodeEncoder.QRCodeBackgroundColor = Color.White;//背景色
            qrCodeEncoder.QRCodeForegroundColor = Color.Black;//前景色

            bt = qrCodeEncoder.Encode(enCodeString, Encoding.UTF8);

            string filename = System.DateTime.Now.ToUniversalTime().Ticks.ToString() + random.Next(1000, 9999).ToString() + "qrcode";

            var sep = Path.AltDirectorySeparatorChar.ToString();
            //指定为根目录
            //  var root = "~" + sep + rootpath + sep; 这方式有可能使上传的图片无法显示
            var root = sep + rootpath + sep;
            //拼接成路径
            var path = root + folder + sep;
            //找到这个路径
            var phicyPath = HostingEnvironment.MapPath(path);


            //string file_path = AppDomain.CurrentDomain.BaseDirectory + "QRCode\\";
            //string codeUrl = file_path + filename + ".jpg";

            string codeUrl = phicyPath + filename + ".jpg";

            //根据文件名称，自动建立对应目录
            if (!System.IO.Directory.Exists(phicyPath))
                System.IO.Directory.CreateDirectory(phicyPath);

            bt.Save(codeUrl);//保存图片
           // return codeUrl;
            return filename + ".jpg";
        }
        #endregion
    }
}