namespace MassageStudioLorem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static Global.GlobalConstants.DataValidations;

    public class Comment
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        [MaxLength(CommentMaxLength)]
        public string Content { get; set; }

        [Required] public string ClientId { get; set; }

        public Client Client { get; set; }

        [Required] public string MasseurId { get; set; }

        public Masseur Masseur { get; set; }
    }
}