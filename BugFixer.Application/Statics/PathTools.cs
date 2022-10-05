namespace BugFixer.Application.Statics;

public static class PathTools
{
    #region User

    public static readonly string DefaultUserAvatar = "DefaultAvatar.png";
    public static readonly string UserAvatarServerPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/user/");
    public static readonly string UserAvatarPath = "/content/user/";

    #endregion
}