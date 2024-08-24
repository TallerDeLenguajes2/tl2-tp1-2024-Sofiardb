public class Pedido
{
    public int Numero { get; set; }
    public string Observacion { get; set; }

    private Cliente cliente;
    public Estados Estado {get;set;}

    public Pedido(int nro, string obs, string nombre, string direcc, string telefono, string referencias)
    {
        Numero = nro;
        Observacion = obs;
        Estado = Estados.Preparación;
        cliente = new Cliente(nombre, direcc, telefono, referencias);
    }


    public void VerDireccionCliente()
    {
        Console.WriteLine($"Dirección de entrega: {cliente.Direccion}");
        if(cliente.DatosReferenciasDireccion != null)
        {
            Console.WriteLine($"Referencias: {cliente.DatosReferenciasDireccion}");
        }

    }
    public void VerDatosCliente()
    {
        Console.WriteLine($"Cliente: {cliente.Nombre}");
        Console.WriteLine($"Direccion: {cliente.Direccion}");
        Console.WriteLine($"Telefono: {cliente.Telefono}");
    }
}
