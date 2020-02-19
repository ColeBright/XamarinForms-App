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
    public partial class NewCourse : ContentPage
    {
        public NewCourse()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Successfully saved!", "Something", "OK");
            Course course = new Course()
            {
                Name = courseName.Text,
                startDate = startdate.Date,
                endDate = enddate.Date,
                Status = status.Items[status.SelectedIndex],
                courseInsName = ciName.Text,
                courseInsEmail =    ciEmail.Text,
                courseInsPhone = ciPhone.Text,
                Notes = cnotes.Text
                
                
            };
            course.TermId = App.CurrentTerm.Id;
            App.CurrentTerm.AssociatedCourse.Add(course);
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DB_PATH))
            {
                conn.CreateTable<Course>();
                var numOfrows = conn.Insert(course);
                //current term.AssociatedCourse.add(course);
                if (numOfrows > 0)
                    DisplayAlert("Successfully saved!", "Something", "OK");
                else
                    DisplayAlert("Unsuccessfully saved!", "Something", "Shoot!");
            }
   

        }
    }
}