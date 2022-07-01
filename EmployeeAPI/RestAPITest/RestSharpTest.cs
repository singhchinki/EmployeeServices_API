using EmployeeAPI;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace RestAPITest
{
    public class RestSharpTest
    {
        RestClient restClient;
        [Test]
        public void CallGetMethod_ReturnEmployeeDetails()
        {
            restClient = new RestClient("http://localhost:4000");
            RestRequest request = new RestRequest("/employees", Method.Get);
            RestResponse response = restClient.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            List<Employee> list = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
            Assert.AreEqual(4, list.Count);
            foreach (Employee e in list)
            {
                Console.WriteLine("ID: " + e.Id + "\nName: " + e.Name + "\nSalary: " + e.Salary);
            }
        }
        [Test]
        public void OnPostingEmployeeData_ShouldAddtoJsonServer()
        {
            restClient = new RestClient("http://localhost:4000");
            RestRequest request = new RestRequest("/employees", Method.Post);
            var body = new Employee { Id = 25, Name = "kavita", Salary = "29000" };
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = restClient.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Employee data = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("Kavita", data.Name);
            Assert.AreEqual("29000", data.Salary);
            Console.WriteLine(response.Content);
        }
    }
}