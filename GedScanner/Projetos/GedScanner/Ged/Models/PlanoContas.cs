using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ged.Models
{
    public class PlanoContas
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Descricao")]
        [Required(ErrorMessage = "O Campo {0} é Obrigatório!")]
        [StringLength(100, ErrorMessage = "O Campo {0} pode ter no máximo {1} e minimo {2} caracteres", MinimumLength = 5)]
        [Index("PlanoContas_Descricao_Unique", IsUnique = true)]
        public string Descricao { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = " O Campo {0} é Obrigatório!")]
        [StringLength(10, ErrorMessage = " O Campo {0} precisa ter {1} caracteres")]
        [Index("PlanoContas_Codigo_Unique", IsUnique = true)]
        public string Codigo { get; set; }

        [Display(Name = "Fechado")]
        public bool Fechado { get; set; }

        [Required]
        public string Inicio { get; set; }

        [Required]
        public int LoginInsert { get; set; }

        public virtual ICollection<Conta> Contas { get; set; }
    }
}