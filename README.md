# 2do-Repo

Tematica del juego:

Bienvenidos al juego de la Ciudadania Espanola!!

Historia:
Cada jugador comienza desde su casa(una esquina del tablero) y su objetivo final es conseguir el pasaporte espanol.
Para esto debe recorrer los caminos del laberinto cubano, con varias trampas y contratiempos. Su objetivo es pasar
primero por las dos metas intermedias que son la ACREDITACION DE LOS PAPELES en el MINREX y la CITA DE LA EMBAJADA,
para luego llegar a la meta final que es la obtencion del PASAPORTE ESPANOL.

Cada jugador esta representado en el tablero por una letra "P".
1er jugador en la esquina izquierda arriba
2do jugador en la esquina derecha arriba
3er jugador en la esquina izquierda abajo
4to jugador en la esquina derecha abajo

Fichas:
Para esto primero cada jugador debe elegir una ficha con la cual hacer el viaje:

1. Millonario:
   Esta ficha tiene mucho dinero y paga por adelantar los tramites.
   Velocidad: 4, Tiempo de Enfriamiento: 5, Habilidad: Avanzar 10 casillas ademas de las de su turno.

2. El Socio:
   Esta ficha es socio de alguna de las personas que hacen los tramites y evidentemente tiene ventajas.
   Velocidad: 5, Tiempo de Enfriamiento: 4, Habilidad: Teletransportarse a la izquierda mientras no haya obstaculo.

3. El Jodedor:
   Esta ficha se caracteriza por joder a los demas y si te encuantras con el estas frito.
   Velocidad: 7, Tiempo de Enfriamiento: 3, Habilidad: Buscar alguna ficha en un rango de 3 casillas y enviarla a una esquina.

4. El Colero:
   Esta ficha esta en todas las colas, con eso hace dinero y relaciones, por lo q adelanta en los tramites.
   Velocidad: 3, Tiempo de Enfriamiento: 6, Habilidad: Teletransportarse a la derecha mientras no haya obstaculo.

5. El Tu Sabe:
   Esta ficha es el tipo corrupto que se beneficia de su posicion politica y ejerce presion en sus tramites locales(MINREX).
   Velocidad: 3, Tiempo de Enfriamiento: infinito, Habilidad: Suma 1 a la puntuacion del jugador si esta es 0.

Nota: Varios jugadores pueden tener la misma ficha. Los pasos en los movimientos del jugador aumentan aunque caminen hacia un obstaculo.

Trampas:

Las Trampas estan esparcidas por el tablero con el simbolo "T".

1. Malas Credenciales:
   Cuando caes en esta trampa quiere decir que te hicieron mal las credenciales, y vuelves al principio.

2. Hacer Cola:
   Tienes q meterte una buena cola, tu velocidad disminuye en 1.

3. Sin Corriente:
   Se fue la corriente mientras estabas haciendo algun tramite, tu Tiempo de Enfriamiento aumenta en 1.

Manual:
Para mover a tu ficha presiona una de las 4 flechas y para activar su habilidad presiona espacio.
Cada movimiento gasta un paso, cuando el numero de pasos del jugador alcanza la velocidad de ese jugador, el turno se pasa automaticamente.

Para correr el juego:
Si usas Window entra al archivo bin/debug/net.8.0/JuegoFino en el ejecutable
Si usas Linux entra a esa misma direccion en una terminal y le das "dotnet run".

Como funciona el juego?:

    Hay una clase Juego a la que intancio, toda la logicca se maneja desde el constuctor de Juego.
    Primero recibe una entrada con la cantidad de jugadores(de 1 a 4).
    Crea una instancia de cada una de las trampas y las guarda en una lista como Trampas Disponibles
    Recibe la dificultad en dependencia de eso se genera el laberinto, segun del tamano.
    El laberinto siempre asegura q va a haber un camino desde todas las esquinas a las 3 metas.
    Instancia cada una de las fichas en una lista de Fichas Disponibles.
    Crea las instancias de los jugadores y clona la ficha de la lista cuando el jugador la elige.
    Imprime el laberinto y las fichas en su posicion inicial
    Luego maneja los turnos y mueve las fichas.

    Nota: Cuando mueve las fichas, el laberinto no se reimprime, para eso maneja el cursor, borra la posicion anterior de la ficha
    y lo imprime nuevamente en la nueva posicion.
