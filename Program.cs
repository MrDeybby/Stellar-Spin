using System.Speech.Synthesis; //LIBRERIA PARA IMPORTAR LAS VOCES
using NAudio.Wave; //LIBRERIA PARA LA MUSICA
using System.Text.Json; //LIBRERIA PARA MANIPULAR LOS ARCHIVOS JSON



class Program
{
    static string[] DESARROLLADORES_SELECCIONADOS = new string [0];
    static string[] FACILITADORES_SELECCIONADOS = new string [0];
    static  string[] DESARROLLADORES = new string[0];
    static  string[] FACILITADORES = new string[0];

    static  string[] LISTAdeNombres = new string[0]; //ARRAY DE LOS NOMBRES DE LOS USUARIOS
    static  string[] LISTAdeRoles = new string[LISTAdeNombres.Length]; //ARRAY DE LOS ROLES
    static string Partida = "ListaDyF.json"; //ARCHIVO QUE CARGA AL EJECUTAR EL CODIGO
    static string rutaMusica = @"MUSICA\INICIO.mp3"; //NOMBRE Y RUTA DE LA MUSICA
    private static AudioFileReader musica; //PARA LEER LA PRIMERA MUSICA
    private static WaveOutEvent sonar; //PARA SONAR LA MUSICA
    

    static Random random = new Random(); 
    static bool RONDA = true;

