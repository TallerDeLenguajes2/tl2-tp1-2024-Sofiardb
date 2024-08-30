using System.Data;
using System.Runtime.Intrinsics.X86;
using Sistema;

public class Cadeteria
{
    private string nombre;

    private string telefono;
    private List<Cadete> cadetes;

    private List<Pedido> pedidos;


    public string Nombre { get => nombre;}

    public string Telefono {get => telefono;}
    public List<Cadete> Cadetes { get => cadetes;}
    public List<Pedido> Pedidos { get => pedidos; set => pedidos = value; }

    public Cadeteria(string nombre, string telefono, List<Cadete> cadetes)
    {
        this.nombre = nombre;
        this.telefono = telefono;
        this.cadetes = cadetes;
        pedidos = new List<Pedido>();
    }

    public void GuardarPedido(Pedido pedido)
    {
        Pedidos.Add(pedido);
    }
    public void AsignarCadeteAPedido(int numPedido, int idCadete)
    {
       var cadeteElegido = cadetes.Where(c => c.Id == idCadete).ToList();
       var pedidoElegido = pedidos.Where(p => p.Numero == numPedido).ToList();
       if(cadeteElegido != null && pedidoElegido != null)
       {
            pedidoElegido[0].CadeteAsignado = cadeteElegido[0];
       }else
       {
            Console.WriteLine("No existe el pedido y/o cadete elegido");
       }

    }

    public void ReasignarPedido(int numero)
    {
        var pedidoAReasignar = Pedidos.Where(p => p.Numero == numero).ToList();
        if (pedidoAReasignar.Count != 0)
        {
            var cadetesDisponibles = cadetes.Where(c => c.Nombre != pedidoAReasignar[0].CadeteAsignado.Nombre).ToList();
            int seleccion = Funciones.ElegirCadete(cadetesDisponibles);
            pedidoAReasignar[0].CadeteAsignado = cadetesDisponibles[seleccion];
            
        }else
        {
            Console.WriteLine("El número ingresado no se corresponde con ningun pedido");
        }
        
    }

    public void CambiarEstadoDelPedido(int numero)
    {
        var pedidoAModificar = pedidos.Where(p => p.Numero == numero).ToList();
        if (pedidoAModificar.Count != 0)
        {
            Menu menuDeSeleccion = new Menu("Seleccione el estado al que desea cambiar", ["En camino", "Entregado"]);
            int seleccion = menuDeSeleccion.MenuDisplay();
            switch (seleccion)
            {
                case 0:
                    pedidoAModificar[0].Estado = Estados.EnCamino;
                    break;
                case 1:
                    pedidoAModificar[0].Estado = Estados.Entregado;
                    break;
            }
        }else
        {
            Console.WriteLine("El número ingresado no se corresponde con ningún pedido"); 
        }

    }

    private int CalculoPedidosCompletados(int idCadete)
    {
        var pedidosEntregados = pedidos.Where(p => p.Estado == Estados.Entregado).ToList();
        var numPedidosCompletados = pedidosEntregados.Count(p => p.CadeteAsignado.Id == idCadete);
        return numPedidosCompletados;
    }

    private float JornalACobrar(int pedidosCompletados)
    {
        return 500*pedidosCompletados;
    }
    public void MostrarJornalesYEnvios()
    {
        int totalEnvios = 0;
        foreach (var cadete in cadetes)
        {
            int numPedidosCompletados = CalculoPedidosCompletados(cadete.Id);
            float pago = JornalACobrar(numPedidosCompletados);
            Console.WriteLine($"{cadete.Nombre}-${pago}");
            totalEnvios += numPedidosCompletados;
        }
        float promedioEnviosPorCadete = (float)totalEnvios/cadetes.Count;
        Console.WriteLine($"Total-Envios: {totalEnvios}"); 
        Console.WriteLine($"Promedio de envios completado por cadete: {promedioEnviosPorCadete}");
    }

}
