using AutoMapper;
using popfragg.Domain.Models.Nades;
using popfragg.Domain.Models.Users;
using popfragg.Shared.DTOS.Auth;
using popfragg.Shared.DTOS.NadesDTO;

namespace popfragg.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add your mappings here
            CreateMap<User, SignUpRequestDTO>().ReverseMap();
            CreateMap<User, SignUpResponseDTO>().ReverseMap();
            CreateMap<NadeDTO, Nades>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.AuthorId, opt => opt.Ignore())
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.Accesses, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src =>
                    src.Tags == null ? new List<NadeTags>() :
                    src.Tags.Select(tag => new NadeTags
                    {
                        Tag = tag,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }).ToList()
                ))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src =>
                    src.Images == null ? new List<NadeImages>() :
                    src.Images.Select(image => new NadeImages
                    {
                        ImageOrder = image.OrderImage,
                        ImageUrl = image.ImageUrl,
                        UploadedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }).ToList()
                ))
                .ForMember(dest => dest.Accesses, opt => opt.MapFrom(src =>
                    src.Accesses == null ? new List<NadeAccess>() :
                    src.Accesses.Select(userId => new NadeAccess
                    {
                        UserId = userId,
                        GrantedAt = DateTime.UtcNow                        

                    }).ToList()
                ));

        }
    }
}
