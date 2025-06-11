using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MalshinonProject
{
    internal static class FunctionsIfNotIn
    {
        //פונקציה שמבקשת למשתמש להכניס ערכים של המדווח ומכניסה את הערכים לעמודה של האנשים ושל המדווח
        public static  void FunSetPersonReporter()
        {
            Console.WriteLine("The system does not recognize you.");
            Console.WriteLine("enter your first name ");
            Person.FirstName = Console.ReadLine();
            Console.WriteLine("enter your last name ");
            Person.LastName = Console.ReadLine();
            Console.WriteLine("enter a code");
            Person.CodePerson = Console.ReadLine();
            //מכניס את הערכים לטבלה של האנשים
            ConnectToSql.InsertToPerson();
            //מקבל את הid של הperson לid של הreporter
            Reporter.ReporterId = ConnectToSql.getpersonid();
            //מכניס את הערכים לטבלה של המדווח
            ConnectToSql.InsertToReporter(Reporter.ReporterId);
        }
        //פונקציה שמבקשת למשתמש להכניס ערכים של המטרה ומכניסה את הערכים לטבלה של האנשים ושל המטרה
        public static void FunSetPersonTarget()
        {
            Console.WriteLine("The system does not recognize the target.");
            Console.WriteLine("enter the targets first name ");
            Person.FirstName = Console.ReadLine();
            Console.WriteLine("enter the targets last name ");
            Person.LastName = Console.ReadLine();
            Console.WriteLine("enter the targets code");
            Person.CodePerson = Console.ReadLine();
            //מכניס ערכים לטבלה  של האנשים
            ConnectToSql.InsertToPerson();
            //מקבל את הid של הperson לid של הtarget
            Target.TargetId = ConnectToSql.getpersonid();
            //מכניס ערכים לטבלה של המטרה
            ConnectToSql.InsertToTarget(Target.TargetId);
            
        }
    }

    
}
