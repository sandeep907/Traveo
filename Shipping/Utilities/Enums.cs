using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shipping.Utilities
{
    public class Enums
    {
        public enum MessageType
        {
            Success,
            Error
        }
        public enum UserType
        {
            Admin = 1,
            DataEntry = 2,
            Employees = 3
        }
        public enum Flag
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Activate = 4
        }


    }
}