using System.Data;
using System.Runtime.Intrinsics.X86;
using System.Text.Json.Serialization;
using Sistema;

public class Cadeteria
{
    private string nombre;

    private string telefono;
    private List<Cadete> cadetes;

    private List<Pedido> pedidos;


    public string Nombre { get => nombre;}

    public string Telefono {get => telefono;}
    public List<Cadete> Cadetes { get => cadetes; set => cadetes = value;}
    public List<Pedido> Pedidos { get => pedidos; set => pedidos = value; }

    public Cadeteria(string nombre, string telefono)
    {
        this.nombre = nombre;
        this.telefono = telefono;
        pedidos = new List<Pedido>();
    }

    private Pedido CrearPedido(string[] informacionPedido)
    {
        int numPedido = Pedidos.Count + 1;
        Pedido pedidoNuevo = new Pedido(numPedido, informacionPedido[0], informacionPedido[1], informacionPedido[2], informacionPedido[3], informacionPedido[4]);
        return pedidoNuevo;
    }

    public void GuardarPedido(string[] informacionPedido)
    {
        Pedido pedido = CrearPedido(informacionPedido);
        Pedidos.Add(pedido);
    }
    public bool AsignarCadeteAPedido(int numPedido, int idCadete)
    {
       bool success = false;
       var cadeteElegido = cadetes.Where(c => c.Id == idCadete).ToList();
       var pedidoElegido = pedidos.Where(p => p.Numero == numPedido).ToList();
       if(cadeteElegido != null && pedidoElegido != null)
       {
            pedidoElegido[0].CadeteAsignado = cadeteElegido[0];
            success = true;
       }
       return success;

    }


    public bool ReasignarPedido(int numero)
    {
        bool success = false;
        var pedidoAReasignar = Pedidos.Where(p => p.Numero == numero).ToList();
        if (pedidoAReasignar.Count != 0)
        {
            var cadetesDisponibles = cadetes.Where(c => c.Nombre != pedidoAReasignar[0].CadeteAsignado.Nombre).ToList();
            int seleccion = Funciones.ElegirCadete(cadetesDisponibles);
            pedidoAReasignar[0].CadeteAsignado = cadetesDisponibles[seleccion];
            success = true;
        }
        return success;
        
    }

    public void CambiarEstadoDelPedido(int numero, int estado)
    {
        var pedidoAModificar = pedidos.Where(p => p.Numero == numero).ToList();
        if (pedidoAModificar.Count != 0)
        {
            switch (estado)
            {
                case 0:
                    pedidoAModificar[0].Estado = Estados.EnCamino;
                    break;
                case 1:
                    pedidoAModificar[0].Estado = Estados.Entregado;
                    break;
            }
        }

    }

    public int CalculoPedidosCompletados(int idCadete)
    {
        var pedidosEntregados = pedidos.Where(p => p.Estado == Estados.Entregado).ToList();
        var numPedidosCompletados = pedidosEntregados.Count(p => p.CadeteAsignado.Id == idCadete);
        return numPedidosCompletados;
    }

    public float JornalACobrar(int pedidosCompletados)
    {
        return 500*pedidosCompletados;
    }

}
