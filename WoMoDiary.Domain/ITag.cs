using System;
namespace WoMoDiary.Domain
{
    public interface IItem
    {
        Guid Id { get; set; }
    }
    public interface ITag
    {
        Guid Id { get; set; }
        DateTimeOffset Created { get; set; }
        DateTimeOffset LastEdit { get; set; }
    }
}
