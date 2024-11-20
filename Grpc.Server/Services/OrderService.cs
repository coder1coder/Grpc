using System;
using System.Threading.Tasks;
using Grpc.Contracts;
using Grpc.Core;

namespace Grpc.Server.Services;

public class OrderService: Contracts.OrderService.OrderServiceBase
{
    public override async Task GetOrders(GetOrdersRequest request, IServerStreamWriter<GetOrdersResponse> responseStream, ServerCallContext context)
    {
        Console.WriteLine("GetOrders");
        
        for (var i = 0; i < 10; i++)
        {
            await responseStream.WriteAsync(new GetOrdersResponse
            {
                OrderId = Random.Shared.Next(0, int.MaxValue),
                Number = Guid.NewGuid().ToString(),
                UserId = request.UserId
            });
        }
    }

    public override Task<GetOrderResponse> GetOrder(GetOrderRequest request, ServerCallContext context)
    {
        return Task.FromResult(new GetOrderResponse
        {
            OrderId = request.OrderId,
            Number = Guid.NewGuid().ToString()
        });
    }
}