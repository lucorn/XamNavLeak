using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace XamNavLeak
{
    public delegate void ChartCallBack();

    public class CoolChart: SKCanvasView
    {
        public event ChartCallBack CallBack;

        public CoolChart()
        {
        }

        public void Cleanup()
        {
            Debug.WriteLine("CoolChart CLEANUP!");
        }

        ~CoolChart()
        {
            Debug.WriteLine("CoolChart DESTROYED!");
        }

        public void Draw(SKCanvas canvas, SKImageInfo info)
        {
            SKRect bkgrnd = new SKRect { Left = 0, Top = 0, Right = info.Width, Bottom = info.Height };

            using (var paint = new SKPaint() { Style = SKPaintStyle.StrokeAndFill, Color = SKColors.Yellow, StrokeWidth = 1 })
            {
                canvas.DrawRect(bkgrnd, paint);
            }

            CallBack?.Invoke();
        }
    }
}
