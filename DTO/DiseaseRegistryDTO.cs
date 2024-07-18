using Governement_Public_Health_Care.Models;
using System.ComponentModel.DataAnnotations;

namespace Governement_Public_Health_Care.DTO
{
    public class DiseaseRegistryDTO : PrimaryEntity<int>
    {
        [Required]
        public string Disease { get; set; }

        // Make these properties nullable if they are optional for the PATCH operation
        public int? NumberOfPeopleAffected { get; set; }
        public int? HospitalDoctorID { get; set; }
    }
}
