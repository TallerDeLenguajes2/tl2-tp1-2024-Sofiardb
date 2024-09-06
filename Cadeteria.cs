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
    public List<Cadete> Cadetes { get => cadetes;}
    public List<Pedido> Pedidos { get => pedidos; set => pedidos = value; }

    public Cadeteria()
    {

    }

    public void AsignarCadetes(List<Cadete> cadetes)
    {
        this.cadetes = cadetes; 
    }
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
       var cadeteElegido = cadetes.Find(c => c.Id == idCadete);
       var pedidoElegido = pedidos.Find(p => p.Numero == numPedido);
       if(cadeteElegido != null && pedidoElegido != null)
       {
            pedidoElegido.CadeteAsignado = cadeteElegido;
            success = true;
       }
       return success;

    }


    public bool ReasignarPedido(int numero)
    {
        bool success = false;
        var pedidoAReasignar = Pedidos.Find(p => p.Numero == numero);
        if (pedidoAReasignar != null)
        {
            var cadetesDisponibles = cadetes.Where(c => c.Nombre != pedidoAReasignar.CadeteAsignado.Nombre).ToList();
            int seleccion = Funciones.ElegirCadete(cadetesDisponibles);
            pedidoAReasignar.CadeteAsignado = cadetesDisponibles[seleccion];
            success = true;
        }
        return success;
        
    }

    public void CambiarEstadoDelPedido(int numero, int estado)
    {
        var pedidoAModificar = pedidos.Find(p => p.Numero == numero);
        if (pedidoAModificar != null)
        {
            switch (estado)
            {
                case 0:
                    pedidoAModificar.Estado = Estados.EnCamino;
                    break;
                case 1:
                    pedidoAModificar.Estado = Estados.Entregado;
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
