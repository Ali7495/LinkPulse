using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Url.Api;
using Url.Application;

namespace MyApp.Namespace
{
    [Route("api/short-urls")]
    [ApiController]
    public class ShortUrlsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShortUrlsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateShortUrl([FromBody] CreateShortUrlRequest request)
        {
            string code = await _mediator.Send(new CreateShortUrlCommand(request.OriginalUrl, null));

            return Ok(new { ShortCode = code });
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetShortUrlByCode(string code)
        {
            var shortUrl = await _mediator.Send(new GetShortUrlsQuery(code));

            return Ok(shortUrl);
        }
    }
}
