using System;
using System.Threading.Tasks;
using System.Linq;
using TinyClothes.Data;
using Microsoft.EntityFrameworkCore;
using TinyClothes.Models;

namespace TinyClothes
{
    public static class AccountDb
    {
        public async static Task<bool> IsUsernameAvailable(string username, StoreContext context)
        {
            bool isTaken =
                await (from acc in context.Accounts
                       where username == acc.Username
                       select acc).AnyAsync();

            return isTaken;
        }

        public static async Task<Account> Register(StoreContext context, Account acc)
        {
            await context.Accounts.AddAsync(acc);
            await context.SaveChangesAsync();
            return acc;
        }

        /// <summary>
        /// Returns the account of the user with
        /// the supplied login credentials. Null
        /// is returned if there is no match
        /// </summary>
        /// <param name="login"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<Account> DoesUserAMatch(LoginViewModel login, StoreContext context)
        {
            Account acc =
                 await (from user in context.Accounts
                        where (user.Email == login.UsernameOrEmail ||
                             user.Username == login.UsernameOrEmail) &&
                             user.Password == login.Password
                        select user).SingleOrDefaultAsync();
           return acc;
        }
    }
}