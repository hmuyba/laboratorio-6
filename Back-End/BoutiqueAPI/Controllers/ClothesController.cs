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
    [Route("api/boutiques/{boutiqueId:int}/[controller]")]
    public class ClothesController : ControllerBase
    {
        private IClothesService _clothesService;

        public ClothesController(IClothesService clothesService)
        {
            _clothesService = clothesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClothesModel>>> GetClothes(int boutiqueId)
        {
            try
            {
                return Ok(await _clothesService.GetClothessAsync(boutiqueId));
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

        [HttpGet("{clothesId:int}", Name = "GetClothes")]
        public async Task<ActionResult<ClothesModel>> GetClothesAsync(int boutiqueId, int clothesId)
        {
            try
            {
                return Ok(await _clothesService.GetClothesAsync(boutiqueId, clothesId));
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
        public async Task<ActionResult<ClothesModel>> CreateClothesAsync(int boutiqueId, [FromBody] ClothesModel clothes)
        {
            try
            {
                var clothesCreated = await _clothesService.CreateClothesAsync(boutiqueId, clothes);
                return CreatedAtRoute("GetClothes", new { boutiqueId = boutiqueId, clothesId = clothesCreated.Id }, clothesCreated);
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

        [HttpPut("{clothesId:int}")]
        public async Task<ActionResult<ClothesModel>> UpdateClothesAsync(int boutiqueId, int clothesId, [FromBody] ClothesModel clothes)
        {
            try
            {
                return Ok(await _clothesService.UpdateClothesAsync(boutiqueId, clothesId, clothes));
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

        [HttpDelete("{clothesId:int}")]
        public async Task<ActionResult<bool>> DeleteClothesAsync(int boutiqueId, int clothesId)
        {
            try
            {
                return Ok(await _clothesService.DeleteClothesAsync(boutiqueId, clothesId));
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
