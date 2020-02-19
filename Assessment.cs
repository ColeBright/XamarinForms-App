using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MobileAppColeBright
{
    [Table("Assessments")]
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public DateTime dueDate { get; set; }
        [ForeignKey(typeof(Course))]
        public int CourseId { get; set; }
    }
}
