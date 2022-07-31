namespace LockManagementSystem.Domain.Enums;

public enum LockEventStatusEnum
{
    None,
    Opened,
    Closed,
    Pending,
    Error,
    Unauthorized,
    UnderMaintenance,
}