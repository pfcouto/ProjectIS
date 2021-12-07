using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministratorConsole
{
    class ImageHandler
    {
        public static string ImageToBase64String(string str_img_path)
        {
            Bitmap bitmap = new Bitmap(str_img_path);

            MemoryStream ms = new MemoryStream();

            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            byte[] arr = new byte[ms.Length];

            ms.Position = 0;
            ms.Read(arr, 0, (int)ms.Length);
            ms.Close();

            string strBase64 = Convert.ToBase64String(arr);

            return strBase64;
        }


        public static Bitmap Base64StringToImage(string str_img_base64)
        {
            byte[] arr = Convert.FromBase64String(str_img_base64);
            MemoryStream ms = new MemoryStream(arr);
            Bitmap bitmap = new Bitmap(ms);

            return bitmap;
        }

    }
}
