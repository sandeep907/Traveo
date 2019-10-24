using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using SilverCode.Autocampx.Core.Class;
using SilverCode.Autocampx.Core.CommonType;
using SilverCode.Autocampx.Core.Utility;
using SilverCode.Autocampx.Core.ViewModel.User;
using SilverCode.Autocampx.Service.Interface.Services;
using SilverCode.Autocampx.Web.Portal.Utilities;
using SilverCode.Autocampx.Web.Portal.Utilities.Constant;
using SilverCode.Autocampx.Service.Interface.Services.User;
using SilverCode.Autocampx.Service.Interface.DataService;
using SilverCode.Autocampx.Core;
using SilverCode.Autocampx.Service.Interface.Services.Student;
using SilverCode.Autocampx.Core.ViewModel.Portal;

namespace SilverCode.Autocampx.Web.Portal.Controllers
{
    [RoutePrefix("Login")]
    public class LoginController : Controller
    {
        private readonly UtilityHelper helper;
        private readonly IAuthService _iAuthService;
        private readonly IRoleAssignerService _iRoleAssignerService;
        private readonly IRoleService _iRoleService;
        private readonly IDataService _iDataService;
        private readonly IUserService _iUserService;
        private readonly IStudentService _iStudentService;

        public LoginController(IAuthService IAuthService, IRoleAssignerService IRoleAssignerService, IRoleService IRoleService, IDataService IDataService, IUserService IUserService, IStudentService IStudentService)
        {
            helper = new UtilityHelper();
            _iRoleService = IRoleService;
            _iRoleAssignerService = IRoleAssignerService;
            _iAuthService = IAuthService;
            _iDataService = IDataService;
            _iUserService = IUserService;
            _iStudentService = IStudentService;
        }

        #region "Login Page Action"
        public ActionResult Index()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logon(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _iAuthService.CheckValidation(model.UserName, new UtilityHelper().EncrpytPassword(model.UserPassword));
                if (user != null)
                {
                    if (user.Flag == "A")
                    {
                        if (user.UserTypeID == (int)UserType.Student)
                        {
                            var roles = _iRoleAssignerService.GetAll().Where(x => x.UserId == user.UserId).FirstOrDefault().lstRoleDetail;
                            if (roles == null)
                            {
                                model.Message = helper.GenerateMessage("Your Account is don't have role", MessageType.Error);
                            }
                            else
                            {
                                AuthenticateUser(user, roles);
                                UpdateUserSettings(user.UserId);
                                return RedirectToAction("Index", new { controller = "Dashboard", area = "PortalManagement" });
                            }
                        }
                        else
                        {
                            var roles = _iRoleAssignerService.GetAll().Where(x => x.UserId == user.UserId).FirstOrDefault().lstRoleDetail;
                            if (roles == null)
                            {
                                model.Message = helper.GenerateMessage("Your Account is don't have role", MessageType.Error);
                            }
                            else
                            {
                                AuthenticateUser(user, roles);
                                UpdateUserSettings(user.UserId);
                                if (CheckUserSavedSession(user.UserId))
                                {
                                    return RedirectToAction("Index", new { controller = "DashBoard", area = "Home" });
                                }
                                return RedirectToAction("SelectAcademicYear");
                            }
                        }
                    }
                    else
                    {
                        model.Message = helper.GenerateMessage("Your Account is de-active", MessageType.Error);
                    }
                }
                else
                {
                    model.Message = helper.GenerateMessage("Invalid UserName and Password", MessageType.Error);
                }
            }
            else
            {
                model.Message = helper.GenerateMessage("UserName and Password is required", MessageType.Error);
            }

            return View("Index", model);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index");
        }
        #endregion

