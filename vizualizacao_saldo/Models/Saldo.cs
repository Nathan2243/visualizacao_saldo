using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vizualizacao_saldo
{
    [Table("Saldos")]
    public class Saldo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Obrigatório informar o valor! ")]
        public int Valor { get; set; }

        public string Resumo { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o tipo de saldo!")]
        [Display(Name = "Tipo de Despesa")]
        public string Tipo_Saldo { get; set; }
    }
}
