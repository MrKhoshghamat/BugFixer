using System.ComponentModel.DataAnnotations;
using BugFixer.Domain.Entities.Common;

namespace BugFixer.Domain.Entities.Account;

public class User : BaseEntity
{
    #region Properties

    [Display(Name = "نام")]
    [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
    public string? FirstName { get; set; }
    
    [Display(Name = "نام خانوادگی")]
    [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
    public string? LastName { get; set; }
    
    [Display(Name = "شماره تماس")]
    [MaxLength(20, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
    public string? PhoneNumber { get; set; }
    
    [Display(Name = "ایمیل")]
    [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمیباشد")]
    public string Email { get; set; }
    
    [Display(Name = "کلمه عبور")]
    [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Password { get; set; }
    
    [Display(Name = "توضیحات")]
    public string? Description { get; set; }
    
    public bool IsEmailConfirmed { get; set; }
    public string EmailActivationCode { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsBanned { get; set; }
    public string Avatar { get; set; }

    #endregion

    #region Relations

    

    #endregion
}