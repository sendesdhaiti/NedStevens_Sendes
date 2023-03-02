using System;
using MODEL;

namespace REPO
{
	public interface Iaccess
	{
        Task<bool> CHECK_if_email_EXISTS(string email);
        Task<bool> SIGNUP(string f, string l, string email, string password);
        Task<User?> LOGIN(string email, string password);
        Task<bool> CHANGE_email(string oldemail, string newemail);
        Task<bool> DELETE_user(string email);

    }
}

