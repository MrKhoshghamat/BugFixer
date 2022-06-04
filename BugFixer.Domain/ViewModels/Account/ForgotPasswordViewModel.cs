using System.ComponentModel.DataAnnotations;
using BugFixer.Domain.ViewModels.Common;

namespace BugFixer.Domain.ViewModels.Account
{
    public class ForgotPasswordViewModel : GoogleRecaptchaViewModel
    {
        [Display(Name = "ایمیل")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمیباشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Email { get; set; }
    }

    public enum ForgotPasswordResult
    {
        UserBanned,
        UserNotFound,
        Success
    }
}
