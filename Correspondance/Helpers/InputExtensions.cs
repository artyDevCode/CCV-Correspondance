using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Reflection;

namespace CCVCorrespondance.Helpers
{
    public static class InputExtensions
    {
        public static IHtmlString RichTextAreaFor(this HtmlHelper helper, string name, string text = "")
        {
            return new MvcHtmlString(string.Format("<textarea name='{0}' id='{1}'>{2}</textarea>", name, name, text));
        }

        public static IHtmlString DisplayCorrespondanceImage(this HtmlHelper helper, string name)
        {
            if (name == "Received")
            {
                return new MvcHtmlString(string.Format("<table border='0' cellspacing='0' cellpadding='4' style='font-family: Arial; font-size: 11pt; background-color: gainsboro; width: 100%;'> <tr> <td> <input type='image' class='CorrespondanceReceived' style='height: 78px; width: 100%'/> </td> </tr> </table>"));
                //return new MvcHtmlString(string.Format("<table border='0' cellspacing='0' cellpadding='4' style='font-family: Arial; font-size: 11pt; background-color: gainsboro; width: 100%;'> <tr> <td> <img alt='' src='/content/images/correspondancereceived.gif' style='height: 78px; width: 100%' /> </td> </tr> </table>"));
            }
            else if (name == "Sent")
            {
                return new MvcHtmlString(string.Format("<table border='0' cellspacing='0' cellpadding='4' style='font-family: Arial; font-size: 11pt; background-color: gainsboro; width: 100%;'> <tr> <td> <input type='image' class='CorrespondanceSent' style='height: 78px; width: 100%'/> </td> </tr> </table>"));
                //return new MvcHtmlString(string.Format("<table border='0' cellspacing='0' cellpadding='4' style='font-family: Arial; font-size: 11pt; background-color: gainsboro; width: 100%;'> <tr> <td> <img alt='' src='/content/images/correspondancesent.gif' style='height: 78px; width: 100%' /> </td> </tr> </table>"));
            }
            else if (name == "Delete")
            {
                return new MvcHtmlString(string.Format("<table border='0' cellspacing='0' cellpadding='4' style='font-family: Arial; font-size: 11pt; background-color: gainsboro; width: 100%;'> <tr> <td> <input type='image' class='CorrespondanceDelete' style='height: 78px; width: 100%'/> </td> </tr> </table>"));
                //return new MvcHtmlString(string.Format("<table border='0' cellspacing='0' cellpadding='4' style='font-family: Arial; font-size: 11pt; background-color: gainsboro; width: 100%;'> <tr> <td> <img alt='' src='/content/images/correspondancedelete.gif' style='height: 78px; width: 100%' /> </td> </tr> </table>"));
            }
            else
            {
                return new MvcHtmlString(string.Format("<table border='0' cellspacing='0' cellpadding='4' style='font-family: Arial; font-size: 11pt; background-color: gainsboro; width: 100%;'> <tr> <td> <input type='image' class='CorrespondanceReceived' style='height: 78px; width: 100%'/> </td> </tr> </table>"));
            }
        }

        public static IHtmlString DatePickerAreaFor(this HtmlHelper helper, string name, object text)
        {
            //    <input type="text" id="datepicker" name="DateOnLetter" value=@Model.CorrespondanceDateOnLetter style="width: 98px;" />
            //
            return new MvcHtmlString(string.Format("<input type='datetime' id='datepicker' name='{0}' value='{1}' style='width: 98px;'", name, name, text.ToString()));
        }

        public static string DatePicker(this HtmlHelper helper, string name)
        {
            return helper.DatePicker(name, null);
        }

        public static string DatePicker(this HtmlHelper helper, string name, string imageUrl)
        {
            return helper.DatePicker(name, imageUrl, null);
        }

        public static string DatePicker(this HtmlHelper helper, string name, object date)
        {
            return helper.DatePicker(name, "/Content/Images/calendar.gif", date);
        }

        public static string DatePicker(this HtmlHelper helper, string name, string imageUrl, object date)
        {
            StringBuilder html = new StringBuilder();

            // Build our base input element
            html.Append("<input type=\"text\" id=\"" + name + "\" name=\"" + name + "\"");

            // Model Binding Support
            if (date != null)
            {
                string dateValue = String.Empty;

                if (date is DateTime? && ((DateTime)date) != DateTime.MinValue)
                    dateValue = ((DateTime)date).ToShortDateString();
                else if (date is DateTime && (DateTime)date != DateTime.MinValue)
                    dateValue = ((DateTime)date).ToShortDateString();
                else if (date is string)
                    dateValue = (string)date;

                html.Append(" value=\"" + dateValue + "\"");
            }

            // We're hard-coding the width here, a better option would be to pass in html attributes and reflect through them
            // here ( default to 75px width if no style attributes )
            html.Append(" style=\"width: 75px;\" />");

            // Now we call the datepicker function, passing in our options.  Again, a future enhancement would be to
            // pass in date options as a list of attributes ( min dates, day/month/year formats, etc. )
            html.Append("<script type=\"text/javascript\">$(document).ready(function() { $('#" + name + "').datepicker({ showOn: 'button', buttonImage: '" + imageUrl + "', duration: 0 }); });</script>");

            return html.ToString();
        }

    }

    //[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    //public class MultipleButtonAttribute : ActionNameSelectorAttribute
    //{
    //    public string Name { get; set; }
    //    public string Argument { get; set; }

    //    public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
    //    {
    //        var isValidName = false;
    //        var keyValue = string.Format("{0}:{1}", Name, Argument);
    //        var value = controllerContext.Controller.ValueProvider.GetValue(keyValue);

    //        if (value != null)
    //        {
    //            controllerContext.Controller.ControllerContext.RouteData.Values[Name] = Argument;
    //            isValidName = true;
    //        }

    //        return isValidName;
    //    }
    //}
}