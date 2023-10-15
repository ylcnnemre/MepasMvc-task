using MepasTask.Models;

namespace MepasTask.Abstract
{
    public interface IUserRepository
    {
        public UserModel? findByUsername(string username);

        public void addUser(UserModel user);
    }
}
