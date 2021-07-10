namespace MassageStudioLorem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MassageStudioLorem.Data.Common.Models;

    //TODO: Check if I need this class
    public class Admin : BaseDeletableModel<string>
    {
        public Admin()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Required] public string FirstName { get; set; }

        [Required] public string LastName { get; set; }

        [Required] public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}