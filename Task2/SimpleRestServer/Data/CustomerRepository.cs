using System.Text.Json;
using SimpleRestServer.Models;

namespace SimpleRestServer.Data
{
    public class CustomerRepository : ICustomerRepository
    {

        string customerJsonPath = "Data/customers.json";
        private void InitializeData()
        {
            var rootPath = _hostingEnvironment.ContentRootPath;
            var path = Path.Combine(rootPath, customerJsonPath);

            var custJsonData = File.ReadAllText(path);

            if (string.IsNullOrWhiteSpace(custJsonData))
                return;

          
            _customerRepo = JsonSerializer.Deserialize<List<Customer>>(custJsonData);
        }

        public  void WriteCustomertoJsonFile()
        {
            var rootPath = _hostingEnvironment.ContentRootPath;
            var path = Path.Combine(rootPath, customerJsonPath);
            var jsonString = JsonSerializer.Serialize(_customerRepo);
            File.WriteAllText(path, jsonString);
        }

        public CustomerRepository(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            InitializeData();

        }
        List<Customer> _customerRepo;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public List<Customer> GetCustomers()
        {
            return _customerRepo.ToList();
        }
        public bool CustomerExist(int id)
        {
            return _customerRepo.Any(d => d.Id == id);
        }

        public void InsertCustomers(List<Customer> customers)
        {
            foreach (var cust in customers)
            {
                if (CustomerExist(cust.Id))
                {
                    continue;
                }
                _customerRepo.Insert(GetInsertIndex(cust), cust);
            }

            //Persist customer data to json file
            WriteCustomertoJsonFile();
        }


        private int GetInsertIndex(Customer customer)
        {
            for (int i = 0; i < _customerRepo.Count; i++)
            {
                if (_customerRepo[i].LastName.CompareTo(customer.LastName) > 0)
                {
                    return i;
                }
                else if (_customerRepo[i].LastName.CompareTo(customer.LastName) == 0)
                {
                    if (_customerRepo[i].FirstName.CompareTo(customer.FirstName) > 0)
                    {
                        return i;
                    }
                }
            }

            return 0;
        }

    }
}