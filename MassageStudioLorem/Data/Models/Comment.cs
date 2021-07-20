namespace MassageStudioLorem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Content { get; set; }

        [Required]
        public string ClientId { get; set; }

        public Client Client { get; set; }

        [Required]
        public string MasseurId { get; set; }
        
        public Masseur Masseur { get; set; }
    }
}
