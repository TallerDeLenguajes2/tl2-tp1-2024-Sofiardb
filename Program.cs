using Sistema;
Menu menuArchivos = new Menu("Elija el archivo con el que desea trabajar", ["Json", "CSV"]);
int eleccion = menuArchivos.MenuDisplay();
List<Cadete> cadetes = null;
Cadeteria cadeteria = null;
switch (eleccion)
{
    case 0:
        AccesoJson archivoJson = new AccesoJson();
        string nombreArchivoCadetesJson = "Cadetes.json";
        string nombreArchivoCadeteriaJson = "Cadeteria.json";
        if(archivoJson.Existe(nombreArchivoCadetesJson) && archivoJson.Existe(nombreArchivoCadeteriaJson))
        {
            cadetes = archivoJson.LeerCadetes(nombreArchivoCadetesJson);
            cadeteria = archivoJson.LeerCadeteria(nombreArchivoCadeteriaJson);
            cadeteria.Cadetes = cadetes;
        }
        {
            Console.WriteLine("No se encontraron los archivos");
        }
        break;
    case 1:
        AccesoCSV archivoCSV = new AccesoCSV();
        string nombreArchivoCadetesCsv = "Cadetes.csv";
        string nombreArchivoCadeteriaCsv = "Cadeteria.csv";
        if(archivoCSV.Existe(nombreArchivoCadetesCsv) && archivoCSV.Existe(nombreArchivoCadeteriaCsv))
        {
            cadetes = archivoCSV.LeerCadetes(nombreArchivoCadetesCsv);
            cadeteria = archivoCSV.LeerCadeteria(nombreArchivoCadeteriaCsv);
            cadeteria.Cadetes = cadetes;
        }else
        {
            Console.WriteLine("No se encontraron los archivos");
        }
        break;
}

int operacion;
int nroPedido = 0;
do
{         
    Menu menu = new Menu($"Cadeteria {cadeteria.Nombre}-{cadeteria.Telefono}", ["Dar pedido de alta", "Asignar pedido", "Cambiar estado del pedido", "Reasignar pedido", "Cerrar"]);
    operacion = menu.MenuDisplay();
    switch (operacion)
    {
            
        case 0:
            nroPedido++;
            string[] infoPedido = Funciones.DarDeAltaPedido(nroPedido);
            cadeteria.GuardarPedido(infoPedido);
            Console.ReadKey();
            break;
        case 1:
            bool hayPedidos = Funciones.MostrarPedidosSinCadete(cadeteria);
            int numeroPedido;
            string ingresa;
            if(hayPedidos)
            {
                do
                {
                    Console.WriteLine("Ingrese el numero del pedido que desea asignar:");
                    ingresa = Console.ReadLine();
                } while (!int.TryParse(ingresa, out numeroPedido));
                int idCadete = Funciones.ElegirCadete(cadeteria.Cadetes);
                if(!cadeteria.AsignarCadeteAPedido(numeroPedido, idCadete))
                {
                    Funciones.MostrarMensajeDeError();
                }
            }
            break;
        case 2:
            string num;
            int numIngresado;
            do
            {
                Console.WriteLine("Ingrese el numero de pedido cuyo estado desea modificar: ");
                num = Console.ReadLine();
            } while (!int.TryParse(num, out numIngresado));
            int estado = Funciones.ElegirEstadoDelPedido();
            cadeteria.CambiarEstadoDelPedido(numIngresado, estado);
            Console.ReadKey();
            break;
        case 3:
            Console.WriteLine("Pedidos disponibles para reasignar");
            string ingreso;
            int numPedido;
            Funciones.MostrarPedidosSinEntregar(cadeteria);
            do
            {
                Console.WriteLine("Ingrese el numero del pedido que desea reasignar:");
                ingreso = Console.ReadLine();
            } while (!int.TryParse(ingreso, out numPedido));
            if(!cadeteria.ReasignarPedido(numPedido))
            {
                Funciones.MostrarMensajeDeError();      
            }
            Console.ReadKey();
            break;
        case 4:
            Console.WriteLine("Final de Jornada-Informe");
            Funciones.MostrarJornalesYEnvios(cadeteria);
            Console.ReadKey();
            break;
        }

} while (operacion != 4);
