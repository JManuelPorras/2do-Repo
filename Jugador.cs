namespace TeVasAMorir
{
    public class Jugador
    {
        public string Nombre { get; set; }
        public int Puntuacion { get; set; } = 0;
        public int xInicial;
        public int yInicial;
        public Ficha FichaJugador { get; set; }

        public Jugador(int xinicial, int yinicial, List<Ficha> FichasDisponibles)
        {
            Nombre = EstablecerNombre();
            xInicial = xinicial;
            yInicial = yinicial;
            FichaJugador = ElegirFicha(FichasDisponibles);
            FichaJugador.xPosicion = xInicial;
            FichaJugador.yPosicion = yInicial;
            Console.Clear();
        }



        Ficha ElegirFicha(List<Ficha> FichasDisponibles)
        {
            Console.WriteLine("Por favor elija una ficha introduciendo el número que le corresponde.");

            for (int i = 0; i < FichasDisponibles.Count; i++)
            {
                Console.WriteLine($"{i}. {FichasDisponibles[i]}");
            }

            int indice;
            while (true)
            {
                string entrada = Console.ReadLine()!;
                Console.CursorVisible = false;
                if (int.TryParse(entrada, out indice) && indice >= 0 && indice < FichasDisponibles.Count)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Entrada no válida. Por favor, introduzca un número válido.");
                }
            }

            Ficha fichaOriginal = FichasDisponibles[indice];
            Ficha fichaClonada = fichaOriginal.Clone();

            return fichaClonada;
        }

        string EstablecerNombre()
        {
            Console.WriteLine("Por favor, introduzca el nombre de un jugador.");
            string nombre = Console.ReadLine()!;
            Console.CursorVisible = false;

            while (string.IsNullOrEmpty(nombre))
            {
                Console.WriteLine("El nombre no puede ser vacío. Por favor, introduzca un nombre válido.");
                nombre = Console.ReadLine()!;
                Console.CursorVisible = false;
            }

            return nombre;
        }



        public void IncrementarPuntuacion()
        {
            Puntuacion++;
        }

        public override string ToString()
        {
            return $"Jugador: {Nombre} ,Puntuacion: {Puntuacion} ,Ficha: {FichaJugador}";
        }
    }
}