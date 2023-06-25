using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Produtos
{
    public class Produto
    {
        public int? Id { get; set; }
        public string? Nome { get; set; }
        public decimal? Preco { get; set; }

        public void Cadastrar(string[] obj)
        {
            Nome = obj[0];
            Preco = decimal.Parse(obj[1]);
        }

    }
}
