using MobileAppColeBright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971ColeBright
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoursePage1 : ContentPage
    {
        public CoursePage1()
        {
            InitializeComponent();
            tname.Text = App.CurrentTerm.TermTitle;
            tstart.Date = App.CurrentTerm.StartDate;
            tend.Date = App.CurrentTerm.EndDate;
            courseListView.ItemsSource = App.CurrentTerm.AssociatedCourse;
            courseListView.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            {
                //App.CurrentCourse.Id = e.SelectedItemIndex;
                BackgroundColor = Color.CornflowerBlue;
                Course SelectedCourse = (Course)e.SelectedItem;
                App.CurrentCourse = SelectedCourse;
                Navigation.PushAsync(new AssessmentPage());
            };
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DB_PATH))
            {
                conn.CreateTable<Course>();
                var courses = conn.Query<Course>($"SELECT * FROM Courses WHERE TermId = '{ App.CurrentTerm.Id}'");
                courseListView.ItemsSource = courses;
            }
        }

        private void ToolbarItem_Activated(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewCourse());
        }

        private void ToolbarItem_Activated_1(object sender, EventArgs e)
        {
            tname.IsEnabled = true;
            tstart.IsEnabled = true;
            tend.IsEnabled = true;
            editbtn.IsVisible = true;
        }

        private void editbtn_Clicked(object sender, EventArgs e)
        {
            if (tstart.Date < tend.Date)
            {
                if (tname.Text != "")
                {
                    Term term = new Term()
                    {
                        TermTitle = tname.Text,
                        StartDate = tstart.Date,
                        EndDate = tend.Date
                    };
                    using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DB_PATH))
                    {
                        conn.CreateTable<Term>();
                        //var numOfrows = 
                        conn.Update(term);
                    }
                }
                else
                {
                    DisplayAlert("Uh-oh", "You have to put a title for the term.", "OK");
                }
            }
            else
            {
                DisplayAlert("Uh-oh", "The end date can't be before the start date.", "Ok");
            }
        }
    }
}