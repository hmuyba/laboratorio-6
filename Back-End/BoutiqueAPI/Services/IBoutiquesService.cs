using BoutiqueAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiqueAPI.Services
{
    public interface IBoutiquesService
    {
        Task<IEnumerable<BoutiqueModel>> GetBoutiquesAsync(string orderBy, bool showClothes);
        Task<BoutiqueModel> GetBoutiqueAsync(int boutiqueId, bool showClothes);
        Task<BoutiqueModel> CreateBoutiqueAsync(BoutiqueModel boutiqueModel);
        Task<DeleteModel> DeleteBoutiqueAsync(int boutiqueId);
        Task<BoutiqueModel> UpdateBoutiqueAsync(int boutiqueId, BoutiqueModel boutique);
    }
}
