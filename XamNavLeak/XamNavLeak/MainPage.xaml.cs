using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamNavLeak
{
    class PageRef
    {
        public WeakReference Ref;
        public int PageId;
    }

    public partial class MainPage : ContentPage, IPageLifetime
    {
        private int LastPageId = 0;

        private List<PageRef> WeakRefs;

        public MainPage()
        {
            InitializeComponent();

            WeakRefs = new List<PageRef>();
        }

        private void GoToChildPage(object sender, EventArgs e)
        {
            var pageId = LastPageId++;
            var page = new SecondPage(pageId);
            var weakRef = new WeakReference(page);

            var pageRef = new PageRef { Ref = weakRef, PageId = pageId };

            WeakRefs.Add(pageRef);
            
            Navigation.PushAsync(page);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Debug.WriteLine($"{Navigation.NavigationStack.Count} pages in the navigation stack");
        }

        public void Cleanup()
        {
            // nothing here
        }

        private void ListPageRefs()
        {
            for (int i = 0; i < WeakRefs.Count; i++)
            {
                PageRef pr = WeakRefs[i];

                if (pr.Ref.IsAlive)
                {
                    Debug.WriteLine($"Page {pr.PageId} is alive");
                }
                else
                {
                    Debug.WriteLine($"Page {pr.PageId} has been released!");
                }
            }
        }

        private void GarbageCollect(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(1000);

                GC.Collect();
                GC.WaitForPendingFinalizers();

                await Task.Delay(1000);

                ListPageRefs();
            });
        }
    }
}
