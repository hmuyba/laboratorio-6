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
    public class BoutiquesService : IBoutiquesService
    {
        ILibraryRepository _libraryRepository;
        private IMapper _mapper;

        private HashSet<string> allowedOrderByParameters = new HashSet<string>()
        {
            "id",
            "name",
            "country",
            "address"
        };

        public BoutiquesService(ILibraryRepository libraryrepository, IMapper mapper)
        {
            _libraryRepository = libraryrepository;
            _mapper = mapper;
        }

        public async Task<BoutiqueModel> CreateBoutiqueAsync(BoutiqueModel boutiqueModel)
        {
            var boutiqueEntity = _mapper.Map<BoutiqueEntity>(boutiqueModel);
            _libraryRepository.CreateBoutique(boutiqueEntity);
            var result = await _libraryRepository.SaveChangesAsync();
            
            if(result)
            {
                return _mapper.Map<BoutiqueModel>(boutiqueEntity);
            }

            throw new Exception("Database Error");
        }

        public async Task<DeleteModel> DeleteBoutiqueAsync(int boutiqueId)
        {
            await GetBoutiqueAsync(boutiqueId);
            var DeleteResult = await _libraryRepository.DeleteBoutiqueAsync(boutiqueId);
            var saveResult = await _libraryRepository.SaveChangesAsync();
            if (!saveResult || !DeleteResult)
            {
                throw new Exception("Database Error");
            }
            if (saveResult)
            {
                return new DeleteModel()
                {
                    IsSuccess = saveResult,
                    Message = "The Boutique was deleted."
                };
            }
            else
            {
                return new DeleteModel()
                {
                    IsSuccess = saveResult,
                    Message = "The Boutique was not deleted."
                };
            }
        }

        public async Task<BoutiqueModel> GetBoutiqueAsync(int boutiqueId, bool showClothes = false)
        {
            var boutique = await _libraryRepository.GetBoutiqueAsync(boutiqueId, showClothes);
            if (boutique ==null)
            {
                throw new NotFoundOperationException($"The Boutique with id: {boutiqueId} doesn't exist");
            }
            return _mapper.Map<BoutiqueModel>(boutique);
        }

        public async Task<IEnumerable<BoutiqueModel>> GetBoutiquesAsync(string orderBy, bool showClothes)
        {
            if (!allowedOrderByParameters.Contains(orderBy.ToLower()))
            {
                throw new BadRequestOperationException($"The field {orderBy} is wrong, please use one of these {string.Join(",", allowedOrderByParameters)}");
            }
            var entityList = await _libraryRepository.GetBoutiquesAsync(orderBy, showClothes);
            var modelList = _mapper.Map<IEnumerable<BoutiqueModel>>(entityList);

            return modelList;
        }

        public async Task<BoutiqueModel> UpdateBoutiqueAsync(int boutiqueId, BoutiqueModel boutiqueModel)
        {
            var boutiqueEntity = _mapper.Map<BoutiqueEntity>(boutiqueModel);
            await GetBoutiqueAsync(boutiqueId);
            boutiqueEntity.Id = boutiqueId;
            _libraryRepository.UpdateBoutique(boutiqueEntity);

            var saveResult = await _libraryRepository.SaveChangesAsync();

            if (!saveResult)
            {
                throw new Exception("Database Error");
            }
            return boutiqueModel;
        }
    }
}
