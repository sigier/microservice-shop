
using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Features.Orders.Commands;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using System;
using System.Threading.Tasks;

namespace Ordering.API.EventBusConsumer
{
    public class CartCheckoutConsumer : IConsumer<CartCheckoutConsumer>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<CartCheckoutConsumer> _logger;

        public CartCheckoutConsumer(IMediator mediator, IMapper mapper, ILogger<CartCheckoutConsumer> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task Consume(ConsumeContext<CartCheckoutConsumer> context)
        {
            CheckoutOrderCommand command = _mapper.Map<CheckoutOrderCommand>(context.Message);
            int result = await _mediator.Send(command);

            _logger.LogInformation("BasketCheckoutEvent consumed successfully. Created Order Id : {newOrderId}", result);
        }
    }
}
