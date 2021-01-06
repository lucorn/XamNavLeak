using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamNavLeak
{
    public interface IPageLifetime
    {
        void Cleanup();
    }

    public class SpecialNavigationPage : NavigationPage
    {
        public SpecialNavigationPage(Xamarin.Forms.Page content) : base(content)
        {
            Init();
        }

        private void Init()
        {
            Popped += (object sender, NavigationEventArgs e) =>
            {
                var navPage = e.Page as IPageLifetime;

                if (navPage != null)
                {
                    navPage.Cleanup();
                }
                e.Page.BindingContext = null;
            };
        }
    }
}
