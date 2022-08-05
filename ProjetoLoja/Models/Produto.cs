using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoLoja.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "Nome obrigatorio")]
        [StringLength(100,MinimumLength = 3,ErrorMessage = "O nome deve conter entre {1} e {2} caracteres!")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage ="Informe um preço!")]
        [Display(Name ="Preço")]
        [Column(TypeName ="decimal(10,2)")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage ="Digite uma descriçao curta para o produto!")]
        [StringLength(100,MinimumLength = 20,ErrorMessage ="A descriçao deve ter entre {1} e {2} caracteres")]
        [Display(Name ="Descriçao curta")]
        public string DescricaoCurta { get; set; }

        [Required(ErrorMessage = "Digite uma descriçao longa para o produto!")]
        [StringLength(1000, MinimumLength = 20, ErrorMessage = "A descriçao deve ter entre {1} e {2} caracteres")]
        [Display(Name = "Descriçao longa")]
        public string DescricaoLonga { get; set; }

        [Required(ErrorMessage ="Informe a quantidade do produto")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage ="Informe a data de compra!")]
        [Display(Name ="Data de compra")]
        [DataType(DataType.Date)]
        public DateTime DataCompra { get; set; }

        [Required(ErrorMessage = "Informe um valor!")]
        [Display(Name = "Valor pago")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorPago { get; set; }

        [Display(Name ="Caminho de imagem comum")]
        public string ImagemUrl { get; set; }

        [Display(Name ="Caminho da imagem do produto")]
        public string ImagemThumbUrl { get; set; }

        [Display(Name ="Preferido?")]
        public bool IsProdutoPreferido { get; set; }

        [Display(Name = "Estoque")]
        public bool EmEstoque { get; set; }
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }

    }
}
