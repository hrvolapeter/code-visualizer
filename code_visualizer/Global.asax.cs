using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;


namespace code_visualizer
{
	public class Global : HttpApplication
	{
		protected void Application_Start()
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);
		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{
            if (!Request.Path.StartsWith("/api"))
            {
                if (!Request.Path.Contains("js") && !Request.Path.Contains("css") && !Request.Path.Contains("html"))
                {
                    Context.RewritePath("/angular/app/index.html" + Request.Path.Substring(1));
                    return;
                }
                Context.RewritePath("/angular/app" + Request.Path);
			}
		}
	}
}
