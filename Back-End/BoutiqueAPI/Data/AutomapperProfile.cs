using AutoMapper;
using BoutiqueAPI.Data.Entities;
using BoutiqueAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiqueAPI.Data
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            this.CreateMap<BoutiqueEntity, BoutiqueModel>()
                .ReverseMap();

            this.CreateMap<ClothesModel, ClothesEntity>()
                .ForMember(des => des.Boutique, opt => opt.MapFrom(scr => new BoutiqueEntity { Id = scr.BoutiqueIde }))
                .ReverseMap()
                .ForMember(dest => dest.BoutiqueIde, opt => opt.MapFrom(scr => scr.Boutique.Id));
        }
    }
}
