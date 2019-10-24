using Shipping.Service.Interface;
using Shipping.Utilities;
using Shipping.ViewModels.Account;
using Shipping.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Shipping.Utilities.Enums;

namespace Shipping.Controllers
{
    [Authorize(Roles = "1,2")]
    public class OrderController : BaseController
    {
        private readonly IOrderService _iOrderService;
        private readonly IUserService _iUserService;
        private readonly IDataService _iDataService;
        public OrderController(IOrderService IOrderService, IUserService IUserService, IDataService IDataService)
        {
            _iOrderService = IOrderService;
            _iUserService = IUserService;
            _iDataService = IDataService;
        }

        public ActionResult Index(int? page)
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            OrderSearchViewModel model = new OrderSearchViewModel();
            var res = HttpContext.User.Identity.Name;
            var user = _iUserService.GetAll().Where(m => m.UserEmailAddress == res).FirstOrDefault();
            AssignSessionVariables(user);
            try
            {
                model.TotalCount = _iOrderService.GetAll(SessionWrapper.Get<int>(AppConstant.UserId), SessionWrapper.Get<int>(AppConstant.RoleId)).Count();
                var pager = new Pager(model.TotalCount, page);
                model.lstOrders = _iOrderService.GetAll(SessionWrapper.Get<int>(AppConstant.UserId), SessionWrapper.Get<int>(AppConstant.RoleId));
                model.status.FreshOrders = model.lstOrders.Where(m => m.CurrentStatus == 1).Count();
                model.status.InProgress = model.lstOrders.Where(m => m.CurrentStatus == 2).Count();
                model.status.OutForDelivery = model.lstOrders.Where(m => m.CurrentStatus == 3).Count();
                model.status.Delivered = model.lstOrders.Where(m => m.CurrentStatus == 4).Count();
                model.status.Returned = model.lstOrders.Where(m => m.CurrentStatus == 5).Count();
                model.status.totalOrders = model.TotalCount;
                model.lstOrders = model.lstOrders
                    .Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList();
                model.Pager = pager;
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.Message = helper.GenerateMessage(" " + ex.Message, MessageType.Error);
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Search(string orderNo, string consignneName, string agentName, Double? consgnNo, string date, string daterange, int? orderType, int? page, int pageSize)
        {
            if (pageSize > 0 && pageSize <= 10)
            {
                pageSize = 10;
            }
            else if (pageSize > 10 && pageSize <= 50)
            {
                pageSize = 50;

            }
            else if (pageSize > 50 && pageSize <= 100)
            {
                pageSize = 100;
            }
            else if (pageSize > 100 && pageSize <= 500)
            {
                pageSize = 500;
            }
            else if (pageSize > 500 && pageSize <= 1000)
            {
                pageSize = 1000;
            }
            else if (pageSize > 1000 && pageSize <= 10000)
            {
                pageSize = 10000;
            }
            else if (pageSize > 10000 && pageSize <= 100000)
            {
                pageSize = 100000;
            }
            OrderSearchViewModel model = new OrderSearchViewModel();
            string[] dates = daterange.Split('-');
            DateTime from = DateTime.ParseExact(dates[0].TrimEnd(), "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime to = DateTime.ParseExact(dates[1].TrimStart(), "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            model.lstOrders = _iOrderService.GetAll(SessionWrapper.Get<int>(AppConstant.UserId), SessionWrapper.Get<int>(AppConstant.RoleId)).ToList();
            if (orderNo != null && !String.IsNullOrEmpty(orderNo))
            {
                model.lstOrders = model.lstOrders.Where(x => x.OrderNo != null);
                model.lstOrders = model.lstOrders.Where(x => x.OrderNo.ToString().Contains(orderNo));
            }
            if (consignneName != null && !String.IsNullOrEmpty(consignneName))
            {
                model.lstOrders = model.lstOrders.Where(x => x.ConsigneeName.ToString().ToUpper().Contains(consignneName.ToUpper()));
            }
            if (agentName != null && !String.IsNullOrEmpty(agentName))
            {
                model.lstOrders = model.lstOrders.Where(x => x.AgentName.ToString().ToUpper().Contains(agentName.ToUpper()));
            }
            if (consgnNo != null && consgnNo != 0)
            {
                model.lstOrders = model.lstOrders.Where(x => x.ConsigneeNumber.ToString().Contains(consgnNo.ToString()));
            }
            if (date != null && !String.IsNullOrEmpty(date))
            {
                DateTime myDate = DateTime.ParseExact(date, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                model.lstOrders = model.lstOrders.Where(x => x.Date == myDate);
            }

            if (daterange != null && !String.IsNullOrEmpty(daterange) && from != DateTime.Now.Date && to != DateTime.Now.Date)
            {
                model.lstOrders = model.lstOrders.Where(x => x.Date >= from && x.Date <= to);
            }
            model.status.totalOrders = model.lstOrders.Count();
            model.status.FreshOrders = model.lstOrders.Where(m => m.CurrentStatus == 1).Count();
            model.status.InProgress = model.lstOrders.Where(m => m.CurrentStatus == 2).Count();
            model.status.OutForDelivery = model.lstOrders.Where(m => m.CurrentStatus == 3).Count();
            model.status.Delivered = model.lstOrders.Where(m => m.CurrentStatus == 4).Count();
            model.status.Returned = model.lstOrders.Where(m => m.CurrentStatus == 6).Count();
            if (orderType != null)
            {
                model.lstOrders = model.lstOrders.Where(x => x.CurrentStatus == orderType);
            }
            model.TotalCount = model.lstOrders.Count();
            var pager = new Pager(model.TotalCount, page, pageSize);

            model.lstOrders = model.lstOrders.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList();
            model.Pager = pager;

            return PartialView("_OrdersList", model);
        }
        public ActionResult NewOrder(int id)
        {
            OrderViewModel order = new OrderViewModel();
            try
            {
                order = _iOrderService.GetById(id, SessionWrapper.Get<int>(AppConstant.UserId), SessionWrapper.Get<int>(AppConstant.RoleId));
            }
            catch (Exception ex)
            {
                order.Message = helper.GenerateMessage(" " + ex.Message, MessageType.Error);

            }
            if (order.Message != null)
            {
                TempData["Message"] = helper.GenerateMessage(order.Message, MessageType.Error);
                return RedirectToAction("Index");
            }
            return View(order);
        }
        [HttpPost]
        public ActionResult AddOrder(OrderViewModel model)
        {
            model.Date = DateTime.Now.Date;
            model.AgentName = SessionWrapper.Get<string>(AppConstant.UserName);
            if (SessionWrapper.Get<int>(AppConstant.RoleId) == 2)
            {
                model.CurrentStatus = 1;
                ModelState.Remove("CurrentStatus");
            }
            if (ModelState.IsValid)
            {
                ProcResult rMaster = new ProcResult();
                try
                {
                    if (model.OrderId == 0)
                    {
                        model.EID = SessionWrapper.Get<int>(AppConstant.UserId);
                        model.EIDDate = DateTime.Now;
                        if (model.Comment == null)
                        {
                            model.Comment = "Order Created By: " + SessionWrapper.Get<string>(AppConstant.UserName);
                        }
                        rMaster = _iOrderService.Add(model);
                    }
                    else
                    {
                        model.UID = SessionWrapper.Get<int>(AppConstant.UserId);
                        model.UIDDate = DateTime.Now;
                        rMaster = _iOrderService.Update(model);
                    }
                    if (rMaster.ErrorID == 0)
                    {
                        TempData["Message"] = helper.GenerateMessage(rMaster.strResult, MessageType.Success);

                    }
                    else
                    {
                        TempData["Message"] = helper.GenerateMessage(rMaster.strResult, MessageType.Error);
                    }
                }
                catch (Exception ex)
                {
                    TempData["Message"] = helper.GenerateMessage(ex.Message, MessageType.Error);
                }
                return RedirectToAction("Index");
            }
            else
            {
                model.lstOrderStatus = _iDataService.GetOrdersStatus();
                model.lstDistricts = _iDataService.GetDistricts();
                model.lstProducts = _iDataService.GetProducts();
                return View("NewOrder", model);
            }
        }
        public ActionResult Status(int id)
        {
            OrderViewModel order = new OrderViewModel();
            try
            {
                order = _iOrderService.GetStatusById(id, SessionWrapper.Get<int>(AppConstant.UserId), SessionWrapper.Get<int>(AppConstant.RoleId));
            }
            catch (Exception ex)
            {
                order.Message = helper.GenerateMessage(" " + ex.Message, MessageType.Error);

            }
            return View(order);
        }
        public ActionResult Delete(int id)
        {
            ProcResult rMaster = new ProcResult();
            try
            {
                rMaster = _iOrderService.Delete(id);

                if (rMaster.ErrorID == 0)
                {
                    TempData["Message"] = helper.GenerateMessage(rMaster.strResult, MessageType.Success);

                }
                else
                {
                    TempData["Message"] = helper.GenerateMessage(rMaster.strResult, MessageType.Error);
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = helper.GenerateMessage(ex.Message, MessageType.Error);
            }
            return RedirectToAction("Index");
        }

        public ActionResult StatusbyConsigneeNo(string consigneeNo)
        {
            OrderViewModel order = new OrderViewModel();
            try
            {
                order = _iOrderService.StatusByConsigneeNo(consigneeNo, SessionWrapper.Get<int>(AppConstant.RoleId));
                if (order.OrderId == 0)
                {
                    return RedirectToAction("Index", "DashBoard");
                }
            }
            catch (Exception ex)
            {
                order.Message = helper.GenerateMessage(" " + ex.Message, MessageType.Error);

            }
            return View("Status", order);
        }
        //[AllowAnonymous]
        //public ActionResult prntStatus(int id)
        //{
        //    OrderViewModel order = new OrderViewModel();
        //    try
        //    {
        //        order = _iOrderService.PrntGetById(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        order.Message = helper.GenerateMessage(" " + ex.Message, MessageType.Error);

        //    }
        //    return View("Status",order);
        //}
        private void AssignSessionVariables(UserViewModel user)
        {
            SessionWrapper.Set(AppConstant.UserId, user.UserId);
            SessionWrapper.Set(AppConstant.UserEmailID, user.UserEmailAddress);
            SessionWrapper.Set(AppConstant.UserName, user.UserName);
            SessionWrapper.Set(AppConstant.UserImageUrl, user.ImageUrl.TrimEnd());
            SessionWrapper.Set(AppConstant.LastLoginDateTime, user.LastLogin);
            SessionWrapper.Set(AppConstant.RoleId, user.RoleId);
        }
        [HttpPost]
        public ActionResult ExportToExcel()
        {
            List<ExportOrderViewModel> model = new List<ExportOrderViewModel>();
            model = _iOrderService.ExportGetAll(SessionWrapper.Get<int>(AppConstant.UserId), SessionWrapper.Get<int>(AppConstant.RoleId)).ToList();
            var gv = new GridView();
            gv.DataSource = model;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);

            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return RedirectToAction("Index");
        }
      
    }
}