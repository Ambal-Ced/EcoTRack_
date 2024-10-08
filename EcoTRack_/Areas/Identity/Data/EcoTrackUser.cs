using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace EcoTRack_.Areas.Identity.Data;

// Add profile data for application users by adding properties to the EcoTrackUser class
public class EcoTrackUser : IdentityUser
{

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Type_ { get; set; }
}


