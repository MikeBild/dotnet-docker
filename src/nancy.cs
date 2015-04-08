using System;
using Nancy;
using Nancy.Hosting.Self;

namespace Mike.MonoTest
{
	public class SampleModule : Nancy.NancyModule
	{
	    public SampleModule()
	    {
	        Get["/"] = _ => "Hello World!";
	    }
	}

    public class Program
    {
        public static void Main()
        {
            using (var host = new NancyHost(new Uri("http://localhost:8080")))
			{
			   Console.WriteLine("Host started.");
			   host.Start();
			   Console.ReadLine();
			}
        }
    }
}