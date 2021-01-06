using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamNavLeak
{
    public class CoolChart: SKCanvasView
    {
        private static SKPaint _boardBackgroundPaint;

        public CoolChart()
        {
            _boardBackgroundPaint = new SKPaint
            {
                Style = SKPaintStyle.StrokeAndFill,
                Color = SKColors.Yellow,
                StrokeWidth = 1
            };

        }

        public void Draw(SKCanvas canvas, SKImageInfo info)
        {
            SKRect bkgrnd = new SKRect { Left = 0, Top = 0, Right = info.Width, Bottom = info.Height };

            canvas.DrawRect(bkgrnd, _boardBackgroundPaint);
        }
    }
}
