using AutoMapper;
using DataAccessGeneral.Interfaces.Reporsitory;
using DataAccessGeneral.Models;
using Services.Dto;
using Services.Interfaces;

namespace Services.Services;

public class OrderService : ServiceBase<OrderDto,Order>, IOrderService
{
    public OrderService(IOrderRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}