using JWT_Authentication.Entity;

namespace JWT_Authentication.Repository
{
    public class UserRepository
    {
        public static User Get(string username,string password)
        {

            var users = new List<User>();   
            users.Add(new User()
            {
                Username = "robert",
                Password = "robertPassword",
                Role = "admin"
            });
            users.Add(new User()
            {
                Username = "gabriel",
                Password = "gabrielPassword",
                Role = "developer"
            });
            users.Add(new User()
            {
                Username = "amanda",
                Password = "amandaPassword",
                Role = "manager"
            });

            return users.FirstOrDefault(x => x.Username == username && x.Password == password);
        }

    }
}
