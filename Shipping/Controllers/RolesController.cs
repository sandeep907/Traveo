using Shipping.Service.Interface;
using Shipping.Utilities;
using Shipping.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static Shipping.Utilities.Enums;

namespace Shipping.Controllers
{
    [Authorize(Roles = "1")]
    public class RolesController : BaseController
    {
        #region "Initilization"
        private readonly IRolesService _iRolesService;

        public RolesController(IRolesService IRolesService)
        {
            _iRolesService = IRolesService;
        }
        #endregion

        public ActionResult Index(int? page)
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            RolesSearchViewModel model = new RolesSearchViewModel();
            try
            {
                model.TotalCount = _iRolesService.GetAll().Count();
                var pager = new Pager(model.TotalCount, page);
                model.LstRoles = _iRolesService.GetAll()
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
            RolesSearchViewModel model = new RolesSearchViewModel();
            model.TotalCount = _iRolesService.GetAll(search).Count();
            var pager = new Pager(model.TotalCount, page, pageSize);
            model.LstRoles = _iRolesService.GetAll(search)
                .Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList();
            model.Pager = pager;
            return PartialView("_RolesList", model);
        }

        public ActionResult NewRole(int id)
        {
            RolesViewModel oRole = new RolesViewModel();
            try
            {
                if (id == 0)
                {
                    return PartialView("_Role", oRole);
                }
                else
                {
                    oRole = _iRolesService.GetById(id);
                }
                return PartialView("_Role",oRole);
            }
            catch (Exception ex)
            {
                oRole.Message = helper.GenerateMessage(" " + ex.Message, MessageType.Error);
                return PartialView("_Role", oRole);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddRoles(RolesViewModel model)
        {
            if (ModelState.IsValid)
            {
                ProcResult rMaster = new ProcResult();
                try
                {
                    model.EID = SessionWrapper.Get<int>(AppConstant.UserId);
                    if (model.RoleId == 0)
                    {
                        rMaster = _iRolesService.Add(model);
                    }
                    else
                    {
                        rMaster = _iRolesService.Update(model);
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
                return RedirectToAction("Index");
            }
        }
    }
}
