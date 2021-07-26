using dotnet5_WebAPI.Services.FightService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet5_WebAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class FightController : ControllerBase
    {
        private readonly IFightService _fightService;
        public FightController(IFightService fightService)
        {
            _fightService = fightService;
        }
    }
}