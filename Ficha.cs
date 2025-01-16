namespace TeVasAMorir
{
    public abstract class Ficha
    {
        public virtual string Tipo { get; set; } = "Tipo Desconocido";
        public int xPosicion { get; set; } = 0;
        public int yPosicion { get; set; } = 0;
        public virtual int Velocidad { get; set; } = 0;
        public virtual int TiempoEnfriamiento { get; set; } = 0;
        public int Puntuacion { get; set; } = 0;

        public Ficha(Laberinto laberinto)
        {

            PonerFicha(laberinto);
        }

        private void PonerFicha(Laberinto laberinto)
        {
            laberinto.Tablero[xPosicion, yPosicion].TieneFicha = true;
        }
        public void MoverFicha(Laberinto laberinto)
        {

            int xActual = xPosicion;
            int yActual = yPosicion;

            for (int paso = 1; paso <= Velocidad;)
            {
                //Leer la flecha de la consola
                ConsoleKeyInfo tecla = Console.ReadKey(true);

                switch (tecla.Key)
                {
                    case ConsoleKey.UpArrow:
                        yActual--;
                        break;
                    case ConsoleKey.DownArrow:
                        yActual++;
                        break;
                    case ConsoleKey.LeftArrow:
                        xActual--;
                        break;
                    case ConsoleKey.RightArrow:
                        xActual++;
                        break;
                }

                //Llego a la meta final
                if (Puntuacion >= 2 && xPosicion == laberinto.tamano / 2 && yPosicion == laberinto.tamano / 2)
                {

                    return;
                }
                else
                if (MovValido(laberinto, xActual, yActual))
                {
                    // Borrar la posición anterior del jugador
                    Console.SetCursorPosition(xPosicion * 2, yPosicion);
                    Console.Write("  ");

                    // Actualizar la posición del jugador
                    laberinto.Tablero[xPosicion, yPosicion].TieneFicha = false;
                    xPosicion = xActual;
                    yPosicion = yActual;
                    laberinto.Tablero[xPosicion, yPosicion].TieneFicha = true;

                    // Dibujar la nueva posición del jugador
                    DibujarFicha();
                    paso++;

                    /* foreach (Trampa trampa in laberinto.Trampas)
                 {
                     if (xPosicion == trampa.X && yPosicion == trampa.Y && trampa.Activa)
                     {
                         trampa.AfectarFicha(this);
                         break;
                     }
                 }*/

                    //Sumarle un punto si llego a una meta intermedia
                    if (xPosicion == laberinto.tamano / 2 && (yPosicion == 0 || yPosicion == laberinto.tamano - 1))
                    {
                        Puntuacion++;
                        //imprime las metas de nuevo cuando el jugador las borra
                        laberinto.ImprimirMetas();
                    }

                    // Mostrar información adicional fuera del laberinto
                    //elimine el metodo de mostrar infromacion

                }
                else
                {
                    //Deshacer el cambio que se introdujo
                    switch (tecla.Key)
                    {
                        case ConsoleKey.UpArrow:
                            yActual++;
                            break;
                        case ConsoleKey.DownArrow:
                            yActual--;
                            break;
                        case ConsoleKey.LeftArrow:
                            xActual++;
                            break;
                        case ConsoleKey.RightArrow:
                            xActual--;
                            break;
                    }
                }


            }
        }

        public void MostrarInformacion(Laberinto laberinto)
        {
            Console.SetCursorPosition(0, laberinto.Tablero.GetLength(1) + 1);
            Console.Write(new string(' ', Console.WindowWidth)); // Limpia la línea
            Console.SetCursorPosition(0, laberinto.Tablero.GetLength(1) + 1);
            Console.WriteLine($"Ficha: {Tipo} en ({xPosicion}, {yPosicion}), Velocidad: {Velocidad}");
        }

        public void DibujarFicha()
        {

            //*2 pq las casillas tienen mas largo que ancho en consola
            Console.SetCursorPosition(xPosicion * 2, yPosicion);
            Console.Write("P");
        }

        public void DibujarFicha(int x, int y)
        {

            //*2 pq las casillas tienen mas largo que ancho en consola
            Console.SetCursorPosition(x * 2, y);
            Console.Write("P");
        }


        public bool MovValido(Laberinto laberinto, int xActual, int yActual)
        {
            if (xActual < 0 || xActual >= laberinto.Tablero.GetLength(0) ||
             yActual < 0 || yActual >= laberinto.Tablero.GetLength(1) ||
             laberinto.Tablero[xActual, yActual].EsObstaculo)
            {
                return false;
            }
            else return true;

        }

        public abstract void UsarHabilidad(Laberinto laberinto);

        public override string ToString()
        {
            return $"Ficha: {Tipo} en ({xPosicion}, {yPosicion})";
        }

    }

    public class FichaMillonario : Ficha
    {
        public override string Tipo { get; set; } = "Milloneta";
        public override int Velocidad { get; set; } = 4;
        public override int TiempoEnfriamiento { get; set; } = 5;
        public FichaMillonario(Laberinto laberinto) : base(laberinto)
        {

        }

        //La habilidad del Millonario es caminar de nuevo pero ahora con 10 casillas
        public override void UsarHabilidad(Laberinto laberinto)
        {

            int xActual = xPosicion;
            int yActual = yPosicion;

            for (int paso = 1; paso <= 10;)
            {
                //Leer la flecha de la consola
                ConsoleKeyInfo tecla = Console.ReadKey(true);

                switch (tecla.Key)
                {
                    case ConsoleKey.UpArrow:
                        yActual--;
                        break;
                    case ConsoleKey.DownArrow:
                        yActual++;
                        break;
                    case ConsoleKey.LeftArrow:
                        xActual--;
                        break;
                    case ConsoleKey.RightArrow:
                        xActual++;
                        break;
                }

                //Llego a la meta final
                if (Puntuacion >= 2 && xPosicion == laberinto.tamano / 2 && yPosicion == laberinto.tamano / 2)
                {
                    return;
                }
                else
                if (MovValido(laberinto, xActual, yActual))
                {
                    // Borrar la posición anterior del jugador
                    Console.SetCursorPosition(xPosicion * 2, yPosicion);
                    Console.Write("  ");

                    // Actualizar la posición del jugador
                    laberinto.Tablero[xPosicion, yPosicion].TieneFicha = false;
                    xPosicion = xActual;
                    yPosicion = yActual;
                    laberinto.Tablero[xPosicion, yPosicion].TieneFicha = true;

                    // Dibujar la nueva posición del jugador
                    DibujarFicha();
                    paso++;

                    //Sumarle un punto si llego a una meta intermedia
                    if (xPosicion == laberinto.tamano / 2 && (yPosicion == 0 || yPosicion == laberinto.tamano - 1))
                    {
                        Puntuacion++;
                    }



                }
                else
                {
                    //Deshacer el cambio que se introdujo
                    switch (tecla.Key)
                    {
                        case ConsoleKey.UpArrow:
                            yActual++;
                            break;
                        case ConsoleKey.DownArrow:
                            yActual--;
                            break;
                        case ConsoleKey.LeftArrow:
                            xActual++;
                            break;
                        case ConsoleKey.RightArrow:
                            xActual--;
                            break;
                    }
                }


            }
        }

    }

    public class FichaElSocio : Ficha
    {
        public override string Tipo { get; set; } = "Socio";
        public override int Velocidad { get; set; } = 5;
        public override int TiempoEnfriamiento { get; set; } = 4;
        public FichaElSocio(Laberinto laberinto) : base(laberinto)
        {

        }

        public override void UsarHabilidad(Laberinto laberinto)
        {
            // aqui tengo q poner la logica de la habilidad del Socio
        }
    }

    public class FichaElJodedor : Ficha
    {
        public override string Tipo { get; set; } = "Jodedor";
        public override int Velocidad { get; set; } = 7;
        public override int TiempoEnfriamiento { get; set; } = 3;
        public FichaElJodedor(Laberinto laberinto) : base(laberinto)
        {

        }
        public override void UsarHabilidad(Laberinto laberinto)
        {
            // aqui tengo q poner la logica de la habilidad del El Jodedor
        }
    }

    public class FichaElColero : Ficha
    {
        public override string Tipo { get; set; } = "Colero";
        public override int Velocidad { get; set; } = 3;
        public override int TiempoEnfriamiento { get; set; } = 6;
        public FichaElColero(Laberinto laberinto) : base(laberinto)
        {

        }

        public override void UsarHabilidad(Laberinto laberinto)
        {
            // aqui tengo q poner la logica de la habilidad del El Colero
        }

    }

    public class FichaElTuSabe : Ficha
    {
        public override string Tipo { get; set; } = "Tu sabes";
        public override int Velocidad { get; set; } = 3;
        public override int TiempoEnfriamiento { get; set; } = int.MaxValue;
        public FichaElTuSabe(Laberinto laberinto) : base(laberinto)
        {

        }

        public override void UsarHabilidad(Laberinto laberinto)
        {
            // aqui tengo q poner la logica de la habilidad del Tu Sabe
        }
    }
}