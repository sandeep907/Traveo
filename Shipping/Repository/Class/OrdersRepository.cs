using Shipping.Repository.Interface;
using Shipping.Utilities;
using System;
using System.Linq;

namespace Shipping.Repository.Class
{
    public class OrdersRepository: IOrderRepository
    {
        #region "Initialisation"
        private readonly ShippingEntities _context;
        public OrdersRepository()
        {
            try
            { _context = new ShippingEntities(); }
            catch (Exception ex)
            { var abc = ex.Message; }
        }
        #endregion

        #region "Methods"
        public IQueryable<OrdersView> Getorders()
        {
            return _context.OrdersViews.Where(m=>m.IsDelete != true);
        }
        public OrdersView GetOrderById(int id)
        {
            return _context.OrdersViews.Where(m => m.OrderId == id && m.IsDelete != true).FirstOrDefault();
        }
        public OrdersView GetOrderById(int id,int userId)
        {
            return _context.OrdersViews.Where(m => m.OrderId == id && m.EID== userId && m.CurrentStatus == 1 && m.IsDelete != true).FirstOrDefault();
        }
        public OrdersView GetOrderStatusById(int id, int userId)
        {
            return _context.OrdersViews.Where(m => m.OrderId == id && m.EID == userId && m.IsDelete != true).FirstOrDefault();
        }

        public OrdersView GetOrderStatusByConsigneeNo(string consigneeNo)
        {
            return _context.OrdersViews.Where(m => m.ConsigneeNumber== consigneeNo && m.IsDelete != true).FirstOrDefault();
        }
        public ProcResult AddOrder(Order order,Remark remark)
        {
            ProcResult res = new ProcResult();
            try
            {
               var result= _context.Orders.Add(order);
                _context.SaveChanges();
                if (remark.Comment != null)
                {
                    remark.OrderId = result.OrderId;
                    _context.Remarks.Add(remark);
                }
                _context.SaveChanges();
                res.ErrorID = 0;
                res.strResult = "Order Added Sucessfully";
            }
            catch
            {
                res.ErrorID = 1;
                res.strResult = "Order Add Failed";
            }
            return res;
        }
        public ProcResult UpdateOrder(Order order, Remark remark)
        {
            ProcResult res = new ProcResult();
            Order obj = new Order();
            try
            {
                obj = _context.Orders.Find(order.OrderId);
                obj.AddressLine1 = order.AddressLine1;
                obj.AlternatePhoneNo = order.AlternatePhoneNo;
                obj.Amount = order.Amount;
                obj.ConsigneeName = order.ConsigneeName;
                obj.ConsigneeNumber = order.ConsigneeNumber;
                obj.CourierName = order.CourierName;
                obj.CurrentStatus = order.CurrentStatus;
                obj.District = order.District;
                obj.MobileNo = order.MobileNo;
                obj.OrderId = order.OrderId;
                obj.OrderNo = order.OrderNo;
                obj.Pincode = order.Pincode;
                obj.Post = order.Post;
                obj.Product = order.Product;
                obj.Taluk = order.Product;
                obj.UID = order.UID;
                obj.UIDDate = order.UIDDate;
                obj.WayBillNo = order.WayBillNo;
                _context.SaveChanges();
                if (remark.Comment != null)
                {
                    _context.Remarks.Add(remark);
                    _context.SaveChanges();
                }
                res.ErrorID = 0;
                res.strResult = "Order Updated Sucessfully";
            }
            catch
            {
                res.ErrorID = 1;
                res.strResult = "Order Update Failed";
            }
            return res;
        }
        public ProcResult DeleteOrder(int Id)
        {
            ProcResult res = new ProcResult();
            Order obj = new Order();
            try
            {
                obj = _context.Orders.Find(Id);
                obj.IsDelete = true;
                _context.SaveChanges();
                res.ErrorID = 0;
                res.strResult = "Order Deleted Sucessfully";
            }
            catch
            {
                res.ErrorID = 1;
                res.strResult = "Order Deleted Failed";
            }
            return res;
        }
        #endregion

    }
}