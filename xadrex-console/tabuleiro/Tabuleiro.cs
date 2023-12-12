namespace tabuleiro
{
    class Tabuleiro
    {
        public int Linhas {  get; set; }
        public int Colunas { get; set; }
        private Peca[,] _pecas;

        public Tabuleiro( int linhas, int colunas)
        {
            this.Linhas = linhas;
            this.Colunas = colunas;
            _pecas = new Peca[linhas, colunas];
        }

        public Peca peca( int linha, int coluna )
        {
            return _pecas[linha, coluna];
        }

        public Peca peca(Posicao pos)
        {
            return _pecas[pos.linha, pos.coluna];
        }

        public bool ExistePeca(Posicao pos)
        {
            ValidarPosicao(pos);
            return peca(pos) != null;
        }

        public void ColocarPeca(Peca p, Posicao pos)
        {
            if (ExistePeca(pos))
            {
                throw new TabuleiroException("Já existe uma peça nessa posição");
            }
            _pecas[pos.linha, pos.coluna] = p;
            p.posicao = pos;
        }

        public Peca RetirarPeca(Posicao pos)
        {
            if(peca(pos) == null)
            {
                return null;
            }

            Peca aux = peca(pos);
            aux.posicao = null;
            _pecas[pos.linha, pos.coluna] = null;
            return aux;
        }
        public bool PosicaoValida(Posicao pos)
        {
            if (pos.linha<0 || pos.linha>=Linhas || pos.coluna<0 || pos.coluna >= Colunas)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void ValidarPosicao(Posicao pos)
        {
            if(!PosicaoValida(pos))
            {
                throw new TabuleiroException("Posição inválida");
            }
        }

    }
}
