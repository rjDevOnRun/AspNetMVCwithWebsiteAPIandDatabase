using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
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

        public static string API_Address_Build_SaveContact = "http://mywebapi:8080/SaveContact";
        public static string API_Address_Debug_SaveContact = "https://localhost:44398/SaveContact";

        public static string API_Address_Build_GetContact = "http://mywebapi:8080/SaveContact";
        public static string API_Address_Debug_GetContact = "https://localhost:44398/";


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
#if DEBUG
                var webResponse = await webClient.PostAsync(API_Address_Debug_SaveContact, httpContent);
#else
                var webResponse = await webClient.PostAsync(API_Address_Build_SaveContact, httpContent);
#endif

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

        internal static List<Contact> GetContacts()
        {
            List<Contact> contacts = new List<Contact>();
            try
            {
                // Init http-client for API calls
                var webClient = new HttpClient();
                // Assign values to http-client (BaseAddress, HeaderTypes...)
#if DEBUG
                webClient.BaseAddress = new Uri(API_Address_Debug_GetContact);
#else
                webClient.BaseAddress = new Uri(API_Address_Build_GetContact);
#endif

                webClient.DefaultRequestHeaders.Clear();
                webClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Call the API's Action Method to get results
                HttpResponseMessage httpResponse = webClient.GetAsync("GetContactsInDB").Result;
                // On Error return empty list
                if (httpResponse.IsSuccessStatusCode == false) return contacts; 

                // Capture JSON result
                var jsonResultFromAPI = httpResponse.Content.ReadAsStringAsync();
                // Convert JSON to object collection
                var jsonConvertedToObjectCollection = JsonConvert.DeserializeObject<List<Contact>>(jsonResultFromAPI.Result);

                // Populate return collection
                contacts.AddRange(jsonConvertedToObjectCollection);

                return contacts;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message + ex.StackTrace);
                return contacts;
            }
        }
    }
}