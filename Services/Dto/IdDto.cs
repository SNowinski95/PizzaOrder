using Services.Interfaces;

namespace Services.Dto;

public class IdDto : IIdDto
{
    public Guid Id { get; set; }
}