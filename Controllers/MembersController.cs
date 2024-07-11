using PerpusApi.Models;
using PerpusApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace PerpusApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class MembersController : ControllerBase
{
    private readonly MembersService _membersService;

    public MembersController(MembersService membersService) =>
        _membersService = membersService;

    //Method GET
    [HttpGet]
    public async Task<List<Member>> Get() =>
        await _membersService.GetAsync();
    
    //Method GET (Get detail data with params)
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Member>> Get(string id)
    {
        var member = await _membersService.GetAsync(id);

        if (member is null)
        {
            return NotFound();
        }

        return member;
    }
    
    //Method POST
    [HttpPost]
    public async Task<IActionResult> Post(Member newMember)
    {
        await _membersService.CreateAsync(newMember);

        return CreatedAtAction(nameof(Get), new { id = newMember.Id }, newMember);
    }
    
    //Method PUT
    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Member updatedMember)
    {
        var member = await _membersService.GetAsync(id);

        if (member is null)
        {
            return NotFound();
        }

        updatedMember.Id = member.Id;

        await _membersService.UpdateAsync(id, updatedMember);

        return NoContent();
    }
    
    //Method DELETE
    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var member = await _membersService.GetAsync(id);

        if (member is null)
        {
            return NotFound();
        }

        await _membersService.RemoveAsync(id);

        return NoContent();
    }

}