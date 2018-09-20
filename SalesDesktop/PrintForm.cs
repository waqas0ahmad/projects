using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace SalesDesktop
{
    public partial class PrintForm_
    {
        List<PrintOrder> list;
        public PrintForm_(List<PrintOrder> orderlist)
        {

            //this.reportViewer1.PrintDialog();
            this.list = new List<PrintOrder>();
            this.list = orderlist;
            Print();
        }
        public void Print()
        {
            var doc = new PrintDocument();
            doc.PrintPage += new PrintPageEventHandler(ProvideContent);
            doc.Print();
        }

        public void ProvideContent(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Courier New", 10);

            float fontHeight = font.GetHeight();

            int startX = 0;
            int startY = 0;
            int Offset = 20;
            e.PageSettings.PaperSize = new PaperSize("Custom", 100, 150);
            //e.PageSettings.PaperSize.Width = 50;
            graphics.DrawString("            Hanif Juice Corner", new Font("calebri", 38),
                                new SolidBrush(Color.Black), startX, startY + 5 + Offset);
            Offset = Offset + 60;

            graphics.DrawString("                Corner Gala Mandi Sharqi,",
                        new Font("Courier New", 16),
                        new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;


            graphics.DrawString("               Gujranwala Road, Hafizabad",
                        new Font("Courier New", 16),
                        new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;

            graphics.DrawString("            Cell:0300-6524806 / 0306-6524806",
                        new Font("Courier New", 16),
                        new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;

            graphics.DrawString("To: ",
                        new Font("Courier New", 16, FontStyle.Bold),
                        new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString("    CASH",
                        new Font("Courier New", 16),
                        new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString("Inv#  122234SL      " + DateTime.Now.ToString("dd/mm/yyy") + "       " + DateTime.Now.ToString("hh:mm tt"),
                      new Font("Courier New", 16),
                      new SolidBrush(Color.Black), startX, startY + Offset + 10);
            Offset = Offset + 40;
            String headers = "Product                       Qty     Rate     Amount";
            graphics.DrawString(headers, new Font("Courier New", 16,FontStyle.Bold),
                        new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            String underLine = "---------------------------------------------";
            graphics.DrawString(underLine, new Font("Courier New", 16,FontStyle.Bold),
                        new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            int total = 0;
            underLine = "---------------------------------------------------------------";
            foreach (var item in list)
            {
                graphics.DrawString(item.product, new Font("Courier New", 16),
                            new SolidBrush(Color.Black), startX, startY + Offset);
                graphics.DrawString(item.quantity.ToString(), new Font("Courier New", 16),
                            new SolidBrush(Color.Black), startX + 430, startY + Offset);
                graphics.DrawString(item.rate.ToString(), new Font("Courier New", 16),
                            new SolidBrush(Color.Black), startX + 430+110, startY + Offset);
                graphics.DrawString(item.amount.ToString(), new Font("Courier New", 16),
                            new SolidBrush(Color.Black), startX + 430 + 110+110, startY + Offset);
                total += item.rate;
                Offset = Offset + 30;                
                graphics.DrawString(underLine, new Font("Courier New", 14),
                            new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 30;
            }
            Offset = Offset + 20;
            String Grosstotal = "Total                                                    " + total;
            graphics.DrawString(Grosstotal, new Font("Courier New", 14),
                        new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 50;            
            graphics.DrawString(underLine, new Font("Courier New", 14),
                        new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString("Thanks, Please visit again", new Font("Courier New", 14),
                        new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Hope you are satisfied", new Font("Courier New", 14),
                        new SolidBrush(Color.Black), startX, startY + Offset);

        }
    }
}