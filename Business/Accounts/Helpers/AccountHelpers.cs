using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Accounts.LogicBusiness
{
    public static class AccountHelpers
    {
        //Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
        private static string PhotoPath = Path.Combine("StaticFiles", "Photos");

        public static string GetDefaultProfilePohot()
        {
            return Path.Combine(PhotoPath, "ProfilePhoto", "DefaultProfile.png");
        }
        public static string GetDefaultCoverPohot()
        {
            return Path.Combine(PhotoPath, "CoverPhoto", "DefaultCover.png");
        }
    }
}
