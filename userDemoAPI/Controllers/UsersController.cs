using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Queries.Persistence;
using userDemo.Data.Core.Domain;
using Helper;

#pragma warning disable
namespace userDemo.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly UserDbContext dbContext;
        public UsersController(UserDbContext context){
            dbContext = context;
        }

        //CREATE//POST
        [HttpPost()]
        public async Task<IActionResult> Post([FromBody]User obj)
        {
            try
            {
                if (obj == null)
                    return BadRequest();
                using (var db = new UnitOfWork(dbContext))
                {
                    if (db.Users.Get(obj.Id) == null)
                    {
                        db.Users.Add(obj);
                        db.Complete();
                        return Created("users/" + obj.Id, obj);
                    }
                    return BadRequest("Entry exists");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
            }

        }

        //READ//GET
        [HttpGet()]//{id}")]
        public async Task<object> Get()//int? id = null)
        {
            try
            {
                var _id = this.GetParameterValue("id");
                int id = 0;
                if (_id != null)
                    if (!Int32.TryParse(_id, out id))
                        return BadRequest();
                using (var db = new UnitOfWork(dbContext))
                {
                    if (_id != null)
                    {
                        var s = db.Users.Get(id);
                        if (s == null)
                            return NotFound();
                        return s;
                    }
                    else
                    {
                        var s = db.Users.GetAll();
                        return s;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
            }
        }

        //UPDATE//PUT
        [HttpPut()]
        public async Task<IActionResult> Put([FromBody]User obj)
        {
            try
            {
                if (obj == null)
                    return BadRequest();
                using (var db = new UnitOfWork(dbContext))
                {
                    var match = db.Users.Get(obj.Id);
                    if (match != null)
                    {
                        //db.Users.Update(match);

                        match.UserName = obj.UserName;
                        match.Password = obj.Password;
                        match.Email = obj.Email;
                        match.Gender = obj.Gender;

                        db.Complete();
                        return Ok();
                    }
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
            }

        }

        //DELETE
        [HttpDelete()]
        public async Task<IActionResult> Delete()
        {
            try
            {
                var _id = this.GetParameterValue("id");
                int id = 0;
                if (!Int32.TryParse(_id, out id))
                    return BadRequest();
                using (var db = new UnitOfWork(dbContext))
                {
                    var match = db.Users.Get(id);
                    if (match != null)
                    {
                        db.Users.Remove(match);
                        db.Complete();
                        return Ok();
                    }
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
            }
        }
    }
}
