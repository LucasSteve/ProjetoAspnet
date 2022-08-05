using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoLoja.Migrations
{
    public partial class PopularProdutos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Produtos(CategoriaId,Nome,Preco,DescricaoCurta,DescricaoLonga,Quantidade,DataCompra,ValorPago,ImagemUrl,ImagemThumbUrl,IsProdutoPreferido,EmEstoque)" +
                "VALUES(1,'XTG500 Pro',150.20,'SmartWatch a prova de agua lançamento','SmartWatch a prova de agua que tem varias funçoes faz pipoca e passa cafe',10,'11/01/2022',100.00,'https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.tecmundo.com.br%2Fproduto%2F152436-smartwatches-baratos-alta-brasil.htm&psig=AOvVaw0dccv27XqfHcbZza0w0yZL&ust=1643741148292000&source=images&cd=vfe&ved=0CAsQjRxqFwoTCOiriIzT3PUCFQAAAAAdAAAAABAI','https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.tecmundo.com.br%2Fproduto%2F152436-smartwatches-baratos-alta-brasil.htm&psig=AOvVaw0dccv27XqfHcbZza0w0yZL&ust=1643741148292000&source=images&cd=vfe&ved=0CAsQjRxqFwoTCOiriIzT3PUCFQAAAAAdAAAAABAI',1,1)");
            migrationBuilder.Sql("INSERT INTO Produtos(CategoriaId,Nome,Preco,DescricaoCurta,DescricaoLonga,Quantidade,DataCompra,ValorPago,ImagemUrl,ImagemThumbUrl,IsProdutoPreferido,EmEstoque)" +
               "VALUES(1,'xing ling Plu',185.50,'SmartWatch de plastico lançamento','SmartWatch a prova de agua que tem varias funçoes faz pipoca e passa cafe',3,'02/12/2021',140.35,'https://a-static.mlcdn.com.br/618x463/smartwatch-relogio-inteligente-y68-resistente-a-agua-fitaws/marcelowatch/10820889287/6b58df05fbcd3738edf98bf5fccbc3d3.jpg','https://a-static.mlcdn.com.br/618x463/smartwatch-relogio-inteligente-y68-resistente-a-agua-fitaws/marcelowatch/10820889287/6b58df05fbcd3738edf98bf5fccbc3d3.jpg',0,1)");
            migrationBuilder.Sql("INSERT INTO Produtos(CategoriaId,Nome,Preco,DescricaoCurta,DescricaoLonga,Quantidade,DataCompra,ValorPago,ImagemUrl,ImagemThumbUrl,IsProdutoPreferido,EmEstoque)" +
               "VALUES(2,'Relogio faustos',500.00,'Relogio pra quem é cego','Relogio grandao que mais parece de parede e que nego usa querendo tirar onda',25,'09/11/2021',35.68,'https://cf.shopee.com.br/file/b41a50b9478e074ade602ef3e65297ca_tn','https://cf.shopee.com.br/file/b41a50b9478e074ade602ef3e65297ca_tn',0,1)");
            migrationBuilder.Sql("INSERT INTO Produtos(CategoriaId,Nome,Preco,DescricaoCurta,DescricaoLonga,Quantidade,DataCompra,ValorPago,ImagemUrl,ImagemThumbUrl,IsProdutoPreferido,EmEstoque)" +
               "VALUES(2,'Relogio de ouro',1500.00,'Relogio pra quem é rico','Relogio grandao que mais parece de parede e que nego usa querendo tirar onda com adicional de ser caro',0,'01/02/2022',500.01,'https://cf.shopee.com.br/file/b41a50b9478e074ade602ef3e65297ca_tn','https://cf.shopee.com.br/file/b41a50b9478e074ade602ef3e65297ca_tn',0,0)");
            migrationBuilder.Sql("INSERT INTO Produtos(CategoriaId,Nome,Preco,DescricaoCurta,DescricaoLonga,Quantidade,DataCompra,ValorPago,ImagemUrl,ImagemThumbUrl,IsProdutoPreferido,EmEstoque)" +
             "VALUES(4,'Pulseiras de smartWhatch',50.00,'Pulseiras coloridas','Pulseiras coloridas de varios modelos e tamanhos pra completar o look',36,'05/01/2022',2.50,'null','null',0,1)");
            migrationBuilder.Sql("INSERT INTO Produtos(CategoriaId,Nome,Preco,DescricaoCurta,DescricaoLonga,Quantidade,DataCompra,ValorPago,ImagemUrl,ImagemThumbUrl,IsProdutoPreferido,EmEstoque)" +
              "VALUES(3,'Fones airpod',180.00,'Fones que parecem originais','Fone de pobre que quer tirar onda de rico mas nao tem dinheiro pra comprar o original',100,'01/10/2021',33.20,'null','null',0,1)");
        }       
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Produtos");
        }
    }
}
