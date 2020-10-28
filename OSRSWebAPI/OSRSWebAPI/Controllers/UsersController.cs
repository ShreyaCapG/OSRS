using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using OSRSWebAPI.Models;
namespace OSRSWebAPI.Controllers
{
    public class UsersController : ApiController
    {
        //get list of all users
        //remove on later stage
        public IHttpActionResult GetAllUsers()
        {
            IList<UsertableDTO> users = null;

            using (var ctx = new OSRSEntities())
            {
                users = ctx.UserTables.Include("RoleType")
                            .Select(u => new UsertableDTO()
                            {
                                name = u.name,
                                username = u.username,
                                password = u.password,
                                email = u.email,
                                contact_number = u.contact_number,
                                userid = u.userid,
                                usertype = new RoletypeDTO()
                                {
                                    roleid = u.RoleType.roleid,
                                    user_type = u.RoleType.user_type
                                }
                            }).ToList<UsertableDTO>();
            }

            if (users.Count == 0)
            {
                return NotFound();
            }

            return Ok(users);
        }

        //get single user 
        //user authentication

        //public IHttpActionResult GetUserByCredentials(string username, string password, int usertype)
        //{
        //    IList<UsertableDTO> users = null;

        //    using (var ctx = new OSRSEntities())
        //    {
        //        users = ctx.UserTables.Include("RoleType")
        //                    .Where(
        //                            u => u.username == username &&
        //                            u.password == password &&
        //                            u.usertype == usertype
        //                    )
        //                    .Select(u => new UsertableDTO()
        //                    {
        //                        name = u.name,
        //                        username = u.username,
        //                        password = u.password,
        //                        email = u.email,
        //                        contact_number = u.contact_number,
        //                        userid = u.userid,
        //                        usertype = new RoletypeDTO()
        //                        {
        //                            roleid = u.RoleType.roleid,
        //                            user_type = u.RoleType.user_type
        //                        }
        //                    }).ToList<UsertableDTO>();
        //    }

        //    if (users.Count == 0)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(users);
        //}

        //get single user 
        //user authentication 
        //same as GetUserByCredentials
        [HttpGet]
        public IHttpActionResult ValidateUser(string username, string password, int usertype)
        {
            IList<UsertableDTO> users = null;

            using (var ctx = new OSRSEntities())
            {
                users = ctx.UserTables.Include("RoleType")
                            .Where(
                                    u => u.username == username &&
                                    u.password == password &&
                                    u.usertype == usertype
                            )
                            .Select(u => new UsertableDTO()
                            {
                                name = u.name,
                                username = u.username,
                                password = u.password,
                                email = u.email,
                                contact_number = u.contact_number,
                                userid = u.userid,
                                usertype = new RoletypeDTO()
                                {
                                    roleid = u.RoleType.roleid,
                                    user_type = u.RoleType.user_type
                                }
                            }).ToList<UsertableDTO>();
            }

            if (users.Count == 0)
            {
                return NotFound();
            }

            return Ok(users);
        }



        //gets user by userid
        //fetches user details for updating 
        //public IHttpActionResult GetUserByID(int userid)
        //{
        //    IList<UsertableDTO> users = null;

        //    using (var ctx = new OSRSEntities())
        //    {
        //        users = ctx.UserTables.Include("RoleType")
        //                    .Where(
        //                            u => u.userid == userid 
        //                    )
        //                    .Select(u => new UsertableDTO()
        //                    {
        //                        name = u.name,
        //                        username = u.username,
        //                        password = u.password,
        //                        email = u.email,
        //                        contact_number = u.contact_number,
        //                        userid = u.userid,
        //                        usertype = new RoletypeDTO()
        //                        {
        //                            roleid = u.RoleType.roleid,
        //                            user_type = u.RoleType.user_type
        //                        }
        //                    }).ToList<UsertableDTO>();
        //    }

        //    if (users.Count == 0)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(users);
        //}

        [HttpGet]
        //gets user by userid
        //fetches user details for updating 
        //same as GetUserByID
        public IHttpActionResult GetUserDetailsByID(int userid)
        {
           UsertableDTO user = null;

            using (var ctx = new OSRSEntities())
            {
                user = ctx.UserTables.Include("RoleType")
                            .Where(
                                    u => u.userid == userid
                            )
                            .Select(u => new UsertableDTO()
                            {
                                name = u.name,
                                username = u.username,
                                password = u.password,
                                email = u.email,
                                contact_number = u.contact_number,
                                userid = u.userid,
                                usertype = new RoletypeDTO()
                                {
                                    roleid = u.RoleType.roleid,
                                    user_type = u.RoleType.user_type
                                }
                            }).FirstOrDefault<UsertableDTO>();
            }

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        //update user details
        //implement edit method on client side
        public IHttpActionResult Put(UsertableDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new OSRSEntities())
            {
                var existingUser = ctx.UserTables.Where(u => u.userid == user.userid)
                                                        .FirstOrDefault<UserTable>();

                if (existingUser != null)
                {
                    existingUser.name = user.name;
                    existingUser.password = user.password;
                    existingUser.email = user.email;
                    existingUser.contact_number = user.contact_number;                                    
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }
        //add new user 
        //sign in
        //change name to add and add attributes on top
        //public IHttpActionResult PostNewUser(UsertableDTO userTable)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest("Invalid data");
        //    }

        //    using (var ctx = new OSRSEntities())
        //    {
        //        ctx.UserTables.Add(new UserTable()
        //        {
        //            userid = userTable.userid,
        //            name = userTable.name,
        //            username = userTable.username,
        //            password = userTable.password,
        //            email = userTable.email,
        //            contact_number = userTable.contact_number,
        //            usertype = userTable.usertype.roleid
        //        });
        //        ctx.SaveChanges();
        //    }
        //    return Ok();
        //}

        //add new user 
        //sign in
        //same as PostNewUser
        [HttpPost]
        public IHttpActionResult AddUser(UsertableDTO userTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            using (var ctx = new OSRSEntities())
            {
                ctx.UserTables.Add(new UserTable()
                {
                    userid = userTable.userid,
                    name = userTable.name,
                    username = userTable.username,
                    password = userTable.password,
                    email = userTable.email,
                    contact_number = userTable.contact_number,
                    usertype = userTable.usertype.roleid
                });
                ctx.SaveChanges();
            }
            return Ok();
        }
    }
}
