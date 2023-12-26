using System.Collections.Generic;
using tabuleiro;

namespace xadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual {  get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> _pecas;
        private HashSet<Peca> _capturadas;

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Verde;
            Terminada = false;
            _pecas = new HashSet<Peca>();
            _capturadas = new HashSet<Peca>();
            ColocarPecas();
        }
         
        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            Turno++;
            MudaJogador();
        }
        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarQtdMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                _capturadas.Add(pecaCapturada);
            }
        }

        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if (Tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (JogadorAtual != Tab.peca(pos).cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!Tab.peca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida");
            }
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.peca(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino inválida");
            }
        }

        public void MudaJogador()
        {
            if (JogadorAtual == Cor.Verde )
            {
                JogadorAtual = Cor.Vermelha;
            }
            else
            {
                JogadorAtual = Cor.Verde;
            }
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
             HashSet<Peca> aux = new HashSet<Peca>();

            foreach (Peca x in _capturadas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();

            foreach (Peca x in _pecas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());  
            _pecas.Add(peca); 
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('c', 1, new Torre(Tab, Cor.Verde));
            ColocarNovaPeca('c', 2, new Torre(Tab, Cor.Verde));
            ColocarNovaPeca('d', 2, new Torre(Tab, Cor.Verde));
            ColocarNovaPeca('e', 2, new Torre(Tab, Cor.Verde));
            ColocarNovaPeca('e', 1, new Torre(Tab, Cor.Verde));
            ColocarNovaPeca('d', 1, new Rei(Tab, Cor.Verde));

            ColocarNovaPeca('c', 7, new Torre(Tab, Cor.Vermelha));
            ColocarNovaPeca('c', 8, new Torre(Tab, Cor.Vermelha));
            ColocarNovaPeca('d', 7, new Torre(Tab, Cor.Vermelha));
            ColocarNovaPeca('e', 7, new Torre(Tab, Cor.Vermelha));
            ColocarNovaPeca('e', 8, new Torre(Tab, Cor.Vermelha));
            ColocarNovaPeca('d', 8, new Rei(Tab, Cor.Vermelha));

        }
    }
}
