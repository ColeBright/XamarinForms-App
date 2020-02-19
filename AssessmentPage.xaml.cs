using MobileAppColeBright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;



using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Globalization;

namespace C971ColeBright
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssessmentPage : ContentPage
    {
        public AssessmentPage()
        {
            InitializeComponent();
            cname.Text = App.CurrentCourse.Name;
            cstart.Date = App.CurrentCourse.startDate;
            cend.Date = App.CurrentCourse.endDate;
            cstatus.SelectedItem = App.CurrentCourse.Status;
            cinstname.Text = App.CurrentCourse.courseInsName;
            cinstemail.Text = App.CurrentCourse.courseInsEmail;
            cinstphone.Text = App.CurrentCourse.courseInsPhone;


        }                                                                                                                                    
        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DB_PATH))
            {
                conn.CreateTable<Assessment>();
                var assessments = conn.Query<Assessment>($"SELECT * FROM Assessments WHERE CourseId  = '{ App.CurrentCourse.Id}'");
                assessmentListView.ItemsSource = assessments;

            }

        }

        private void ToolbarItem_Activated(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewAssessment());
        }

        private void ToolbarItem_Activated_1(object sender, EventArgs e)
        {
            cname.IsEnabled = true;
            cstart.IsEnabled = true;
            cend.IsEnabled = true;
            cstatus.IsEnabled = true;
            cinstname.IsEnabled = true;
            cinstemail.IsEnabled = true;
            cinstphone.IsEnabled = true;
            cnotes.IsEnabled = true;
            editbtn.IsVisible = true;
        }

        private void editbtn_Clicked(object sender, EventArgs e)
        {
            Course course = new Course()
            {
                Name = cname.Text,
                startDate = cstart.Date,
                endDate = cend.Date,
                Status = cstatus.SelectedItem.ToString(),
                courseInsName = cinstname.Text,
                courseInsEmail = cinstemail.Text,
                courseInsPhone = cinstphone.Text,
                Notes = cnotes.Text 
                
            };
            if(string.IsNullOrEmpty(cname.Text) || string.IsNullOrEmpty(cinstname.Text) 
                || string.IsNullOrEmpty(cinstphone.Text))
            {
                DisplayAlert("Oops, something is wrong.", "You must fill in all entries.", "OK");
            }
            else
            {
                if (course.IsValidEmail(cinstemail.Text))
                {
                    using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DB_PATH))
                    {
                        conn.CreateTable<Course>();
                        //var numOfrows = 
                        conn.Update(course);
                    }
                }
                else {
                    DisplayAlert("Uh oh", "Invalid email, please try again.", "OK");
                
                }

            }
             
            }
        }
        
    }


