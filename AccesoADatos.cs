
public interface IAccesoADatos
{
    public bool Existe(string nombreArchivo);
    public List<Cadete> LeerCadetes(string nombreArchivo);
    public Cadeteria LeerCadeteria(string nombreArchivo);
 
}
