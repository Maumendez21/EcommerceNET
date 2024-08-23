using System.Net;
using Ecommerce.Application.Features.Auths.Countries.Queries.GetCountrys;
using Ecommerce.Application.Features.Auths.Countries.Vms;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CpuntryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CpuntryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpGet(Name = "GetCountries")]
    [ProducesResponseType(typeof(IReadOnlyList<CountryVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyList<CountryVm>>> GetCountries()
    {
        GetCountrysQuery getCountryQuery = new GetCountrysQuery();
        return Ok(await _mediator.Send(getCountryQuery));  
    }
}