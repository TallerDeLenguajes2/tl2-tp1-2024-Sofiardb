using Sistema;
string nombreArchivoCadetes = "Cadetes.csv";
string nombreArchivoCadeteria = "Cadeteria.csv";
HelperDeCSV helperCSV = new HelperDeCSV();
if(helperCSV.Existe(nombreArchivoCadetes) && helperCSV.Existe(nombreArchivoCadeteria))
{

    List<Cadete> cadetes = helperCSV.LeerCadetes(nombreArchivoCadetes);
    string[] infoCadeteria = helperCSV.LeerCadeteria(nombreArchivoCadeteria).Split(";");
    Cadeteria cadeteria = new Cadeteria(infoCadeteria[0],infoCadeteria[1], cadetes); 

    int nroPedido = 0;
    List<Pedido> pedidosSinAsignar = new List<Pedido>();

    Menu menu = new Menu($"Cadeteria {cadeteria.Nombre}-{cadeteria.Telefono}", ["Dar pedido de alta", "Asignar pedido", "Cambiar estado del pedido", "Reasignar pedido", "Cerrar"]);
    int operacion = menu.MenuDisplay();
    switch (operacion)
    {
        
        case 0:
            nroPedido++;
            Funciones.DarDeAltaPedido(pedidosSinAsignar, nroPedido);
            break;
        case 1:
            if(pedidosSinAsignar.Count != 0)
            {
                Console.WriteLine("El pedido a asignar es el siguiente: ");
                Funciones.MostrarPedido(pedidosSinAsignar[0]);
                cadeteria.AsignarPedido(pedidosSinAsignar[0]);
                pedidosSinAsignar.RemoveAt(0);
            }else
            {
                Console.WriteLine("No hay pedidos para asignar");
            }
            break;
        case 2:
            Console.WriteLine("Los pedidos no entregados son:");
            Console.WriteLine("Ingrese el numero de pedido cuyo estado desea modificar");
            string num;
            int numIngresado;
            Funciones.MostrarPedidosSinEntregar(cadeteria);
            do
            {
                Console.WriteLine("Ingrese el numero del pedido que desea reasignar:");
                num = Console.ReadLine();
            } while (!int.TryParse(num, out numIngresado));
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
            cadeteria.ReasignarPedido(numPedido);
            break;
        case 4:
            Console.WriteLine("Final de Jornada-Informe");
            cadeteria.MostrarJornalesYEnvios();
            break;
    }

}else
{
    Console.WriteLine("No se encontró la información de la cadetería");
}