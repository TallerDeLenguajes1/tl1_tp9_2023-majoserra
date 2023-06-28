using System;
using EspacioMonedas;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;


internal class Program{
    static void Main(string[ ] args){
        Get(); //Hacemos una llamada a un elemento Privado
    }
    private static void Get(){
        var url = $"https://api.coindesk.com/v1/bpi/currentprice.json";
        var request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";// metodo Get
        request.ContentType = "application/json";
        request.Accept = "application/json"; //Aplicacion que tiene que devolver un json 

        try //Vamos convirtiendo 
        {
            using (WebResponse response = request.GetResponse()){ //request o convierte en un elemento de tipo webresponse
                using(Stream sr = response.GetResponseStream()){ //a esa respuesta en un tipo steam
                    if (sr == null) return;
                    //si es distinto de nulo, entonces seguira y nos creara un objeto de tipo stream Reader
                    using (StreamReader objReader = new StreamReader(sr))
                    {
                        // a ese objeto de tipo stream reader lo convertimos en un texto
                        string responseBody = objReader.ReadToEnd();
                        Root moneda = JsonSerializer.Deserialize<Root>(responseBody);

                        // No tengo una lista asi que mostramos los precios por console
                        Console.WriteLine("-> "+moneda.bpi.EUR.code+" :"+moneda.bpi.EUR.rate_float);
                        Console.WriteLine("-> "+moneda.bpi.GBP.code+" :"+moneda.bpi.GBP.rate_float);
                        Console.WriteLine("-> "+moneda.bpi.USD.code+" :"+moneda.bpi.USD.rate_float);
                        Console.WriteLine();
                        Console.WriteLine("Ingrese una Moneda: EUR USD GBP");
                        string? resp = Console.ReadLine();
                        switch (resp)
                        {
                            case "EUR":
                                Console.WriteLine("Code: "+moneda.bpi.EUR.code);
                                Console.WriteLine("Simbolo: "+moneda.bpi.EUR.symbol);
                                Console.WriteLine("Descripcion: "+moneda.bpi.EUR.description);
                                Console.WriteLine("Tasa: "+moneda.bpi.EUR.rate);
                            break;
                            case "USD":
                                Console.WriteLine("Code: "+moneda.bpi.USD.code);
                                Console.WriteLine("Simbolo: "+moneda.bpi.USD.symbol);
                                Console.WriteLine("Descripcion: "+moneda.bpi.USD.description);
                                Console.WriteLine("Tasa: "+moneda.bpi.USD.rate);
                            break;
                            case "GBP":
                                Console.WriteLine("Code: "+moneda.bpi.GBP.code);
                                Console.WriteLine("Simbolo: "+moneda.bpi.GBP.symbol);
                                Console.WriteLine("Descripcion: "+moneda.bpi.GBP.description);
                                Console.WriteLine("Tasa: "+moneda.bpi.GBP.rate);
                            break;
                            default:
                                Console.WriteLine("No se ingreso Correctamente la moneda");
                            break;
                        }

                    }
                }
            }
            
        }
        catch (WebException)
        {
            
            Console.WriteLine("Hubo poblemas en la conexion");
        }


    } 
}