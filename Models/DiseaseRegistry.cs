using System.ComponentModel.DataAnnotations;

namespace Governement_Public_Health_Care.Models
{
    public class DiseaseRegistry : PrimaryEntity<int>
    {
        public string Disease { get; set; }
        public int NumberOfPeopleAffected { get; set; }

        //Refference from Hospital DB
        public int HospitalPatientID { get; set; }


    }
}