  //MENU PINCIPAL
    static void Main()
    {
        
        //SOLO SE EJECUTA LA PRIMERA VEZ QUE SE ABRE EL PROGRAMA
        if (RONDA)
        {   Console.WriteLine("K");
            Console.ReadKey();
            rutaMusica = @"MUSICA\SONIDO NAVE.mp3";
            Console.Clear();
            SonarMusica();
            CargarDatosJSON(Partida);
            Nave();
            Console.Clear();
            
            Musica(true);
            TextoMenu();
            Console.WriteLine(@"
            ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(CentrarTextoX("PRESIONA PARA CONTINUAR"));
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
            RONDA = false;
            rutaMusica = @"MUSICA\INICIO.mp3";
            SonarMusica();
        }
        
        Musica(true);
        Musica(false);
        

        string[] Opciones = {"     GIRAR LA RULETA    ", "  CARGAR/GUARDAR DATOS  ", "       TRIPULANTES      ", "        FINALIZAR       "};
        while (true)
        {        
        int opcion = MenuOpciones(Opciones);



            switch (opcion)
            {
                case 0:
                    Console.Clear();
                    Musica(true);
                    GirarLaRuleta();
                    break;

                case 1:
                    Console.Clear();
                    CGdatos();
                    break;

                case 2:
                    Console.Clear();
                    TRIPULANTES();
                    break;

                case 3:
                    Animacion2("GRACIAS POR JUGAR", 0);
                    Animacion2("HECHO CON MUCHO AMOR Y POCAS HORAS DE SUEÑO <3", 1);
                    Environment.Exit(0);
                    break;
            }
        
        }
    static void SonarMusica()
    {
        musica = new AudioFileReader(rutaMusica);
        sonar = new WaveOutEvent();
    }
    static void Musica(bool Parar)
    {
        if(Parar)
        {
          sonar.Stop();  
        }
        else{

            sonar.Init(musica);
            sonar.Play();
        }
    }

 //FUNCION DE MENUS (PARA USAR LAS TECLAS DE FLECHAS Y DEZPLAZARNOS POR LAS OPCIONES)
    static int MenuOpciones(string[] Opciones)
    {
        
        int seleccion = 0;
        while (true)
        {   
            Console.Clear();
            TextoMenu();
            Console.CursorVisible = false;
            Console.WriteLine("");
            Console.WriteLine("");
            for (int i = 0; i < Opciones.Length; i++)
            {
                    Console.WriteLine();
                    int anchoConsola = Console.WindowWidth;
                    int longitudTexto = Opciones[i].Length;
                    int posicionX = (anchoConsola - longitudTexto) / 2;
                    Console.SetCursorPosition(posicionX, Console.CursorTop);

                if (i == seleccion)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.WriteLine(Opciones[i]);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(Opciones[i]);
                }
            }

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.UpArrow:
                    if (seleccion > 0)
                    {
                        seleccion--;
                        EfectoAudo(0);
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (seleccion < Opciones.Length - 1)
                    {
                        seleccion++;
                        EfectoAudo(1);
                    }
                    break;

                case ConsoleKey.Enter:
                    {
                        EfectoAudo(2);
                        return seleccion;
                    }

                case ConsoleKey.Escape:
                {
                       EfectoAudo(2);
                       Main();
                }
                    break;
            }
        }

    }

      //FUNCION CENTRAL PARA SELECCIONAR LOS USUARIOS

    static void GirarLaRuleta()
    {
        
        int D;
        int F;
        bool Girar = true;
        int vuelta = 0;
        bool QuieresEliminar = false;

        do
        {
            Musica(true);
            if (DESARROLLADORES.Length < 1 || FACILITADORES.Length < 1)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                CentrarTextoY();
                Console.WriteLine(CentrarTextoX("DATOS INSUFICIENTES"));
                Console.WriteLine("");
                Console.ResetColor();
                Console.WriteLine(CentrarTextoX("Presiona [ESC] para salir"));
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Escape:
                        Girar = false;
                        break;
                    
                    default: Console.Clear();
                        break;
                }
            }
            else
            {
                if (vuelta == 0) {AnimacionNumero();}
                bool continuar = false;
                string desarrollador;
                string facilitador;
                
                do{
                    Musica(true);
                    QuieresEliminar = false;
                    continuar = false;

                    do
                    {

                        D = random.Next(DESARROLLADORES.Length);
                        F = random.Next(FACILITADORES.Length);
                        desarrollador = DESARROLLADORES[D];
                        facilitador = FACILITADORES[F];

                    } while (desarrollador == facilitador);
                    
                
                    Ruleta(DESARROLLADORES, D, "");
                    Console.WriteLine(CentrarTextoX($"¡El Desarrollador seleccionado es: {desarrollador}!"));
                    voz(desarrollador, 1, "");

                    bool TeclaDisponible = true;
                    while (!continuar)
                    {
                        
                        if(TeclaDisponible)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(CentrarTextoX("[Enter] CONTINUAR - [1] AGREGAR ROL - [2] ELIMINAR ROL - [3] NUEVO DESAROLLADOR"));
                            Console.ResetColor(); 
                        }
                        

                        switch(Console.ReadKey(true).Key)
                        {
                            case ConsoleKey.Enter:
                                EfectoAudo(1);
                                continuar = true;
                                break;

                            case ConsoleKey.D1:
                                EfectoAudo(1);
                                AgregarRol(desarrollador);
                                
                                break;

                            case ConsoleKey.D2:
                                EfectoAudo(1);
                                EliminarROl(desarrollador);
                                
                                break;
                            
                            case ConsoleKey.D3:
                                QuieresEliminar = true;
                                continuar = true;
                            break;
                                
                            default:
                                TeclaDisponible = false;
                            break;
                        }
                    }  
                    
                    if (QuieresEliminar)
                    {
                        Console.Clear();
                        CentrarTextoY();
                        Console.WriteLine(CentrarTextoX("VAMOS A ELEGIR UN NUEVO DESARROLLADOR"));
                        Thread.Sleep(1000);
                    }

                }while(QuieresEliminar);

                Console.Clear();
                CentrarTextoY();
                Console.WriteLine(CentrarTextoX("VAMOS A SELECCIONAR AL FACILITADOR"));
                Thread.Sleep(1000);

                do{
                    Musica(true);
                    QuieresEliminar = false;
                    continuar = false;

                    Ruleta(FACILITADORES, F, desarrollador);
                    Console.WriteLine(CentrarTextoX($"¡El Facilitador seleccionado es: {facilitador}!"));
                    Console.WriteLine(CentrarTextoX($"Pasa al frente junto a {desarrollador}"));
                    voz(facilitador, 2, desarrollador);

                    bool TeclaDisponible = true;
                    while (!continuar)
                    {
                        if(TeclaDisponible)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(CentrarTextoX("[Enter] CONTINUAR - [1] AGREGAR ROL - [2] ELIMINAR ROL -  [3] NUEVO FACILITADOR - [ESC] SALIR"));
                            Console.ResetColor();    
                        }

                        switch(Console.ReadKey(true).Key)
                        {
                            case ConsoleKey.Enter:
                                EfectoAudo(1);
                                continuar = true;
                                break;

                            case ConsoleKey.D1:
                                EfectoAudo(1);
                                AgregarRol(facilitador);
                                
                                break;

                            case ConsoleKey.D2:
                                EfectoAudo(1);
                                EliminarROl(facilitador);
                                
                                break;

                            case ConsoleKey.D3:
                                QuieresEliminar = true;
                                continuar = true;
                                break;

                            case ConsoleKey.Escape:
                                EfectoAudo(0);
                                DESARROLLADORES_SELECCIONADOS = AgregarParticipante(DESARROLLADORES_SELECCIONADOS, desarrollador, vuelta);
                                FACILITADORES_SELECCIONADOS = AgregarParticipante(FACILITADORES_SELECCIONADOS, facilitador, vuelta);
                                DESARROLLADORES = EliminarElemento(DESARROLLADORES, desarrollador);
                                FACILITADORES = EliminarElemento(FACILITADORES, facilitador);
                                Main();
                                break;
                                
                                    
                                default:
                                TeclaDisponible= false;
                                break;
                        }
                    }

                    if (QuieresEliminar)
                    {
                        Console.Clear();
                        CentrarTextoY();
                        Console.WriteLine(CentrarTextoX("VAMOS A ELEGIR UN NUEVO FACILITADOR"));
                        Thread.Sleep(1000);

                        do
                        {
                            F = random.Next(FACILITADORES.Length);
                            facilitador = FACILITADORES[F];

                        } while (desarrollador == facilitador);
                    }
                      

                }while(QuieresEliminar);

