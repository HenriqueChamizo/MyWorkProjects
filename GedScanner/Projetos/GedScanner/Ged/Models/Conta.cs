using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ged.Models
{
    public class Conta
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = " O Campo {0} é Obrigatório!")]
        [StringLength(100, ErrorMessage = " O Campo {0} pode ter no máximo {1} e minimo {2} caracteres ", MinimumLength = 5)]
        [Index("Conta_Descricao_Unique", IsUnique = true)]
        public string Descricao { get; set; }

        [Required(ErrorMessage = " O Campo {0} é Obrigatório!")]
        [StringLength(7, ErrorMessage = " O Campo {0} precisa ter {1} caracteres ", MinimumLength = 7)]
        [Index("Conta_Acesso_Unique", IsUnique = true)]
        public string Acesso { get; set; }

        [Required]
        public DateTime Inicio { get; set; }

        [Required]
        public int LoginInsert { get; set; }

        public virtual PlanoContas PlanoContas { get; set; }
    }
}