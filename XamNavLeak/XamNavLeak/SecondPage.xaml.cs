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

            ViewModel = new SecondVM(PageId, coolChart);

            BindingContext = ViewModel;
        }

        public void Cleanup()
        {
            ViewModel.Cleanup();
            ViewModel = null;
        }

        ~SecondPage()
        {
            Debug.WriteLine($"Page {PageId} destroyed");
        }
    }
}