using System.Net;
using Ecommerce.Application.Features.Products.Queries.GetProductById;
using Ecommerce.Application.Features.Products.Queries.GetProductList;
using Ecommerce.Application.Features.Products.Queries.PaginationProducts;
using Ecommerce.Application.Features.Products.Queries.Vms;
using Ecommerce.Application.Features.Shared.Queries;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductController: ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [AllowAnonymous]
    [HttpGet("list", Name = "GetProductList")]
    [ProducesResponseType(typeof(IReadOnlyList<Product>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProductList()
    {
        var query = new GetProductListQuery();
        return Ok(await _mediator.Send(query));
    }
    
    [AllowAnonymous]
    [HttpGet("listPagination", Name = "GetProductListPagination")]
    [ProducesResponseType(typeof(PaginationVm<ProductVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PaginationVm<ProductVm>>> GetProductListPagination(
        [FromQuery] PaginationProductQuery paginationProductQuery
    )
    {
        paginationProductQuery.Status = ProductStatus.Active;
        return Ok(await _mediator.Send(paginationProductQuery));
    }
    
    [AllowAnonymous]
    [HttpGet("{id}", Name = "GetProductById")]
    [ProducesResponseType(typeof(ProductVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductVm>> GetProductById(int id)
    {
        GetProductByIdQuery productByIdQuery = new GetProductByIdQuery(id);
        return Ok(await _mediator.Send(productByIdQuery));
    }
}
