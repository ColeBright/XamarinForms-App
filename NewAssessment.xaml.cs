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
    public partial class NewAssessment : ContentPage
    {
        
        public NewAssessment()
        {
            InitializeComponent();
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            Assessment assessment = new Assessment()
            {
                name = assessmentName.Text,
                type = assessmentType.Items[assessmentType.SelectedIndex],
                dueDate = duedate.Date       
            };
            App.CurrentCourse.AssociatedAssessments.Add(assessment);
            
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DB_PATH))
            {
                conn.CreateTable<Assessment>();
                var numOfrows = conn.Insert(assessment);
                //current course.AssociatedAssessment.add(assessment);
               
                if (numOfrows > 0)
                    DisplayAlert("Successfully saved!", "Something", "OK");
                else
                    DisplayAlert("Unsuccessfully saved!", "Something", "Shoot!");
            }
            
        }
    }
}