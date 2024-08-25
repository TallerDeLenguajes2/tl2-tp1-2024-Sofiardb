using System.Runtime.Intrinsics.X86;

public class Cadeteria
{
    private string nombre;

    private string telefono;
    private List<Cadete> cadetes;


    public string Nombre { get => nombre;}

    public string Telefono {get => telefono;}
    public List<Cadete> Cadetes { get => cadetes;}

    public Cadeteria(string nombre, string telefono, List<Cadete> cadetes)
    {
        this.nombre = nombre;
        this.telefono = telefono;
        this.cadetes = cadetes;
    }

    public void AsignarPedido(Pedido pedido)
    {
        List<string> opcionesMenu = new List<string>();
        foreach (var cadete in cadetes)
        {
            opcionesMenu.Add(cadete.Nombre); 
        }
        string[] opcionesCadetes = opcionesMenu.ToArray();
        Menu menuDeSeleccion = new Menu("Seleccione el cadete al que asignará el pedido", opcionesCadetes);
        int seleccion = menuDeSeleccion.MenuDisplay();
        cadetes[seleccion].Pedidos.Add(pedido);

    }

    public void ReasignarPedido(int numero)
    {
        var cadeteConPedido = cadetes.Where(c => c.Pedidos.Any(p => p.Numero == numero)).ToList();
        if (cadeteConPedido.Count != 0)
        {
            var cadetesDisponibles = cadetes.Where(c => c.Nombre != cadeteConPedido[0].Nombre).ToList();
            List<string> opcionesMenu = new List<string>();
            Pedido pedidoAReasignar = cadeteConPedido[0].DarDeBajaPedido(numero);
            foreach (var cadete in cadetesDisponibles)
            {
                opcionesMenu.Add(cadete.Nombre); 
            }
            string[] opcionesCadetes = opcionesMenu.ToArray();
            Menu menuDeSeleccion = new Menu("Seleccione el cadete al que reasignará el pedido", opcionesCadetes);
            int seleccion = menuDeSeleccion.MenuDisplay();
            cadetesDisponibles[seleccion].Pedidos.Add(pedidoAReasignar);  
        }else
        {
            Console.WriteLine("El número ingresado no se corresponde con ningun pedido");
        }
        
    }

    public void CambiarEstadoDelPedido(int numero)
    {
        var cadeteConPedido = cadetes.Where(c => c.Pedidos.Any(p => p.Numero == numero)).ToList();
        if (cadeteConPedido.Count != 0)
        {
            Menu menuDeSeleccion = new Menu("Seleccione el estado al que desea cambiar", ["En camino", "Entregado"]);
            int seleccion = menuDeSeleccion.MenuDisplay();
            switch (seleccion)
            {
                case 0:
                    cadeteConPedido[0].RetirarPedido(numero);
                    break;
                case 1:
                    cadeteConPedido[0].CompletarPedido(numero);
                    break;
            }
        }else
        {
            Console.WriteLine("El número ingresado no se corresponde con ningún pedido"); 
        }

    }
    public void MostrarJornalesYEnvios()
    {
        int totalEnvios = 0;
        foreach (var cadete in cadetes)
        {
            float pago = cadete.JornalACobrar();
            Console.WriteLine($"{cadete.Nombre}-${pago}");
            totalEnvios += cadete.CantidadDePedidosCompletados();
        }
        float promedioEnviosPorCadete = (float)totalEnvios/cadetes.Count;
        Console.WriteLine($"Total-Envios: {totalEnvios}"); 
        Console.WriteLine($"Promedio de envios completado por cadete: {promedioEnviosPorCadete}");
    }

}
