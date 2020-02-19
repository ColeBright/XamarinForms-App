using MobileAppColeBright;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.LocalNotifications;
using System.Collections.ObjectModel;


namespace C971ColeBright
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
       public ObservableCollection<Term> TermList;
       
        
        
        public MainPage()
        {
            InitializeComponent();
            termListView.ItemTapped += new EventHandler<ItemTappedEventArgs>(Term_Clicked);
            //termListView.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            //{
            //    App.CurrentTerm.Id = e.SelectedItemIndex;
            //    BackgroundColor = Color.CornflowerBlue;
            //    Term SelectedTerm = (Term)e.SelectedItem;
            //    Navigation.PushAsync(new CoursePage1());
            //};
        }

        private void Term_Clicked(object sender, ItemTappedEventArgs e)
        {
            Term SelectedTerm = (Term)e.Item;
            App.CurrentTerm = SelectedTerm;
                Navigation.PushAsync(new CoursePage1());
            
        }

        protected override void OnAppearing()
        {

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DB_PATH))
            {
                conn.CreateTable<Term>();
                conn.CreateTable<Course>();
                conn.CreateTable<Assessment>();

                List<Term> terms = conn.Table<Term>().ToList();

                if (!terms.Any())
                {
                    Term myTerm = new Term();
                    myTerm.TermTitle = "Term 1";
                    myTerm.StartDate = new DateTime(2020, 1, 16);
                    myTerm.EndDate = new DateTime(2020, 5, 16);
                    conn.Insert(myTerm);
                    terms.Add(myTerm);
                    //TermList.Add(myTerm);

                    Course myCourse = new Course();
                    myCourse.Name = "Mobile Application With C#";
                    myCourse.startDate = new DateTime(2020, 1, 16);
                    myCourse.endDate = new DateTime(2020, 5, 16);
                    myCourse.Status = "In Progress";
                    myCourse.courseInsName = "Cole Bright";
                    myCourse.courseInsPhone = "773-355-0112";
                    myCourse.courseInsEmail = "cbrig32@wgu.edu";
                    myCourse.Notes = "Nothing here, yet.";
                    myCourse.TermId = myTerm.Id;
                    conn.Insert(myCourse);

                    Assessment myObjAssessment = new Assessment();
                    myObjAssessment.name = "Test 1";
                    myObjAssessment.dueDate = new DateTime(2020, 2, 28);
                    //newObjAssessment.EndDate = new DateTime(2020, 3, 1);
                    myObjAssessment.CourseId = myCourse.Id;
                    myObjAssessment.type = "Objective";
                    conn.Insert(myObjAssessment);

                    Assessment myPerfAssessment = new Assessment();
                    myPerfAssessment.name = "Test 2";
                    myPerfAssessment.dueDate = new DateTime(2020, 3, 2);
                    //newPerfAssessment.EndDate = new DateTime(2020, 3, 3);
                    myPerfAssessment.CourseId = myCourse.Id;
                    myPerfAssessment.type = "Performance";
                    conn.Insert(myPerfAssessment);
                    //myTerm.AssociatedCourse = new List<Course>(myCourse);

                }
                List<Assessment> aList = conn.Table<Assessment>().ToList();
                List<Course> cList = conn.Table<Course>().ToList();
                foreach (Course course in cList)
                {
                    if (course.startDate == DateTime.Today)
                    {
                        CrossLocalNotifications.Current.Show("{0} starts today!", course.Name);
                    }
                    else if (course.endDate == DateTime.Today)
                    {
                        CrossLocalNotifications.Current.Show("{0} ends today!", course.Name);
                    }
                }
                foreach (Assessment assessment in aList)
                {
                    if (assessment.dueDate == DateTime.Today)
                    {
                        CrossLocalNotifications.Current.Show("{0} is today!", assessment.name);
                    }
                }

                TermList = new ObservableCollection<Term>(terms);
                termListView.ItemsSource = TermList;
            }

            base.OnAppearing();

        }


        private void ToolbarItem_Activated(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewTerm());
        }
    }


}
