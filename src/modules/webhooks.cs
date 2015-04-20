using System;
using System.Linq;
using LiteDB;
using Nancy;

namespace MikeBild.Booker
{
	public class WebHook
	{
		public int Id { get; set; }
		public string Url { get; set; }
	}

	public class WebHookModule : Nancy.NancyModule
	{
	    public WebHookModule() : base("/webhooks")
	    {
			Get["/views"] = _ => {
			    return View["done.html", new { Foo = "bar" }];
			};

	        Get["/"] = _ => {
	        	using(var db = new LiteDatabase(@"MyData.db"))
				{
					return db.GetCollection<WebHook>("webhooks")
						.FindAll()
						.ToArray();
	        	}
	        };

	        Post["/"] = _ => {
				using(var db = new LiteDatabase(@"MyData.db"))
				{
					var col = db.GetCollection<WebHook>("webhooks");
					var WebHook = new WebHook
					{ 
					};

					col.Insert(WebHook);
					return HttpStatusCode.Created;
				}
	        };

	        Delete["/"] = _ => {
	        	return HttpStatusCode.Accepted;
	        };
	    }
	}
}