using System.Security.Claims;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;

namespace Shipping
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            UnityConfig.RegisterComponents();
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
            Mapper.Initialize(MappingConfig.AutoMapperConfig.RegisterMappings);
        }
    }
}
