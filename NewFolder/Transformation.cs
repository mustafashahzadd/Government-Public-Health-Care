//using Governement_Public_Health_Care.DB_Context;
//using Governement_Public_Health_Care.DTO;
//using Governement_Public_Health_Care.LogErrors;

//namespace Governement_Public_Health_Care.NewFolder
//{
//    public class PatientTransformation : ITransformation<DiseaseRegistryDTO,DiseaseRegisteryDTO2>
//    {
//        private readonly ErrorsFile errorsFile;
//        private readonly HealthCareContext healthCareContext;

//        public PatientTransformation(ErrorsFile errorsFile, HealthCareContext healthCareContext)
//        {
//            this.errorsFile = errorsFile;
//            this.healthCareContext = healthCareContext;
//        }

//        public Task<DiseaseRegisteryDTO2> TransformingData(DiseaseRegistryDTO entity)
//        {
//            if (entity == null)
//            {
//                errorsFile.ErrorsDetail("Entity is Null");
//                return Task.FromResult<DiseaseRegisteryDTO2>(null);
//            }

//            // Perform the transformation
//            DiseaseRegisteryDTO2 transformedEntity = new DiseaseRegisteryDTO2
//            {
//                DoctorName = entity.Disease, // Make sure 'Disease' is a correct property to map to 'DoctorName'.
//                designationID = entity.NumberOfPeopleAffected, // Assuming 'NumberOfPeopleAffected' maps to 'designationID'.
//                doctorfees = entity.HospitalDoctorID, // Ensure this is the correct logic for mapping 'HospitalDoctorID' to 'doctorfees'.
//                doctorId = entity.HospitalDoctorID // Now using the HospitalDoctorID for the doctorId.
//            };

//            return Task.FromResult(transformedEntity);
//        }


//    }
//}

