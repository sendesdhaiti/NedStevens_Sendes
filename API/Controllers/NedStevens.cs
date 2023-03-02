using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using REPO;
using MODEL;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class NedStevens : Controller
    {
        private readonly Iaccess repo;
        public NedStevens(Iaccess a)
        {
            repo = a;
        }


        /// <summary>
        /// Check for email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("check-email")]
        public async Task<ActionResult<bool>> CHECK_if_account_exists(string email)
        {
            bool check = false;
            if (ModelState.IsValid)
            {
                check = await this.repo.CHECK_if_email_EXISTS(email);
                switch (check)
                {
                    case true:
                        return Ok(check);
                    default:
                        return NotFound(check);
                }
            }
            return BadRequest();
        }

        /// <summary>
        /// Log in to retrieve your info
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet("login")]
        public async Task<ActionResult<User>> LOGIN(string email, string password)
        {
            bool check = false;
            if (ModelState.IsValid)
            {
                check = await this.repo.CHECK_if_email_EXISTS(email);
                switch (check)
                {
                    case true:
                        User? user = await this.repo.LOGIN(email, password);
                        if (user != null)
                        {
                            return Ok(user);
                        }
                        else {
                            return NotFound(check);
                        }
                    default:
                        return NotFound(check);
                }
            }
            return BadRequest();
        }

        [HttpPost("signup")]
        public async Task<ActionResult<bool>> SIGNUP(string first_name, string last_name, string email, string password)
        {
            bool check = false;
            if (ModelState.IsValid)
            {
                check = await this.repo.CHECK_if_email_EXISTS(email);
                switch (check)
                {
                    case true:
                        return Conflict($"user already exists");
                    case false:
                        check = await this.repo.SIGNUP(first_name, last_name, email, password);
                        if (check)
                        {
                            return Created("signup", check);
                        }
                        else {
                            return Conflict(check);
                        }
                }
            }
            return BadRequest();
        }


        /// <summary>
        /// This changes a user's email
        /// </summary>
        /// <param name="newemail"></param>
        /// <param name="oldemail"></param>
        /// <returns></returns>
        [HttpPut("change-email")]
        public async Task<ActionResult<bool>> CHANGE_EMAIL(string newemail, string oldemail)
        {
            bool check = false;
            if (ModelState.IsValid)
            {
                check = await this.repo.CHECK_if_email_EXISTS(oldemail);
                switch (check)
                {
                    case true:
                        check = await this.repo.CHANGE_email(oldemail, newemail);
                        if (check)
                        {
                            return Ok(check);
                        }
                        else
                        {
                            return Conflict(check);
                        }
                    default:
                        return NotFound(check);
                }
            }
            return BadRequest();
        }


        [HttpDelete("delete-user")]
        public async Task<ActionResult<bool>> DELETE_USER(string email)
        {
            bool check = false;
            if (ModelState.IsValid)
            {
                check = await this.repo.CHECK_if_email_EXISTS(email);
                switch (check)
                {
                    case true:
                        check = await this.repo.DELETE_user(email);
                        if (check)
                        {
                            return Ok(check);
                        }
                        else
                        {
                            return Conflict(check);
                        }
                    default:
                        return NotFound(check);
                }
            }
            return BadRequest();
        }

    }
}

