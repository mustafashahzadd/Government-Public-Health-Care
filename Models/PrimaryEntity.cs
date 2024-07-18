using System.ComponentModel.DataAnnotations;

namespace Governement_Public_Health_Care.Models
{
    public class PrimaryEntity <TEntity> 
    {
        [Key]
        public TEntity Id { get; set; }

    }
}
