using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Shipping.Utilities
{
    public class SessionWrapper
    {
        /// <summary>
        /// Gets Assign HttpSessionState 
        /// </summary>
        private static HttpSessionState Session
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    throw new ApplicationException("No Http Context, No Session to Get!");
                }

                return HttpContext.Current.Session;
            }
        }

        /// <summary>
        /// Gets the sesion value according to type
        /// </summary>
        /// <typeparam name="T">object of class</typeparam>
        /// <param name="key">key of session</param>
        /// <returns>object of values assign in session</returns>
        public static T Get<T>(string key)
        {
            if (Session[key] == null)
            {
                return default(T);
            }
            else
            {
                return (T)Session[key];
            }
        }

        /// <summary>
        /// Set the session value 
        /// </summary>
        /// <typeparam name="T">object of class</typeparam>
        /// <param name="key">key of session</param>
        /// <param name="value">value of session</param>
        public static void Set<T>(string key, T value)
        {
            Session[key] = value;
        }

        /// <summary>
        /// remove the value of session 
        /// </summary>
        /// <typeparam name="T">object of class</typeparam>
        /// <param name="key">key of sesion</param>
        public static void Remove<T>(string key)
        {
            if (Session[key] != null)
            {
                Session.Remove(key);
            }
        }
    }
}
