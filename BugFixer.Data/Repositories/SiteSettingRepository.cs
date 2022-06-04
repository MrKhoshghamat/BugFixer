using BugFixer.Data.Context;
using BugFixer.Domain.Entities.SiteSetting;
using BugFixer.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BugFixer.Data.Repositories
{
    public class SiteSettingRepository : ISiteSettingRepository
    {
        #region Ctor

        private BugFixerDbContext _context;

        public SiteSettingRepository(BugFixerDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<EmailSetting> GetDefaultEmail()
        {
            return await _context.EmailSettings.FirstOrDefaultAsync(s => !s.IsDeleted && s.IsDefault);
        }
    }
}
