namespace TeVasAMorir
{
    class Juego
    {
        Laberinto laberinto { get; set; } = null!;
        List<Ficha> fichasDisponibles { get; set; }
        List<Jugador> JugadoresActivos { get; set; }
        List<Trampa> TrampasDisponibles { get; set; }
        bool JuegoTerminado = false;
        int cantJug { get; set; }
        string Dificultad { get; set; }
        Jugador jugadorActual = null!;

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
            int turno = 1;

            while (!JuegoTerminado)
            {
                jugadorActual = JugadoresActivos[turno % JugadoresActivos.Count];
                jugadorActual.FichaJugador.MoverFicha(laberinto);

                if (jugadorActual.FichaJugador.Puntuacion >= 2 &&
                    jugadorActual.FichaJugador.xPosicion == laberinto.tamano / 2 &&
                    jugadorActual.FichaJugador.yPosicion == laberinto.tamano / 2)
                {
                    Console.Clear();
                    Console.WriteLine("Termino el juego, ha ganado {0}", jugadorActual.Nombre);
                    JuegoTerminado = true;
                }

                turno++;
            }
        }

        void PonerFichasIniciales()
        {
            for (int i = 0; i < JugadoresActivos.Count; i++)
            {
                JugadoresActivos[i].FichaJugador.DibujarFicha(JugadoresActivos[i].xInicial, JugadoresActivos[i].yInicial);
            }
        }

        List<Jugador> CrearJugadores(int cantJug, Laberinto laberinto, List<Ficha> fichasDisponibles)
        {
            List<Jugador> jugadores = new List<Jugador>();

            for (int i = 1; i <= cantJug; i++)
            {
                int xPos = 0;
                int yPos = 0;

                switch (i)
                {
                    case 1:
                        xPos = 0;
                        yPos = 0;
                        break;
                    case 2:
                        xPos = 0;
                        yPos = laberinto.tamano - 1;
                        break;
                    case 3:
                        xPos = laberinto.tamano - 1;
                        yPos = 0;
                        break;
                    case 4:
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
            FichaMillonario Milloneta = new FichaMillonario(laberinto);
            FichaElColero Colero = new FichaElColero(laberinto);
            FichaElJodedor Jodedor = new FichaElJodedor(laberinto);
            FichaElSocio Socio = new FichaElSocio(laberinto);
            FichaElTuSabe TuSabe = new FichaElTuSabe(laberinto);
            List<Ficha> fichasDisponibles = new List<Ficha> { Milloneta, Colero, Jodedor, Socio, TuSabe, };
            return fichasDisponibles;
        }


        Laberinto MakeLaberinto(string Dificultad)
        {
            Laberinto laberinto;

            switch (Dificultad.ToLower())
            {
                case "facil":
                    laberinto = new Laberinto(31);
                    break;

                case "media":
                    laberinto = new Laberinto(41);
                    break;

                case "dificil":
                    laberinto = new Laberinto(51);
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