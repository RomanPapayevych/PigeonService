using Microsoft.EntityFrameworkCore;
using Project.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.ViewModels
{
    
    public class PigeonDTO
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name can't be blank")]

        public string? PigeonName { get; set; }
        [Required(ErrorMessage = "Name can't be blank")]
        public string? Color { get; set; }
        [Required(ErrorMessage = "Name can't be blank")]
        public string? Number { get; set; }
        [Required(ErrorMessage = "Name can't be blank")]
        public DateTime? Year { get; set; }
        [Required(ErrorMessage = "Name can't be blank")]
        public Gender Gender { get; set; }
        //---------------------Normal structura---------------------------
        public string? Father { get; set; }
        public string? Mother { get; set; }
        //----------------------------------------------------------------


        //public Guid? Father { get; set; }
        //public Guid? Mother { get; set; }
        //[ForeignKey("Father")]
        //public virtual PigeonDTO? FatherPigeon { get; set; }
        //[ForeignKey("Mother")]
        //public virtual PigeonDTO? MotherPigeon { get; set; }

        [Required(ErrorMessage = "Name can't be blank")]
        public PigeonOptions IsAlive { get; set; }
        public string? Description { get; set; }
        //public PigeonDTO? Parent { get; set; }
        //public ICollection<PigeonDTO> PigeonsTree { get; } = new List<PigeonDTO>();
        
    }
}
