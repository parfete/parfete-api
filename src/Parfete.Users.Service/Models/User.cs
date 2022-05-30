using System.ComponentModel.DataAnnotations;

namespace Parfete.Users.Service.Models
{
    public record User
    {
        [Key]
        public Guid Id { get; init; } = Guid.Empty;
        
        [Required]
        [MaxLength(250)]
        public string Name { get; init; } = "";

        public UserRole Role { get; init; } = UserRole.Guest;
    }
}