using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shipping.Service.Interface;
using Shipping.Utilities;
using Shipping.ViewModels.Account;
using static Shipping.Utilities.Enums;

namespace Shipping.Controllers
{
    [Authorize(Roles = "1")]
    public class RegistrationController : BaseController
    {

        #region "Initilization"
        private readonly IUserService _iUserService;
        public readonly IDataService _iDataService;
        public RegistrationController(IUserService IUserService, IDataService IDataService)
        {
            _iUserService = IUserService;
            _iDataService = IDataService;
        }
        #endregion

        public ActionResult Index(int? page)
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            UserSeachViewModel model = new UserSeachViewModel();
            try
            {
                model.TotalCount = _iUserService.GetAll().Count();
                var pager = new Pager(model.TotalCount, page, 5);
                model.lstUser = _iUserService.GetAll()
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
        public ActionResult Search(string search, int? page, int pageSize)
        {
            UserSeachViewModel model = new UserSeachViewModel();
            model.TotalCount = _iUserService.GetAll(search).Count();
            var pager = new Pager(model.TotalCount, page, pageSize);
            model.lstUser = _iUserService.GetAll(search)
                .Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList();
            model.Pager = pager;
            return PartialView("_userList", model);
        }

        public ActionResult NewUser(int id)
        {
            UserViewModel oRole = new UserViewModel();

            try
            {
                if (id != 0)
                {
                    oRole = _iUserService.GetById(id);
                    oRole.lstRoles = _iDataService.GetRoles();
                }
                else
                {
                    oRole = _iUserService.GetById(id);
                    oRole.lstRoles = _iDataService.GetRoles();
                }
                return View(oRole);
            }
            catch (Exception ex)
            {
                oRole.Message = helper.GenerateMessage(" " + ex.Message, MessageType.Error);
                return View(oRole);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddUser(LoginRegisterViewModel model)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];
            if (file.ContentLength != 0)
            {
                var allowedExtensions = new[] {
            ".Jpg", ".png", ".jpg", "jpeg"
        };
                model.user.ImageUrl = file.ToString(); //getting complete url 
                var fileName = Path.GetFileName(file.FileName);
                var ext = Path.GetExtension(file.FileName);
                if (allowedExtensions.Contains(ext)) //check what type of extension  
                {
                    string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                    string myfile = model.user.UserEmailAddress + ext; //appending the name with id  
                                                                       // store the file inside ~/project folder(Img)  
                    var path = Path.Combine(Server.MapPath("~/UserImages"), myfile);
                    model.user.ImageUrl = "UserImages/" + myfile;
                    file.SaveAs(path);
                }
            }
            else
            {
                model.user.ImageUrl = "UserImages/" + "avatar5.png";
            }
            ModelState.Remove("user.lstRoles");
            model.user.lstRoles = _iDataService.GetRoles();
            model.IsRegister = true;
            if (ModelState.IsValid)
            {
                ProcResult rMaster = new ProcResult();
                try
                {
                    model.user.EID = SessionWrapper.Get<int>(AppConstant.UserId);
                    if (!string.IsNullOrEmpty(model.user.UserPassword))
                    {
                        model.user.UserPassword = new UtilityHelper().EncrpytPassword(model.user.UserPassword);
                    }
                    if (model.user.UserId == 0)
                    {
                       model.user.Flag = "D";
                        rMaster = _iUserService.Save(model.user);

                    }
                    else
                    {
                        rMaster = _iUserService.Save(model.user);
                    }
                    if (rMaster.ErrorID == 0)
                    {
                        model.user = new UserViewModel();
                        model.user.lstRoles = _iDataService.GetRoles();
                        TempData["Message"] = helper.GenerateMessage(rMaster.strResult + " Wait for Admin Approval", MessageType.Success);

                    }
                    else
                    {
                        model.user.UserPassword = null;
                        model.user.UserPassword = null;
                        TempData["Message"] = helper.GenerateMessage(rMaster.strResult, MessageType.Error);
                    }
                }
                catch (Exception ex)
                {
                    TempData["Message"] = helper.GenerateMessage(ex.Message, MessageType.Error);
                }
                return View("../Account/Index", model);
            }
            else          
            {
                return View("../Account/Index", model);
            }
        }

        public ActionResult AdminAddUser(UserViewModel model)
        {
            model.ImageUrl = "UserImages/" + "avatar5.png";
            ModelState.Remove("lstRoles");
            model.lstRoles = _iDataService.GetRoles();
            if (ModelState.IsValid)
            {
                ProcResult rMaster = new ProcResult();
                try
                {
                    model.EID = SessionWrapper.Get<int>(AppConstant.UserId);
                    if (!string.IsNullOrEmpty(model.UserPassword))
                    {
                        model.UserPassword = new UtilityHelper().EncrpytPassword(model.UserPassword);
                    }
                    if (model.UserId == 0)
                    {
                        model.Flag = "A";
                        rMaster = _iUserService.Save(model);

                    }
                    else
                    {

                        rMaster = _iUserService.Save(model);
                    }
                    if (rMaster.ErrorID == 0)
                    {
                        model = new UserViewModel();
                        model.lstRoles = _iDataService.GetRoles();
                        TempData["Message"] = helper.GenerateMessage(rMaster.strResult + " Wait for Admin Approval", MessageType.Success);

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
                return View("NewUser", model);
            }
        }

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }
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

                rMaster = _iUserService.ActivateAndDeActivate(id, model);
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }

    }
}