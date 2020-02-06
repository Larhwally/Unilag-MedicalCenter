using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Unilag_Medic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JsonTryController : ControllerBase
    {
        public class MyDate
        {
            public int year;
            public int month;
            public int day;
        }

        public class Lad
        {
            public string firstName;
            public string lastName;
            public MyDate dateOfBirth;
        }

        class Program
        {
            [HttpGet]
            public string DisplayJson()
            {
                var obj = new Lad
                {
                    firstName = "Markoff",
                    lastName = "Chaney",
                    dateOfBirth = new MyDate
                    {
                        year = 1901,
                        month = 4,
                        day = 30
                    }
                };
                var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                //Console.WriteLine(jsonString);
                return jsonString;
            }
        }
    }
}