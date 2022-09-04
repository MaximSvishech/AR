
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using Newtonsoft.Json;

public class ArrowCreator : MonoBehaviour
{
    [SerializeField] GameObject line;

    // Start is called before the first frame update
    void Start()
    {
       
       //Run("1","2");
        CreatArrow(new Feature
        {
            property = new Property{
                Angle = 0.50f,
                Meter = 10
            }
        });
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Run(string sourse, string destination)
    {
        var data = Resources.LoadAll("GoeJson");

        //TODO: need to connect to resours file
        var configFileName = $"{sourse}_To_{destination}.geojson";

        var movments = GetFromConfig(configFileName);
        if (movments != null)
            return;
  
        foreach (var feature in movments.features)
        {
            CreatArrow(feature);
        }
    }

    public void CreatArrow(Feature feture)
    {

        //rotate
        transform.rotation = Quaternion.Euler(0, feture.property.Angle, 0);

        //Craete Cude
        GameObject tempLine = Instantiate(line, transform.position, transform.rotation);

        //change by meters
        tempLine.transform.localScale = new Vector3(0, 0, 60);

        //change position
        transform.position += transform.forward * feture.property.Meter;



    }

    private Movment GetFromConfig(string fileName)
    {
        Movment movment;
        using (StreamReader r = new StreamReader(fileName))
        {
            string json = r.ReadToEnd();
            //need to change to JsonUtility.FromJson<MyClass>(json.text);
            movment = JsonUtility.FromJson<Movment>(json);
        }
        return movment;
    }
}
#region models

public class Movment
{
    public string type { get; set; }
    public string name { get; set; }
    public List<Feature> features { get; set; }
}
public class Feature
{
    public string type { get; set; }
    public Property property { get; set; }
}
public class Property
{
    public int id { get; set; }
    public float Angle { get; set; }
    public int Meter { get; set; }

}

#endregion