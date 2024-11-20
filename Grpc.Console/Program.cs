// See https://aka.ms/new-console-template for more information

using System;
using Grpc.Contracts;
using Grpc.Core;
using Grpc.Net.Client;

Console.WriteLine("Hello, World!");

using var channel = GrpcChannel.ForAddress("http://localhost:55001", new GrpcChannelOptions
{
    Credentials = ChannelCredentials.Insecure
});

var client = new OrderService.OrderServiceClient(channel);

using var streamCall = client.GetOrders(new GetOrdersRequest
{
    UserId = 5
});

await foreach (var order in streamCall.ResponseStream.ReadAllAsync())
{
    Console.WriteLine($"Streamed OrderId: {order.OrderId}, Number: {order.Number}");
}

ConsoleKey key;

do
{
    var reply = client.GetOrder(new GetOrderRequest
    {
        OrderId = Random.Shared.Next(0, int.MaxValue)
    });

    Console.WriteLine($"OrderId: {reply.OrderId}, Number: {reply.Number}");
    Console.WriteLine("Press ESC to exit...");
    key = Console.ReadKey().Key;

} while (key != ConsoleKey.Escape);