                    DESARROLLADORES_SELECCIONADOS = AgregarParticipante(DESARROLLADORES_SELECCIONADOS, desarrollador, vuelta);
                    FACILITADORES_SELECCIONADOS = AgregarParticipante(FACILITADORES_SELECCIONADOS, facilitador, vuelta);

                    DESARROLLADORES = EliminarElemento(DESARROLLADORES, desarrollador);
                    FACILITADORES = EliminarElemento(FACILITADORES, facilitador);

                    vuelta += 1;

                Console.Clear();
            }
        } while (Girar);
    }


//ANIMACION DE LA SELECCION DE LOS USUARIOS
        static void Ruleta(string[] LISTA, int SeleccionParticipante, string dev)
    {
        int datosListas = LISTA.Length;

        int Framestotales = 25;
        
        if (datosListas > 10 && datosListas< 20)    {Framestotales = 15;}
        else if (datosListas < 10)                  {Framestotales = 10;}

        int SeleccionRandom;
        for (int frame = 0; frame < Framestotales; frame++)
        {
            Console.Clear();

            SeleccionRandom = random.Next(datosListas);

            if (frame == Framestotales - 1)
                {
                    SeleccionRandom = SeleccionParticipante;
                }
                

                Console.WriteLine(CentrarTextoX("=== ESTELLAR SPIN ==="));
                for (int i = 0; i < datosListas; i++)
                {   
                    if (LISTA[i] == dev){}
                    
                    else
                    {
                        if (i == SeleccionRandom)
                        {

                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine(CentrarTextoX($" -> {LISTA[i]} <- "));
                            
                        }
                        else
                        {
                            Console.ResetColor();
                            Console.WriteLine(CentrarTextoX($"{LISTA[i]}"));
                        }
                    }
                }
                Console.ResetColor();
                Console.WriteLine(CentrarTextoX("======================"));
                Console.Beep(600, 200);


                Console.ResetColor();

                double aumento = (double)frame / Framestotales;
                int frameDelay = (int)(50 + 500 * Math.Pow(aumento, 2));
                

                Thread.Sleep(frameDelay);
            
                Console.Clear();
        }    
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(CentrarTextoX("=== ESTELLAR SPIN ==="));
            for (int i = 0; i < datosListas; i++)
            {
                if (LISTA[i] == dev){}

                else{
                    if (i == SeleccionParticipante)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(CentrarTextoX($" -> {LISTA[i]} <- "));
                    }
                    else
                    {
                        Console.ResetColor();
                        Console.WriteLine(CentrarTextoX($"{LISTA[i]}"));
                    }
                }
            }
            EfectoAudo(5);
            Console.ResetColor();
            Console.WriteLine(CentrarTextoX("======================"));
            Console.ResetColor();
        
    }

      //FUNCION PARA LAS VOCES
    static void voz(string seleccionado, int puesto, string Dev)
    {
        //#pragma warning disable CA1416
        using (SpeechSynthesizer synth = new SpeechSynthesizer())
        {
            synth.Volume = 100;
            synth.Rate = 0;
            synth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult, 0, new System.Globalization.CultureInfo("es-ES"));

            string texto;

            if (puesto == 1)
            {
                texto = $"Desarrollador seleccionado es {seleccionado}";
            }
            else
            {
                texto = $"Facilitador seleccionado es {seleccionado}, pon a prueba los conocimientos de {Dev} y ¡QUE COMIENCE EL JUEGO!";
            }

            synth.Speak(texto);
        }
    }

    
    
