namespace BugFixer.Application.Statics
{
    public static class PathTools
    {
        #region User

        public static readonly string DefaultUserAvatar = "DefaultAvatar.png";
        public static readonly string UserAvatarServerPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/User/");
        public static readonly string UserAvatarPath = "/Content/User/";

        #endregion

        #region Site

        public static readonly string SiteAddress = "https://localhost:7097";

        #endregion
    }
}
