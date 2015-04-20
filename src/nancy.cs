using System;
using Nancy;
using Nancy.Hosting.Self;
//using CronNET;

namespace MikeBild.Booker
{
    class Program
    {
        static void Main()
        {
            //var cron_daemon = new CronDaemon(); 
            //cron_daemon.add_job(new CronJob("* * * * *", Task));
            //cron_daemon.start();

            using (var host = new NancyHost(new Uri("http://localhost:8080")))
			{
			   Console.WriteLine("Nancy host (http://localhost:8080) started.");
			   host.Start();
			   Console.ReadLine();
			}
        }

        static void Task()
        {
          Console.WriteLine("Hello, world.");
        }        
    }
}