using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebService.Models;

namespace ContactManagerWebSite.Repositories
{
    public class ContactManagerRepository
    {
        /*  NOTEs: 
         *          Add reference to API application DLL 
         *          Make SQL Server with sa login and passwords
         *          
         *          Keep IIS Publish website Ports seperate for API and Website
        */


        /// <summary>
        /// Saves Contact to API backend (Databases)
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public static async Task<bool> SaveContact(Contact contact)
        {

            try
            {
                // Init http to make calls to the API
                var webClient = new HttpClient();

                // Convert contact object to JSON
                var jsonContent = JsonConvert.SerializeObject(contact);

                // Convert json-content to http-content to enable sending across the web
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Send the http-content to the API via the http-client (async call) and get reponse back
                //var webResponse = await webClient.PostAsync("https://localhost:44398/SaveContact", httpContent);
                var webResponse = await webClient.PostAsync("http://mywebapi:8080/SaveContact", httpContent);

                // Convert the web-respose to a readble string..
                var stringReponse = await webResponse.Content.ReadAsStringAsync();

                // Validate the return response...
                if (stringReponse == "false")
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message + ex.StackTrace);
                return false;
            }
        }

    }
}