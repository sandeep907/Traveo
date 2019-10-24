using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Shipping.Service.Interface;
using Shipping.Utilities;
using Shipping.ViewModels.Account;
using static Shipping.Utilities.Enums;

namespace Shipping.Controllers
{
    public class AccountController : Controller
    {
        public readonly IAccountService _iAccountService;
        public readonly IDataService _iDataService;

        public AccountController(IAccountService IAccountService, IDataService IDataService)
        {
            _iAccountService = IAccountService;
            _iDataService = IDataService;
        }
        [OutputCache(Duration =6000000, VaryByParam = "none", Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult Index(string returnUrl)
        {
            if (this.Request.IsAuthenticated)
            {
                // Info.    
                return this.RedirectToLocal(returnUrl);
            }
            LoginRegisterViewModel model = new LoginRegisterViewModel();
            model.user.lstRoles = _iDataService.GetRoles();
            SessionWrapper.Set<IEnumerable<IListEntry>>(AppConstant.Roles, model.user.lstRoles);
            return View(model);
        }

        [HttpPost]
        public ActionResult Logon(LoginRegisterViewModel model)
        {
            model.user.lstRoles = SessionWrapper.Get<IEnumerable<IListEntry>>(AppConstant.Roles)==null? _iDataService.GetRoles(): SessionWrapper.Get<IEnumerable<IListEntry>>(AppConstant.Roles);
            if (ModelState.IsValid)
            {
                var user = _iAccountService.CheckValidation(model.login.UserName, new UtilityHelper().EncrpytPassword(model.login.UserPassword));
                if (user != null)
                {
                    var roles = _iAccountService.GetAll(user.RoleId);
                    if (roles == null)
                    {
                        model.login.Message = new UtilityHelper().GenerateMessage("Your Account is don't have role", MessageType.Error);
                    }
                    else
                    {
                        AuthenticateUser(user, roles);
                        UpdateUserSettings(user.UserId);
                        if (roles.RoleId == 1)
                        {
                            return RedirectToAction("Index", "Order");
                        }
                        else
                        {
                            return RedirectToAction("Index","Order");
                        }
                        }
                }
                else
                {
                    TempData["LoginMessage"] = new UtilityHelper().GenerateMessage("Invalid UserName and Password", MessageType.Error);
                }
            }
            else
            {
                TempData["LoginMessage"] = new UtilityHelper().GenerateMessage("UserName and Password is required", MessageType.Error);
            }

            return View("Index", model);
        }

        public ActionResult Logout()
        {
            _iAccountService.UpdateLoginSatus(SessionWrapper.Get<int>(AppConstant.UserId), 0);
            Session.Clear();
            Session.Abandon();
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            // Sign Out.    
            authenticationManager.SignOut();
            
            return RedirectToAction("Index", "Account");
        }
        private void AuthenticateUser(UserViewModel model, RoleViewModel lstRole)
        {
            AssignSessionVariables(model, lstRole);

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, model.UserEmailAddress));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, model.UserPassword));
            claims.Add(new Claim(ClaimTypes.Role, model.RoleId.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, model.UserEmailAddress));
            var claimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;

            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, claimIdenties);
        }
        private void AssignSessionVariables(UserViewModel user, RoleViewModel lstRole)
        {
            SessionWrapper.Set(AppConstant.UserId, user.UserId);
            SessionWrapper.Set(AppConstant.UserEmailID, user.UserEmailAddress);
            SessionWrapper.Set(AppConstant.UserName, user.UserName);
            SessionWrapper.Set(AppConstant.UserImageUrl, user.ImageUrl.TrimEnd());
            SessionWrapper.Set(AppConstant.LastLoginDateTime, user.LastLogin);
            var roleId = lstRole.RoleId;
            var roleName = lstRole.RoleName;
            SessionWrapper.Set(AppConstant.RoleId, roleId);
            SessionWrapper.Set(AppConstant.RoleName, roleName);

        }

        private void UpdateUserSettings(int userId)
        {
            _iAccountService.UpdateLoginSatus(userId,1);
            _iAccountService.UpdateUserLastLoginDate(userId);
            _iAccountService.UpdateUserLoginCount(userId);
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            try
            {
                // Verification.    
                if (Url.IsLocalUrl(returnUrl))
                {
                    // Info.    
                    return this.Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                // Info    
                throw ex;
            }
            // Info.    
            return this.RedirectToAction("Index", "DashBoard");
        }

        public ActionResult Error()
        {
            return View("error");
        }

    }
}