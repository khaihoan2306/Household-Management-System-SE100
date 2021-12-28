using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using SautinSoft;

namespace Household_Management_System.Helpers
{
    public static class ExportToPDFHelper
    {
        public static void ExportToPDF()
        {
            // Convert Screenshot to PNG file.
            SautinSoft.PdfVision v = new SautinSoft.PdfVision();
            v.PageStyle.PageSize.Auto();
            // Create screen with 1920*1040 px.
            Rectangle rect = new Rectangle(20, 200, 1850, 600);
            Bitmap bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(rect.Left, rect.Top, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "Lưu báo cáo";
            saveFileDialog1.Filter = "PNG file (*.png)|*.png";
           
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bmp.Save(saveFileDialog1.FileName, ImageFormat.Png);
                System.Windows.MessageBox.Show("Đã xuất file thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
    }
}
