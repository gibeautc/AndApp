using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using System.IO;
using Globals;
using Newtonsoft.Json;



namespace Core
{   public class status
    {
        public float setpt { get; set; }
        public bool hbool { get; set; }
    }
    public class WebService
    {
        private static byte[] getBody()
        {
            byte[] msg=new byte[1];
            //get set point
            //get status of any switches, other stuff
            status now = new status
            {
                setpt = Globals.Vars.setpoint,
                hbool = Globals.Vars.HeatBool
            };
            //create json of all elements
            //convert to string
            //encrypt string
            //string to byte array
            //return byte array
            string text=Newtonsoft.Json.JsonConvert.SerializeObject(now);

            byte[] bA = Encoding.UTF8.GetBytes(text);
            return bA;
        }
        public static int Push(float set)
        {
            string url = "http://dustoff.servebeer.com:5000/app";
            var request = WebRequest.Create(url) as WebRequest;
            request.Method = "POST";
            //string body = "Test Message";
            byte[] body = getBody();
            request.ContentLength = body.Length;
            Stream dataStream=request.GetRequestStream();
            //byte[] bA = Encoding.UTF8.GetBytes(body);
            dataStream.Write(body, 0, body.Length);
            
            request.ContentType = "application/data";
            try
            {
                request.GetResponse();
            }
            catch(System.Net.WebException)
            {
                return 4;
            }

            return 1;
     //       System.Json.JsonObject jsonNotif = new System.Json.JsonObject()
     //{{ "Id" , 1}, { "Description" , notif.Description}};
     //       string body = jsonNotif.ToString();
     //       request.ContentLength = body.Length;
     //       request.Method = "POST";
     //       request.ContentType = "application/json";

     //       StreamWriter stOut = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
     //       stOut.Write(body);
     //       stOut.Close();
     //       request.GetResponse();

     //       StartLocationMonitor();
     //   }



    }
    }
}