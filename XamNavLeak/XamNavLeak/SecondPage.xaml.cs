using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamNavLeak.ViewModels;

namespace XamNavLeak
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SecondPage : ContentPage, IPageLifetime
    {
        private int PageId;
        private SecondVM ViewModel;
        public SecondPage(int pageId)
        {
            PageId = pageId;
            InitializeComponent();

            Debug.WriteLine($"Page {PageId} created");

            var coolChart = new CoolChart();

            coolchartContainer.Content = coolChart;

            ViewModel = new SecondVM(PageId, coolChart);

            BindingContext = ViewModel;
        }

        public void Cleanup()
        {
            coolchartContainer.Content = null;
            ViewModel.Cleanup();
            ViewModel = null;
            Debug.WriteLine($"Page {PageId} CLEANUP");
        }

        ~SecondPage()
        {
            Debug.WriteLine($"Page {PageId} DESTROYED");
        }
    }
}