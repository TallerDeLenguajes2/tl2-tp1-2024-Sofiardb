namespace Sistema
{    
    public class Funciones
    {
        public static void DarDeAltaPedido(List<Pedido> pedidosSinAsignar,int nroPedido)
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
            } while (observacionPedido.Length == 0 && nombreCliente.Length == 0 && direccCliente.Length == 0 && telefonoCliente.Length == 0);
            Pedido pedidoNuevo = new Pedido(nroPedido,observacionPedido,nombreCliente,direccCliente,telefonoCliente,referenciasCliente);
            pedidosSinAsignar.Add(pedidoNuevo);
        }

        public static void MostrarPedido(Pedido pedido)
        {
            Console.WriteLine($"Pedido Nro: {pedido.Numero}");
            Console.WriteLine($"Observaciones: {pedido.Observacion}");
            Console.WriteLine($"Estado: {pedido.Estado}");
            pedido.VerDatosCliente();
        }

        public static void MostrarPedidosSinEntregar(Cadeteria cadeteria)
        {
            foreach (var cadete in cadeteria.Cadetes)
            {
                Console.WriteLine($"Cadete-{cadete.Nombre}");
                foreach (var pedido in cadete.Pedidos.Where(p => p.Estado != Estados.Entregado).ToList())
                {
                    Funciones.MostrarPedido(pedido);
                }          
            }

        }

        
    }
}