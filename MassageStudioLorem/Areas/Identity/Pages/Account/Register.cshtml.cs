namespace MassageStudioLorem.Areas.Identity.Pages.Account
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Data;
    using Data.Models;
    using static Global.GlobalConstants.ErrorMessages;
    using static Client.ClientConstants;


    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly LoremDbContext _data;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            LoremDbContext data)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._data = data;
        }

        [BindProperty] 
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress(ErrorMessage = "The provided e-mail is not valid!")]
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
            [Display(Name = "Confirm Password")]
            [Compare("Password",
                ErrorMessage = PasswordConformation)]
            public string ConfirmPassword { get; set; }

            [Required]
            [RegularExpression
                (PhoneNumberRegex, ErrorMessage = InvalidPhoneNumber)]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }
        }

        public void OnGet(string returnUrl = null) => this.ReturnUrl = returnUrl;

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

            if (this.ModelState.IsValid)
            {
                if (phoneNumberFromBase == null)
                {
                    IdentityUser user = new()
                    {
                        Email = this.Input.Email,
                        UserName = this.Input.UserName,
                        PhoneNumber = this.Input.PhoneNumber
                    };

                    IdentityResult result = await this._userManager
                        .CreateAsync(user, this.Input.Password);

                    if (result.Succeeded)
                    {
                        await this._data.Clients
                            .AddAsync(new Client() {UserId = user.Id});
                        await this._data.SaveChangesAsync();
                        await this._userManager.AddToRoleAsync(user, ClientRoleName);
                        await this._signInManager.SignInAsync(user,
                            false);

                        return this.LocalRedirect(returnUrl);
                    }

                    foreach (IdentityError error in result.Errors)
                    {
                        this.ModelState.AddModelError(string.Empty,
                            error.Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Please enter a different phone number!");
                    return this.Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }
    }
}