using System;
using tabuleiro;

namespace xadrex_console
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (tab.peca(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        ImprimirPeca(tab.peca(i,j));
                    }
                }
                Console.WriteLine();
            }
            Console.Write(" ");
            for (int i = 0; i < tab.Colunas; i++)
            {
                int numAscii = 65 + i;
                Console.Write( " " + (char)numAscii);
            }
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca.cor == Cor.Vermelha)
            {
                ConsoleColor corAtual = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(peca + " ");
                Console.ForegroundColor = corAtual;
            }
            else
            {
                ConsoleColor corAtual = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(peca + " ");
                Console.ForegroundColor = corAtual;
            }
        }
    }
}
