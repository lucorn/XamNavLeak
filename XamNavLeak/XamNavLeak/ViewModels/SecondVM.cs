using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamNavLeak.ViewModels
{
    public class SecondVM : INotifyPropertyChanged
    {
        #region PropertyChange
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion PropertyChange

        #region Title
        private string _Title;
        public string Title
        {
            get { return _Title; }
            set { if (_Title != value) { _Title = value; NotifyPropertyChanged(); } }
        }
        #endregion Title

        public ICommand DoStuff { protected set; get; }

        private int PageId;
        private CoolChart CoolChart;
        public SecondVM(int pageId, CoolChart chart)
        {
            PageId = pageId;
            CoolChart = chart;

            CoolChart.PaintSurface += PaintCoolChart;
            CoolChart.CallBack += CoolChart_CallBack;

            Title = "Second page";

            Debug.WriteLine($"ViewModel for page {PageId} created");

            DoStuff = new Command(() =>
            {
                _DoStuff();
            });
        }

        ~SecondVM()
        {
            Debug.WriteLine($"ViewModel for page {PageId} DESTROYED!");
        }

        private void CoolChart_CallBack()
        {
            Debug.WriteLine($"CoolChart called back!");
        }

        public void Cleanup()
        {
            CoolChart.CallBack -= CoolChart_CallBack;
            CoolChart.PaintSurface -= PaintCoolChart;
        }


        private void _DoStuff()
        {
            Title = "Did some stuff";
            CoolChart.InvalidateSurface();
        }

        public void PaintCoolChart(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            SKImageInfo info = e.Info;

            canvas.Clear();

            CoolChart.Draw(canvas, info);
        }
    }
}
