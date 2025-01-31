using System.Security.AccessControl;

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
        public Juego juego;
        public Laberinto laberinto;
        public int paso = 0;
        public virtual string Habilidad { get; set; } = "Desconocido";

        public Ficha(Laberinto laberinto, Juego juego)
        {
            this.juego = juego;
            this.laberinto = laberinto;
            PonerFicha(laberinto);

        }

        public abstract Ficha Clone();

        private void PonerFicha(Laberinto laberinto)
        {
            laberinto.Tablero[xPosicion, yPosicion].TieneFicha = true;
        }
        public void MoverFicha(Laberinto laberinto, Juego juego)
        {

            Console.CursorVisible = false;
            int xActual = xPosicion;
            int yActual = yPosicion;

            for (paso = 0; paso < Velocidad; paso++)
            {
                //Leer la flecha de la consola
                ConsoleKeyInfo tecla = Console.ReadKey(true);
                Console.CursorVisible = false;

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
                    case ConsoleKey.Spacebar:
                        UsarHabilidad(laberinto);
                        continue;

                }

                //Llego a la meta final
                if (Puntuacion >= 2 && xActual == laberinto.tamano / 2 && yActual == laberinto.tamano / 2)
                {
                    xPosicion = xActual;
                    yPosicion = yActual;
                    return;
                }
                else
                if (MovValido(laberinto, xActual, yActual))
                {
                    //borrar la posicion anterior del jugador
                    BorrarFicha();

                    // Actualizar la posición del jugador
                    laberinto.Tablero[xPosicion, yPosicion].TieneFicha = false;
                    xPosicion = xActual;
                    yPosicion = yActual;
                    laberinto.Tablero[xPosicion, yPosicion].TieneFicha = true;

                    // Dibujar la nueva posición del jugador
                    DibujarFicha();


                    //si la nueva celda tiene trampa lo afecta
                    if (laberinto.Tablero[xPosicion, yPosicion].TieneTrampa)
                    {
                        laberinto.Tablero[xPosicion, yPosicion].TrampaDeCelda.AfectarFicha(juego.jugadorActual, juego);
                        xActual = xPosicion;
                        yActual = yPosicion;
                    }

                    //Sumarle un punto si llego a una meta intermedia
                    if (xPosicion == laberinto.tamano / 2 && (yPosicion == 0 || yPosicion == laberinto.tamano - 1 || yPosicion == (laberinto.tamano - 1) / 2))
                    {
                        Puntuacion++;
                        //imprime las metas de nuevo cuando el jugador las borra
                        laberinto.ReImprimirMetas();
                    }

                    // Mostrar información adicional fuera del laberinto
                    MostrarInformacion(laberinto, juego);
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
                    MostrarInformacion(laberinto, juego);
                }


            }
        }

        public void BorrarFicha()
        {
            // Borrar la posición anterior del jugador
            Console.SetCursorPosition(xPosicion * 2, yPosicion);
            Console.Write("  ");
        }

        public void MostrarInformacion(Laberinto laberinto, Juego juego)
        {
            // Calcular la línea de información de manera segura
            int lineaInfo = laberinto.Tablero.GetLength(1) + 1;
            if (lineaInfo >= Console.WindowHeight)
            {
                lineaInfo = Console.WindowHeight - 1;
            }

            // Limpiar la línea
            Console.SetCursorPosition(0, lineaInfo);
            Console.Write(new string(' ', Console.WindowWidth));

            // Mostrar la información
            Console.SetCursorPosition(0, lineaInfo);
            Console.WriteLine($"Turno de: {juego.jugadorActual.Nombre}");
            Console.SetCursorPosition(0, lineaInfo + 1);
            Console.WriteLine($"Ficha tipo: {Tipo}, Velocidad: {Velocidad}, Paso: {paso}");
            Console.SetCursorPosition(0, lineaInfo + 2);
            Console.WriteLine($"Habilidad de la ficha: {Habilidad}, Tiempo de Enfriamiento: {TiempoEnfriamiento}");

        }

        public void DibujarFicha()
        {

            //*2 pq las casillas tienen mas largo que ancho en consola
            Console.SetCursorPosition(xPosicion * 2, yPosicion);
            Console.Write("P ");
        }

        public void DibujarFicha(int x, int y)
        {

            //*2 pq las casillas tienen mas largo que ancho en consola
            Console.SetCursorPosition(x * 2, y);
            Console.Write("P ");
        }


        public bool MovValido(Laberinto laberinto, int xActual, int yActual)
        {
            if (xActual < 0 || xActual >= laberinto.Tablero.GetLength(0) ||
             yActual < 0 || yActual >= laberinto.Tablero.GetLength(1) ||
             laberinto.Tablero[xActual, yActual].EsObstaculo || laberinto.Tablero[xActual, yActual].TieneFicha)
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
        public override string Habilidad { get; set; } = "Avanzar 10 casillas";
        public FichaMillonario(Laberinto laberinto, Juego juego) : base(laberinto, juego)
        {

        }

        public override Ficha Clone()
        {
            return new FichaMillonario(this.laberinto, this.juego)
            {
                xPosicion = this.xPosicion,
                yPosicion = this.yPosicion,
                Velocidad = this.Velocidad,
                TiempoEnfriamiento = this.TiempoEnfriamiento,
                Tipo = this.Tipo,
                Puntuacion = this.Puntuacion,
                paso = this.paso


            };
        }


        //La habilidad del Millonario es caminar de nuevo pero ahora con 10 casillas
        public override void UsarHabilidad(Laberinto laberinto)
        {

            int xActual = xPosicion;
            int yActual = yPosicion;

            for (int paso = 0; paso < 10;)
            {
                //Leer la flecha de la consola
                ConsoleKeyInfo tecla = Console.ReadKey(true);
                Console.CursorVisible = false;

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
                    //borrar la posicion anterior del jugador
                    BorrarFicha();


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
        public override string Habilidad { get; set; } = "Teletransportarse a la izquierda mientras no haya obstaculo";
        public FichaElSocio(Laberinto laberinto, Juego juego) : base(laberinto, juego)
        {

        }
        public override Ficha Clone()
        {
            return new FichaElSocio(this.laberinto, this.juego)
            {
                xPosicion = this.xPosicion,
                yPosicion = this.yPosicion,
                Velocidad = this.Velocidad,
                TiempoEnfriamiento = this.TiempoEnfriamiento,
                Tipo = this.Tipo,
                Puntuacion = this.Puntuacion,
                paso = this.paso

            };
        }
        //mientras la casilla de la izquierda no tenga obstaculo se teletransporta
        public override void UsarHabilidad(Laberinto laberinto)
        {
            BorrarFicha();
            if (yPosicion == 0)
            {
                return;
            }
            else
            {
                while (laberinto.Tablero[xPosicion, yPosicion - 1].EsObstaculo == false)
                {
                    yPosicion -= 1;
                    if (yPosicion == 0)
                    {
                        return;
                    }
                }

            }
            DibujarFicha();
        }
    }

    public class FichaElJodedor : Ficha
    {
        public override string Tipo { get; set; } = "Jodedor";
        public override int Velocidad { get; set; } = 7;
        public override int TiempoEnfriamiento { get; set; } = 3;
        public override string Habilidad { get; set; } = "Buscar alguna ficha en un rango de 3 casillas y enviarla a una esquina";
        public FichaElJodedor(Laberinto laberinto, Juego juego) : base(laberinto, juego)
        {

        }
        public override Ficha Clone()
        {
            return new FichaElJodedor(this.laberinto, this.juego)
            {
                xPosicion = this.xPosicion,
                yPosicion = this.yPosicion,
                Velocidad = this.Velocidad,
                TiempoEnfriamiento = this.TiempoEnfriamiento,
                Tipo = this.Tipo,
                Puntuacion = this.Puntuacion,
                paso = this.paso

            };
        }
        //busca alguna ficha en un rango de 3 casillas y la envia a una esquina
        public override void UsarHabilidad(Laberinto laberinto)
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (laberinto.Tablero[xPosicion + i - 3, yPosicion + j - 3].TieneFicha == true)
                    {
                        foreach (Ficha ficha in juego.fichasDisponibles)
                        {
                            if (ficha.xPosicion == xPosicion + i - 3 && ficha.yPosicion == yPosicion + i - 3)
                            {
                                ficha.xPosicion = 0;
                                ficha.yPosicion = 0;
                            }
                        }
                    }
                }
            }
        }
    }

    public class FichaElColero : Ficha
    {
        public override string Tipo { get; set; } = "Colero";
        public override int Velocidad { get; set; } = 3;
        public override int TiempoEnfriamiento { get; set; } = 6;
        public override string Habilidad { get; set; } = "Teletransportarse a la derecha mientras no haya obstaculo";
        public FichaElColero(Laberinto laberinto, Juego juego) : base(laberinto, juego)
        {

        }
        public override Ficha Clone()
        {
            return new FichaElColero(this.laberinto, this.juego)
            {
                xPosicion = this.xPosicion,
                yPosicion = this.yPosicion,
                Velocidad = this.Velocidad,
                TiempoEnfriamiento = this.TiempoEnfriamiento,
                Tipo = this.Tipo,
                Puntuacion = this.Puntuacion,
                paso = this.paso

            };
        }

        //mientras la casilla de la derecha no tenga obstaculo se teletransporta
        public override void UsarHabilidad(Laberinto laberinto)
        {
            BorrarFicha();
            if (yPosicion == laberinto.tamano - 1)
            {
                return;
            }
            else
            {
                while (laberinto.Tablero[xPosicion, yPosicion + 1].EsObstaculo == false)
                {
                    yPosicion += 1;
                    if (yPosicion == laberinto.tamano - 1)
                    {
                        return;
                    }
                }

            }
            DibujarFicha();
        }

    }

    public class FichaElTuSabe : Ficha
    {
        public override string Tipo { get; set; } = "Tu sabes";
        public override int Velocidad { get; set; } = 3;
        public override int TiempoEnfriamiento { get; set; } = int.MaxValue;
        public override string Habilidad { get; set; } = "Suma 1 a la puntuacion del jugador si esta es 0";
        public FichaElTuSabe(Laberinto laberinto, Juego juego) : base(laberinto, juego)
        {

        }
        public override Ficha Clone()
        {
            return new FichaElTuSabe(this.laberinto, this.juego)
            {
                xPosicion = this.xPosicion,
                yPosicion = this.yPosicion,
                Velocidad = this.Velocidad,
                TiempoEnfriamiento = this.TiempoEnfriamiento,
                Tipo = this.Tipo,
                Puntuacion = this.Puntuacion,
                paso = this.paso

            };
        }

        // si la puntuacion del jugador es 0 le suma uno automaticamente
        public override void UsarHabilidad(Laberinto laberinto)
        {
            if (juego.jugadorActual.Puntuacion < 1)
            {
                juego.jugadorActual.Puntuacion += 1;
            }
        }
    }
}