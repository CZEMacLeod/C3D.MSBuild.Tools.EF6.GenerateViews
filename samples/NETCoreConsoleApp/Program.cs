using NETCoreConsoleApp.Models;
using System;

namespace NETCoreConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new SchoolContext())
            {
                var stud = new Student() { StudentName = "Bill" };

                ctx.Students.Add(stud);
                ctx.SaveChanges();

                using (var edmxFile = System.IO.File.OpenWrite(
                    System.IO.Path.ChangeExtension(ctx.GetType().Assembly.Location, ".edmx")))
                {
                    using (var edmx = System.Xml.XmlWriter.Create(edmxFile)) {
                        System.Data.Entity.Infrastructure.EdmxWriter.WriteEdmx(ctx, edmx);
                    }
                }
            }
        }
    }
}
