using Microsoft.EntityFrameworkCore;
using SignalR_SqlTableDependency.Models;

namespace SignalR_SqlTableDependency.Repositories
{
    public class UserRepo
    {
        private readonly SignalRWithEFContext dbContext;

        public UserRepo(DbContextOptions<SignalRWithEFContext> options, IConfiguration configuration)
        {
            dbContext = new SignalRWithEFContext(options, configuration);
        }

        public async Task<User> GetUserDetails(string username, string password)
        {
            return await dbContext.Users.FirstOrDefaultAsync(user => user.Username == username && user.Password == password);
        }
    }
}