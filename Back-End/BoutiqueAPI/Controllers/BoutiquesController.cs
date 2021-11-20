using BoutiqueAPI.Exceptions;
using BoutiqueAPI.Models;
using BoutiqueAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiqueAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    public class BoutiquesController : Controller
    {
        private IBoutiquesService _boutiqueService;
        public BoutiquesController(IBoutiquesService boutiqueService)
        {
            this._boutiqueService = boutiqueService;
        }

        //api/boutiques
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BoutiqueModel>>> GetBoutiquesAsync(string orderBy = "Id", bool showClothes = false)
        {
            try
            {
                return Ok(await _boutiqueService.GetBoutiquesAsync(orderBy, showClothes));
            }
            catch(BadRequestOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happend: {ex.Message}");
            }
        }

        //api/boutiques/boutiqueId
        [HttpGet("{boutiqueId:int}", Name ="GetBoutique")]
        public async Task<ActionResult<BoutiqueModel>> GetBoutiqueAsync(int boutiqueId, bool showClothes = false)
        {
            try
            {
                return await _boutiqueService.GetBoutiqueAsync(boutiqueId, showClothes);
            }
            catch (NotFoundOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happend: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<BoutiqueModel>> CreateBoutiqueAsync([FromBody] BoutiqueModel boutiqueModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var url = HttpContext.Request.Host;
                var newBoutique = await _boutiqueService.CreateBoutiqueAsync(boutiqueModel);
                return CreatedAtRoute("GetBoutique", new { boutiqueId = newBoutique.Id }, newBoutique);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happend: {ex.Message}");
            }
        }

        [HttpDelete("{boutiqueId:int}")]
        public async Task<ActionResult<DeleteModel>> DeleteBoutiqueAsync(int boutiqueId)
        {
            try
            {
                return Ok(await _boutiqueService.DeleteBoutiqueAsync(boutiqueId));
            }
            catch (NotFoundOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happend: {ex.Message}");
            }
        }

        [HttpPut("{boutiqueId:int}")]
        public async Task<IActionResult> UpdateBoutiqueAsync(int boutiqueId, [FromBody] BoutiqueModel boutiqueModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    foreach (var pair in ModelState)
                    {
                        if (pair.Key == nameof(boutiqueModel.Address) && pair.Value.Errors.Count > 0)
                        {
                            return BadRequest(pair.Value.Errors);
                        }
                    }
                }
                return Ok(await _boutiqueService.UpdateBoutiqueAsync(boutiqueId, boutiqueModel));
            }
            catch (NotFoundOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happend: {ex.Message}");
            }
        }
    }
}
