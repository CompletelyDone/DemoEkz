using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace Model
{
    public class Captcha
    {
        public static string GenerateCaptchaText(int length)
        {
            const string chars = "ABCDEFGHJKMNPQRSTUVWXYZabcdefghjkmnpqrstuvwxyz123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        public static RenderTargetBitmap DrawCaptchaImage(string captchaText)
        {
            var drawingVisual = new DrawingVisual();
            using (var drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.DrawRectangle(Brushes.White, null, new Rect(new Point(0, 0), new Size(100, 50)));
                drawingContext.DrawText(new FormattedText(captchaText, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                    new Typeface("Arial"), 20, Brushes.Black), new Point(10, 10));
                drawingContext.DrawLine(new Pen(Brushes.Black, 1), new Point(0, 25), new Point(100, 25));
            }

            var renderBitmap = new RenderTargetBitmap(100, 50, 96, 96, PixelFormats.Pbgra32);
            renderBitmap.Render(drawingVisual);

            return renderBitmap;
        }
    }
}
