using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Utils
{
    public class ImageHelp
    {
        public Image ResourceImage;
        private int ImageWidth;
        private int ImageHeight;
        public string ErrMessage;

        /// <summary>   
        /// 类的构造函数   
        /// </summary>   
        /// <param name="ImageFileName">图片文件的全路径名称</param>   
        public ImageHelp(string ImageFileName)
        {
            var BeImage = Image.FromFile(ImageFileName);
            ResourceImage = ImageTailor(ImageFileName);
            ErrMessage = "";
        }

        public bool ThumbnailCallback()
        {
            return false;
        }

        /// <summary>
        /// 裁剪居中
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Image ImageTailor(string path)
        {
            Bitmap bmp = new Bitmap(path);
            var width = 0;
            var height = 0;
            var x = 0;
            var y = 0;
            if (bmp.Width > bmp.Height)
            {
                width = bmp.Height;
                height = bmp.Height;
                y = 0;
                x = (bmp.Width - bmp.Height) / 2;
            }
            else
            {
                width = bmp.Width;
                height = bmp.Width;
                y = (bmp.Height - bmp.Width) / 2;
                x = 0;
            }

            Bitmap newbm = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(newbm);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            //前Rectangle代表画布大小，后Rectangle代表裁剪后右边留下的区域
            g.DrawImage(bmp, new Rectangle(0, 0, width, height), new Rectangle(x, y, width, height), GraphicsUnit.Pixel);
            g.Dispose();
            return newbm;
        }

        /// <summary>   
        /// 生成缩略图重载方法1，返回缩略图的Image对象   
        /// </summary>   
        /// <param name="Width">缩略图的宽度</param>   
        /// <param name="Height">缩略图的高度</param>   
        /// <returns>缩略图的Image对象</returns>   
        public Image GetReducedImage(int Width, int Height)
        {
            try
            {
                Image ReducedImage;

                Image.GetThumbnailImageAbort callb = new Image.GetThumbnailImageAbort(ThumbnailCallback);

                ReducedImage = ResourceImage.GetThumbnailImage(Width, Height, callb, IntPtr.Zero);

                return ReducedImage;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }
        }

        /// <summary>   
        /// 生成缩略图重载方法2，将缩略图文件保存到指定的路径   
        /// </summary>   
        /// <param name="Width">缩略图的宽度</param>   
        /// <param name="Height">缩略图的高度</param>   
        /// <param name="targetFilePath">缩略图保存的全文件名，(带路径)，参数格式：D:Images ilename.jpg</param>   
        /// <returns>成功返回true，否则返回false</returns>   
        public bool GetReducedImage(int Width, int Height, string targetFilePath)
        {
            try
            {
                Image ReducedImage;

                Image.GetThumbnailImageAbort callb = new Image.GetThumbnailImageAbort(ThumbnailCallback);

                ReducedImage = ResourceImage.GetThumbnailImage(Width, Height, callb, IntPtr.Zero);
                ReducedImage.Save(@targetFilePath, ImageFormat.Jpeg);

                ReducedImage.Dispose();

                return true;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return false;
            }
        }

        /// <summary>   
        /// 生成缩略图重载方法3，返回缩略图的Image对象   
        /// </summary>   
        /// <param name="Percent">缩略图的宽度百分比 如：需要百分之80，就填0.8</param>     
        /// <returns>缩略图的Image对象</returns>   
        public Image GetReducedImage(double Percent)
        {
            try
            {
                Image ReducedImage;

                Image.GetThumbnailImageAbort callb = new Image.GetThumbnailImageAbort(ThumbnailCallback);

                ImageWidth = Convert.ToInt32(ResourceImage.Width * Percent);
                ImageHeight = Convert.ToInt32(ResourceImage.Height * Percent);

                ReducedImage = ResourceImage.GetThumbnailImage(ImageWidth, ImageHeight, callb, IntPtr.Zero);

                return ReducedImage;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return null;
            }
        }


        /// <summary>   
        /// 生成缩略图重载方法4，返回缩略图的Image对象   
        /// </summary>   
        /// <param name="Percent">缩略图的宽度百分比 如：需要百分之80，就填0.8</param>     
        /// <param name="targetFilePath">缩略图保存的全文件名，(带路径)，参数格式：D:Images ilename.jpg</param>   
        /// <returns>成功返回true,否则返回false</returns>   
        public bool GetReducedImage(double Percent, string targetFilePath)
        {
            try
            {
                Image ReducedImage;

                Image.GetThumbnailImageAbort callb = new Image.GetThumbnailImageAbort(ThumbnailCallback);

                ImageWidth = Convert.ToInt32(ResourceImage.Width * Percent);
                ImageHeight = Convert.ToInt32(ResourceImage.Height * Percent);

                ReducedImage = ResourceImage.GetThumbnailImage(ImageWidth, ImageHeight, callb, IntPtr.Zero);

                ReducedImage.Save(@targetFilePath, ImageFormat.Jpeg);

                ReducedImage.Dispose();

                return true;
            }
            catch (Exception e)
            {
                ErrMessage = e.Message;
                return false;
            }
        }

    }
}
