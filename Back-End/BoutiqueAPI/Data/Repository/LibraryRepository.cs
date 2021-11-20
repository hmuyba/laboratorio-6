using BoutiqueAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiqueAPI.Data.Repository
{
    public class LibraryRepository : ILibraryRepository
    {
        private LibraryDbContext _dbContext;
        public LibraryRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /*private List<BoutiqueEntity> boutiques = new List<BoutiqueEntity>
        {
            new BoutiqueEntity() {Id = 1, Address="Quillacollo", Country="Cochabamba", MobilePhone="66771123", Name="Boutique Quillacollo", Owner="Juan Boutique"},
            new BoutiqueEntity() {Id = 2, Address="Tiquipaya", Country="Cochabamba", MobilePhone="76762123", Name="Boutique Tiquipaya", Owner="Pedro Boutique"},
            new BoutiqueEntity() {Id = 3, Address="Sacaba", Country="Cochabamba", MobilePhone="77455423", Name="Boutique Sacaba", Owner="Boris Boutique"}
        };*/
        // BOUTIQUES
        public void CreateBoutique(BoutiqueEntity boutique)
        {
            _dbContext.Boutiques.Add(boutique);

        }

        public void CreateClothes(ClothesEntity clothes)
        {
            if (clothes.Boutique != null)
            {
                _dbContext.Entry(clothes.Boutique).State = EntityState.Unchanged;
            }
            _dbContext.Clothes.Add(clothes);
        }

        public async Task<bool> DeleteBoutiqueAsync(int boutiqueId)
        {
            var boutiqueToDelete = await _dbContext.Boutiques.FirstOrDefaultAsync(b => b.Id == boutiqueId);
            _dbContext.Boutiques.Remove(boutiqueToDelete);
            //boutiques.Remove(boutiqueToDelete);
            return true;
        }

        public bool DeleteClothes(int clothesId)
        {
            var clothesToDelete = new ClothesEntity() { Id = clothesId };
            _dbContext.Entry(clothesToDelete).State = EntityState.Deleted;
            return true;
        }

        public async Task<BoutiqueEntity> GetBoutiqueAsync(int boutiqueId, bool showClothes = false)
        {
            IQueryable<BoutiqueEntity> query = _dbContext.Boutiques;
            query = query.AsNoTracking();

            if (showClothes)
            {
                query = query.Include(b => b.Clothes);
            }

            return await query.FirstOrDefaultAsync(b => b.Id == boutiqueId);
        }

        public async Task<IEnumerable<BoutiqueEntity>> GetBoutiquesAsync(string orderBy, bool showClothes = false)
        {
            IQueryable<BoutiqueEntity> query = _dbContext.Boutiques;
            query = query.AsNoTracking();

            switch(orderBy)
            {
                case "id":
                    query = query.OrderBy(b => b.Id);
                    break;
                case "name":
                    query = query.OrderBy(b => b.Name);
                    break;
                case "country":
                    query = query.OrderBy(b => b.Country);
                    break;
                case "address":
                    query = query.OrderBy(b => b.Address);
                    break;
                default:
                    query = query.OrderBy(b => b.Id);
                    break;
            }
            return await query.ToListAsync();
        }

        public async Task<ClothesEntity> GetClothesAsync(int clothesId)
        {
            IQueryable<ClothesEntity> query = _dbContext.Clothes;
            query = query.Include(c => c.Boutique);
            query = query.AsNoTracking();
            var clothes = await query.SingleOrDefaultAsync(c => c.Id == clothesId);
            return clothes;
        }

        public async Task<IEnumerable<ClothesEntity>> GetClothessAsync(int boutiqueId)
        {
            IQueryable<ClothesEntity> query = _dbContext.Clothes;
            query = query.Where(c => c.Boutique.Id == boutiqueId);
            query = query.Include(c=>c.Boutique);
            query = query.AsNoTracking();

            return await query.ToArrayAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                var res = await _dbContext.SaveChangesAsync();
                return res > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateBoutique(BoutiqueEntity boutiqueModel)
        {
            var boutiqueToUpdate = _dbContext.Boutiques.FirstOrDefault(b => b.Id == boutiqueModel.Id);
            _dbContext.Entry(boutiqueToUpdate).CurrentValues.SetValues(boutiqueModel);
            return true;
        }

        public async Task<bool> UpdateClothesAsync(ClothesEntity clothes)
        {
            var clothesToUpdate = await _dbContext.Clothes.FirstOrDefaultAsync(c => c.Id == clothes.Id);
            clothesToUpdate.Name = clothes.Name ?? clothesToUpdate.Name;
            clothesToUpdate.Size = clothes.Size ?? clothesToUpdate.Size;
            clothesToUpdate.Brand = clothes.Brand ?? clothesToUpdate.Brand;
            clothesToUpdate.Price = clothes.Price ?? clothesToUpdate.Price;
            clothesToUpdate.Genre = clothes.Genre ?? clothesToUpdate.Genre;
            clothesToUpdate.Stock = clothes.Stock ?? clothesToUpdate.Stock;
            clothesToUpdate.Sell = clothes.Sell ?? clothesToUpdate.Sell;
            return true;
        }
    }
}
