using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Drawing;

    public class ImageMes
    {
        /// <summary>
        /// 生成缩略图开始
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param> 
        //修改后的代码 支持动态控件传递多个　
        public static void CreateXimg(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            Graphics draw = null;
            string FileExt = "";

            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(originalImagePath));

            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形） 
                    break;
                case "W"://指定宽，高按比例 
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例 
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形） 
                    if ((double)towidth / (double)toheight < (double)width / (double)width / (double)height)
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    else
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
            System.Drawing.Image bitmap2 = new System.Drawing.Bitmap(towidth, toheight);
            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);


            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //设置高质量
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight));

            try
            {
                //以jpg格式保存缩略图
                FileExt = Path.GetFileNameWithoutExtension(originalImagePath);
                //用新建立的image对象拷贝bitmap对象 让g对象可以释放资源
                draw = Graphics.FromImage(bitmap2);
                draw.DrawImage(bitmap, 0, 0);

            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
                //保存调整在这里即可
                bitmap2.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        //生成缩略图结束
    }
