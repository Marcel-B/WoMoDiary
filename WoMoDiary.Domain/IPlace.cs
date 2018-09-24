using System;
namespace WoMoDiary.Domain
{
    public interface IPlace
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string AssetName { get; set; }
        bool? IsGood { get; set; }
        Location Location { get; set; }
    }
}
