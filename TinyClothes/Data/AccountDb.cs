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
        /// Returns true if the username/email and password
        /// match a record in the database
        /// </summary>
        /// <param name="login"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<bool> DoesUserAMatch(LoginViewModel login, StoreContext context)
        {
            bool doesMatch =
                 await (from user in context.Accounts
                        where (user.Email == login.UsernameOrEmail ||
                             user.Username == login.UsernameOrEmail) &&
                             user.Password == login.Password
                        select user).AnyAsync();
           return doesMatch;
        }
    }
}