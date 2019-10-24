using Shipping.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shipping.ViewModels.Orders
{
    public class OrderSearchViewModel
    {
        public OrderSearchViewModel()
        {
            status = new Status();
        }

        public Pager Pager { get; set; }
        public int TotalCount { get; set; }
        public Status status { get; set; }
        public IEnumerable<OrderViewModel> lstOrders { get; set; }
    }
    public class OrderViewModel : BaseViewModel
    {
        public OrderViewModel()
        {
            Remarks = new List<RemarksViewModel>();
            lstOrderStatus = new List<IListEntry>();
        }
        public int OrderId { get; set; }
        public string OrderNo { get; set; }
        public string WayBillNo { get; set; }
        public string CourierName { get; set; }
        [Required(ErrorMessage = "Current Status Required")]
        public Nullable<int> CurrentStatus { get; set; }
        public string AgentName { get; set; }
        [Required(ErrorMessage = "Consignee Name Required")]
        public string ConsigneeName { get; set; }
        [Required(ErrorMessage = "Consignee Number Required")]
        public string ConsigneeNumber { get; set; }
        [Required(ErrorMessage = "AddressLine1 Required")]
        public string AddressLine1 { get; set; }
        public string AlternatePhoneNo { get; set; }
        public string Post { get; set; }
        public string Taluk { get; set; }
        [Required(ErrorMessage = "District Required")]
        public string District { get; set; }

        [Required(ErrorMessage = "Pincode Required")]
        public string Pincode { get; set; }
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "Product Required")]
        public string Product { get; set; }
        [Required(ErrorMessage = "Amount Required")]
        public string Amount { get; set; }
        public Nullable<int> UID { get; set; }
        public Nullable<System.DateTime> UIDDate { get; set; }
        public Nullable<int> EID { get; set; }
        public Nullable<System.DateTime> EIDDate { get; set; }
        public System.DateTime Date { get; set; }
        public string UserName { get; set; }
        public string UserEmailAddress { get; set; }
        public string ImageUrl { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> RoleId { get; set; }
        public string CurrentStatusValue { get; set; }
        public List<RemarksViewModel> Remarks { get; set; }
        public IEnumerable<IListEntry> lstOrderStatus { get; set; }
        public IEnumerable<IListEntry> lstDistricts { get; set; }
        public IEnumerable<IListEntry> lstProducts { get; set; }
        public string Comment { get; set; }

    }
    public class RemarksViewModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Comment { get; set; }
        public Nullable<int> UID { get; set; }
        public Nullable<System.DateTime> UIDDate { get; set; }
        public Nullable<int> EID { get; set; }
        public Nullable<System.DateTime> EIDDate { get; set; }
    }
    public class Status
    {
        public int totalOrders { get; set; }
        public int FreshOrders { get; set; }
        public int InProgress { get; set; }
        public int OutForDelivery { get; set; }
        public int Delivered { get; set; }
        public int Returned { get; set; }
    }

    public class ExportOrderViewModel 
    {
        public System.DateTime Date { get; set; }
        public string OrderNo { get; set; }
        public string WayBillNo { get; set; }
        public string CourierName { get; set; }
        [Required(ErrorMessage = "Current Status Required")]
        public string CurrentStatusValue { get; set; }
        public string AgentName { get; set; }
        [Required(ErrorMessage = "Consignee Name Required")]
        public string ConsigneeName { get; set; }
        [Required(ErrorMessage = "Consignee Number Required")]
        public string ConsigneeNumber { get; set; }
        [Required(ErrorMessage = "AddressLine1 Required")]
        public string AddressLine1 { get; set; }
        public string AlternatePhoneNo { get; set; }
        public string Post { get; set; }
        public string Taluk { get; set; }
        [Required(ErrorMessage = "District Required")]
        public string District { get; set; }

        [Required(ErrorMessage = "Pincode Required")]
        public string Pincode { get; set; }
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "Product Required")]
        public string Product { get; set; }
        [Required(ErrorMessage = "Amount Required")]
        public string Amount { get; set; }
        public string Comment { get; set; }
        public int OrderId { get; set; }
        public Nullable<int> CurrentStatus { get; set; }
    }
}