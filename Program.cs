using Sistema;
Menu menuArchivos = new Menu("Elija el archivo con el que desea trabajar", ["Json", "CSV"]);
int eleccion = menuArchivos.MenuDisplay();
List<Cadete> cadetes;
Cadeteria cadeteria;
IAccesoADatos archivos = null;
switch (eleccion)
{
    case 0:
        archivos = new AccesoJson();
        break;
    case 1:
        archivos = new AccesoCSV();
        break;
}

cadetes = archivos.LeerCadetes();
cadeteria = archivos.LeerCadeteria();
cadeteria.AsignarCadetes(cadetes);

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
            List<Pedido> pedidosSinEntregar = cadeteria.PedidosSinCompletar();
            Funciones.MostrarPedidosSinEntregar(pedidosSinEntregar);
            do
            {
                Console.WriteLine("Ingrese el numero del pedido que desea reasignar:");
                ingreso = Console.ReadLine();
            } while (!int.TryParse(ingreso, out numPedido));
            Console.WriteLine("Elija al cadete que le reasignará el pedido");
            int cadeteSeleccionado = Funciones.ElegirCadete(cadeteria.Cadetes);
            if(!cadeteria.ReasignarPedido(numPedido, cadeteSeleccionado))
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
