using System.Text.Json;

public class AccesoJson : IAccesoADatos
{
    private string archivoCadeteria;

    private string archivoCadetes;

    public AccesoJson()
    {
        archivoCadeteria = "Cadeteria.json";
        archivoCadetes = "Cadetes.json";
    }

    public bool Existe(string ruta)
    {
        return File.Exists(ruta);
    }

    public List<Cadete> LeerCadetes()
    {
        string ruta = "json/"+archivoCadetes;
        List<Cadete> cadetes; 
        string cadetesJson;
        if (Existe(ruta))
        {
            using (var archivoOpen = new FileStream(ruta, FileMode.Open))
            {
                using (var strReader = new StreamReader(archivoOpen))
                {
                    cadetesJson = strReader.ReadToEnd();
                    archivoOpen.Close();
                }
            }
            cadetes = JsonSerializer.Deserialize<List<Cadete>>(cadetesJson);
        }else
        {
            cadetes = new List<Cadete>();
        }
        return cadetes;
    }

    public Cadeteria LeerCadeteria()
    {
        string ruta = "json/"+archivoCadeteria;
        string cadeteriaJson;
        Cadeteria cadeteria;
        if (Existe(ruta))
        { 
            using (var archivoOpen = new FileStream(ruta, FileMode.Open))
            {
                using (var strReader = new StreamReader(archivoOpen))
                {
                    cadeteriaJson = strReader.ReadToEnd();
                    archivoOpen.Close();
                }
            }
            cadeteria = JsonSerializer.Deserialize<Cadeteria>(cadeteriaJson);
            
        }else
        {
            cadeteria = new Cadeteria();
        }
        return cadeteria;
    }
}