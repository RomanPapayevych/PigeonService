using Project.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels
{
    public class NewPigeon
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name can't be blank")]
        public string? PigeonName { get; set; }

        [Required(ErrorMessage = "Color can't be blank")]
        public string? Color { get; set; }

        [Required(ErrorMessage = "Number can't be blank")]
        public string? Number { get; set; }

        [Required(ErrorMessage = "Year can't be blank")]
        public DateTime Year { get; set; }

        [Required(ErrorMessage = "Gender can't be blank")]
        public Gender Gender { get; set; }

        public Guid? FatherId { get; set; }  
        public Guid? MotherId { get; set; }  

        [ForeignKey("FatherId")]
        public virtual PigeonDTO? FatherPigeon { get; set; }

        [ForeignKey("MotherId")]
        public virtual PigeonDTO? MotherPigeon { get; set; }

        [Required(ErrorMessage = "IsAlive can't be blank")]
        public PigeonOptions IsAlive { get; set; }

        public string? Description { get; set; }
        public virtual List<PigeonDTO>? Children { get; set; }

        public static PigeonDTO MapToPigeonDTO(PigeonDTO pigeonDTO)
        {
            return new PigeonDTO
            {
                Id = pigeonDTO.Id,
                Color = pigeonDTO.Color,
                Number = pigeonDTO.Number,
                Year = pigeonDTO.Year,
                Gender = pigeonDTO.Gender,
                Father = pigeonDTO.Father,
                Mother = pigeonDTO.Mother,
                IsAlive = pigeonDTO.IsAlive,
                Description = pigeonDTO.Description
            };
        }
    }
}
