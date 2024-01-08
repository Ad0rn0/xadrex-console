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
        public bool Xeque {  get; private set; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Verde;
            Terminada = false;
            Xeque = false;
            _pecas = new HashSet<Peca>();
            _capturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);
            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);

                throw new TabuleiroException("Você não pode se colocar que xeque");
            }

            if (EstaEmXeque(Adversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            if(TesteXequemate(Adversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
            Turno++;
            MudaJogador();
            }

        }
        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarQtdMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                _capturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }
        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tab.RetirarPeca(destino);
            p.DecrementarQtdMovimentos();
            if (pecaCapturada != null)
            {
                Tab.ColocarPeca(pecaCapturada, destino);
                _capturadas.Remove(pecaCapturada);
            }
            Tab.ColocarPeca(p, origem);
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
            if (!Tab.peca(origem).MovimentoPossivel(destino))
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

        private Cor Adversaria(Cor cor)
        {
            if (cor == Cor.Verde)
            {
                return Cor.Vermelha;
            }
            else
            {
                return Cor.Verde;
            }
        }

        private Peca Rei(Cor cor)
        {
            foreach (Peca x in PecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca R = Rei(cor);

            if (R == null)
            {
                throw new TabuleiroException($"Não há rei da cor {cor} no tabuleiro");
            }

            foreach (Peca x in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[R.posicao.linha, R.posicao.coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool TesteXequemate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }

            foreach( Peca x in PecasEmJogo(cor))
            {
                bool[,] mat = x.MovimentosPossiveis();
                for (int i = 0; i < Tab.Linhas; i++)
                {
                    for (int j = 0; j < Tab.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());  
            _pecas.Add(peca); 
        }

        private void ColocarPecas()
        {

            ColocarNovaPeca('c', 1, new Torre(Tab, Cor.Verde));
            ColocarNovaPeca('d', 1, new Rei(Tab, Cor.Verde));
            ColocarNovaPeca('h', 7, new Torre(Tab, Cor.Verde));

            ColocarNovaPeca('a', 8, new Rei(Tab, Cor.Vermelha));
            ColocarNovaPeca('b', 8, new Torre(Tab, Cor.Vermelha));


            //ColocarNovaPeca('c', 1, new Torre(Tab, Cor.Verde));
            //ColocarNovaPeca('c', 2, new Torre(Tab, Cor.Verde));
            //ColocarNovaPeca('d', 2, new Torre(Tab, Cor.Verde));
            //ColocarNovaPeca('e', 2, new Torre(Tab, Cor.Verde));
            //ColocarNovaPeca('e', 1, new Torre(Tab, Cor.Verde));
            //ColocarNovaPeca('d', 1, new Rei(Tab, Cor.Verde));

            //ColocarNovaPeca('c', 7, new Torre(Tab, Cor.Vermelha));
            //ColocarNovaPeca('c', 8, new Torre(Tab, Cor.Vermelha));
            //ColocarNovaPeca('d', 7, new Torre(Tab, Cor.Vermelha));
            //ColocarNovaPeca('e', 7, new Torre(Tab, Cor.Vermelha));
            //ColocarNovaPeca('e', 8, new Torre(Tab, Cor.Vermelha));
            //ColocarNovaPeca('d', 8, new Rei(Tab, Cor.Vermelha));

        }
    }
}