//MENU PARA VER EL LISTADO DE USUARIOS Y SUS ROLES, ADEMAS CONECTA CON AGREGAR-ELIMINAR ROL
    static void TRIPULANTES()
    {

        int seleccion = 0;
        bool continuar = false;
        
        do{
            Console.Clear();
        for (int i = 0; i < LISTAdeNombres.Length; i++)
            {
                string NombreOrden = string.IsNullOrEmpty (LISTAdeRoles[i]) ? (LISTAdeNombres[i]) : (LISTAdeNombres[i] + " " + LISTAdeRoles[i]);
                int anchoConsola = Console.WindowWidth;
                int longitudTexto = NombreOrden.Length;
                int posicionX = (anchoConsola - longitudTexto) / 2;
                Console.SetCursorPosition(posicionX, Console.CursorTop);

                if (i == seleccion)
                {
                    
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(NombreOrden);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(NombreOrden);
                }
            }

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.UpArrow:
                    if (seleccion > 0)
                    {
                        seleccion--;
                        EfectoAudo(0);
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (seleccion < LISTAdeNombres.Length - 1)
                    {
                        seleccion++;
                        EfectoAudo(1);
                    }
                    break;

                case ConsoleKey.Enter:
                    {
                        EfectoAudo(2);
                        continuar = true;
                        break;
                    }

                case ConsoleKey.Escape:
                {
                       EfectoAudo(2);
                       Main();
                }
                    break;
            }
        }while (!continuar);
        

        do{

        
        string[] Opciones = {"       AGREGAR ROL      ", "      ELIMINAR ROL      "};

            Console.Clear();
            TextoMenu();
            Console.WriteLine(CentrarTextoX(LISTAdeNombres[seleccion]));

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(CentrarTextoX("[1] AGREGAR ROL, [2] ELIMINAR ROL"));
            Console.ResetColor();

            bool salirdewhile = false;
            
            while (!salirdewhile)
            {         
                switch(Console.ReadKey(true).Key)
                {
                case ConsoleKey.D1:
                    Console.Clear();
                    EfectoAudo(2);
                    AgregarRol(LISTAdeNombres[seleccion]);
                    salirdewhile = true;
                    continuar = true;
                    break;

                case ConsoleKey.D2:
                    Console.Clear();
                    EfectoAudo(2);
                    EliminarROl(LISTAdeNombres[seleccion]);
                    salirdewhile = true;
                    continuar = true;
                    break;
                }
            }  

        }while(!continuar);
        
        
    }

        //PARA AGREGAR ROL A UN USUARIO

    static void AgregarRol(string Estudiante)
    {
        Musica(true);
        Musica(false);
        Console.Clear();
        TextoMenu();
        int conteo = 0;
        
        while (LISTAdeNombres[conteo] != Estudiante)
        {
            conteo++;
        }

        string RolesQueTiene = LISTAdeRoles[conteo];
        string RolParaImpresion = "";
        string rol;

        try
        {
            RolesQueTiene = RolesQueTiene.Replace(")", "");
            RolParaImpresion = RolesQueTiene.Replace("(", "");
        }
        catch (Exception){}

        do{
        Console.WriteLine(CentrarTextoX(Estudiante));
        try
        {
            Console.WriteLine(CentrarTextoX(RolParaImpresion));
        }
        catch {}
        
        Console.WriteLine(CentrarTextoX("¿QUE ROL DESEAS AGREGAR?"));
        rol = Console.ReadLine()!;
        Console.Clear();
        } while (string.IsNullOrEmpty(rol));
        
        Console.WriteLine($"Rol agregado con exito");

        Estudiante = string.IsNullOrEmpty (RolesQueTiene) ? $"({rol})" : $"{RolesQueTiene} - {rol})";

        LISTAdeRoles[conteo] = Estudiante;

        Thread.Sleep(500);
    }

  //PARA ELIMINAR UN ROL DE ESTUDIANTE

    static void EliminarROl(string Estudiante)
    {
        Musica(true);
        Musica(false);
        Console.Clear();
        int conteo = 0;
        
        while (LISTAdeNombres[conteo] != Estudiante)
        {
            conteo++;
        }

        string RolesQueTiene = LISTAdeRoles[conteo];
        string[] RolesQueTienePeroEnArray;
        bool seguir = true;

        TextoMenu();
        Console.WriteLine(CentrarTextoX(Estudiante));

        try
        {
            Console.WriteLine(CentrarTextoX(RolesQueTiene));
            RolesQueTiene = RolesQueTiene.Replace(")", "");
            RolesQueTiene = RolesQueTiene.Replace("(", "");
            RolesQueTienePeroEnArray = RolesQueTiene.Split("-");
            
        }
            catch
            {   
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(CentrarTextoX("ESTE TRIPULANTE NO TIENE ROLES"));
                seguir = false;
                Console.ResetColor();
            }
    
        int seleccion = 0;
               

        while (seguir)
        {   
            bool continuar = false;
            RolesQueTienePeroEnArray = RolesQueTiene.Split("-");

            while (!continuar)
            {
                Console.Clear();
                
                TextoMenu();
                Console.WriteLine(CentrarTextoX("¿QUÉ ROL DESEAS ELIMINAR?"));

                for (int i = 0; i < RolesQueTienePeroEnArray.Length; i++)
                {
                        string Rol = RolesQueTienePeroEnArray[i].Trim();
                        Console.WriteLine();
                        int anchoConsola = Console.WindowWidth;
                        int longitudTexto = Rol.Length;
                        int posicionX = (anchoConsola - longitudTexto) / 2;
                        Console.SetCursorPosition(posicionX, Console.CursorTop);

                    if (i == seleccion)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(Rol);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(Rol);
                    }
                }

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        if (seleccion > 0)
                        {
                            seleccion--;
                            EfectoAudo(0);
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (seleccion < RolesQueTienePeroEnArray.Length - 1)
                        {
                            seleccion++;
                            EfectoAudo(1);
                        }
                        break;

                    case ConsoleKey.Enter:
                        {
                            EfectoAudo(2);
                            continuar = true;
                            seguir = false;
                        }
                        break;

                    case ConsoleKey.Escape:
                    {
                        EfectoAudo(2);
                        continuar = true;
                        seguir = false;
                    }
                        break;
                }
            }

            string RolAEliminar = RolesQueTienePeroEnArray[seleccion];

            RolesQueTienePeroEnArray = EliminarElemento(RolesQueTienePeroEnArray, RolAEliminar);
            LISTAdeRoles[conteo] = "";


            for(int i = 0; i < RolesQueTienePeroEnArray.Length; i++)
            {

                if (RolesQueTienePeroEnArray.Length == 1)
                {
                    LISTAdeRoles[conteo] = $"({RolesQueTienePeroEnArray[i].Trim()})";
                }
                else if (i == RolesQueTienePeroEnArray.Length-1)
                {
                    LISTAdeRoles[conteo] += $"{RolesQueTienePeroEnArray[i].Trim()})";
                }
                else if(i == 0)
                {
                    LISTAdeRoles[conteo] += $"({RolesQueTienePeroEnArray[i].Trim()} - ";
                }
                else
                {
                    LISTAdeRoles[conteo] += RolesQueTienePeroEnArray[i].Trim() + " ";
                }
            }
        }
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(CentrarTextoX ("PRESIONA PARA CONTINUAR"));
        Console.ResetColor();
        Console.ReadKey();
    }


