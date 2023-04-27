using JwtApp.Back.Core.Application.Dto;
using MediatR;

namespace JwtApp.Back.Core.Application.Features.CQRS.Queries
{
    public class GetProductQueryRequest : IRequest<ProductListDto>
    {
        public int Id { get; set; }

        public GetProductQueryRequest(int id)
        {
            Id = id;
        }


    }
}
