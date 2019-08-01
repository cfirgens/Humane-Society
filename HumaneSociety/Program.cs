using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    class Program
    {
        static void Main(string[] args)
        {
            //HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            //var tjing = db.Animals.Where(s => s.AnimalId == 1).Single();
            //db.Employees.InsertOnSubmit(new Employee { FirstName = "Mike", EmployeeNumber = 10005 });
            //db.SubmitChanges();
            PointOfEntry.Run();
        }
    }
}
