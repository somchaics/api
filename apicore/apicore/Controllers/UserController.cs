using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using apicore.Model;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace apicore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _iconfiguration;
        public UserController(IConfiguration configuration)
        {
            _iconfiguration = configuration;
        }

        // GET: api/<UserController>
        [HttpGet]
        public JsonResult Get()
        {
            MySqlConnection conn;
            string ConnectionString = _iconfiguration.GetConnectionString("mysqlcon");

            string query = @"SELECT * From user";
            MySqlDataReader myReader;
            DataTable tb = new DataTable();

            try
            {
                conn = new MySqlConnection(ConnectionString);
                conn.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, conn))
                {
                    myCommand.CommandType = CommandType.Text;
                    myReader = myCommand.ExecuteReader();
                    tb.Load(myReader);
                    myReader.Close();
                    conn.Close();
                }
            }
            catch (MySqlException ex)
            {
                return new JsonResult(ex.Message);
            }

            return new JsonResult(tb);


        }

        [HttpGet("{cname}")]
        public JsonResult Get(string cname) //overload
        {
            MySqlConnection conn;
            string ConnectionString = _iconfiguration.GetConnectionString("mysqlcon");

            string query = @"SELECT * From user where name = " + cname;
            MySqlDataReader myReader;
            DataTable tb = new DataTable();

            try
            {
                conn = new MySqlConnection(ConnectionString);
                conn.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, conn))
                {
                    myCommand.CommandType = CommandType.Text;
                    myReader = myCommand.ExecuteReader();
                    tb.Load(myReader);
                    myReader.Close();
                    conn.Close();
                }
            }
            catch (MySqlException ex)
            {
                return new JsonResult(ex.Message);
            }

            return new JsonResult(tb);
        }

        // POST api/<UserController> //add new
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/<UserController>/5 //update edit
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/<UserController>/5 //delete
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
