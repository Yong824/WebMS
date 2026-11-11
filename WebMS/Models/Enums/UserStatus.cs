namespace WebMS.Models.Enums;

public enum UserStatus
{
    Normal = 0,     // active
    Frozen = 1,     // temporary suspension (FrozenUntil set)
    Blocked = 2     // permanent ban
}