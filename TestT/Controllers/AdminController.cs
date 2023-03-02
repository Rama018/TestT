using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using TestT.Models;

namespace TestT.Controllers
{
    public class AdminController : Controller
    {
        public async Task<IActionResult> List2()
        {

            List<CompanyTest> stores = CompanyTest.GetAll();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7014/api/adminapi"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    stores = JsonConvert.DeserializeObject<List<CompanyTest>>(apiResponse);

                }
            }
            return View(stores);
        }

        public IActionResult Create()
        {
            return View();
        }


        //public async Task<IActionResult> BtnCreate(CompanyTest test)
        //{
        //    CompanyTest receivedReservation = new CompanyTest();            
        //    StringContent content = new StringContent(JsonConvert.SerializeObject(test), Encoding.UTF8, "application/json");

        //    using (var httpClient = new HttpClient())
        //    {

        //        using (var response = await httpClient.PostAsync("https://localhost:7014/api/adminapi", content))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();
        //            receivedReservation = JsonConvert.DeserializeObject<CompanyTest>(apiResponse);
        //        }
        //    }
        //    return View(nameof(List2));
        //}
        public async Task<IActionResult> BtnCreate(CompanyTest test)
        {
            string a = JsonConvert.SerializeObject(test, Formatting.Indented);
            HttpContent content = new StringContent(a, Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync("https://localhost:7014/api/adminapi", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    string? result = JsonConvert.DeserializeObject<string>(apiResponse);
                }
            }
            return RedirectToAction(nameof(List2));
        }


        public IActionResult Delete(int id)
        {
            CompanyTest test = CompanyTest.getID(id);
            return View(test);
        }

        public async Task<IActionResult> BtnDelete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"https://localhost:7014/api/adminapi/{id}"))
                {

                    string apiResponse = await response.Content.ReadAsStringAsync();


                }

                return RedirectToAction("List2");


            }

        }

        public IActionResult Update(int id)
        {
            CompanyTest test = CompanyTest.getID(id);
            return View(test);
        }






        public async Task<IActionResult> BtnUpdate(int id, CompanyTest test)
        {
            using var client = new HttpClient();

            var json = JsonConvert.SerializeObject(test);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"https://localhost:7014/api/adminapi/{id}", data);

            var content = await response.Content.ReadAsStringAsync();

            CompanyTest? updated = JsonConvert.DeserializeObject<CompanyTest>(content);

            return RedirectToAction(nameof(List2));

        }






        //public async Task<IActionResult> BtnUpdate(CompanyTest test, int id)
        //{
        //    using var client = new HttpClient();
        //    HttpContent body = new StringContent(JsonConvert.SerializeObject(test), Encoding.UTF8, "application/json");

        //    var response = await client.PutAsync($"https://localhost:7014/api/adminapi/{id}", body);
        //    var content = await response.Content.ReadAsStringAsync();
        //    CompanyTest? result = JsonConvert.DeserializeObject<CompanyTest>(content);

        //    return RedirectToAction(nameof(List2));


        //}



    }
}