using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RateYourMovie.Areas.Identity.Data;

// Add profile data for application users by adding properties to the RateYourMovieUser class
public class RateYourMovieUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Pronounce { get; set; }
}

