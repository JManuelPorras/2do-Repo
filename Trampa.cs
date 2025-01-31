namespace TeVasAMorir
{
    public abstract class Trampa
    {
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public bool Activa { get; set; }
        public virtual string Descripcion { get; set; }

        public Trampa()
        {

            Activa = true;
            Descripcion = "esta trampa esta vacia";
        }

        public void Activar()
        {
            Activa = true;
        }

        public void Desactivar()
        {
            Activa = false;
        }

        public virtual void AfectarFicha(Jugador jugador, Juego juego)
        {

        }

        public void MostrarInformacion(Juego juego)
        {
            Console.SetCursorPosition(0, juego.laberinto.Tablero.GetLength(1) + 4);
            Console.WriteLine($"{Descripcion}");
            Console.WriteLine("Presione enter para continuar");
            Console.ReadLine();
            Console.SetCursorPosition(0, juego.laberinto.Tablero.GetLength(1) + 4);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, juego.laberinto.Tablero.GetLength(1) + 5);
            Console.Write(new string(' ', Console.WindowWidth));

        }
    }

    public class MalasCredenciales : Trampa
    {
        public MalasCredenciales()
        {

            Descripcion = "Error en la acreditacion de los papeles, regresas al principio";
        }

        public override void AfectarFicha(Jugador jugador, Juego juego)
        {
            if (Activa)
            {
                //esto lo pone en una esquina de nuevo
                jugador.FichaJugador.BorrarFicha();
                jugador.FichaJugador.xPosicion = jugador.xInicial;
                jugador.FichaJugador.yPosicion = jugador.yInicial;
                jugador.FichaJugador.DibujarFicha();
                MostrarInformacion(juego);
                this.Desactivar();
            }
        }
    }

    public class HacerCola : Trampa
    {
        public HacerCola()
        {

            Descripcion = "Tienes q meterte una buena cola, tu velocidad disminuye en uno";
        }

        public override void AfectarFicha(Jugador jugador, Juego juego)
        {
            if (Activa)
            {
                if (jugador.FichaJugador.Velocidad > 1)
                {
                    jugador.FichaJugador.Velocidad--;
                    MostrarInformacion(juego);
                }
                this.Desactivar();
            }
        }
    }

    public class SinCorriente : Trampa
    {
        public SinCorriente()
        {

            Descripcion = "Se fue la corriente, el tiempo de enfriamiento de tu habilidad aumenta en 1";
        }

        public override void AfectarFicha(Jugador jugador, Juego juego)
        {
            if (Activa)
            {
                if (jugador.FichaJugador.TiempoEnfriamiento < 10)
                {
                    jugador.FichaJugador.TiempoEnfriamiento++;
                    MostrarInformacion(juego);
                }
                this.Desactivar();
            }
        }
    }

}
