using System.ComponentModel.DataAnnotations;

namespace SWH.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "اسم کاربر")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور فعلی")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "باید حد اقل {0} کرکتر باشد", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور جدید")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تثبت رمز عبور جدید")]
        [Compare("NewPassword", ErrorMessage = "رمز عبور و تثبیت آن یکسان نمیباشد")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "اسم کاربر")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "اسم کاربر")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تثبیت رمز عبور")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
