using System.Net;
using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Features.Auths.Users.Commands;
using Ecommerce.Application.Features.Auths.Users.Commands.RegisterUser;
using Ecommerce.Application.Features.Auths.Users.Vms;
using Ecommerce.Application.Models.ImageMangment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IManageImageService _manageImageService;

        public UserController(IMediator mediator, IManageImageService manageImageService)
        {
           this._mediator = mediator;
           _manageImageService = manageImageService;
        }

        [AllowAnonymous]
        [HttpPost("singIn", Name = "Login")]
        [ProducesResponseType(typeof(ActionResult<AuthResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthResponse>> Login(
           [FromBody] LoginUserCommand request
        )
        {
           return Ok(await _mediator.Send(request));
        }
        
        [AllowAnonymous]
        [HttpPost("register", Name = "Register")]
        [ProducesResponseType(typeof(ActionResult<AuthResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthResponse>> Register(
           [FromForm] RegisterUserCommand request
        )
        {
            if (request.Avatar is not null)
            {
               ImageResponse resultImage = await _manageImageService.UploadImage(new ImageData
               {
                  ImageStream = request.Avatar!.OpenReadStream(),
                  ImageName = request.Avatar.Name
               }
               );

               request.AvatarId = resultImage.ImagePublicId;
               request.AvatarUrl = resultImage.ImageUrl;
            }

           return Ok(await _mediator.Send(request));
        }
    }
}