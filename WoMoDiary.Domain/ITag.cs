using System;
namespace WoMoDiary.Domain
{
    public interface ITag
    {
        Guid Id { get; set; }
        DateTimeOffset Created { get; set; }
        DateTimeOffset LastEdit { get; set; }
    }
}
