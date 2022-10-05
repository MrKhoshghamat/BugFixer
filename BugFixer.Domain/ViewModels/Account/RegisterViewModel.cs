using System.ComponentModel.DataAnnotations;

namespace BugFixer.Domain.ViewModels.Account;

public class RegisterViewModel
{
    [Display(Name = "ایمیل")]
    [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمیباشد")]
    public string Email { get; set; }
    
    [Display(Name = "کلمه عبور")]
    [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Password { get; set; }
    
    [Display(Name = "کلمه عبور")]
    [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
    public string RePassword { get; set; }
}

public enum RegisterResult
{
    EmailExisted,
    Success
}