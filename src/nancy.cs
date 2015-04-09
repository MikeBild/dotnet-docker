using System;
using System.Linq;
using Nancy;
using Nancy.Hosting.Self;
using LiteDB;

namespace Mike.MonoTest
{
	public class Customer
	{
	    public int Id { get; set; }
	    public string Name { get; set; }
	    public string[] Phones { get; set; }
	    public bool IsActive { get; set; }
	}

	public class SampleModule : Nancy.NancyModule
	{
	    public SampleModule()
	    {
	        Get["/"] = _ => {
	        	using(var db = new LiteDatabase(@"MyData.db"))
				{
					var col = db.GetCollection<Customer>("customers");
					col.EnsureIndex(x => x.Name);
					return col.Find(x => x.Name.StartsWith("Jo")).ToArray();
	        	}
	        };

	        Post["/"] = _ => {
				using(var db = new LiteDatabase(@"MyData.db"))
				{
					var col = db.GetCollection<Customer>("customers");
					var customer = new Customer
					{ 
					    Name = "John Doe", 
					    Phones = new string[] { "8000-0000", "9000-0000" }, 
					    IsActive = true
					};

					col.Insert(customer);
					return HttpStatusCode.Created;
				}
	        };
	    }
	}

    public class Program
    {
        public static void Main()
        {
            using (var host = new NancyHost(new Uri("http://localhost:8080")))
			{
			   Console.WriteLine("Nancy host (http://localhost:8080) started.");
			   host.Start();
			   Console.ReadLine();
			}
        }
    }
}