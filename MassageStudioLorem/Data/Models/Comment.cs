namespace MassageStudioLorem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MassageStudioLorem.Data.Common.Models;

    public class Comment : BaseDeletableModel<string>
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Content { get; set; }

        [Required]
        public string ClientId { get; set; }

        public virtual Client Client { get; set; }

        [Required]
        public string MasseurId { get; set; }
        
        public virtual Masseur Masseur { get; set; }
    }
}
