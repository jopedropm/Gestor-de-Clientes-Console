using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Gestor_de_Clientes_sem_POO
{
    class Program
    {
        [System.Serializable]
        struct Cliente
        {
            public string nome;
            public string email;
            public string cpf;
        }

        static List<Cliente> clientes = new List<Cliente>();

        enum Menu { Listagem = 1, Adicionar = 2, Remover = 3, Sair = 4 }

        static void Main(string[] args)
        {

            Carregar();
            bool escolheuSair = false;
            while (!escolheuSair)
            {
                try
                {
                    Console.WriteLine("Sistema de clientes - Bem Vindo!");
                    Console.WriteLine("1-Listagem\n2-Adicionar\n3-Remover\n4-Sair");
                    int index = int.Parse(Console.ReadLine());
                    Menu opcao = (Menu)index;

                    switch (opcao)
                    {
                        case Menu.Listagem:
                            Listagem();
                            break;
                        case Menu.Adicionar:
                            Adicionar();
                            break;
                        case Menu.Remover:
                            Remover();
                            break;
                        case Menu.Sair:
                            escolheuSair = true;
                            break;
                    }

                    Console.Clear();
                } catch
                {
                    Console.WriteLine("Digite somente numeros! Aperte ENTER para tentar novamente.");
                    Console.ReadLine();
                    Console.Clear();
                }


            }
        }

        static void Adicionar()
        {
            Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro de cliente: ");
            Console.WriteLine("Nome: ");
            cliente.nome = Console.ReadLine();
            Console.WriteLine("Email: ");
            cliente.email = Console.ReadLine();
            Console.WriteLine("CPF: ");
            cliente.cpf = Console.ReadLine();

            clientes.Add(cliente);
            Salvar();

            Console.WriteLine("Cadastro concluido! Aperte ENTER para sair.");

            Console.ReadLine();
        }

        static void Listagem()
        {

            if (clientes.Count > 0)
            {
                Console.WriteLine("Lista de clientes: ");
                int i = 0;
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome: {cliente.nome}");
                    Console.WriteLine($"Email: {cliente.email}");
                    Console.WriteLine($"CPF: {cliente.cpf}");
                    Console.WriteLine("-------------------------------");
                    i++;
                }   
            }
            else
            {
                Console.WriteLine("Nenhum cliente cadastrado.");
            }
            Console.WriteLine("Aperte ENTER para sair.");
            Console.ReadLine();

        }

        static void Remover()
        {
            Listagem();

            Console.WriteLine("Digite o ID do cliente que quer remover: ");
            int id = int.Parse(Console.ReadLine());

            if (id >= 0 && id < clientes.Count)
            {
                clientes.RemoveAt(id);
                Salvar();

                Console.WriteLine("Cliente removido! Aperte ENTER para sair.");
            } else
            {
                Console.WriteLine("ID digitado é invalido.");
                Console.ReadLine();
            }

        }

        static void Salvar()
        {
            FileStream stream = new FileStream("clientes.dat", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(stream, clientes);

            stream.Close();
        }

        static void Carregar()
        {
            FileStream stream = new FileStream("clientes.dat", FileMode.OpenOrCreate);
            try
            {
                BinaryFormatter encoder = new BinaryFormatter();

                clientes = (List<Cliente>)encoder.Deserialize(stream);

            } catch
            {
                clientes = new List<Cliente>();
            }
            stream.Close();
        }

    }
}
