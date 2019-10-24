using AutoMapper;
using AutoMapper.QueryableExtensions;
using Shipping.Repository;
using Shipping.Repository.Interface;
using Shipping.Service.Interface;
using Shipping.Utilities;
using Shipping.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shipping.Service.Class
{
    public class OrderService : IOrderService
    {
        #region "Initialisation"
        private readonly IOrderRepository _iOrderRepository;
        private readonly IRemarksRepository _iRemarksRepository;
        private readonly IDataService _iDataService;
        public OrderService(IOrderRepository IOrderRepository, IRemarksRepository IRemarksRepository, IDataService IDataService)
        {
            _iOrderRepository = IOrderRepository;
            _iRemarksRepository = IRemarksRepository;
            _iDataService = IDataService;
        }

        #endregion
        #region "methods"
        public IEnumerable<OrderViewModel> GetAll(int Id, int roleId)
        {
            if (roleId == 1)
            {
                return _iOrderRepository.Getorders().UseAsDataSource(Mapper.Configuration).For<OrderViewModel>();
            }
            else
            {
                return _iOrderRepository.Getorders().Where(m => m.EID == Id).UseAsDataSource(Mapper.Configuration).For<OrderViewModel>();
            }
        }

        public IEnumerable<ExportOrderViewModel> ExportGetAll(int Id, int roleId)
        {
            if (roleId == 1)
            {
                var orders= _iOrderRepository.Getorders().UseAsDataSource(Mapper.Configuration).For<ExportOrderViewModel>().ToList();
                for (int i=0;i<=orders.ToList().Count -1;i++)
                {
                    orders[i].Comment = _iRemarksRepository.GetRemarkById(orders[i].OrderId).FirstOrDefault().ToString();
                    orders[i].CurrentStatusValue = _iDataService.GetOrdersStatus().Where(m => m.ID == orders[i].CurrentStatus).FirstOrDefault().ToString();
                }
                return orders;
            }
            else
            {
                var orders = _iOrderRepository.Getorders().Where(m => m.EID == Id).UseAsDataSource(Mapper.Configuration).For<ExportOrderViewModel>().ToList();
                for (int i = 0; i <= orders.ToList().Count - 1; i++)
                {
                    orders[i].Comment = _iRemarksRepository.GetRemarkById(orders[i].OrderId).FirstOrDefault().ToString();
                    orders[i].CurrentStatusValue = _iDataService.GetOrdersStatus().Where(m => m.ID == orders[i].CurrentStatus).FirstOrDefault().ToString();
                }
                return orders;
            }
        }
        public IEnumerable<OrderViewModel> GetAll(string search, int Id, int roleId)
        {
            if (roleId == 1)
            {
                return _iOrderRepository.Getorders().Where(m => m.ConsigneeName.Contains(search) || m.OrderNo.Contains(search)).UseAsDataSource(Mapper.Configuration).For<OrderViewModel>().ToList();
            }
            else
            {
                return _iOrderRepository.Getorders().Where(m => m.EID == Id && m.ConsigneeName.Contains(search) || m.OrderNo.Contains(search)).UseAsDataSource(Mapper.Configuration).For<OrderViewModel>().ToList();
            }
        }
        public OrderViewModel GetById(int id, int UserId, int roleId)
        {
            OrderViewModel model = new OrderViewModel();
            var orderstatus = _iDataService.GetOrdersStatus();
            model.lstOrderStatus = orderstatus;
            var districts = _iDataService.GetDistricts();
            model.lstDistricts = districts;
            var products= _iDataService.GetProducts();
            model.lstProducts = products;
            model.Date = DateTime.Now.Date;
            model.AgentName = SessionWrapper.Get<string>(AppConstant.UserName);
            if (roleId == 1)
            {
                if (id > 0)
                {
                    model = Mapper.Map<OrderViewModel>(_iOrderRepository.GetOrderById(id));
                    model.Remarks = _iRemarksRepository.GetRemarkById(model.OrderId).UseAsDataSource(Mapper.Configuration).For<RemarksViewModel>().ToList();
                    var s = orderstatus.Where(m => m.ID == model.CurrentStatus).FirstOrDefault();
                    model.CurrentStatusValue = s.Name;
                }
                model.lstOrderStatus = orderstatus;
                model.lstDistricts = districts;
                model.lstProducts = products;
            }
            else
            {
                if (id > 0)
                {
                    model = Mapper.Map<OrderViewModel>(_iOrderRepository.GetOrderById(id, UserId));
                    if (model != null)
                    {
                        model.Remarks = _iRemarksRepository.GetRemarkById(model.OrderId).UseAsDataSource(Mapper.Configuration).For<RemarksViewModel>().ToList();
                        var s = orderstatus.Where(m => m.ID == model.CurrentStatus).FirstOrDefault();
                        model.CurrentStatusValue = s.Name;
                        model.lstOrderStatus = orderstatus;
                        model.lstDistricts = districts;
                        model.lstProducts = products;

                    }
                    else
                    {
                        OrderViewModel obj = new OrderViewModel();
                        obj.Message = "Not AUthorized";
                        return obj;
                    }
                }
            }
            return model;
        }

        public OrderViewModel GetStatusById(int id, int UserId, int roleId)
        {
            OrderViewModel model = new OrderViewModel();
            var orderstatus = _iDataService.GetOrdersStatus();
            model.lstOrderStatus = orderstatus;

            model.Date = DateTime.Now.Date;
            if (roleId == 1)
            {
                if (id > 0)
                {
                    model = Mapper.Map<OrderViewModel>(_iOrderRepository.GetOrderById(id));
                    model.Remarks = _iRemarksRepository.GetRemarkById(model.OrderId).UseAsDataSource(Mapper.Configuration).For<RemarksViewModel>().ToList();
                    var s = orderstatus.Where(m => m.ID == model.CurrentStatus).FirstOrDefault();
                    model.CurrentStatusValue = s.Name;
                }
                model.lstOrderStatus = orderstatus;
            }
            else
            {
                if (id > 0)
                {
                    model = Mapper.Map<OrderViewModel>(_iOrderRepository.GetOrderStatusById(id, UserId));
                    if (model != null)
                    {
                        model.Remarks = _iRemarksRepository.GetRemarkById(model.OrderId).UseAsDataSource(Mapper.Configuration).For<RemarksViewModel>().ToList();
                        var s = orderstatus.Where(m => m.ID == model.CurrentStatus).FirstOrDefault();
                        model.CurrentStatusValue = s.Name;

                    }
                    else
                    {
                        OrderViewModel obj = new OrderViewModel();
                        obj.Message = "Not AUthorized";
                        return obj;
                    }
                }
            }
            return model;
        }

        public OrderViewModel StatusByConsigneeNo(string consigneeNo, int roleId)
        {
            OrderViewModel model = new OrderViewModel();
            var orderstatus = _iDataService.GetOrdersStatus();
            model.lstOrderStatus = orderstatus;
            if (roleId == 1)
            {
                model = Mapper.Map<OrderViewModel>(_iOrderRepository.GetOrderStatusByConsigneeNo(consigneeNo));
                if (model != null)
                {
                    model.Remarks = _iRemarksRepository.GetRemarkById(model.OrderId).UseAsDataSource(Mapper.Configuration).For<RemarksViewModel>().ToList();
                    var s = orderstatus.Where(m => m.ID == model.CurrentStatus).FirstOrDefault();
                    model.CurrentStatusValue = s.Name;
                    return model;
                }
                else
                {
                    return new OrderViewModel();
                }
            }
            else
            {
                OrderViewModel obj = new OrderViewModel();
                obj.Message = "Not AUthorized";
                return obj;
            }
        }

        public OrderViewModel PrntGetById(int id)
        {
            OrderViewModel model = new OrderViewModel();
            var orderstatus = _iDataService.GetOrdersStatus();
            model.lstOrderStatus = orderstatus;

            if (id > 0)
            {
                model = Mapper.Map<OrderViewModel>(_iOrderRepository.GetOrderById(id));
                model.Remarks = _iRemarksRepository.GetRemarkById(model.OrderId).UseAsDataSource(Mapper.Configuration).For<RemarksViewModel>().ToList();
                var s = orderstatus.Where(m => m.ID == model.CurrentStatus).FirstOrDefault();
                model.CurrentStatusValue = s.Name;
            }
            model.lstOrderStatus = orderstatus;
            return model;
        }
        public ProcResult Add(OrderViewModel model)
        {
            RemarksViewModel remarks = new RemarksViewModel();
            remarks.EID = SessionWrapper.Get<int>(AppConstant.UserId);
            remarks.EIDDate = DateTime.Now;
            remarks.Comment = model.Comment;
            return _iOrderRepository.AddOrder(Mapper.Map<Order>(model), Mapper.Map<Remark>(remarks));
        }
        public ProcResult Update(OrderViewModel model)
        {
            RemarksViewModel remarks = new RemarksViewModel();
            remarks.EID = SessionWrapper.Get<int>(AppConstant.UserId);
            remarks.EIDDate = DateTime.Now;
            remarks.Comment = model.Comment;
            remarks.OrderId = model.OrderId;
            return _iOrderRepository.UpdateOrder(Mapper.Map<Order>(model), Mapper.Map<Remark>(remarks));
        }
        public ProcResult Delete(int id)
        {
            RemarksViewModel remarks = new RemarksViewModel();
            return _iOrderRepository.DeleteOrder(id);
        }
        #endregion
    }
}