using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace CCVCorrespondance.Helpers
{
public class AppConfiguration
{
	    public AppConfiguration()
	    {
            this.AdminGroupName = System.Configuration.ConfigurationManager.AppSettings["AdminGroupName"];
            this.ReadOnlyGroupName = System.Configuration.ConfigurationManager.AppSettings["ReadOnlyGroupName"];
            this.DeleteGroupName = System.Configuration.ConfigurationManager.AppSettings["DeleteGroupName"];
	    }
    
    	public string ReadOnlyGroupName { get; private set; }
        public string AdminGroupName { get; private set; }
        public string DeleteGroupName { get; private set; }
}
}