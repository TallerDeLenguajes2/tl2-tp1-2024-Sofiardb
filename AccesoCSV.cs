
public class AccesoCSV : IAccesoADatos
{
    public bool Existe(string nombreArchivo)
    {
        string ruta = "csv/"+nombreArchivo;
        return File.Exists(ruta);
    }

    public List<Cadete> LeerCadetes(string nombreArchivo)
    {
        string ruta = "csv/"+nombreArchivo;
        List<Cadete> cadetes = new List<Cadete>();
        using (var archivoOpen = new FileStream(ruta, FileMode.Open))
        {
            using (var strReader = new StreamReader(archivoOpen))
            {
                string linea;
                while ((linea = strReader.ReadLine()) != null)
                {
                    var datos = linea.Split(';');
                    var cadete = new Cadete(int.Parse(datos[0]), datos[1],  datos[2], datos[3]);
                    cadetes.Add(cadete);
                }
            }
        }
        return cadetes;
    }
    public Cadeteria LeerCadeteria(string nombreArchivo)
    {
        string ruta = "csv/"+nombreArchivo;
        string informacionCadeteria;
        using (var archivoOpen = new FileStream(ruta, FileMode.Open))
        {
            using (var strReader = new StreamReader(archivoOpen))
            {
                informacionCadeteria = strReader.ReadToEnd();
                archivoOpen.Close();
            }
        }  
        string[] datos = informacionCadeteria.Split(";");
        Cadeteria cadeteria = new Cadeteria(datos[0],datos[1]);
        return cadeteria;
    }
    
}