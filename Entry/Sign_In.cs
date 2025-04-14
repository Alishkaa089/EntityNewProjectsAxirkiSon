    public class Sign_In
    {
        public User SignIn(string username, string password)
        {
            using (var context = new StepShopDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
                if (user == null)
                {
                    throw new Exception("Login failed!");
                }
                return user;
            }
        }

    }




