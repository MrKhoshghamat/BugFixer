using System.ComponentModel.DataAnnotations;
using BugFixer.Domain.ViewModels.Common;

namespace BugFixer.Domain.ViewModels.Account
{
    public class ResetPasswordViewModel : GoogleRecaptchaViewModel
    {
        [Required]
        public string EmailActivationCode { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public string RePassword { get; set; }
    }

    public enum ResetPasswordResult
    {
        Success,
        UserNotFound,
        UserIsBanned
    }
}