//PARA ELIMINAR UN DATO DE UN ARRAY
    static string[] EliminarElemento(string[] array, string elemento)
    {
        int NuevoLargo = array.Length - 1;

        string[] nuevoArray = new string[NuevoLargo];
        int j = 0;
        for (int i = 0; i < array.Length;i++)
        {
            if (array[i] != elemento)
            {
                nuevoArray[j] = array[i];
                
                
                j++;
            }
            
        }
        return nuevoArray;
    }

    //MENU DE CARGAR-GUARDAR DATOS
    static void CGdatos()
    {
        Musica(true);
        Musica(false);
        string[] Opciones = { "      CARGAR DATOS      ", "      GUARDAR DATOS     ", "    GUARDAR HISTORIAL   ", "          ATRAS         " };

        while (true)
        {      
        int seleccion = (MenuOpciones(Opciones));
        

         switch (seleccion)
        {
            case 0:
                Console.Clear();
                PartidasCargar();
                break;

            case 1:
                Console.Clear();
                GuardarDatosJSON();
                break;

            case 2:
                GuardarDatosCVS();
                break;

            case 3:
                Main();
                break;
        }
        }
    }

    //MENU DE CARGAR PARTIDAS

    static void PartidasCargar()
    {
        Musica(true);
        Musica(false);
        string[] Opciones = {"   PARTIDA DE PRUEBA    ", "      TOP GLOBALES      ", "       SABADO A.M       ", "       SABADO P.M       ","          ATRAS         " }; 
        
        int opcion = MenuOpciones(Opciones);

        switch (opcion)
            {
                case 0:
                    Console.Clear();
                    Partida = "PartidadePrueba.json";
                    CargarDatosJSON(Partida);
                    break;

                case 1:
                    Console.Clear();
                    Partida = "ListaDyF.json";
                    CargarDatosJSON(Partida);
                    break;

                case 2:
                    Console.Clear();
                    Partida = "SABADOAM.json";
                    CargarDatosJSON(Partida);
                    break;

                case 3:
                    Console.Clear();
                    Partida = "SABADOPM.json";
                    CargarDatosJSON(Partida);
                    break;

                case 4:
                    Console.Clear();
                    CGdatos();
                    break;

            }
    }

