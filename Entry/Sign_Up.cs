using System;

public class Sign_Up
{
    public string SignUp(User user)
    {
        if (string.IsNullOrWhiteSpace(user.Username) || user.Username.Length < 3 || !user.Username.All(char.IsLetter))
        {
            return "Username must be at least 3 characters long and cannot contain numbers or special characters.";
        }

        if (string.IsNullOrWhiteSpace(user.Password) || user.Password.Length < 8)
        {
            return "Password must be at least 8 characters long.";
        }



        using (var context = new StepShopDbContext())
        {
            if (context.Users.Any(u => u.Username.ToLower() == user.Username.ToLower()))
            {
                return "This username already exists!";
            }

            context.Users.Add(user);
            context.SaveChanges();
            return "Qeydiyyat uğurla tamamlandı!";





        }
    }
}
