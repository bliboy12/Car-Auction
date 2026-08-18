// BuildingBlocks or Persistence
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class UtcDateTimeConverter : ValueConverter<DateTime, DateTime>
{
    public UtcDateTimeConverter() :
    base(v => DateTime.SpecifyKind(v, DateTimeKind.Unspecified), v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
    {
    }
}