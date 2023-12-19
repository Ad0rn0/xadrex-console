﻿using System;
using tabuleiro;
using xadrez;

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
                    ImprimirPeca(tab.peca(i,j));
                }
                Console.WriteLine();
            }
            Console.Write(" ");
            for (int i = 0; i < tab.Colunas; i++)
            {
                int numAscii = 65 + i;
                Console.Write( " " + (char)numAscii);
            }
            Console.WriteLine();
        }

        public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (posicoesPossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    ImprimirPeca(tab.peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.Write(" ");
            for (int i = 0; i < tab.Colunas; i++)
            {
                int numAscii = 65 + i;
                Console.Write(" " + (char)numAscii);
            }
            Console.WriteLine();
            Console.BackgroundColor = fundoOriginal;
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string posicao = Console.ReadLine();
            char coluna = posicao[0];
            int linha = int.Parse(posicao[1] + "");

            return new PosicaoXadrez(coluna, linha);
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
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
}
