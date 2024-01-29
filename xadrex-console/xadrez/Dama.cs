using System.Data.Common;
using System.Text.RegularExpressions;
using tabuleiro;

namespace xadrex_console.xadrez
{
    class Dama : Peca
    {
        public Dama (Tabuleiro tab, Cor cor) : base(tab, cor) { }

        public override string ToString()
        {
            return "D";
        }

        private bool PodeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.cor != cor;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[tab.Linhas, tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            // Norte
            pos.DefinirValores(posicao.linha - 1, posicao.coluna);
            while (tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if(tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                    break;
                }
                pos.DefinirValores(pos.linha - 1, posicao.coluna);
            }

            // Nordeste
            pos.DefinirValores(posicao.linha - 1, posicao.coluna + 1);
            while(tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if(tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                    break;
                }
                pos.DefinirValores(pos.linha - 1, pos.coluna + 1);
            }

            // Leste
            pos.DefinirValores(posicao.linha, posicao.coluna + 1);
            while(tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if(tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                    break;
                }
                pos.DefinirValores(pos.linha, pos.coluna + 1);
            }

            // Sudeste
            pos.DefinirValores(posicao.linha + 1, posicao.coluna + 1);
            while(tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                    break;
                }
                pos.DefinirValores(pos.linha + 1, pos.coluna + 1);
            }

            // Sul
            pos.DefinirValores(posicao.linha + 1, posicao.coluna);
            while (tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if(tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                    break;
                }
                pos.DefinirValores(pos.linha + 1, pos.coluna);
            }

            // Sudoeste
            pos.DefinirValores(posicao.linha + 1, posicao.coluna - 1);
            while(tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if(tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                    break;
                }
                pos.DefinirValores(pos.linha + 1, pos.coluna - 1);
            }

            // Oeste
            pos.DefinirValores(posicao.linha, posicao.coluna - 1);
            while(tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if(tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                    break;
                }
                pos.DefinirValores(pos.linha, pos.coluna - 1);
            }

            // Noroeste
            pos.DefinirValores(posicao.linha - 1, posicao.coluna - 1);
            while(tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if(tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                    break;
                }
                pos.DefinirValores(pos.linha - 1, pos.coluna - 1);
            }

            return mat;
        }
    }
}
