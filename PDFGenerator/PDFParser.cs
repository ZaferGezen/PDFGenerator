using Newtonsoft.Json;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PDFGenerator
{
    public static class PDFParser
    {
        public static byte[] Generate(string templateData, int rotation = 0)
        {
            if (string.IsNullOrEmpty(templateData))
                return Array.Empty<byte>();

            PDFTemplate template;
            
            try
            {
                template = JsonConvert.DeserializeObject<PDFTemplate>(templateData);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Cannot convert template data to PDF template\n\n{e.Message}","Error");

                return new byte[] { };
            }

            float ratio = template.Ratio;
            float fontRatio = template.FontRatio;

            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            page.Width = XUnit.FromInch(4.0);
            page.Height = XUnit.FromInch(6.0);
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode);

            foreach (var item in template.Items)
            {
                float itemSize = item.Size * ratio;
                float itemX = item.X * ratio;
                float itemY = item.Y * ratio;
                float itemW = item.W * ratio;
                float itemH = item.H * ratio;

                if (item.IsHidden)
                {
                    continue;
                }

                switch (item.Type)
                {
                    case PDFTemplateItemType.Rect:

                        var pen = new XPen(XColor.FromKnownColor(XKnownColor.Black), itemSize);

                        if (item.Fill)
                        {
                            pen.Width = 1;

                            var x2 = itemX + itemW;
                            var y2 = itemY + itemH;

                            while (itemY < y2)
                            {
                                gfx.DrawLine(pen, itemX, itemY, x2, itemY);
                                itemY++;
                            }
                        }
                        else
                        {
                            gfx.DrawRectangle(pen, itemX, itemY, itemW, itemH);
                        }

                        break;

                    case PDFTemplateItemType.Text:
                        XFont font = new XFont(item.Font, item.Size * fontRatio, item.Bold ? XFontStyle.Bold : XFontStyle.Regular, options);
                        XSolidBrush textColor;
                        if (item.TextColor == Colors.white)
                        {
                            textColor = XBrushes.White;
                        }
                        else
                        {
                            textColor = XBrushes.Black;
                        }

                        if (item.Vertical > 0)
                        {
                            XGraphicsState state = gfx.Save();

                            gfx.RotateAtTransform(item.Vertical, new XPoint(itemX, itemY));
                            gfx.DrawString(item.Value, font, textColor, new XPoint(itemX, itemY));
                            gfx.Restore(state);
                        }
                        else if (item.RTL)
                        {
                            XStringFormat format = new XStringFormat
                            {
                                Alignment = XStringAlignment.Far
                            };
                            gfx.DrawString(item.Value, font, textColor, new XPoint(itemX, itemY), format);
                        }
                        else
                        {
                            gfx.DrawString(item.Value, font, textColor, new XPoint(itemX, itemY));
                        }

                        break;

                    case PDFTemplateItemType.Image:
                        string path = item.Value;

                        if (!Path.IsPathRooted(path))
                        {
                            path = AppDomain.CurrentDomain.BaseDirectory;
                            if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
                                path += Path.DirectorySeparatorChar;
                            path += item.Value;
                        }

                        if (File.Exists(path))
                        {
                            if (item.Vertical > 0)
                            {
                                XGraphicsState state = gfx.Save();

                                gfx.RotateAtTransform(item.Vertical, new XPoint(itemX, itemY));

                                gfx.DrawImage(XImage.FromFile(path), itemX, itemY, itemW, itemH);

                                gfx.Restore(state);
                            }
                            else
                            {
                                gfx.DrawImage(XImage.FromFile(path), itemX, itemY, itemW, itemH);
                            }
                        }
                        else
                        {
                            throw new Exception($"Cannot find template image content at: {path}");
                        }

                        break;

                    case PDFTemplateItemType.Barcode:
                        string value = string.IsNullOrEmpty(item.Value) ? "Empty Value" : item.Value;
                        int.TryParse(item.Size.ToString(), out int barWeight);
                        var barcode = GenCode128.Code128Rendering.MakeBarcodeImage(value, barWeight, true);
                        var stream = new MemoryStream();
                        barcode.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        stream.Position = 0;

                        XImage xImage = XImage.FromStream(() => stream);
                        xImage.Interpolate = false;
                        XGraphicsState barcodeState = gfx.Save();
                        gfx.RotateAtTransform(item.Vertical, new XPoint(itemX, itemY));
                        gfx.DrawImage(xImage, itemX, itemY, item.W, item.H);
                        gfx.Restore(barcodeState);
                        break;

                    case PDFTemplateItemType.QRCode:
                        QRCodeGenerator qrGenerator = new QRCodeGenerator();
                        QRCodeData qrCodeData = qrGenerator.CreateQrCode(item.Value, QRCodeGenerator.ECCLevel.Q);
                        QRCode qrCode = new QRCode(qrCodeData);
                        Bitmap qrCodeImage = new Bitmap(qrCode.GetGraphic(20), new Size(item.W, item.H));
                        byte[] imageByte = BitmapToBytes(qrCodeImage);
                        gfx.DrawImage(XImage.FromStream(() => new MemoryStream(imageByte)), itemX, itemY);
                        break;

                    case PDFTemplateItemType.Circle:
                        gfx.Save();
                        XPen pen1 = new XPen(XColors.Black, 1.5);
                        if (item.Fill)
                        {
                            gfx.DrawEllipse(pen1, XBrushes.Black, itemX, itemY, itemW, itemH);
                        }
                        else
                        {
                            gfx.DrawEllipse(pen1, XBrushes.White, itemX, itemY, itemW, itemH);
                        }
                        gfx.Restore();
                        break;

                    case PDFTemplateItemType.DataMatrix:
                        var dmtxImageEncoder = new DataMatrix.NetCore.DmtxImageEncoder();
                        var dataMatrixImage = dmtxImageEncoder.EncodeImage(item.Value);
                        using (var ms = new MemoryStream())
                        {
                            var state = gfx.Save();
                            gfx.RotateAtTransform(item.Vertical, new XPoint(itemX, itemY));
                            dataMatrixImage.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            gfx.DrawImage(XImage.FromStream(() => new MemoryStream(ms.ToArray())), itemX, itemY, item.W, item.H);
                            gfx.Restore(state);
                        }
                        break;

                    default:
                        break;
                }
            }

            byte[] pdfData;
            using (MemoryStream stream = new MemoryStream())
            {
                document.Pages[0].Rotate = rotation;
                document.Save(stream);
                pdfData = stream.ToArray();
            }

            return pdfData;
        }

        private static byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }

    public class PDFTemplate
    {
        public float Width { get; set; }
        public float Height { get; set; }
        public int DPI { get; set; }
        public float Ratio { get; set; }
        public float FontRatio { get; set; }

        public List<PDFTemplateItem> Items { get; set; }
    }

    public class PDFTemplateItem
    {
        public PDFTemplateItemType Type { get; set; }
        public Colors TextColor { get; set; }
        public string Font { get; set; }
        public float Size { get; set; }
        public bool Bold { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int W { get; set; }
        public int H { get; set; }
        public int Vertical { get; set; }
        public string Value { get; set; }
        public bool Fill { get; set; }
        public bool RTL { get; set; }
        public bool IsHidden { get; set; }
    }

    public enum PDFTemplateItemType
    {
        Rect,
        Text,
        Image,
        Barcode,
        QRCode,
        Circle,
        DataMatrix
    }

    public enum Colors
    {
        black = 1,
        white
    }
}