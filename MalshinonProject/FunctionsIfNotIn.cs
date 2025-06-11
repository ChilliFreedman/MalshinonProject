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
            Console.WriteLine("The system does not recognize you.");
            Console.WriteLine("enter your first name ");
            Person.FirstName = Console.ReadLine();
            Console.WriteLine("enter your last name ");
            Person.LastName = Console.ReadLine();
            Console.WriteLine("enter a code");
            Person.CodePerson = Console.ReadLine();
            ConnectToSql.InsertToPerson();
            Reporter.ReporterId = ConnectToSql.getpersonid();
            //Console.WriteLine(Reporter.ReporterId);
            ConnectToSql.InsertToReporter(Reporter.ReporterId);
        }
        public static void FunSetPersonTarget()
        {
            Console.WriteLine("The system does not recognize the target.");
            Console.WriteLine("enter the targets first name ");
            Person.FirstName = Console.ReadLine();
            Console.WriteLine("enter the targets last name ");
            Person.LastName = Console.ReadLine();
            Console.WriteLine("enter the targets code");
            Person.CodePerson = Console.ReadLine();
            ConnectToSql.InsertToPerson();
            Target.TargetId = ConnectToSql.getpersonid();
            ConnectToSql.InsertToTarget(Target.TargetId);
        }
    }

    
}
