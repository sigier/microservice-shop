
using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Queries
{
    public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrderDto>>
    {

        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<OrderDto>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Order> orderList = await _orderRepository.GetOrdersByUserName(request.UserName);
            return _mapper.Map<List<OrderDto>>(orderList);
        }
    }
}
