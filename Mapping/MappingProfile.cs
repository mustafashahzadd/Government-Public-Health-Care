using AutoMapper;
using Governement_Public_Health_Care.DTO;
using Governement_Public_Health_Care.Models;

namespace Governement_Public_Health_Care.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<DiseaseRegistryDTO, DiseaseRegistry>();
            CreateMap<DiseaseRegistry, DiseaseRegistryDTO>();
            CreateMap<DiseaseRegisteryDTO2, DiseaseRegistry>();
            CreateMap<DiseaseRegistry, DiseaseRegisteryDTO2>();

        }
    }
}
