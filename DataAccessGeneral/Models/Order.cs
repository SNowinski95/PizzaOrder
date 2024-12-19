using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessGeneral.Models;

public class Order : BaseEntity
{
    public Guid PizzaId { get; set; }
    public Pizza Pizza { get; set; }
    public string Person { get; set; }
}

