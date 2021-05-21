using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using MachinePortal.Areas.Identity.Data;
using MachinePortal.Controllers;
using MachinePortal.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MachinePortal.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<MachinePortalUser> _userManager;
        private readonly SignInManager<MachinePortalUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IHostingEnvironment _appEnvironment;

        public IndexModel(
            IHostingEnvironment enviroment,
            UserManager<MachinePortalUser> userManager,
            SignInManager<MachinePortalUser> signInManager,
            IEmailSender emailSender)
        {
            _appEnvironment = enviroment;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string UserName { get; set; }

            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Phone]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }

            [Phone]
            [Display(Name = "Mobile Number")]
            public string Mobile { get; set; }

            public string Department { get; set; }
            public string JobRole { get; set; }
            public string PhotoPath { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            //await Permissions();
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userName = await _userManager.GetUserNameAsync(user);


            Username = userName;

            Input = new InputModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = userName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Mobile = user.Mobile,
                JobRole = user.JobRole,
                PhotoPath = user.PhotoPath
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile photo)
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Input.FirstName != user.FirstName)
            {
                user.FirstName = Input.FirstName;
            }
            if (Input.LastName != user.LastName)
            {
                user.LastName = Input.LastName;
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
            }

            if (Input.PhoneNumber != user.PhoneNumber)
            {
                user.PhoneNumber = Input.PhoneNumber;
            }
            if (Input.Mobile != user.Mobile)
            {
                user.Mobile = Input.Mobile;
            }
            if (Input.JobRole != user.JobRole)
            {
                user.JobRole = Input.JobRole;
            }

            if (photo != null && photo.Length > 0)
            {
                if (System.IO.File.Exists(_appEnvironment.WebRootPath + "\\" + Input.PhotoPath))
                {
                    System.IO.File.Delete(_appEnvironment.WebRootPath + "\\" + Input.PhotoPath);
                }

                //long filesSize = photo.Length;
                //var filePath = Path.GetTempFileName();

                string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
                fileName += photo.FileName.Substring(photo.FileName.LastIndexOf("."), (photo.FileName.Length - photo.FileName.LastIndexOf(".")));
                string destinationPath = _appEnvironment.WebRootPath + "\\resources\\Users\\Photos\\" + fileName;
                user.PhotoPath = @"/resources/Users/Photos/" + fileName;

                using (var stream = new FileStream(destinationPath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }
            }

            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId, code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }
    }
}
