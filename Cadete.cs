using System.Reflection;

public class Cadete
{
    private int id;
    private string nombre;
    private string direccion;
    private string telefono;
    private List<Pedido> pedidos;


    public Cadete(int id, string nombre, string direccion, string telefono)
    {
        this.id = id;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
    }

    public int Id { get => id;}
    public string Nombre { get => nombre; }
    public string Direccion { get => direccion; }
    public string Telefono { get => telefono; }
    public List<Pedido> Pedidos { get => pedidos; set => pedidos = value; }
   

    public int CantidadDePedidosCompletados()
    {
        return pedidos.Count(p => p.Estado == Estados.Entregado);
    }
    public int JornalACobrar()
    {
        return 500 * CantidadDePedidosCompletados();

    }
    public Pedido DarDeBajaPedido(int numero)
    {
       var pedidoAQuitar = Pedidos.Where(p => p.Numero == numero).ToList();
       Pedidos.Remove(pedidoAQuitar[0]);
       return pedidoAQuitar[0];
    }
}
