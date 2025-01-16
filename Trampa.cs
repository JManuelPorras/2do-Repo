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

        public virtual void AfectarFicha(Jugador jugador)
        {

        }
    }

    public class MalasCredenciales : Trampa
    {
        public MalasCredenciales()
        {

            Descripcion = "Error en la acreditacion de los papeles, regresas al principio";
        }

        public override void AfectarFicha(Jugador jugador)
        {
            if (Activa)
            {
                jugador.FichaJugador.xPosicion = jugador.xInicial;
                jugador.FichaJugador.yPosicion = jugador.yInicial;
                Activa = false;
            }
        }
    }

    public class HacerCola : Trampa
    {
        public HacerCola()
        {

            Descripcion = "Tienes q meterte una buena cola, tu velocidad disminuye en uno";
        }

        public override void AfectarFicha(Jugador jugador)
        {
            if (jugador.FichaJugador.Velocidad > 1)
            {
                jugador.FichaJugador.Velocidad--;
            }
        }
    }

    public class SinCorriente : Trampa
    {
        public SinCorriente()
        {

            Descripcion = "Tienes q meterte una buena cola, tu velocidad disminuye en uno";
        }

        public override void AfectarFicha(Jugador jugador)
        {
            if (jugador.FichaJugador.TiempoEnfriamiento < 10)
            {
                jugador.FichaJugador.TiempoEnfriamiento++;
            }
        }
    }

}
