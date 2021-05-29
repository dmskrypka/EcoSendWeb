using EcoSendWeb.Infrastructure.Attributes;
using EcoSendWeb.Models.BO.Account;
using EcoSendWeb.Models.View.Account;
using EcoSendWeb.Services.Api;
using System;
using System.Web.Mvc;
using EcoSendWeb.Infrastructure;
using EcoSendWeb.App_Start;
using System.Web.Security;

namespace EcoSendWeb.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountServ accountServ;
        public AccountController(IAccountServ accountServ)
        {
            this.accountServ = accountServ;
        }

        [Route("login")]
        [AllowAnonymous]
        [NoClientCacheAttribute]
        public ActionResult Login()
        {
            LoginVM vm = new LoginVM();
            return View(vm);
        }

        [Route("registration")]
        [AllowAnonymous]
        [NoClientCacheAttribute]
        public ActionResult Registration()
        {
            RegistrationVM vm = new RegistrationVM();
            return View(vm);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult UserLogin(LoginVM vm)
        {
            if (!ModelState.IsValid)
            {
                return JavaScript(@"require(['bootbox'], function (bootbox) {
                    bootbox.alert('Login data is not valid.', function () { $('#Message').val(''); });
                });");
            }

            UserBO user = accountServ.GetUserOnLogin(vm.Email, vm.Password);
            if (user != null)
            {
                FormAuth.SetAuthCookie(user, Response);
                return JavaScript("window.location = '" + Url.Action("Index", "Home") + @"'");
            }

            return JavaScript(@"require(['bootbox'], function (bootbox) {
                bootbox.alert('Not registered.', function () { $('#Message').val(''); });
            });");

        }

        [HttpPost]
        [AllowAnonymous]
        [NoClientCacheAttribute]
        [ValidateAntiForgeryToken]
        public ActionResult UserRegistration(RegistrationVM vm)
        {
            if (ModelState.IsValid)
            {
                string msg = accountServ.RegisterUser(MappingProfilesConfig.Mapper.Map<UserBO>(vm), vm.Password);

                return JavaScript(@"require(['bootbox'], function (bootbox) {
                    bootbox.alert('" + msg + @"', function () { $('#Message').val(''); });
                });");
            }

            return JavaScript(@"require(['bootbox'], function (bootbox) {
                bootbox.alert('Login data is not valid.', function () { $('#Message').val(''); });
            });");
        }

        [Route("user-info")]
        public ActionResult UserInfo()
        {
            if(accountServ.GetUser((User as IExtendedPrincipal)?.Id ?? Guid.Empty) is UserBO user)
            {
                return View(MappingProfilesConfig.Mapper.Map<UserVM>(user));
            }
            else
            {
                FormsAuthentication.SignOut();

                throw new Exception("Unknown user.");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveUserInfo(UserVM vm)
        {
            string msg = "Data is not valid.";
            if (ModelState.IsValid)
            {
                UserBO user = MappingProfilesConfig.Mapper.Map<UserBO>(vm);
                user.Id = (User as IExtendedPrincipal)?.Id ?? Guid.Empty;
                
                accountServ.SaveUserInfo(user);
                msg = "Successfully saved.";
            }

            return JavaScript(@"require(['bootbox'], function (bootbox) {
                    bootbox.alert('" + msg + @"', function () { $('#Message').val(''); });
                });");
        }

        [HttpPost]
        public ActionResult LogoutUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}