//CARGAR ARCHIVO EN JSON (PARTIDAS) 
    static void CargarDatosJSON(string rutaJson)
    {
        Musica(true);
        Musica(false);
        if (File.Exists(rutaJson))
        {
            string DatosEnJson = File.ReadAllText(rutaJson);
            var Datos = JsonSerializer.Deserialize<Dictionary<string, string[]>>(DatosEnJson);

            if (Datos != null) 
            {
                LISTAdeNombres = Datos.ContainsKey("LISTA DE NOMBRES") ? Datos["LISTA DE NOMBRES"] : new string[0];
                LISTAdeRoles = Datos.ContainsKey("ROLES") ? Datos["ROLES"] : new string[0];
                DESARROLLADORES = Datos.ContainsKey("DESARROLLADORES DISPONIBLES") ? Datos["DESARROLLADORES DISPONIBLES"] : new string[0];
                FACILITADORES = Datos.ContainsKey("FACILITADORES DISPONIBLES") ? Datos["FACILITADORES DISPONIBLES"] : new string[0];
                DESARROLLADORES_SELECCIONADOS = Datos.ContainsKey("DESARROLLADORES") ? Datos["DESARROLLADORES"] : new string[0];
                FACILITADORES_SELECCIONADOS = Datos.ContainsKey("FACILITADORES") ? Datos["FACILITADORES"] : new string[0];
                Console.WriteLine("Datos cargados con éxito.");
            }
        }
        else {Console.WriteLine("El archivo no existe."); Console.ReadLine();}
        
    }

       //GUARDAR ARCHIVOS EN JSON (PARTIDAS)

    static void GuardarDatosJSON()
    {
        Musica(true);
        Musica(false);
        if (Partida == "PartidadePrueba.json")
        {
            CentrarTextoY();
            Console.WriteLine(CentrarTextoX("NO PUEDES GUARDAR UN PARTIDA DE PRUEBA"));
            Thread.Sleep(1000);
            Console.Clear();
            CGdatos();
        }

        var Datos = new Dictionary<string, string[]>
        {   
            { "LISTA DE NOMBRES", LISTAdeNombres },
            { "ROLES", LISTAdeRoles },
            { "DESARROLLADORES DISPONIBLES", DESARROLLADORES },
            { "FACILITADORES DISPONIBLES", FACILITADORES },
            { "DESARROLLADORES", DESARROLLADORES_SELECCIONADOS },
            { "FACILITADORES", FACILITADORES_SELECCIONADOS }, 
        };

        var options = new JsonSerializerOptions {WriteIndented = true};

        string DatosEnJson = JsonSerializer.Serialize(Datos, options);
        File.WriteAllText(Partida, DatosEnJson);
        Console.WriteLine("Datos guardados con éxito.");

        Console.WriteLine("Presiona cualquier tecla para continuar...");
        Console.ReadKey(true);
    }

      //GUARDAR ARCHIVOS EN CVS (HISTORIAL)

    static void GuardarDatosCVS()
    {
        Musica(true);
        Musica(false);
        string nombreArchivo;
        bool prohibido = false;
        do{
        prohibido = false;
        Console.Clear();
        TextoMenu();
        Console.WriteLine(CentrarTextoX("Ingresa el nombre que deseas para tu archivo:"));
        nombreArchivo = Console.ReadLine() + ".csv";

        char[] nombreArchivoPorLetra = nombreArchivo.ToCharArray();
        char[] CaracteresProhibidos = Path.GetInvalidFileNameChars();

        for (int i = 0; i < nombreArchivoPorLetra.Length && !prohibido; i++ )
        {
            for (int k = 0; k < CaracteresProhibidos.Length; k++)
            {
                if (nombreArchivoPorLetra[i] == CaracteresProhibidos[k])
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(CentrarTextoX("EL NOMBRE DE ARCHIVO NO ES VALIDO"));
                    Console.ResetColor();
                    Console.ReadKey();
                    Console.Clear();
                    prohibido = true;
                    break;
                }
            }
        }
        } while (prohibido);

        string rutaArchivo = Path.Combine(Environment.CurrentDirectory, nombreArchivo);

        Animacion("GUARDANDO HISTORIAL");

        using (StreamWriter sw = new StreamWriter(rutaArchivo, true))
        {
            DateTime Hora = DateTime.Now;

            sw.WriteLine();
            sw.WriteLine(Hora);
            sw.WriteLine();
            
            sw.WriteLine("");
            sw.WriteLine("LISTA Y ROLES");

            for(int i = 0; i < LISTAdeNombres.Length; i++)
            {
                sw.WriteLine($"{i+1}. {LISTAdeNombres[i]}, {LISTAdeRoles[i]}");
            }

            sw.WriteLine("");
            sw.WriteLine("DESARROLLADORES");

            for (int i = 0; i < DESARROLLADORES_SELECCIONADOS.Length; i++)
            {
                sw.WriteLine($"{i+1}. {DESARROLLADORES_SELECCIONADOS[i]}");
            }

            sw.WriteLine("");
            sw.WriteLine("FACILITADORES");
            
            for (int i = 0; i < FACILITADORES_SELECCIONADOS.Length; i++)
            {
                sw.WriteLine($"{i+1}. {FACILITADORES_SELECCIONADOS[i]}");
            }

            sw.WriteLine("");
            sw.WriteLine("DESARROLLADORES DISPONIBLES");
            
            for (int i = 0; i < DESARROLLADORES.Length; i++)
            {
                {sw.WriteLine($"{i+1}. {DESARROLLADORES[i]}");}
            }

            sw.WriteLine("");
            sw.WriteLine("FACILITADORES DISPONIBLES");
            
            for (int i = 0; i < FACILITADORES.Length; i++)
            {
                {sw.WriteLine($"{i+1}. {FACILITADORES[i]}");}
            }

        }

        Console.SetCursorPosition(0, Console.WindowHeight - 2);
        Console.WriteLine($"Archivo '{nombreArchivo}' creado con éxito.");
        Console.Write("Historial guardado con éxito. Presiona cualquier tecla para continuar.");
        Console.ReadKey();
    }

    static string[] AgregarParticipante(string[] participante, string nombre, int vuelta)
    {
        if (vuelta >= participante.Length)
        {
            Array.Resize(ref participante, vuelta + 1);
        }

        participante[vuelta] = (nombre);

        return participante;
    }

    static string CentrarTextoX(string texto)
    {
        int EspacioConsola = Console.WindowWidth;
        int espacio = (EspacioConsola - texto.Length) / 2;
        return new string(' ', espacio) + texto;
    } 

    static void CentrarTextoY()
    {
        int AlturaConsola = Console.WindowHeight;
        int espacioVertical = AlturaConsola / 2;

        for (int i = 0; i < espacioVertical; i++)
        {
            Console.WriteLine();
        }
    }

      //ANIMACION DEL COHETE
    static void Nave()
    {

        Console.CursorVisible = false;

        int largoConsola = Console.WindowHeight;
        int posicionY = largoConsola - 12;
        int CantidadFuego = 5;
        int mitadPantalla = Console.WindowWidth / 2;

        do
        {
                
            Console.Clear();

                
            if (posicionY >= 0 && posicionY < largoConsola + 12)
            { 
                 string[] caracterNave = {"  .", " .'.", " |o|", ".'o'.", "|.-.|", "'   '"};

                for (int i = 0; i < 6; i++)
                {
                    Console.SetCursorPosition(mitadPantalla, posicionY + i);
                    Console.WriteLine(caracterNave[i]);
                }
            }

            string[] fuegoCaracter = { "  |", "  |", "  |", " | |", " | |",};

            for (int i = 0; i < CantidadFuego; i++)
            {
                if (i < 1) { Console.ForegroundColor = ConsoleColor.Red; }
                else if (i < 3) { Console.ForegroundColor = ConsoleColor.Yellow; }
                else { Console.ForegroundColor = ConsoleColor.White;}

                Console.SetCursorPosition(mitadPantalla, posicionY + 6 + i);
                Console.WriteLine(fuegoCaracter[random.Next(CantidadFuego)]);
            }

            Console.ResetColor();
            posicionY--;
            Thread.Sleep(100);

            } while (posicionY >= 0);

            Console.Clear();
    }
      
