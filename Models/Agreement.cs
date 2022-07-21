using System.ComponentModel.DataAnnotations;


namespace SandP.Models
{
    public class Agreement : IValidatableObject
    {
        [Key]
        [Required]
        [StringLength(7, ErrorMessage = "The CNP/CUI should have  7 characters. ")]
        public string CNP { get; set; } = "";
        public string Nume { get; set; } = "";
        public string Prenume { get; set; } = "";
        public string DenumireCompanie { get; set; } = "";
        [Required]
        public string Judet { get; set; } = "";
        [Required]
        public string Telefon { get; set; } = "";
        [Required]
        public string Mail { get; set; } = "";
        [Required]
        public bool Acord { get; set; }
        [Required]
        public bool Market { get; set; } = false;
        [Required]
        public bool ComunicareMail { get; set; } 
        [Required]
        public bool ComunicareSMS { get; set; }  
        [Required]
        public bool ComunicarePosta { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ComunicareMail == false && ComunicareSMS == false && ComunicarePosta == false)
            {
                yield return new ValidationResult(
                    $"At least one should be true"
                );
            }

            if (CNP.Length != 7)
            {
                yield return new ValidationResult(
                    $"The CNP/CUI should have  7 characters. "
                );
            }
            else if (CNP.Substring(0,3)!="CNP" && CNP.Substring(0, 3) != "CUI")
            {
                yield return new ValidationResult(
                    $"CNP should contains CNP / CUI"
                );
            }
            else 
            {
                var lastdigits = CNP.Substring(CNP.Length - 4);

                if (!lastdigits.All(char.IsDigit))
                {
                    yield return new ValidationResult(
                        $"Last characters should be digits"
                    );
                }
            }
            

        }
    }
}

