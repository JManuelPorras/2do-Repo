namespace TeVasAMorir
{
    public class Celda
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool EsObstaculo { get; set; }
        public bool Visitada { get; set; }
        public int Distancia { get; set; }
        public bool TieneFicha { get; set; }
        public bool EsMetaFinal { get; set; }
        public bool EsMetaIntermedia { get; set; }

        public Celda(int x, int y)
        {
            X = x;
            Y = y;
            EsObstaculo = false;  // Inicializa sin obstáculos
            Visitada = false;
            Distancia = -1;
            TieneFicha = false;
            EsMetaFinal = false;
            EsMetaIntermedia = false;
        }

        public void ImprimirCelda()
        {
            if (EsObstaculo)
            {
                Console.Write("■ ");
            }
            else if (EsMetaFinal)
            {
                Console.Write("▒▒");
            }
            else if (EsMetaIntermedia)
            {
                Console.Write("۝۝");
            }
            else
            {
                Console.Write("  ");
            }
        }


    }

}