//TEXTO GIGANTE
    static void TextoMenu()
    {
                    Console.WriteLine(@"
            

                                                                                                                                 .-.
                                                                                                                                ( (
                                                                                                                                 `-'
            ");
            
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(CentrarTextoX(" _______ _______ _______ ___     ___     _______ ______     _______ _______ ___ __    _ "));
            Console.WriteLine(CentrarTextoX("|       |       |       |   |   |   |   |   _   |    _ |   |       |       |   |  |  | |"));
            Console.WriteLine(CentrarTextoX("|  _____|_     _|    ___|   |   |   |   |  |_|  |   | ||   |  _____|    _  |   |   |_| |"));
            Console.WriteLine(CentrarTextoX("| |_____  |   | |   |___|   |   |   |   |       |   |_||_  | |_____|   |_| |   |       |"));
            Console.WriteLine(CentrarTextoX("|_____  | |   | |    ___|   |___|   |___|       |    __  | |_____  |    ___|   |  _    |"));
            Console.WriteLine(CentrarTextoX(" _____| | |   | |   |___|       |       |   _   |   |  | |  _____| |   |   |   | | |   |"));
            Console.WriteLine(CentrarTextoX("|_______| |___| |_______|_______|_______|__| |__|___|  |_| |_______|___|   |___|_|  |__|"));
            Console.ResetColor();
            Console.WriteLine(@"");
    }

      //ANIMACION DE NUMEROS ANTES DE IR A RULETA
    static void AnimacionNumero()
    {   
        string[] Numero = { 
            @"  __ ",
            @" /_ |",
            @"  | |",
            @"  | |",
            @"  | |",
            @"  |_|",
          
            @"  ___  ",
            @" |__ \ ",
            @"    ) |",
            @"   / / ",
            @"  / /_ ",
            @" |____|",
          
            @"  ____  ",
            @" |___ \ ",
            @"   __) |",
            @"  |__ < ",
            @"  ___) |",
            @" |____/ ",
            };
        Console.CursorVisible = false;
        Thread.Sleep(500); 
        for (int i = 0; i < 18; i+=6)
        {
        
        Console.Clear();      
        Console.WriteLine(@"
            

                                                                                                                                 .-.
                                                                                                                                ( (
                                                                                                                                 `-'
            ");
        if (i == 0 || i == 12) {Console.ForegroundColor = ConsoleColor.Blue;};
        Console.WriteLine(CentrarTextoX(Numero[i]));
        Console.WriteLine(CentrarTextoX(Numero[i + 1]));
        Console.WriteLine(CentrarTextoX(Numero[i + 2]));
        Console.WriteLine(CentrarTextoX(Numero[i + 3]));
        Console.WriteLine(CentrarTextoX(Numero[i + 4]));
        Console.WriteLine(CentrarTextoX(Numero[i + 5]));
        Console.ResetColor();
        EfectoAudo(4);
        Thread.Sleep(500);
        }
    }

//ANIMACION DE CARGANDO
    static void Animacion(string mensaje)
    {
        
            Console.Clear();
            for (int i = 0; i < 13; i++)
            {
            Console.ForegroundColor = ConsoleColor.Green; 
            string[] puntos = ["",".", "..", "..."];
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write($"{mensaje}{puntos[i % 4]}");
            Thread.Sleep (150);
            Console.Clear();
            }
            Console.ResetColor();
    }

      //ANIMACION FINAL
        static void Animacion2(string mensaje, int color)
    {
        Musica(true);
        rutaMusica = @"MUSICA\SONIDO TECLAS.mp3";
        SonarMusica();

        Musica(false);
        

        Console.Clear();
        CentrarTextoY();
        int EspacioConsola = Console.WindowWidth;
        int espacio = (EspacioConsola - mensaje.Length) / 2;
        Console.SetCursorPosition(espacio, Console.CursorTop);
        Console.CursorVisible = true;
        
        switch (color)
        {
            case 0: Console.ForegroundColor = ConsoleColor.Blue; break;
            case 1: Console.ForegroundColor = ConsoleColor.Red; break;
        }
        for (int i = 0; i < mensaje.Length; i++)
        {
            Console.Write((mensaje[i]));
            Thread.Sleep(100);
        }

        Musica(true);
        Console.WriteLine();
        Console.ResetColor();
        Thread.Sleep(1000);
    }

//SONIDO DE EFECTOS
    static void EfectoAudo(int numero)
    {
        string[] Sonidos = {
        @"MUSICA\STELAR SPIN FLECHAS.mp3",
        @"MUSICA\STELAR SPIN FLECHAS.mp3",
        @"MUSICA\STELAR SPIN ENTER.mp3",
        @"MUSICA\TECLA.mp3",
        @"MUSICA\COUNTDOWN.mp3",
        @"MUSICA\COUNTDOWN FIN.mp3", 
    };
    
    var Reproducir = new WaveOutEvent();
    Reproducir.Stop();
    var audios = new AudioFileReader[Sonidos.Length];

    audios[numero] = new AudioFileReader(Sonidos[numero]);
    Reproducir.Init(audios[numero]);
    Reproducir.Play();
    }

}
}
