using MYMA.GrpcDataStore.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MYMA.BlazerApp.RehmanSchool.Pages
{
    public partial class StudentsList
    {
        public StudentsList()
        {
            Students = new List<Student>();
        }

        protected override Task OnInitializedAsync()
        {
            InitializeStudents();
            return base.OnInitializedAsync();
        }
        public IEnumerable<Student> Students
        {
            get; set;
        }
        private void InitializeStudents()
        {
            //Students = StudentDataStore.GetItemsAsync().Result;
            Students = new List<Student>() { StudentDataStore.GetItemAsync("189958b6-7909-46f6-807c-7b2a2a4f3b42").Result };
        }
    }
}
