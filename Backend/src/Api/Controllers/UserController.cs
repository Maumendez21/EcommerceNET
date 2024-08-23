using System.Net;
using Ecommerce.Application.Features.Auths.Users.Vms;
using Ecommerce.Application.Models.ImageMangment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Features.Auths.Users.Commands;
using Ecommerce.Application.Features.Auths.Users.Commands.RegisterUser;
using Ecommerce.Application.Features.Auths.Users.Commands.ResetPassword;
using Ecommerce.Application.Features.Auths.Users.Commands.ResetPasswordByToken;
using Ecommerce.Application.Features.Auths.Users.Commands.SendPassword;
using Ecommerce.Application.Features.Auths.Users.Commands.UpdateUser;
using Ecommerce.Domain;
using Ecommerce.Application.Features.Auths.Users.Commands.UpdateAdminUser;
using Ecommerce.Application.Models.Authorization;
using Ecommerce.Application.Features.Auths.Users.Commands.UpdateAdminStatusUser;
using Ecommerce.Application.Features.Auths.Users.Queries.GetUserById;
using Ecommerce.Application.Features.Auths.Users.Queries.GetUserByToken;
using Ecommerce.Application.Features.Auths.Users.Queries.GetUserByUsername;
using Ecommerce.Application.Features.Shared.Queries;
using Ecommerce.Application.Features.Auths.Users.Queries.PaginationUsers;
using Ecommerce.Application.Features.Auths.Roles.Queries.GetRoles;

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

      [HttpPut("update", Name = "Update")]
      [ProducesResponseType(typeof(ActionResult<Unit>), (int)HttpStatusCode.OK)]
      public async Task<ActionResult<Unit>> UpdatePassword([FromForm] UpdateUserCommand request)
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

      [Authorize(Roles = Role.ADMIN)]
      [HttpPut("updateAdminUser", Name = "UpdateAdminUser")]
      [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
      public async Task<ActionResult<User>> UpdateAdminUser([FromBody] UpdateAdminUserCommand request)
      {
         return Ok(await _mediator.Send(request));
      }
      
      
      [Authorize(Roles = Role.ADMIN)]
      [HttpPut("updateAdminStatusUser", Name = "UpdateAdminStatusUser")]
      [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
      public async Task<ActionResult<User>> UpdateAdminStatusUser([FromBody] UpdateAdminStatusUserCommand request)
      {
         return Ok(await _mediator.Send(request));
      }

      [AllowAnonymous]
      [HttpPost("forgotPassword", Name = "ForgotPassword")]
      [ProducesResponseType((int)HttpStatusCode.OK)]
      public async Task<ActionResult<string>> ForgotPassword([FromBody] SendPasswordCommand request)
      {
         return Ok( await _mediator.Send(request));
      }


      [AllowAnonymous]
      [HttpPost("resetPassword", Name = "ResetPassword")]
      [ProducesResponseType((int)HttpStatusCode.OK)]
      public async Task<ActionResult<string>> ResetPassword([FromBody] ResetPasswordByTokenCommand request)
      {
         return Ok(await _mediator.Send(request));
      }
      
      
      [HttpPost("updatePassword", Name = "UpdatePassword")]
      [ProducesResponseType((int)HttpStatusCode.OK)]
      public async Task<ActionResult<Unit>> UpdatePassword([FromBody] ResetPasswordCommand request)
      {
         return Ok(await _mediator.Send(request));
      }

      [Authorize(Roles = Role.ADMIN)]
      [HttpGet("{id}", Name = "GetUserById")]
      [ProducesResponseType(typeof(AuthResponse), (int)HttpStatusCode.OK)]
      public async Task<ActionResult<AuthResponse>> GetUserById(string id)
      {
         GetUserByIdQuery getUserQuery = new GetUserByIdQuery(id);
         return Ok(await _mediator.Send(getUserQuery));
      }
      
      [Authorize]
      [HttpGet("", Name = "GetUserByToken")]
      [ProducesResponseType(typeof(AuthResponse), (int)HttpStatusCode.OK)]
      public async Task<ActionResult<AuthResponse>> GetUserByToken()
      {
         GetUserByTokenQuery getUserQuery = new GetUserByTokenQuery();
         return Ok(await _mediator.Send(getUserQuery));
      }

      [Authorize(Roles = Role.ADMIN)]
      [HttpGet("username/{username}", Name = "GetUserByUsername")]
      [ProducesResponseType(typeof(AuthResponse), (int)HttpStatusCode.OK)]
      public async Task<ActionResult<AuthResponse>> GetUserByUsername(string username)
      {
         GetUserByUsernameQuery getUserQuery = new GetUserByUsernameQuery(username);
         return Ok(await _mediator.Send(getUserQuery));
      }
      [Authorize(Roles = Role.ADMIN)]
      [HttpGet("pagintation-admin", Name = "GetUsersPagination")]
      [ProducesResponseType(typeof(PaginationVm<User>), (int)HttpStatusCode.OK)]
      public async Task<ActionResult<PaginationVm<User>>> GetUsersPagination([FromQuery] PaginationUsersQuery request)
      {
         return Ok(await _mediator.Send(request));
      }

      [AllowAnonymous]
      [HttpGet("roles", Name = "GetRoles")]
      [ProducesResponseType(typeof(List<string>), (int)HttpStatusCode.OK)]
      public async Task<ActionResult<List<string>>> GetRoles()
      {
         GetRolesQuery getRolesQuery = new GetRolesQuery();
         return Ok(await _mediator.Send(getRolesQuery));
      }
   }
}