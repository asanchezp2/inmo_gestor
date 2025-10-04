using AutoMapper;
using RealEstateAPI.Application.DTOs;
using RealEstateAPI.Domain.Entities;
using System.Text.Json;

namespace RealEstateAPI.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Property, PropertyResponseDTO>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Zone, opt => opt.MapFrom(src => src.Zone.ToString()))
            .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => DeserializeImageUrls(src.ImageUrls)));

        CreateMap<PropertyCreateDTO, Property>()
            .ForMember(dest => dest.PropertyId, opt => opt.Ignore())
            .ForMember(dest => dest.PropertyCode, opt => opt.Ignore())
            .ForMember(dest => dest.HasImages, opt => opt.MapFrom(src => src.ImageUrls.Count > 0))
            .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => SerializeImageUrls(src.ImageUrls)))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.ClosedDate, opt => opt.Ignore())
            .ForMember(dest => dest.Advisor, opt => opt.Ignore());

        CreateMap<PropertyUpdateDTO, Property>()
            .ForMember(dest => dest.PropertyId, opt => opt.Ignore())
            .ForMember(dest => dest.PropertyCode, opt => opt.Ignore())
            .ForMember(dest => dest.Type, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.Zone, opt => opt.Ignore())
            .ForMember(dest => dest.ClosedDate, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.AdvisorId, opt => opt.Ignore())
            .ForMember(dest => dest.Advisor, opt => opt.Ignore())
            .ForMember(dest => dest.HasImages, opt => opt.MapFrom(src => src.ImageUrls.Count > 0))
            .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => SerializeImageUrls(src.ImageUrls)))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<Advisor, AdvisorResponseDTO>();
        CreateMap<Advisor, AdvisorSummaryDTO>();
        
        CreateMap<AdvisorCreateDTO, Advisor>()
            .ForMember(dest => dest.AdvisorId, opt => opt.Ignore())
            .ForMember(dest => dest.Properties, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        
        CreateMap<AdvisorUpdateDTO, Advisor>()
            .ForMember(dest => dest.AdvisorId, opt => opt.Ignore())
            .ForMember(dest => dest.Properties, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
    }

    private static List<string> DeserializeImageUrls(string imageUrls)
    {
        try
        {
            return JsonSerializer.Deserialize<List<string>>(imageUrls) ?? new List<string>();
        }
        catch
        {
            return new List<string>();
        }
    }

    private static string SerializeImageUrls(List<string> imageUrls)
    {
        return JsonSerializer.Serialize(imageUrls);
    }
}
