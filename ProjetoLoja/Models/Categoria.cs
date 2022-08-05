using System.ComponentModel.DataAnnotations;

namespace ProjetoLoja.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; }

        [Required(ErrorMessage ="Informe o nome da categoria!")]
        [StringLength(100,MinimumLength = 5,ErrorMessage ="O nome deve ter entre {1} e {2} caracteres!")]
        [Display(Name ="Nome")]
        public string CategoriaNome { get; set; }
        [Required(ErrorMessage = "Digite uma descriçao curta para a categoria")]
        [StringLength(100, MinimumLength = 20, ErrorMessage = "A descriçao deve ter entre {1} e {2} caracteres")]
        [Display(Name = "Descriçao")]
        public string Descricao { get; set; }
        public List<Produto> Produtos { get; set; }
    }
}
