using AutoMapper;
using Shipping.Repository;
using Shipping.ViewModels.Account;
using Shipping.ViewModels.Orders;
using Shipping.ViewModels.Roles;

namespace Shipping.MappingConfig
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<UserViewModel, MTLoginMaster>();
            cfg.CreateMap<MTLoginMaster, UserViewModel>();
            cfg.CreateMap<RoleViewModel, MTRoleMaster>();
            cfg.CreateMap<MTRoleMaster, RoleViewModel>();
            cfg.CreateMap<UserViewModel, UserSeachViewModel>();
            cfg.CreateMap<UserSeachViewModel, UserViewModel>();
            cfg.CreateMap<EmailViewModel, MTEmail>();
            cfg.CreateMap<MTEmail, EmailViewModel>();
            cfg.CreateMap<EmailViewModel, EmailSearchViewModel>();
            cfg.CreateMap<EmailSearchViewModel, EmailViewModel>();
            cfg.CreateMap<SentEmailViewModel, SentEmail>();
            cfg.CreateMap<SentEmail, SentEmailViewModel>();
            cfg.CreateMap<SentEmailViewModel, SentEmailSearchViewModel>();
            cfg.CreateMap<SentEmailSearchViewModel, SentEmailViewModel>();
            cfg.CreateMap<RolesViewModel, RolesSearchViewModel>();
            cfg.CreateMap<RolesSearchViewModel, RolesViewModel>();
            cfg.CreateMap<RolesViewModel, MTRoleMaster>();
            cfg.CreateMap<MTRoleMaster, RolesViewModel>();
            cfg.CreateMap<OrderSearchViewModel, OrderViewModel>();
            cfg.CreateMap<OrderViewModel, Order>();
            cfg.CreateMap<Order, OrderViewModel>();
            cfg.CreateMap<RemarksViewModel, Remark>();
            cfg.CreateMap<Remark, RemarksViewModel>();
            cfg.CreateMap<OrderViewModel, OrdersView>();
            cfg.CreateMap<OrdersView, OrderViewModel>();
            cfg.CreateMap<ExportOrderViewModel, OrdersView>();
            cfg.CreateMap<OrdersView, ExportOrderViewModel>();
            cfg.CreateMap<Order, OrdersView>();
            cfg.CreateMap<OrdersView, Order>();
        }
    }
}