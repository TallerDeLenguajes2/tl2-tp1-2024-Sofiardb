namespace Sistema
{    
    public class Funciones
    {
        public static Pedido DarDeAltaPedido(int nroPedido)
        {
            string observacionPedido;
            string nombreCliente;
            string direccCliente;
            string telefonoCliente;
            string referenciasCliente;
            do
            {
                Console.WriteLine("Ingrese información del pedido: ");
                observacionPedido = Console.ReadLine();
                Console.WriteLine("Ingrese el nombre del cliente");
                nombreCliente = Console.ReadLine();
                Console.WriteLine("Ingrese la dirección del cliente");
                direccCliente = Console.ReadLine();
                Console.WriteLine("Ingrese el telefono del cliente");
                telefonoCliente = Console.ReadLine();
                Console.WriteLine("Ingrese una referencia de la dirección (Opcional): ");
                referenciasCliente = Console.ReadLine();
                if(observacionPedido.Length == 0 && nombreCliente.Length == 0 && direccCliente.Length == 0 && telefonoCliente.Length == 0)
                {
                    Console.WriteLine("Debe rellenar correctamente los campos");
                }
            } while (string.IsNullOrWhiteSpace(observacionPedido) || string.IsNullOrWhiteSpace(nombreCliente) || string.IsNullOrWhiteSpace(direccCliente) || string.IsNullOrWhiteSpace(telefonoCliente));
            Pedido pedidoNuevo = new Pedido(nroPedido,observacionPedido,nombreCliente,direccCliente,telefonoCliente,referenciasCliente);
            return pedidoNuevo;
            
        }

        public static int ElegirCadete(List<Cadete> cadetes)
        {
            List<string> opcionesMenu = new List<string>();
            foreach (var cadete in cadetes)
            {
                opcionesMenu.Add(cadete.Nombre); 
            }
            string[] opcionesCadetes = opcionesMenu.ToArray();
            Menu menuDeSeleccion = new Menu("Seleccione el cadete al que asignará el pedido", opcionesCadetes);
            int seleccion = menuDeSeleccion.MenuDisplay();
            return seleccion;
        }

        public static void MostrarPedido(Pedido pedido)
        {
            if (pedido != null)
            {  
                Console.WriteLine($"Pedido Nro: {pedido.Numero}");
                Console.WriteLine($"Observaciones: {pedido.Observacion}");
                Console.WriteLine($"Estado: {pedido.Estado}");
                pedido.VerDatosCliente();
                if(pedido.CadeteAsignado != null)
                {
                    Console.WriteLine($"Cadete Asignado: {pedido.CadeteAsignado.Nombre}");
                }
            }else
            {
                Console.WriteLine("El pedido no existe");
            }
        }

        public static void MostrarPedidosSinEntregar(Cadeteria cadeteria)
        {
            var pedidosSinEntregar = cadeteria.Pedidos.Where(p => p.Estado != Estados.Entregado).ToList();
            if(pedidosSinEntregar.Count != 0)
            {
                foreach (var pedido in pedidosSinEntregar)
                {
                    MostrarPedido(pedido);
                }          
            }else
            {
                Console.WriteLine("El cadete no tiene pedidos sin entregar");
            }
            
        }
        public static bool MostrarPedidosSinCadete(Cadeteria cadeteria)
        {    
            var pedidosSinCadete = cadeteria.Pedidos.Where(p => p.CadeteAsignado.Nombre == null).ToList();
            if(pedidosSinCadete.Count != 0)
            {
                Console.WriteLine("Pedidos sin asignar");
                foreach (var pedido in pedidosSinCadete)
                {
                    MostrarPedido(pedido);
                }
                return true;          
            }else
            {
                Console.WriteLine("No hay pedidos sin asignar");
                return false;
            }
        }
        public static void MostrarCadetes(List<Cadete> cadetes1)
        {
            foreach (var cadete in cadetes1)
            {
                Console.WriteLine($"Id: {cadete.Id}");
                Console.WriteLine($"Id: {cadete.Nombre}");
                Console.WriteLine($"Id: {cadete.Telefono}");
                Console.WriteLine($"Id: {cadete.Direccion}");
                
            }
        }
    }

        
}