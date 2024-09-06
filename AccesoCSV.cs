
public class AccesoCSV : IAccesoADatos
{
    private string archivoCadeteria;

    private string archivoCadetes;
    public bool Existe(string ruta)
    {

        return File.Exists(ruta);
    }

    public List<Cadete> LeerCadetes()
    {
        string ruta = "csv/"+archivoCadetes;
        List<Cadete> cadetes = new List<Cadete>();
        if (Existe(ruta))
        {  
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
        }
        return cadetes;
    }
    public Cadeteria LeerCadeteria()
    {
        string ruta = "csv/"+archivoCadeteria;
        Cadeteria cadeteria;
        string informacionCadeteria;
        if (Existe(ruta))
        {
            using (var archivoOpen = new FileStream(ruta, FileMode.Open))
            {
                using (var strReader = new StreamReader(archivoOpen))
                {
                    informacionCadeteria = strReader.ReadToEnd();
                    archivoOpen.Close();
                }
            }  
            string[] datos = informacionCadeteria.Split(";");
            cadeteria = new Cadeteria(datos[0],datos[1]);
        }else
        {
            cadeteria = new Cadeteria();
        }
        return cadeteria;
    }
    
}