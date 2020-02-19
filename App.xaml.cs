using MobileAppColeBright;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971ColeBright
{
    public partial class App : Application
    {
        public static Term CurrentTerm;
        public static Course CurrentCourse;
        public static Assessment CurrentAssessment;
        public static String DB_PATH = String.Empty;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }
        public App(String DB_Path)
        {
            InitializeComponent();

            DB_PATH = DB_Path;

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
