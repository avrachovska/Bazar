using AutoMapper;
using Bazar.Data.Models;
using Bazar.Models;

namespace Bazar.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Ad, AdViewModel>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner.UserName));

            CreateMap<Ad, AdAddEditViewModel>()
                .ForMember(dest => dest.Categories, opt => opt.Ignore());

            CreateMap<AdAddEditViewModel, Ad>()
                .ForMember(dest => dest.OwnerId, opt => opt.Ignore())
                .ForMember(dest => dest.Owner, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore());

            CreateMap<Ad, AdListingViewModel>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner.UserName));

            CreateMap<Category, CategoryDropdownViewModel>();

            CreateMap<Ad, AdViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner.UserName));
        }
    }
}