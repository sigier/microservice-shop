using AutoMapper;
using EventBus.Messages.Events;
using Ordering.Application.Features.Orders.Commands;

namespace Ordering.API.Mapper
{
    public class OrderingProfile : Profile
    {
        public OrderingProfile()
        {
            CreateMap<CheckoutOrderCommand, CartCheckoutEvent>().ReverseMap();
        }
    }
}
