using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DBManager
    {
        public TradeEntities db = new TradeEntities();

        public int role = 0;

        public async Task<bool> Auth(string login, string password)
        {
            bool auth = false;
            int id = 0;

            id = await db.User.AsNoTracking()
                .Where(o=>o.UserLogin== login && o.UserPassword == password)
                .Select(o=>o.UserID)
                .FirstOrDefaultAsync();
            if(id != 0)
            {
                role = await db.User
                    .AsNoTracking()
                    .Where(o=>o.UserLogin == login && o.UserPassword == password)
                    .Select(o=>o.UserRole)
                    .FirstOrDefaultAsync();
                auth = true;
            }
            return auth;
        }
    }
}
