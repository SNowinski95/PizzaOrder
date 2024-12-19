using DataAccessGeneral.Interfaces;

namespace DataAccessGeneral.Models;

public class BaseEntity : IId
{
    public Guid Id { get; set; } 
}