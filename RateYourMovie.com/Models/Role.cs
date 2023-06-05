using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RateYourMovie.Models
{
    public class Role
    {
        //public Role(int roleId, string roleName)
        //{
        //    RoleId = roleId;
        //    RoleName = roleName;
        //}

        //[Key]
        //public int RoleId { get; set; }


        //[Required]
        [Key]
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string RoleName { get; set; }

    }
}
