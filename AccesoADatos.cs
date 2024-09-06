
public interface IAccesoADatos
{
    public bool Existe(string ruta);
    public List<Cadete> LeerCadetes();
    public Cadeteria LeerCadeteria();
 
}
