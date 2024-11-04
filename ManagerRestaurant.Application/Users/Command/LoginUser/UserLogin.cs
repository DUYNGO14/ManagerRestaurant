using ManagerRestaurant.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerRestaurant.Application.Users.Command.LoginUser
{
    public class UserLogin : IRequest<string>
    {
        [Required(ErrorMessage ="Email is requied!")]
        [EmailAddress(ErrorMessage ="Email not valid format!")]
        public string Email {  get; set; }
        [Required(ErrorMessage = "Password is requied!")]
        public string Password { get; set; }

    }
}
