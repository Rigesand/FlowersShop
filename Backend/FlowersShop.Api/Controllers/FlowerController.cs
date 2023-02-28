using FlowersShop.Api.Models;
using FlowersShop.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlowersShop.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class FlowerController : ControllerBase
{
    private readonly FlowerService _service;

    public FlowerController(FlowerService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task AddFlower(Flower flower)
    {
        await _service.AddFlower(flower);
    }
    
    [HttpGet]
    public async Task<IEnumerable<Flower>> GetAllFlowers() => await _service.GetAllFlowers();
    
    [HttpGet]
    public async Task<Flower> GetById(string name)
    {
       return await _service.GetById(name);
    }
}