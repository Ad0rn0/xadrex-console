using tabuleiro;

namespace xadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; set; }
        private int _turno;
        private Cor _jogadorAtual;
        public bool Terminada { get; private set; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            _turno = 1;
            _jogadorAtual = Cor.Verde;
            Terminada = false;
            ColocarPecas();
        }
         
        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarQtdMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
        }

        private void ColocarPecas()
        {
            Tab.ColocarPeca(new Torre(Tab, Cor.Vermelha), new PosicaoXadrez('C', 1).ToPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Vermelha), new PosicaoXadrez('C', 2).ToPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Vermelha), new PosicaoXadrez('d', 2).ToPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Vermelha), new PosicaoXadrez('e', 2).ToPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Vermelha), new PosicaoXadrez('e', 1).ToPosicao());
            Tab.ColocarPeca(new Rei(Tab, Cor.Vermelha), new PosicaoXadrez('d', 1).ToPosicao());


            Tab.ColocarPeca(new Torre(Tab, Cor.Verde), new PosicaoXadrez('C', 7).ToPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Verde), new PosicaoXadrez('C', 8).ToPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Verde), new PosicaoXadrez('d', 7).ToPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Verde), new PosicaoXadrez('e', 7).ToPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Verde), new PosicaoXadrez('e', 8).ToPosicao());
            Tab.ColocarPeca(new Rei(Tab, Cor.Verde), new PosicaoXadrez('d', 8).ToPosicao());

        }
    }
}
