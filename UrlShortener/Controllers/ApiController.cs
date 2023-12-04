using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using UrlShortener.Commands;
using UrlShortener.Extensions;
using UrlShortener.Queries;

namespace UrlShortener.Controllers;

[ApiController]
public class ApiController: ControllerBase
{
    private readonly ISender sender;
    private readonly IHttpContextAccessor httpContextAccessor;

    public ApiController(ISender sender, IHttpContextAccessor httpContextAccessor)
    {
        this.sender = sender ?? throw new ArgumentNullException(nameof(sender));
        this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    [Route("/")]
    [HttpPost]
    public async Task<IActionResult> ShortenUrl([FromBody][Required] string url)
    {
        if (Uri.TryCreate(url, UriKind.Absolute, out Uri uri) == false)
        {
            return BadRequest("Invalid url");
        }

        var result = await this.sender.Send(new ShortentUrlCommand(uri));
        var baseUrl = httpContextAccessor.HttpContext?.Request.BaseUrl();

        return new JsonResult(new Uri(baseUrl, result).AbsoluteUri);
    }

    [Route("/{slug}")]
    [HttpGet]
    public async Task<IActionResult> Index([FromRoute] string slug)
    {
        var result = await this.sender.Send(new FindUrlMappingQuery(slug));
        if (result == null)
        {
            return NotFound();
        }

        return Redirect(result.AbsoluteUri);
    }
}

