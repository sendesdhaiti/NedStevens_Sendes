namespace MODEL;
public class User
{
    string first_name { get; set; }
    string last_name { get; set; }
    string email { get; set; }
    DateTime added { get; set; }

    public string First { get => first_name; }
    public string Last { get => first_name; }
    public string Email { get => first_name; }
    public DateTime Added { get => added; }

    public User(string f, string l, string e, DateTime d)
    {
        first_name = f;
        last_name = l;
        email = e;
        added = d;
    }
}
