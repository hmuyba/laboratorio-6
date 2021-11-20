using BoutiqueAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiqueAPI.Services
{
    public interface IClothesService
    {
        Task<ClothesModel> CreateClothesAsync(int BoutiqueId, ClothesModel clothes);
        Task<ClothesModel> GetClothesAsync(int BoutiqueId, int clothesId);
        Task<IEnumerable<ClothesModel>> GetClothessAsync(int BoutiqueId);
        Task<bool> UpdateClothesAsync(int BoutiqueId, int clothesId, ClothesModel clothes);
        Task<bool> DeleteClothesAsync(int BoutiqueId, int clothesId);
    }
}
