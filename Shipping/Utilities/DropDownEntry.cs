using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shipping.Utilities
{
    public class DropDownEntry : IListEntry
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}