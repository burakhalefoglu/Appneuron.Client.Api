using System.Net;
using Core.Utilities.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using IResult = Core.Utilities.Results.IResult;

namespace Business.Handlers;

public class GetIpQuery : IRequest< IDataResult< Dictionary<string,string>>>
{
    public class GetIpQueryHandler : IRequestHandler<GetIpQuery, IDataResult< Dictionary<string,string>>>
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetIpQueryHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task< IDataResult< Dictionary<string,string>>> Handle(GetIpQuery request,
            CancellationToken cancellationToken)
        {
            var headers = _httpContextAccessor.HttpContext.Request.Headers;
            Dictionary<string, string> requestHeaders =
                new Dictionary<string, string>();
            foreach (var header in headers)
            {
                requestHeaders.Add(header.Key, header.Value);
            }
            return new SuccessDataResult<Dictionary<string,string>>(requestHeaders);
        }
    }
}