        #region "Select School and Academic Year"
        public ActionResult SelectAcademicYear()
        {
            if (TempData[AppConstant.Message] != null)
            {
                ViewBag.Message = TempData[AppConstant.Message];
            }
            SessionAcademicViewModel model = new SessionAcademicViewModel();
            try
            {
                model = _iAuthService.GetInitilization(SessionWrapper.Get<int>(AppConstant.UserId));
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.Message = helper.GenerateMessage(" " + ex.Message, MessageType.Error);
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult SubmitAcademicYear(SessionAcademicViewModel model)
        {
            var SchoolName = _iDataService.SchoolAll().Where(x => x.ID == model.SchoolId).FirstOrDefault();
            if (SchoolName != null)
                SessionWrapper.Set(AppConstant.SchoolName, SchoolName.Name);
            var AcademicName = _iDataService.AcademicYear(model.SchoolId).Where(x => x.ID == model.AcademicId).FirstOrDefault();
            if (AcademicName != null)
                SessionWrapper.Set(AppConstant.AcademicName, AcademicName.Name);
            SessionWrapper.Set(AppConstant.SchoolId, model.SchoolId);
            SessionWrapper.Set(AppConstant.AcademicId, model.AcademicId);

            var result = _iUserService.UpdateUserSessionDetail(SessionWrapper.Get<int>(AppConstant.UserId), model.SchoolId, model.AcademicId);
            var menu = _iRoleService.GetRoleAndMenuDetailsByRoleId(SessionWrapper.Get<int>(AppConstant.RoleId));
            menu.lstModuleList = menu.lstModuleList.Where(x => x.ModuleID == SessionWrapper.Get<int>(AppConstant.ModuleId)).ToList();
            TempData["Menu"] = menu;
            return RedirectToAction("Index", new { controller = "DashBoard", area = "Home" });
        }

        public ActionResult SetModuleId(int id)
        {
            var menu = _iRoleService.GetRoleAndMenuDetailsByRoleId(SessionWrapper.Get<int>(AppConstant.RoleId));
            menu.lstModuleList = menu.lstModuleList.Where(x => x.ModuleID == id).ToList();
            TempData["Menu"] = menu;
            SessionWrapper.Set(AppConstant.ModuleId, id);
            return RedirectToAction("Index", new { controller = "DashBoard", area = "Home" });
        }
        #endregion
        private void AuthenticateUser(UserViewModel model, List<RoleDetails> lstRole)
        {
            AssignSessionVariables(model, lstRole);
            if (!string.IsNullOrEmpty(model.UserName))
            {
                var identity = new ClaimsIdentity(new List<Claim> {
                new Claim(ClaimTypes.Name, model.UserName),
                new Claim(ClaimTypes.NameIdentifier , model.UserName)
            }, CookieAuthenticationDefaults.AuthenticationType);

                AuthenticationManager.SignIn(new AuthenticationProperties
                {
                    AllowRefresh = true,
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(1)
                }, identity);
            }
        }
        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }
        private void AssignSessionVariables(UserViewModel user, List<RoleDetails> lstRole)
        {
            SessionWrapper.Set(AppConstant.UserId, user.UserId);
            SessionWrapper.Set(AppConstant.UserEmailID, user.UserEmailAddress);
            SessionWrapper.Set(AppConstant.UserName, user.UserName);
            SessionWrapper.Set(AppConstant.UserType, user.UserType);
            SessionWrapper.Set(AppConstant.LastLoginDateTime, user.LastLogin);
            var roleId = lstRole.FirstOrDefault().RoleId;
            var roleName = lstRole.FirstOrDefault().RoleName;
            SessionWrapper.Set(AppConstant.RoleId, roleId);
            SessionWrapper.Set(AppConstant.RoleName, roleName);
            if (user.UserTypeID == (int)UserType.Student)
            {
                // Assign SchoolId and Academic Year.
                SessionWrapper.Set(AppConstant.SchoolId, user.SchoolId);
                SessionWrapper.Set(AppConstant.StudentId, user.EmployeeID);
                var SchoolName = _iDataService.SchoolAll().Where(x => x.ID == user.SchoolId).FirstOrDefault();
                if (SchoolName != null)
                    SessionWrapper.Set(AppConstant.SchoolName, SchoolName.Name);

                List<MyStudentViewModel> lststudentData = _iStudentService.GetStudentDetailByStudentId(user.EmployeeID == null ? 0 : user.EmployeeID.Value, user.SchoolId);

                int academicYearId = lststudentData.Max(x => x.AcademicID.Value);
                int StudentDetailId = lststudentData.Where(x => x.AcademicID == academicYearId).FirstOrDefault().StudentDetailID;
                SessionWrapper.Set(AppConstant.StudentIdDetailId, StudentDetailId);
                int[] academicYearList = lststudentData.Select(x => x.AcademicID.Value).ToArray();

                var AcademicName = _iDataService.AcademicYear(user.SchoolId).Where(x => x.ID == academicYearId).FirstOrDefault();
                if (AcademicName != null)
                    SessionWrapper.Set(AppConstant.AcademicName, AcademicName.Name);
                SessionWrapper.Set(AppConstant.AcademicId, academicYearId);

                SessionWrapper.Set(AppConstant.AcademicYearList, (from t in _iDataService.AcademicYear(user.SchoolId).Where(x => academicYearList.Contains(x.ID))
                                                                  select new DropDownEntry { ID = t.ID, Name = t.Name }).ToList());
                int classId = lststudentData.Where(x => x.AcademicID == academicYearId).FirstOrDefault().ClassID;
                int sectionId = lststudentData.Where(x => x.AcademicID == academicYearId).FirstOrDefault().SectionID;

                SessionWrapper.Set(AppConstant.ClassId, classId);
                SessionWrapper.Set(AppConstant.SectionId, sectionId);
                var menu = _iRoleService.GetRoleAndMenuDetailsByRoleId(SessionWrapper.Get<int>(AppConstant.RoleId));               
                TempData["Menu"] = menu;
            }
        }

        public bool CheckUserSavedSession(int userId)
        {
            bool result = false;
            var userSessionData = _iUserService.GetUserSessionDetail(userId);
            if (userSessionData != null)
            {
                SessionWrapper.Set(AppConstant.SchoolId, userSessionData.SchoolId);
                SessionWrapper.Set(AppConstant.AcademicId, userSessionData.AcademicId);
                var SchoolName = _iDataService.SchoolAll().Where(x => x.ID == userSessionData.SchoolId).FirstOrDefault();
                if (SchoolName != null)
                    SessionWrapper.Set(AppConstant.SchoolName, SchoolName.Name);
                var AcademicName = _iDataService.AcademicYear(userSessionData.SchoolId).Where(x => x.ID == userSessionData.AcademicId).FirstOrDefault();
                if (AcademicName != null)
                    SessionWrapper.Set(AppConstant.AcademicName, AcademicName.Name);
                SessionWrapper.Set(AppConstant.SchoolId, userSessionData.SchoolId);
                SessionWrapper.Set(AppConstant.AcademicId, userSessionData.AcademicId);

                var menu = _iRoleService.GetRoleAndMenuDetailsByRoleId(SessionWrapper.Get<int>(AppConstant.RoleId));               
                TempData["Menu"] = menu;
                result = true;
            }

            return result;
        }

        private void UpdateUserSettings(int userId)
        {
            _iAuthService.UpdateUserLastLoginDate(userId);
            _iAuthService.UpdateUserLoginCount(userId);
        }
        #region "Forgot Page"
        public ActionResult ForgotPassword()
        {
            return View();
        }
        #endregion
    }
}