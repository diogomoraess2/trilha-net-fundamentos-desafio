using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<string> veiculos = new List<string>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public void AdicionarVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para estacionar:");

            string placaVeiculo = Console.ReadLine().ToUpper().Replace("-", "").Trim();

            bool resultado = ValidarPlaca(placaVeiculo);
            string mensagem = resultado ? "é válida" : "não é válida";

            if (!ValidarPlaca(placaVeiculo))
            {
                Console.WriteLine($"A placa informada {mensagem}...");
                Console.WriteLine("Verifique a placa do veículo e tente novamente");
            }
            else if (veiculos.Any(x => x.ToUpper() == placaVeiculo.ToUpper()))
            {
                Console.WriteLine($"O Veiculo de placa {placaVeiculo} já está cadastrado");
                Console.WriteLine("Verifique a placa do veículo e tente novamente");
            }
            else
            {
                veiculos.Add(placaVeiculo);
                Console.WriteLine($"Veículo de placa {placaVeiculo} cadastrado com sucesso!");
            }
        }

        //Verifica se a placa digitada é uma placa válida
        private static bool ValidarPlaca(string placa)
        {
            if (string.IsNullOrWhiteSpace(placa) || placa.Length > 8 || placa.Length < 7 || char.IsLetter(placa, 3))
            { 
                return false;
            }

            /*
             *  Verifica se o caractere da posição 4 é uma letra, se sim, aplica a validação para o formato de placa do Mercosul,
             *  senão, aplica a validação do formato de placa padrão.
             */
            if (char.IsLetter(placa, 4))
            {
                /*
                 *  Verifica se a placa está no formato: três letras, um número, uma letra e dois números.
                 */
                var padraoMercosul = new Regex("[a-zA-Z]{3}[0-9]{1}[a-zA-Z]{1}[0-9]{2}");
                return padraoMercosul.IsMatch(placa);
            }
            else
            {
                // Verifica se os 3 primeiros caracteres são letras e se os 4 últimos são números.
                var padraoNormal = new Regex("[a-zA-Z]{3}[0-9]{4}");
                return padraoNormal.IsMatch(placa);
            }
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");
            string placa = "";
            placa = Console.ReadLine().ToUpper();

            // Verifica se o veículo existe
            if (veiculos.Any(x => x.ToUpper() == placa.ToUpper()))
            {
                Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");

                int horas = 0;
                horas = Int32.Parse(Console.ReadLine());

                decimal valorTotal = 0;
                valorTotal = precoInicial + precoPorHora * horas;

                veiculos.Remove(placa);

                Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: R$ {valorTotal}");
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
            }
        }

        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                
                int contador = 1;
                foreach (string item in veiculos)
                {
                    Console.WriteLine($"Veículo {contador}: {item}");
                    contador++;
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }
    }
}