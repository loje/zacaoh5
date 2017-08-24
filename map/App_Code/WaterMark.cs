using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
/// <summary>
/// WaterMark 的摘要说明
/// </summary>
public class WaterMark
{
    public enum MarkType
    {
        Text,
        Image
    }
    private Image _image;
    private string _imgPath;
    private Image _markedImage;
    private MarkType _markType;
    private int _markX;
    private int _markY;
    private float _transparency;
    private int[] sizes;

	public WaterMark()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public WaterMark(string imagePath, float tranparence, int markX, int markY)
    {
        this._imgPath = "";
        this._markX = 0;
        this._markY = 0;
        this._transparency = 1f;
        this.sizes = new int[] { 0x30, 0x20, 0x10, 8, 6, 4 };
        this._image = null;
        this._markedImage = null;
        this._markType = MarkType.Text;
        this._markType = MarkType.Image;
        this._imgPath = imagePath;
        this._markX = markX;
        this._markY = this.MarkY;
        this._transparency = tranparence;
    }

    public static void Mark(string file, string waterImg, int markx, int marky, float transparence, int guanggao)
    {
        Image img = Image.FromFile(file);
        foreach (Guid guid in img.FrameDimensionsList)
        {
            FrameDimension dimension = new FrameDimension(guid);
            if (img.GetFrameCount(dimension) > 1)
            {
                return;
            }
        }
        try
        {
            int picWidth = img.Width;
            int picHeight = img.Height;
            if (img.Width > 550)
            {
                //picWidth = 550;
               //picHeight = (picWidth * img.Height) / img.Width;
                //Image.GetThumbnailImageAbort callback2 = null;
                //System.Drawing.Image image4 = new Bitmap(picWidth, picHeight);
                //Graphics graphicsPic = Graphics.FromImage(image4);
                //graphicsPic.InterpolationMode = InterpolationMode.High;
                //graphicsPic.SmoothingMode = SmoothingMode.HighQuality;
                //graphicsPic.Clear(Color.White);
            }
            Image image = Image.FromFile(waterImg);
            float[][] numArray2 = new float[5][];
            float[] numArray3 = new float[5];
            numArray3[0] = 1f;
            numArray2[0] = numArray3;
            float[] numArray4 = new float[5];
            numArray4[1] = 1f;
            numArray2[1] = numArray4;
            float[] numArray5 = new float[5];
            numArray5[2] = 1f;
            numArray2[2] = numArray5;
            float[] numArray6 = new float[5];
            numArray6[3] = transparence;
            numArray2[3] = numArray6;
            float[] numArray7 = new float[5];
            numArray7[4] = 1f;
            numArray2[4] = numArray7;
            float[][] newColorMatrix = numArray2;
            ColorMatrix matrix = new ColorMatrix(newColorMatrix);
            ImageAttributes imageAttr = new ImageAttributes();
            imageAttr.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Default);
            Bitmap bitmap = new Bitmap(picWidth, picHeight, PixelFormat.Format24bppRgb);
            bitmap.SetResolution(img.HorizontalResolution, img.VerticalResolution);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawImage(img, new Rectangle(0, 0, picWidth, picHeight), 0, 0, img.Width,img.Height, GraphicsUnit.Pixel);
            if (guanggao == 0)
            {
                //if ((image.Width > img.Width) || (image.Height > img.Height))
                //{
                //    Image.GetThumbnailImageAbort callback = null;
                //    Image image2 = image.GetThumbnailImage(img.Width / 4, (image.Height * img.Width) / image.Width, callback, new IntPtr());
                //    graphics.DrawImage(image2, new Rectangle(markx, marky, image2.Width, image2.Height), 0, 0, image2.Width, image2.Height, GraphicsUnit.Pixel, imageAttr);
                //    image2.Dispose();
                //    graphics.Dispose();
                //    MemoryStream stream = new MemoryStream();
                //    bitmap.Save(stream, ImageFormat.Jpeg);
                //    img = Image.FromStream(stream);
                //    return img;
                //}
                graphics.DrawImage(image, new Rectangle(img.Width-image.Width, img.Height - image.Height, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
            }
            graphics.Dispose();
            img.Dispose();
            image.Dispose();
            File.Delete(file);
            bitmap.Save(file, ImageFormat.Jpeg);
            bitmap.Dispose();
        }
        catch
        {
        }
    }

    public Image MarkedImage
    {
        get
        {
            return this._markedImage;
        }
    }

    public int MarkX
    {
        get
        {
            return this._markX;
        }
        set
        {
            this._markX = value;
        }
    }

    public int MarkY
    {
        get
        {
            return this._markY;
        }
        set
        {
            this._markY = value;
        }
    }

    public Image SourceImage
    {
        get
        {
            return this._image;
        }
        set
        {
            this._image = value;
        }
    }

    public float Transparency
    {
        get
        {
            if (this._transparency > 1f)
            {
                this._transparency = 1f;
            }
            return this._transparency;
        }
        set
        {
            this._transparency = value;
        }
    }

    public Image WaterImage
    {
        get
        {
            try
            {
                return Image.FromFile(this.WaterImagePath);
            }
            catch
            {
                return null;
            }
        }
    }

    public string WaterImagePath
    {
        get
        {
            return this._imgPath;
        }
        set
        {
            this._imgPath = value;
        }
    }

    public MarkType WaterMarkType
    {
        get
        {
            return this._markType;
        }
        set
        {
            this._markType = value;
        }
    }

    /// <summary> 
    /// 生成缩略图 
    /// </summary> 
    /// <param name="originalImagePath">源图路径（物理路径）</param> 
    /// <param name="thumbnailPath">缩略图路径（物理路径）</param> 
    /// <param name="width">缩略图宽度</param> 
    /// <param name="height">缩略图高度</param> 
    /// <param name="mode">生成缩略图的方式</param>
    public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
    {
        Image originalImage = Image.FromFile(originalImagePath);
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
                if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                {
                    oh = originalImage.Height;
                    ow = originalImage.Height * towidth / toheight;
                    y = 0;
                    x = (originalImage.Width - ow) / 2;
                }
                else
                {
                    ow = originalImage.Width;
                    oh = originalImage.Width * height / towidth;
                    x = 0;
                    y = (originalImage.Height - oh) / 2;
                }
                break;
            default:
                break;
        }
        //新建一个bmp图片 
        Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
        //新建一个画板 
        Graphics g = System.Drawing.Graphics.FromImage(bitmap);
        //设置高质量插值法 
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
        //设置高质量,低速度呈现平滑程度 
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //清空画布并以透明背景色填充 
        g.Clear(Color.Transparent);
        //在指定位置并且按指定大小绘制原图片的指定部分 
        g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
        new Rectangle(x, y, ow, oh),
        GraphicsUnit.Pixel);
        try
        {
            //以jpg格式保存缩略图 
            bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
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
        }

    }

    public static void MakeThumbnail(Image i, string thumbnailPath, int width, int height, string mode)
    {
        Image originalImage = i;
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
                if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                {
                    oh = originalImage.Height;
                    ow = originalImage.Height * towidth / toheight;
                    y = 0;
                    x = (originalImage.Width - ow) / 2;
                }
                else
                {
                    ow = originalImage.Width;
                    oh = originalImage.Width * height / towidth;
                    x = 0;
                    y = (originalImage.Height - oh) / 2;
                }
                break;
            default:
                break;
        }
        //新建一个bmp图片 
        Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
        //新建一个画板 
        Graphics g = System.Drawing.Graphics.FromImage(bitmap);
        //设置高质量插值法 
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
        //设置高质量,低速度呈现平滑程度 
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //清空画布并以透明背景色填充 
        g.Clear(Color.Transparent);
        //在指定位置并且按指定大小绘制原图片的指定部分 
        g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
        new Rectangle(x, y, ow, oh),
        GraphicsUnit.Pixel);
        try
        {
            //以jpg格式保存缩略图 
            bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
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
        }

    }
}
