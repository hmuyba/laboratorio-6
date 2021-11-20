using BoutiqueAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiqueAPI.Data.Repository
{
    public interface ILibraryRepository
    {
        //Boutique
        Task<IEnumerable<BoutiqueEntity>> GetBoutiquesAsync(string orderBy, bool showClothes = false);
        Task<BoutiqueEntity> GetBoutiqueAsync(int boutiqueId, bool showClothes = false);
        void CreateBoutique(BoutiqueEntity boutiqueModel);
        Task<bool> DeleteBoutiqueAsync(int boutiqueId);
        bool UpdateBoutique(BoutiqueEntity boutiqueModel);

        //Clothes
        void CreateClothes(ClothesEntity clothes);
        Task<ClothesEntity> GetClothesAsync(int clothesId);
        Task<IEnumerable<ClothesEntity>> GetClothessAsync(int boutiqueId);
        Task<bool> UpdateClothesAsync(ClothesEntity clothes);
        bool DeleteClothes(int clothesId);

        //save changes
        Task<bool> SaveChangesAsync();
    }
}
