﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCVCorrespondance.Helpers
{
    static class HideNameFor
    {
        //        using System;
        //using System.Web.Mvc;

        //namespace MvcApplication1.Helpers
        //{
        //public static class LabelExtensions
        //{
        public static string Label(this HtmlHelper helper, string target, string text)
        {

            return String.Format("<label for='{0}'>{1}</label>", target, text);

        }
        //     }
        //}

    }
}