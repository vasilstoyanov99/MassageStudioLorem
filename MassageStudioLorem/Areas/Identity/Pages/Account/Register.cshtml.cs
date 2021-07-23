namespace MassageStudioLorem.Areas.Identity.Pages.Account
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;

    using Data;
    using static Global.GlobalConstants.ErrorMessages;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly LoremDbContext _data;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            LoremDbContext data)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._logger = logger;
            this._emailSender = emailSender;
            this._data = data;
        }

        [BindProperty] public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Username")]
            public string UserName { get; set; }

            [Required]
            [StringLength(100,
                ErrorMessage = PasswordLength,
                MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password",
                ErrorMessage = PasswordConformation)]
            public string ConfirmPassword { get; set; }

            [Required]
            [Phone]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;
            this.ExternalLogins = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync
            (string returnUrl = null)
        {
            string phoneNumberFromBase = this._data
                .Users
                .Select(u => u.PhoneNumber)
                .Where(pn =>
                    pn.Trim() == this.Input.PhoneNumber.Trim())
                .FirstOrDefault();

            returnUrl ??= this.Url.Content("~/");
            this.ExternalLogins = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (this.ModelState.IsValid)
            {
                IdentityResult result = null;

                if (phoneNumberFromBase == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        Email = this.Input.Email,
                        UserName = this.Input.UserName,
                        PhoneNumber = this.Input.PhoneNumber
                    };

                    result = await this._userManager
                        .CreateAsync(user, this.Input.Password);

                    if (result.Succeeded)
                    {
                        this._logger.LogInformation
                            ("User created a new account with password.");

                        string code = await this._userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders
                            .Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                        string callbackUrl = this.Url.Page(
                            "/Account/ConfirmEmail",
                            null,
                            new {area = "Identity", userId = user.Id, code, returnUrl},
                            this.Request.Scheme);

                        await this._emailSender.SendEmailAsync(this.Input.Email,
                            "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        if (this._userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return this.RedirectToPage("RegisterConfirmation",
                                new {email = this.Input.Email, returnUrl});
                        }

                        await this._signInManager.SignInAsync(user,
                            false);
                        return this.LocalRedirect(returnUrl);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Please enter a different phone number!");
                }

                if (result != null)
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        this.ModelState.AddModelError(string.Empty,
                            error.Description);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }
    }
}