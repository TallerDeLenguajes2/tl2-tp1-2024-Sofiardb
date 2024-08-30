﻿using Sistema;
string nombreArchivoCadetes = "Cadetes.csv";
string nombreArchivoCadeteria = "Cadeteria.csv";
HelperDeCSV helperCSV = new HelperDeCSV();
if(helperCSV.Existe(nombreArchivoCadetes) && helperCSV.Existe(nombreArchivoCadeteria))
{
    int operacion;
    int nroPedido = 0;
    List<Cadete> cadetes = helperCSV.LeerCadetes(nombreArchivoCadetes);
    string[] infoCadeteria = helperCSV.LeerCadeteria(nombreArchivoCadeteria).Split(";");
    Cadeteria cadeteria = new Cadeteria(infoCadeteria[0],infoCadeteria[1], cadetes); 
    do
    {         
        Menu menu = new Menu($"Cadeteria {cadeteria.Nombre}-{cadeteria.Telefono}", ["Dar pedido de alta", "Asignar pedido", "Cambiar estado del pedido", "Reasignar pedido", "Cerrar"]);
        operacion = menu.MenuDisplay();
        switch (operacion)
        {
            
            case 0:
                nroPedido++;
                Pedido pedidoNuevo = Funciones.DarDeAltaPedido(nroPedido);
                cadeteria.GuardarPedido(pedidoNuevo);
                Console.ReadKey();
                break;
            case 1:
                Funciones.MostrarPedidosSinCadete(cadeteria);
                int numeroPedido;
                string ingresa;
                do
                {
                    Console.WriteLine("Ingrese el numero del pedido que desea asignar:");
                    ingresa = Console.ReadLine();
                } while (!int.TryParse(ingresa, out numeroPedido));
                int idCadete = Funciones.ElegirCadete(cadeteria.Cadetes);
                cadeteria.AsignarCadeteAPedido(numeroPedido, idCadete);
                break;
            case 2:
                string num;
                int numIngresado;
                do
                {
                    Console.WriteLine("Ingrese el numero de pedido cuyo estado desea modificar: ");
                    num = Console.ReadLine();
                } while (!int.TryParse(num, out numIngresado));
                cadeteria.CambiarEstadoDelPedido(numIngresado);
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
                cadeteria.ReasignarPedido(numPedido);
                Console.ReadKey();
                break;
            case 4:
                Console.WriteLine("Final de Jornada-Informe");
                cadeteria.MostrarJornalesYEnvios();
                Console.ReadKey();
                break;
        }

    } while (operacion != 4);
}else
{
    Console.WriteLine("No se encontró la información de la cadetería");
}