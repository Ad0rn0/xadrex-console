using System;
using tabuleiro;
using xadrex_console;
using xadrez;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                Tabuleiro tab = new Tabuleiro(8, 8);

                tab.ColocarPeca(new Torre(tab, Cor.Vermelha), new Posicao(0, 0));
                tab.ColocarPeca(new Torre(tab, Cor.Verde), new Posicao(1, 3));
                tab.ColocarPeca(new Rei(tab, Cor.Vermelha), new Posicao(0, 2));

                tab.ColocarPeca(new Rei(tab, Cor.Verde), new Posicao(0, 2));

                Tela.ImprimirTabuleiro(tab);

                Console.ReadLine();
            }
            catch (TabuleiroException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
