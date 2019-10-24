using System.Web.Mvc;
using Shipping.Repository.Class;
using Shipping.Repository.Interface;
using Shipping.Service.Class;
using Shipping.Service.Interface;
using Unity;
using Unity.Mvc5;

namespace Shipping
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            container.RegisterType<IAccountService, AccountService>();
            container.RegisterType<IAccountRepository, AccountRepository>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IEmailRepository, EmailRepository>();
            container.RegisterType<IEmailService, EmailService>();
            container.RegisterType<IDashBoardService,DashBoardService>();
            container.RegisterType<IDataService, DataService>();
            container.RegisterType<IListRepository, ListRepository>();
            container.RegisterType<IRolesService, RolesService>();
            container.RegisterType<IRolesRepository, RolesRepository>();
            container.RegisterType<IOrderRepository, OrdersRepository>();
            container.RegisterType<IOrderService, OrderService>();
            container.RegisterType<IRemarksRepository, RemarksRepository>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}