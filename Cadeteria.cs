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
        cadetes.OrderDescending();
    }

    public void AsignarPedido(Pedido pedido)
    {
        cadetes[cadetes.Count-1].Pedidos.Add(pedido);
        cadetes.OrderDescending();
        
    }

    public void ReasignarPedido(int numero)
    {
        var cadeteConPedido = cadetes.Where(c => c.Pedidos.Any(p => p.Numero == numero)).ToList();
        if (cadeteConPedido != null)
        {
            var cadetesDisponibles = cadetes.Where(c => c.Nombre != cadeteConPedido[0].Nombre).ToList();
            List<string> opcionesMenu = new List<string>();
            Pedido pedidoAResignar = cadeteConPedido[0].DarDeBajaPedido(numero);
            foreach (var cadete in cadetesDisponibles)
            {
            opcionesMenu.Add(cadete.Nombre); 
            }
            string[] opcionesCadetes = opcionesMenu.ToArray();
            Menu menuDeSeleccion = new Menu("Seleccione el cadete al que reasignará el pedido", opcionesCadetes);
            int seleccion = menuDeSeleccion.MenuDisplay();
            cadetesDisponibles[seleccion].Pedidos.Add(pedidoAResignar);  
        }else
        {
            Console.WriteLine("El número ingresado no se corresponde con ningun pedido");
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
        float promedioEnviosPorCadete = totalEnvios/cadetes.Count;
        Console.WriteLine($"Total-Envios: {totalEnvios}"); 
        Console.WriteLine($"Promedio de envios completado por cadete: {promedioEnviosPorCadete}");
    }

}
