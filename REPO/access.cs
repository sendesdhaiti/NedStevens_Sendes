namespace REPO;
using MySql.Data;
using MySql.Data.MySqlClient;
using MODEL;
public class Access :Iaccess
{
    private string Conn = "Server=sql9.freesqldatabase.com;User ID=sql9602141;Password=JEd1fxhJp8;Database=sql9602141; Convert Zero Datetime=True";

    /// <summary>
    /// This checks to see if user exists
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<bool> CHECK_if_email_EXISTS(string email)
    {
        string cmd = " SELECT email from User where email = @email ; ";
        bool CHECK = false;
        using (MySqlConnection conn = new MySqlConnection(this.Conn)) {
            MySqlCommand command = new MySqlCommand(cmd , conn);
            command.Parameters.AddWithValue("@email", email);

            conn.Open();
            Console.WriteLine($"\n\n\t\t The check for {email} is checking at {DateTime.Now}");
            var ret = await command.ExecuteReaderAsync();
            if (ret.Read())
            {
                CHECK = true;
            }

            conn.Close();

        }
        Console.WriteLine($"\n\n\t\t The check for {email} was {CHECK} at {DateTime.Now}");
        return CHECK;
    }

    /// <summary>
    /// This signs a user up and saves them to the DB
    /// </summary>
    /// <param name="f"></param>
    /// <param name="l"></param>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<bool> SIGNUP(string f, string l, string email, string password)
    {
        string cmd = " INSERT into User (firstname, lastname, email, password, added) VALUES( @f, @l, @email, @pass, @added ) ; ";
        bool CHECK = false;
        using (MySqlConnection conn = new MySqlConnection(this.Conn))
        {
            MySqlCommand command = new MySqlCommand(cmd, conn);
            command.Parameters.AddWithValue("@f", f);
            command.Parameters.AddWithValue("@l", l);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@pass", password);
            command.Parameters.AddWithValue("@added", DateTime.UtcNow);

            conn.Open();
            Console.WriteLine($"\n\n\t\t The signup for {email} is saving at {DateTime.Now}");
            var ret = await command.ExecuteNonQueryAsync();
            if (ret > 0)
            {
                CHECK = true;
            }

            conn.Close();

        }
        Console.WriteLine($"\n\n\t\t The signup for {email} was {CHECK} at {DateTime.Now}");
        return CHECK;
    }

    /// <summary>
    /// This logs the user in by getting their info
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<User?> LOGIN(string email, string password)
    {
        string cmd = " SELECT firstname, lastname, email, added from User where email = @email AND password = @pass; ";
        User? user = null;
        using (MySqlConnection conn = new MySqlConnection(this.Conn))
        {
            MySqlCommand command = new MySqlCommand(cmd, conn);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@pass", password);

            conn.Open();
            Console.WriteLine($"\n\n\t\t The login for {email} is logging in at {DateTime.Now}");
            var ret = await command.ExecuteReaderAsync();
            if (ret.Read())
            {
                user = new User(
                    ret.GetString(0),
                    ret.GetString(1),
                    ret.GetString(2),
                    ret.GetDateTime(3)
                    );
            }

            conn.Close();

        }
        Console.WriteLine($"\n\n\t\t The login for {email} was {user?.First ?? "Null"} at {DateTime.Now}");
        return user;
    }

    /// <summary>
    /// This changes the user's email
    /// </summary>
    /// <param name="oldemail"></param>
    /// <param name="newemail"></param>
    /// <returns></returns>
    public async Task<bool> CHANGE_email(string oldemail, string newemail)
    {
        string cmd = " UPDATE User  SET email = @newemail where email = @oldemail ; ";
        bool CHECK = false;
        using (MySqlConnection conn = new MySqlConnection(this.Conn))
        {
            MySqlCommand command = new MySqlCommand(cmd, conn);
            command.Parameters.AddWithValue("@oldemail", oldemail);
            command.Parameters.AddWithValue("@newemail", newemail);

            conn.Open();
            Console.WriteLine($"\n\n\t\t The update for {oldemail} is changing at {DateTime.Now}");
            var ret = await command.ExecuteNonQueryAsync();
            if (ret > 0)
            {
                CHECK = true;
            }

            conn.Close();

        }
        Console.WriteLine($"\n\n\t\t The update for {oldemail} was {CHECK} at {DateTime.Now}");
        return CHECK;
    }

    /// <summary>
    /// This deletes the user's email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<bool> DELETE_user( string email)
    {
        string cmd = " DELETE from User where email = @email ; ";
        bool CHECK = false;
        using (MySqlConnection conn = new MySqlConnection(this.Conn))
        {
            MySqlCommand command = new MySqlCommand(cmd, conn);
            command.Parameters.AddWithValue("@email", email);

            conn.Open();
            Console.WriteLine($"\n\n\t\t The deletion for {email} is deleting at {DateTime.Now}");
            var ret = await command.ExecuteNonQueryAsync();
            if (ret > 0)
            {
                CHECK = true;
            }

            conn.Close();

        }
        Console.WriteLine($"\n\n\t\t The deletion for {email} was {CHECK} at {DateTime.Now}");
        return CHECK;
    }






}
