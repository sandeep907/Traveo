using System;
using System.Linq;
using System.Web.Mvc;
using Shipping.Service.Interface;
using Shipping.Utilities;
using Shipping.ViewModels.Account;
using Shipping.ViewModels.DashBoard;

namespace Shipping.Controllers
{
    [Authorize(Roles = "1")]
    public class DashBoardController : BaseController
    {
        public readonly IDashBoardService _iDashBoardService;
        public readonly IUserService _iUserService;
        public readonly IDataService _iDataService;
        public DashBoardController(IDashBoardService IDashBoardService, IUserService IUserService, IDataService IDataService)
        {
            _iUserService = IUserService;
            _iDashBoardService = IDashBoardService;
            _iDataService = IDataService;
        }
        // GET: DashBoard
        public ActionResult Index()
        {
            DashBoardViewModel model = new DashBoardViewModel();
            var res = HttpContext.User.Identity.Name;
            var user = _iUserService.GetAll().Where(m => m.UserEmailAddress == res).FirstOrDefault();
            AssignSessionVariables(user);
            if (SessionWrapper.Get<DashBoardViewModel>(AppConstant.DashBoardViewModel) != null)
            {
                model = SessionWrapper.Get<DashBoardViewModel>(AppConstant.DashBoardViewModel);
            }
            else
            {
                model.Badges = _iDashBoardService.GetBadges();
                model.UsersPending = _iDashBoardService.GetUsers().ToList();
                model.OrdersList = _iDashBoardService.GetOrders().ToList();
                model.lstRoles = _iDataService.GetRoles();
                SessionWrapper.Set(AppConstant.DashBoardViewModel, model);
            }
            return View(model);
        }

        // GET: DashBoard/Details/5
        public ActionResult AccountFlag(int id, string flag)
        {
            ProcResult rMaster = new ProcResult();
            UserViewModel model = new UserViewModel();
            try
            {
                model = _iUserService.GetById(id);
                model.Flag = flag;
                model.EID = SessionWrapper.Get<int>(AppConstant.UserId);
                rMaster = _iUserService.ActivateAndDeActivate(id, model);
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }

        public ActionResult ChangeRole(int id, int roleId)
        {
            ProcResult rMaster = new ProcResult();
            UserViewModel model = new UserViewModel();
            try
            {
                model = _iUserService.GetById(id);
                model.RoleId = roleId;
                model.EID = SessionWrapper.Get<int>(AppConstant.UserId);
                rMaster = _iUserService.ChangeRole(id, model);
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }


        private void AssignSessionVariables(UserViewModel user)
        {
            SessionWrapper.Set(AppConstant.UserId, user.UserId);
            SessionWrapper.Set(AppConstant.UserEmailID, user.UserEmailAddress);
            SessionWrapper.Set(AppConstant.UserName, user.UserName);
            SessionWrapper.Set(AppConstant.UserImageUrl, user.ImageUrl.TrimEnd());
            SessionWrapper.Set(AppConstant.LastLoginDateTime, user.LastLogin);
            SessionWrapper.Set(AppConstant.RoleId, user.RoleId);
        }
    }
}
