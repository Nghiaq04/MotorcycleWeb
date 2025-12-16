using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MotorcycleWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MotorcycleWeb.Areas.Admin.Controllers
{
    
    public class AccountController : Controller
    {
        // GET: Admin/Account

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        [AllowAnonymous]
        public ActionResult AccessDenied()
        {
            Response.StatusCode = 403; // HTTP 403 Forbidden
            return View();
        }

        [Authorize(Roles = "Admin,Employee")]
        public ActionResult Index()
        {
            var users = db.Users.OrderBy(u => u.CreatedDate).ToList();
            var userRoles = new Dictionary<string, string>();

            foreach (var user in users)
            {
                var roles = UserManager.GetRoles(user.Id);
                userRoles[user.Id] = string.Join(", ", roles); 
            }

            ViewBag.UserRoles = userRoles;
            return View(users);
        }

        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.Role = new SelectList(db.Roles.ToList(),"Name","Name");
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email,Fullname = model.FullName,Phone=model.Phone };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id,model.Role);
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }
            ViewBag.Role = new SelectList(db.Roles.ToList(), "Name", "Name");
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // ✅ Tìm theo UserName, không phải Id
            var user = UserManager.FindById(id);

            if (user == null)
                return HttpNotFound();

            var roles = UserManager.GetRoles(user.Id);
            var roleName = roles.FirstOrDefault();

            var model = new CreateAccountViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                FullName = user.Fullname,
                Phone = user.Phone,
                Role = roleName
            };

            ViewBag.Role = new SelectList(db.Roles.ToList(), "Name", "Name", roleName);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CreateAccountViewModel model)
        {
            // ✅ Không kiểm tra Password khi sửa
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");

            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(model.Id);

                if (user == null)
                    return HttpNotFound();

                user.Email = model.Email;
                user.Fullname = model.FullName;
                user.Phone = model.Phone;

                var result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    // ✅ Cập nhật Role
                    var currentRoles = await UserManager.GetRolesAsync(user.Id);
                    await UserManager.RemoveFromRolesAsync(user.Id, currentRoles.ToArray());
                    await UserManager.AddToRoleAsync(user.Id, model.Role);

                    return RedirectToAction("Index");
                }

                AddErrors(result);
            }

            ViewBag.Role = new SelectList(db.Roles.ToList(), "Name", "Name", model.Role);
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Delete(string username)
        {
            if (string.IsNullOrEmpty(username))
                return Json(new { success = false, message = "Không tìm thấy tài khoản" });

            var user = await UserManager.FindByNameAsync(username);
            if (user == null)
                return Json(new { success = false, message = "Không tìm thấy người dùng" });

            var result = await UserManager.DeleteAsync(user);
            if (result.Succeeded)
                return Json(new { success = true, message = "Xóa tài khoản thành công" });

            return Json(new { success = false, message = string.Join(", ", result.Errors) });
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}