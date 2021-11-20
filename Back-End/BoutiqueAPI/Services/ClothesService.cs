using AutoMapper;
using BoutiqueAPI.Data.Entities;
using BoutiqueAPI.Data.Repository;
using BoutiqueAPI.Exceptions;
using BoutiqueAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiqueAPI.Services
{
    public class ClothesService : IClothesService
    {
        ILibraryRepository _libraryRepository;
        private IMapper _mapper;

        public ClothesService(IMapper mapper, ILibraryRepository libraryRepository)
        {
            _mapper = mapper;
            _libraryRepository = libraryRepository;
        }
        public async Task<ClothesModel> CreateClothesAsync(int BoutiqueId, ClothesModel clothesModel)
        {
            await validateBoutique(BoutiqueId);
            var clothesEntity = _mapper.Map<ClothesEntity>(clothesModel);
            _libraryRepository.CreateClothes(clothesEntity);
            var saveResult = await _libraryRepository.SaveChangesAsync();
            if (!saveResult)
            {
                throw new Exception("Save Error");
            }

            var modelToReturn = _mapper.Map<ClothesModel>(clothesEntity);
            modelToReturn.BoutiqueIde = BoutiqueId;
            return modelToReturn;
        }

        public async Task<bool> DeleteClothesAsync(int BoutiqueId, int clothesId)
        {
            await GetClothesAsync(BoutiqueId, clothesId);
            _libraryRepository.DeleteClothes(clothesId);
            var saveResult = await _libraryRepository.SaveChangesAsync();
            if (!saveResult)
            {
                throw new Exception("Error while saving.");
            }
            return true;
        }

        public async Task<ClothesModel> GetClothesAsync(int BoutiqueId, int clothesId)
        {
            await validateBoutique(BoutiqueId);
            await validateClothes(clothesId);
            var clothes = await _libraryRepository.GetClothesAsync(clothesId);
            if (clothes.Boutique.Id != BoutiqueId)
            {
                throw new NotFoundOperationException($"The clothes with id: {clothesId} does not exist for boutique id: {BoutiqueId}");
            }
            return _mapper.Map<ClothesModel>(clothes);
        }

        public async Task<IEnumerable<ClothesModel>> GetClothessAsync(int BoutiqueId)
        {
            await validateBoutique(BoutiqueId);
            var clothess = await _libraryRepository.GetClothessAsync(BoutiqueId);
            return _mapper.Map<IEnumerable<ClothesModel>>(clothess);
        }

        public async Task<bool> UpdateClothesAsync(int BoutiqueId, int clothesId, ClothesModel clothes)
        {
            await GetClothesAsync(BoutiqueId, clothesId);
            clothes.Id = clothesId;
            await _libraryRepository.UpdateClothesAsync(_mapper.Map<ClothesEntity>(clothes));
            var saveResult = await _libraryRepository.SaveChangesAsync();
            if (!saveResult)
            {
                throw new Exception("Error while saving.");
            }
            return true;
        }

        private async Task validateBoutique(int boutiqueId)
        {
            var boutique = await _libraryRepository.GetBoutiqueAsync(boutiqueId);
            if (boutique==null)
            {
                throw new NotFoundOperationException($"The Boutique with id: {boutiqueId}, does not exist");
            }
        }

        private async Task validateClothes(int clothesId)
        {
            var clothes = await _libraryRepository.GetClothesAsync(clothesId);
            if (clothes == null)
            {
                throw new NotFoundOperationException($"The clothes with id: {clothesId}, does not exist");
            }
        }

    }
}
