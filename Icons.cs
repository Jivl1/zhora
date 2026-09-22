using System.Drawing;
using System.Drawing.Text;

namespace Лр3
{
    // Іконки малюються з системного шрифту Windows «Segoe MDL2 Assets»,
    // тому проєкту не потрібні окремі файли зображень.
    internal static class Icons
    {
        public static readonly Image Save = Glyph('', Color.FromArgb(0, 84, 166), 16);
        public static readonly Image Copy = Glyph('', Color.FromArgb(70, 110, 170), 16);
        public static readonly Image Cut = Glyph('', Color.FromArgb(60, 60, 60), 16);
        public static readonly Image Paste = Glyph('', Color.FromArgb(180, 120, 20), 16);
        public static readonly Image Notepad = Glyph('', Color.FromArgb(0, 84, 166), 64);

        private static Image Glyph(char glyph, Color color, int size)
        {
            var bitmap = new Bitmap(size, size);
            using var g = Graphics.FromImage(bitmap);
            using var font = new Font("Segoe MDL2 Assets", size * 0.8f, GraphicsUnit.Pixel);
            using var brush = new SolidBrush(color);
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            if (font.Name == "Segoe MDL2 Assets")
            {
                using var format = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                g.DrawString(glyph.ToString(), font, brush, new RectangleF(0, 0, size, size), format);
            }
            else
            {
                g.FillRectangle(brush, size / 4, size / 4, size / 2, size / 2);
            }
            return bitmap;
        }
    }
}
