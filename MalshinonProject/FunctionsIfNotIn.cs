using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MalshinonProject
{
    internal static class FunctionsIfNotIn
    {
        public static  void FunSetPersonReporter()
        {
            Console.WriteLine("enter your first name ");
            Person.FirstName = Console.ReadLine();
            Console.WriteLine("enter your last name ");
            Person.LastName = Console.ReadLine();
            Console.WriteLine("enter a code");
            Person.CodePerson = Console.ReadLine();
            ConnectToSql.InsertToPerson();
            Reporter.ReporterId = ConnectToSql.getpersonid(Person.CodePerson);
            ConnectToSql.InsertToReporter(Reporter.ReporterId);
        }
        public static void FunSetPersonTarget()
        {
            Console.WriteLine("enter your first name ");
            Person.FirstName = Console.ReadLine();
            Console.WriteLine("enter your last name ");
            Person.LastName = Console.ReadLine();
            Console.WriteLine("enter a code");
            Person.CodePerson = Console.ReadLine();
            Target.TargetId = ConnectToSql.getpersonid(Person.CodePerson);
            ConnectToSql.InsertToTarget(Target.TargetId);
        }
    }

    
}
