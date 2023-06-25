using Produtos;
using System.Text;
using Newtonsoft.Json;
using System.Formats.Asn1;
using System.Globalization;
using CsvHelper;

init:
try
{
    int option = 0;

    while(option != 6)
    {
        Console.Write(
            "Menu:\n" +
            "1 – Cadastrar Produto:\n"+
            "2 – Atualizar Produto:\n"+
            "3 – Remover Produto:\n"+
            "4 – Listar Produtos\n"+
            "5 – Exportar para CSV\n"+
            "6 – Sair\n" +
            ".: "
        );
        option = int.Parse( Console.ReadLine() );
        Console.Clear();
        using (HttpClient client = new HttpClient())
        {
            string url = "http://localhost:3000/produtos/";
            HttpResponseMessage response;
            var product = new Produto();
            switch (option)
            {
                case 1:
                    Console.Write(">: ");
                    product.Cadastrar(Console.ReadLine().Split(','));
                    var json = JsonConvert.SerializeObject(product);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    response = await client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Novo produto inserido com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Erro ao inserir novo produto");
                    }
                    break;
                case 2:
                    Console.Write("Escreva o ID do produto: ");
                    string putUrl = url + Console.ReadLine();
                    Console.Write("produto: ");
                    product.Cadastrar(Console.ReadLine().Split(','));

                    json = JsonConvert.SerializeObject(product);
                    content = new StringContent(json, Encoding.UTF8, "application/json");
                    response = await client.PutAsync(putUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Produto atualizado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("A atualização falhou!");
                    }
                    break;
                case 3:
                    Console.Write("Escreva o ID do produto: ");
                    string delUrl = url + Console.ReadLine();
                    response = await client.DeleteAsync(delUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Produto excluído com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("A exclusão falhou!");
                    }
                    break;
                case 4:
                    response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var products = JsonConvert.DeserializeObject<List<Produto>>(responseBody);
                    foreach (var produto in products)
                    {
                        Console.WriteLine($" Id: {produto.Id}, Nome: {produto.Nome}, Preco: {produto.Preco}");
                    }
                    break;
                case 5:
                    //CAminho da pasta CSV no seu localhost
                    string filePath = "C:\\_repository\\Produtos\\Produtos\\CSV\\";
                    Console.Write("Nome do Arquivo: ");
                    filePath += Console.ReadLine();

                    response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    responseBody = await response.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<Produto>>(responseBody);

                    using (var writer = new StreamWriter(filePath + ".csv"))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(products);
                    }

                    Console.WriteLine("Dados exportados para o arquivo CSV com sucesso!");
                    break;
                default:
                    break;
            }

        }

        if (option != 6) Console.ReadLine();
        Console.Clear();
    }
    Console.WriteLine("Programa Finalizado");
}
catch (Exception ex)
{
    Console.Clear();
    Console.WriteLine("Hello, World!");
    goto init;
}

