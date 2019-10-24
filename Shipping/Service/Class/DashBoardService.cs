using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Shipping.Repository.Interface;
using Shipping.Service.Interface;
using Shipping.ViewModels.Account;
using Shipping.ViewModels.DashBoard;
using Shipping.ViewModels.Orders;

namespace Shipping.Service.Class
{
    public class DashBoardService : IDashBoardService
    {
        public readonly IUserRepository _iUserRepository;
        public readonly IEmailRepository _iEmailRepository;
        private readonly IOrderRepository _iOrderRepository;
        private readonly IRemarksRepository _iRemarksRepository;
        public DashBoardService(IUserRepository IUserRepository, IEmailRepository IEmailRepository, IOrderRepository IOrderRepository, IRemarksRepository IRemarksRepository)
        {
            _iUserRepository = IUserRepository;
            _iEmailRepository = IEmailRepository;
            _iOrderRepository = IOrderRepository;
            _iRemarksRepository = IRemarksRepository;
        }

        public BadgesViewModel GetBadges()
        {
            BadgesViewModel model = new BadgesViewModel();
            model.TotalOrders = _iOrderRepository.Getorders().Count();
            model.FreshOrders = _iOrderRepository.Getorders().Where(m=>m.CurrentStatus==1).Count();
            model.InProgress = _iOrderRepository.Getorders().Where(m => m.CurrentStatus == 2).Count();
            model.OutForDelivery = _iOrderRepository.Getorders().Where(m => m.CurrentStatus == 3).Count();
            model.Delivered = _iOrderRepository.Getorders().Where(m => m.CurrentStatus == 4).Count();
            model.Returned = _iOrderRepository.Getorders().Where(m => m.CurrentStatus == 5).Count();
            return model;
        }
        public IEnumerable<UserViewModel> GetUsers()
        {
            return Mapper.Map<IEnumerable<UserViewModel>>(_iUserRepository.GetAll().Where(m => m.RoleId != 1));
        }
        public IEnumerable<OrderViewModel> GetOrders()
        {
            var orders = Mapper.Map<IEnumerable<OrderViewModel>>(_iOrderRepository.Getorders().ToList()).ToList();
            for(int i=0;i<orders.Count();i++)
            {
                orders[i].Comment =_iRemarksRepository.GetRemarkById(orders[i].OrderId).ToList().LastOrDefault().Comment;
            }
            return orders;
        }

    }
}