using tabuleiro;

namespace xadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual {  get; private set; }
        public bool Terminada { get; private set; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Verde;
            Terminada = false;
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

        private void ColocarPecas()
        {
            Tab.ColocarPeca(new Torre(Tab, Cor.Verde), new PosicaoXadrez('C', 1).ToPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Verde), new PosicaoXadrez('C', 2).ToPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Verde), new PosicaoXadrez('d', 2).ToPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Verde), new PosicaoXadrez('e', 2).ToPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Verde), new PosicaoXadrez('e', 1).ToPosicao());
            Tab.ColocarPeca(new Rei(Tab, Cor.Verde), new PosicaoXadrez('d', 1).ToPosicao());


            Tab.ColocarPeca(new Torre(Tab, Cor.Vermelha), new PosicaoXadrez('C', 7).ToPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Vermelha), new PosicaoXadrez('C', 8).ToPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Vermelha), new PosicaoXadrez('d', 7).ToPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Vermelha), new PosicaoXadrez('e', 7).ToPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Vermelha), new PosicaoXadrez('e', 8).ToPosicao());
            Tab.ColocarPeca(new Rei(Tab, Cor.Vermelha), new PosicaoXadrez('d', 8).ToPosicao());

        }
    }
}
