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
    public partial class NewTerm : ContentPage
    {
        public NewTerm()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (startDate.Date < endDate.Date)
            {
                if (termTitle.Text != "")
                {
                    Term term = new Term()
                    {
                        TermTitle = termTitle.Text,
                        StartDate = startDate.Date,
                        EndDate = endDate.Date
                    };
                    using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DB_PATH))
                    {
                        conn.CreateTable<Term>();
                        //var numOfrows = 
                        conn.Insert(term);
                        Navigation.PopAsync();
                        //if (numOfrows > 0)
                        //    DisplayAlert("Successfully saved!", "Something", "OK");
                        //else
                        //    DisplayAlert("Unsuccessfully saved!", "Something", "Shoot!");
                    }
                }
                else
                {
                    DisplayAlert("Uh-oh", "You have to put a title for the term.", "OK");
                }
            }
            else
            {
                DisplayAlert("Uh-oh","The end date can't be before the start date.", "Ok");
            }
        }
    }
}