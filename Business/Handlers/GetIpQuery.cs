using System.Net;
using System.Net.Sockets;
using Core.Utilities.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using IResult = Core.Utilities.Results.IResult;

namespace Business.Handlers;

public class GetIpQuery : IRequest<IResult>
{
    public class GetIpQueryHandler : IRequestHandler<GetIpQuery, IResult>
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetIpQueryHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IResult> Handle(GetIpQuery request,
            CancellationToken cancellationToken)
        {

            return new SuccessResult(GetIPAddress());
        }
    }
    public static string GetIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("No network adapters with an IPv4 address in the system!");
    }
}