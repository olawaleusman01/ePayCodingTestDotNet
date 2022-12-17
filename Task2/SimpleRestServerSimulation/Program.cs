using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text;
using static SimpleRestServerSimulation.Program;
using System.Collections.Generic;

namespace SimpleRestServerSimulation;
class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Simulation for Simple Rest Server");

        await SimulateGetRequests();
        await SimulatePostRequests();
  
    }

   static string[] firstNames = new string[] { "Leia", "Sadie", "Jose", "Sara", "Frank", "Dewey", "Tomas", "Joel", "Lukas", "Carlos" };
   static string[] lastNames = new string[] {   "Liberty","Ray","Harrison","Ronan","Drew","Powell","Larsen","Chan","Anderson","Lane"};
    // Simulate POST requests
    private static async Task SimulatePostRequests()
    {
        try
        {
            // Generate random 5 post request 
            int idx = 1;
            for (int i = 0; i < 5; i++)
            {
                var randomCustomers = new List<Customer>();
                //Determine random number of customers for the post request
                var randomPostCustIndex = new Random().Next(2, 5);
                for (int j = 0; j < randomPostCustIndex; j++)
                {
                    var randomIndex = new Random().Next(1, firstNames.Length);

                    var customer = new Customer
                    {
                        FirstName = firstNames[randomIndex],
                        LastName = lastNames[randomIndex],
                        Age = new Random().Next(10, 90),
                        Id = idx++
                    };
                    randomCustomers.Add(customer);
                }

                // Send POST request
                using (var client = new HttpClient())
                {
                    var httpContent = new StringContent(
                   JsonSerializer.Serialize(randomCustomers),
                   Encoding.UTF8,
                   "application/json"
               );
                    var response = await client.PostAsync("https://localhost:7082/customers", httpContent);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Post to customer end point was OK");
                    }
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Post Simulation return an exception {ex.Message}");
        }
    }

    // Simulate GET requests
    private static async Task SimulateGetRequests()
    {
        try
        {
            // Send GET requests
            for (int i = 0; i < 10; i++)
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync("https://localhost:7082/customers");
                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        Console.WriteLine("Get Request successful");
                        var contents = await response.Content.ReadAsStringAsync();
                        var customers = JsonSerializer.Deserialize<List<Customer>>(contents, options);
                        customers.ForEach(i => Console.WriteLine($"lastName:{i.LastName}\tfirstName:{i.FirstName}\tage:{i.Age}\tid:{i.Id}\t", i));
                    }
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Get Simulation return an exception {ex.Message}");

        }
    }

    public class Customer
    {

        public string LastName { get; set; }

        public string FirstName { get; set; }
   
        public int Age { get; set; }

        public int Id { get; set; }

    }
}
