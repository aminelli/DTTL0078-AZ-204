using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

[ApiController]
[Route("/[controller]")]
[OutputCache(PolicyName = "ExpireImages")]
public class TestController : ControllerBase
{

    public async Task GetAsync()
    {
        await Gravatar.WriteGravatar(HttpContext);
    }

}