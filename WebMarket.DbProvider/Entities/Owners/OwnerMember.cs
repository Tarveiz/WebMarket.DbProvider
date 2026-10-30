using WebMarket.DbProvider.Entities.Owners.Enums;
using WebMarket.DbProvider.Entities.Users;

namespace WebMarket.DbProvider.Entities.Owners;

/// <summary>
/// Участник владельца
/// </summary>
public class OwnerMember
{
    public Guid OwnerId { get; set; }

    public Guid UserId { get; set; }

    public OwnerMemberRole Role { get; set; }

    public DateTime CreatedAt { get; set; }

    // Навигационные свойства
    public Owner Owner { get; set; } = null!;

    public User User { get; set; } = null!;
}
