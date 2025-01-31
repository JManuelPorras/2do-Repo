namespace TeVasAMorir
{
    public class Juego
    {
        public Laberinto laberinto { get; set; } = null!;
        public List<Ficha> fichasDisponibles { get; set; }
        public List<Jugador> JugadoresActivos { get; set; }
        public List<Trampa> TrampasDisponibles { get; set; }
        bool JuegoTerminado = false;
        int cantJug { get; set; }
        string Dificultad { get; set; }
        public Jugador jugadorActual = null!;

        public Juego()
        {
            Console.WriteLine("Bienvenido a el proceso para obtener tu ciudadania espanola");
            cantJug = PonerCantJug();
            TrampasDisponibles = EstablecerTrampas();
            Dificultad = PonerDificultad();
            //aqui ahora tengo que pasarle al laberinto las trampas disponibles para que las distribuya por el tablero
            laberinto = MakeLaberinto(Dificultad);
            fichasDisponibles = CrearFichas(laberinto);
            JugadoresActivos = CrearJugadores(cantJug, laberinto, fichasDisponibles);
            Console.CursorVisible = false;
            Console.Clear();
            laberinto.ImprimirTablero();
            PonerFichasIniciales();
            ManejarTurnos();

        }

        List<Trampa> EstablecerTrampas()
        {
            MalasCredenciales malasCredenciales = new MalasCredenciales();
            HacerCola hacerCola = new HacerCola();
            SinCorriente sinCorriente = new SinCorriente();
            List<Trampa> trampasDisponbles = new List<Trampa> { malasCredenciales, hacerCola, sinCorriente };
            return trampasDisponbles;
        }
        void ManejarTurnos()
        {
            Console.CursorVisible = false;
            int turno = 0;

            while (!JuegoTerminado)
            {
                jugadorActual = JugadoresActivos[turno % JugadoresActivos.Count];
                jugadorActual.FichaJugador.MoverFicha(laberinto, this);

                if (jugadorActual.FichaJugador.Puntuacion >= 2 &&
                    jugadorActual.FichaJugador.xPosicion == laberinto.tamano / 2 &&
                    jugadorActual.FichaJugador.yPosicion == laberinto.tamano / 2)
                {
                    Console.Clear();
                    Console.WriteLine("Termino el juego, ha ganado {0}", jugadorActual.Nombre);
                    JuegoTerminado = true;
                }

                turno += 1;
            }
        }

        public void PonerFichasIniciales()
        {
            foreach (var jugador in JugadoresActivos)
            {
                if (jugador.FichaJugador.xPosicion >= 0 && jugador.FichaJugador.xPosicion < laberinto.tamano &&
                    jugador.FichaJugador.yPosicion >= 0 && jugador.FichaJugador.yPosicion < laberinto.tamano)
                {
                    jugador.FichaJugador.DibujarFicha(jugador.FichaJugador.xPosicion, jugador.FichaJugador.yPosicion);
                }
                else
                {
                    Console.WriteLine($"Posición inicial inválida para {jugador.Nombre}: ({jugador.FichaJugador.xPosicion}, {jugador.FichaJugador.yPosicion})");
                }
            }
        }
        List<Jugador> CrearJugadores(int cantJug, Laberinto laberinto, List<Ficha> fichasDisponibles)
        {
            List<Jugador> jugadores = new List<Jugador>();

            int xPos = 0;
            int yPos = 0;

            for (int i = 0; i < cantJug; i++)
            {

                switch (i)
                {
                    case 0:
                        xPos = 0;
                        yPos = 0;
                        break;
                    case 1:
                        xPos = laberinto.tamano - 1;
                        yPos = 0;
                        break;
                    case 2:
                        xPos = 0;
                        yPos = laberinto.tamano - 1;
                        break;
                    case 3:
                        xPos = laberinto.tamano - 1;
                        yPos = laberinto.tamano - 1;
                        break;
                }

                Jugador jugador = new Jugador(xPos, yPos, fichasDisponibles);
                jugadores.Add(jugador);
            }

            return jugadores;
        }

        List<Ficha> CrearFichas(Laberinto laberinto)
        {
            FichaMillonario Milloneta = new FichaMillonario(laberinto, this);
            FichaElColero Colero = new FichaElColero(laberinto, this);
            FichaElJodedor Jodedor = new FichaElJodedor(laberinto, this);
            FichaElSocio Socio = new FichaElSocio(laberinto, this);
            FichaElTuSabe TuSabe = new FichaElTuSabe(laberinto, this);
            List<Ficha> fichasDisponibles = new List<Ficha> { Milloneta, Colero, Jodedor, Socio, TuSabe, };
            return fichasDisponibles;
        }


        Laberinto MakeLaberinto(string Dificultad)
        {
            Laberinto laberinto;
            int tamanoLaberinto;

            switch (Dificultad.ToLower())
            {
                case "facil":

                    tamanoLaberinto = 31;//tamano del laberinto en dificultad facil 



                    laberinto = new Laberinto(tamanoLaberinto, this);
                    break;

                case "media":
                    tamanoLaberinto = 41;//tamano del laberinto en dificultad media

                    laberinto = new Laberinto(tamanoLaberinto, this);
                    break;

                case "dificil":
                    tamanoLaberinto = 51;//tamano del laberinto en dificultad dificil

                    laberinto = new Laberinto(tamanoLaberinto, this);
                    break;

                default:
                    throw new ArgumentException("Dificultad no válida. Por favor, elija 'facil', 'media' o 'dificil'.");
            }

            return laberinto;
        }

        int PonerCantJug()
        {
            int cantJug = 0;
            bool entradaValida = false;

            //esto sigue leyendo hasta q entre algo valido
            while (!entradaValida)
            {
                Console.WriteLine("Por favor introduzca el número de jugadores de 1 a 4");
                string entrada = Console.ReadLine()!;
                Console.CursorVisible = false;

                if (string.IsNullOrEmpty(entrada))
                {
                    Console.WriteLine("Entrada vacía, por favor introduce un número.");
                }
                else if (int.TryParse(entrada, out cantJug))
                {
                    if (cantJug >= 1 && cantJug <= 4)
                    {
                        entradaValida = true;
                    }
                    else
                    {
                        Console.WriteLine("El número debe estar entre 1 y 4, vuelva a intentar.");
                    }
                }
                else
                {
                    Console.WriteLine("Entrada no válida, por favor introduce un número.");
                }
            }

            return cantJug;
        }

        string PonerDificultad()
        {
            List<string> opcionesValidas = new List<string> { "facil", "media", "dificil" };
            string dificultad = "";
            bool entradaValida = false;

            while (!entradaValida)
            {
                Console.WriteLine("Por favor introduzca la dificultad (facil, media, dificil):");
                dificultad = Console.ReadLine()!;
                Console.CursorVisible = false;

                if (string.IsNullOrEmpty(dificultad))
                {
                    Console.WriteLine("Entrada vacía, por favor introduce una dificultad.");
                }
                else if (opcionesValidas.Contains(dificultad.ToLower()))
                {
                    entradaValida = true;
                }
                else
                {
                    Console.WriteLine("Entrada no válida, por favor introduce una dificultad válida (facil, media, dificil).");
                }
            }

            return dificultad;
        }


    }
}