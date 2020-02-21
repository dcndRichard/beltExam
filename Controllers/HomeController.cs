using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using beltExam.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;//for PaswordHasher


namespace beltExam.Controllers
{
    public class HomeController : Controller
    {
        /* ************ ADDING THIS ************ */
        private MyContext dbContext; //field
        public HomeController(MyContext context)
        {
            dbContext = context;
        }
        /* *** ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ *** */



        [HttpGet("")]
        public IActionResult Index()
        {
            /* **************************************************************** */
            // Checks if user has a session before they can view this page
            int? sessionId = HttpContext.Session.GetInt32("userId");
            if (sessionId != null)
            {
                return RedirectToAction("dashboard");
            }
            /* **************************************************************** */
            // ViewBag.sessionId = sessionId;
           
            return View();
        }

        

        [HttpPost("register/process")] 
        public IActionResult registerProcess(User newUser)
        {
            if (ModelState.IsValid)
            {
                // Check if user exisits with provided Email
                if (dbContext.Users.Any(u => u.Email == newUser.Email))
                {
                    // Manually add a ModelState error to the Email field
                    ModelState.AddModelError("Email", "The provided email already exists.");
                    return View("Index");
                }
                // Hash the password
                PasswordHasher<User> hasher = new PasswordHasher<User>();
                newUser.Password = hasher.HashPassword(newUser, newUser.Password);

                // add to database
                dbContext.Add(newUser);
                dbContext.SaveChanges();

                // Set up session 
                HttpContext.Session.SetInt32("userId", newUser.UserId);
                HttpContext.Session.SetString("userName", newUser.Name);

                return RedirectToAction("dashboard");
            }
            return View("Index");
        }



        [HttpPost("login/process")]
        public IActionResult loginProcess(Login loginUser)
        {
            // Gets sessionId to Check if user is in session already
            int? sessionId = HttpContext.Session.GetInt32("userId");
            if (sessionId != null)
            {
                return RedirectToAction("dashboard");
            }


            if (ModelState.IsValid)
            {
                User isInDb = dbContext.Users.FirstOrDefault(u => u.Email == loginUser.LoginEmail);
                if (isInDb == null)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid Credentials");
                    return View("Index");
                }

                // verify hashed password
                var hasher = new PasswordHasher<Login>();
                var result = hasher.VerifyHashedPassword(loginUser, isInDb.Password, loginUser.LoginPassword);

                //result will return a boolean
                if (result == 0)
                {
                    ModelState.AddModelError("LoginPassword", "Invalid Credentials");
                    return View("Index");
                }
                // Set up session 
                HttpContext.Session.SetInt32("userId", isInDb.UserId);
                HttpContext.Session.SetString("userName", isInDb.Name);
               
                
                /*  */

                return RedirectToAction("dashboard");
            }
            return View("Index");
        }


        [HttpGet("logout")]
        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }


        [HttpGet("dashboard")]
        public IActionResult dashboard(User user)
        {
            /* **************************************************************** */
            // Checks if user has a session before they can view this page
            int? sessionId = HttpContext.Session.GetInt32("userId");
            if (sessionId == null)
            {
                return RedirectToAction("Index");
            }
            /* **************************************************************** */
            List<FunThing> allActivities = dbContext.FunThings
                .Include(w => w.Participants)
                .ThenInclude(w => w.Attendant)
                .Include(w =>w.FunThingCreator)
                .OrderByDescending(w => w.CreatedAt)
                .ToList();

            // ViewBag.sessionId = sessionId;
            return View(allActivities);
        }




        [HttpGet("FunThingNew")]

        public IActionResult FunThingNew()
        {
            /* **************************************************************** */
            // Checks if user has a session before they can view this page
            int? sessionId = HttpContext.Session.GetInt32("userId");
            if (sessionId == null)
            {
                return RedirectToAction("Index");
            }
            /* **************************************************************** */

            return View();
        }





        [HttpPost("FunThingNewProcess")]
        public IActionResult FunThingNewProcess(FunThing newFunThing)
        {
            int? sessionId = HttpContext.Session.GetInt32("userId");
            if (ModelState.IsValid)
            {
                /* ********Add to database *********** */
                newFunThing.UserId = (int)sessionId;
                dbContext.Add(newFunThing);
                dbContext.SaveChanges();
                // newFunThing.WeddingId is created after dbContext.SaveChanges();
                var newFunThingId = newFunThing.FunThingId;
                /* *********************************** */

                return RedirectToAction("FunThingDetails", new { funId = newFunThingId });
            }
            return View("FunThingNew");
        }

 


        [HttpGet("funThingDetails/{funId}")]
        public IActionResult funThingDetails(int funId)
        {
            /* **************************************************************** */
            // Checks if user has a session before they can view this page
            int? sessionId = HttpContext.Session.GetInt32("userId");
            if (sessionId == null)
            {
                return RedirectToAction("Index");
            }
            /* **************************************************************** */


            Console.WriteLine(funId);
            // query the wedding using the funId parameter
            FunThing CreatedWedding = dbContext.FunThings.Include(w => w.Participants)
            .ThenInclude(w => w.Attendant)
            .FirstOrDefault(w => w.FunThingId == funId);
            Console.WriteLine(CreatedWedding);
            // pass the wedding object to the view to be displayed as @Model
            return View(CreatedWedding);
        }


        [HttpGet("removeFunThing")]
        public IActionResult removeFunThing()
        {
            /* **************************************************************** */
            // Checks if user has a session before they can view this page
            int? sessionId = HttpContext.Session.GetInt32("userId");
            if (sessionId == null)
            {
                return RedirectToAction("Index");
            }
            /* **************************************************************** */
            
            FunThing funthingObj = dbContext.FunThings.FirstOrDefault(u => u.UserId == sessionId);
            dbContext.Remove(funthingObj);
            dbContext.SaveChanges();

            return RedirectToAction("dashboard");

        }

        [HttpPost("rsvpFunThing")]
        public IActionResult rsvpFunThing(bool going, int funId)
        {
         /* **************************************************************** */
            // Checks if user has a session before they can view this page
            int? sessionId = HttpContext.Session.GetInt32("userId");
            if (sessionId == null)
            {
                return RedirectToAction("Index");
            }
            /* **************************************************************** */ 
            //returns found object or null
            //checks if there is a rsvp on a wedding by the user logged in
            Participant hasDecided = dbContext.Participants.Where(r => r.FunThingId == funId).FirstOrDefault(u => u.UserId == sessionId);
            if(hasDecided == null){//if there is not rsvp by user for this wedding, create one
                
                var activityToJoin = dbContext.FunThings.FirstOrDefault(f => f.FunThingId == funId);
                DateTime actToJoinDate = activityToJoin.Date;
                int actToJoinDur = activityToJoin.Duration;

                DateTime actToJoinTime = DateTime.Parse(actToJoinDate.ToString("HH:mm"));
                int actToJoinSpan = actToJoinTime.Hour + actToJoinDur;
                int actToJoinHourStart = actToJoinTime.Hour;


                var currActivities = dbContext.Participants.Include(u => u.FunThing)
                            .FirstOrDefault(u => u.UserId == sessionId && u.FunThing.Date == activityToJoin.Date);
                
                // DateTime actToJoinDate = activityToJoin.Date;
                

                if(currActivities != null)
                {
                    int currActivitiesDur = currActivities.FunThing.Duration;
                    DateTime currActivitiesTime = DateTime.Parse(currActivities.FunThing.Date.ToString("HH:mm"));
                    int currActivitiesHourStart = currActivitiesTime.Hour;
                    int currActivitiesSpan = currActivitiesTime.Hour + currActivitiesDur;


                    if( actToJoinHourStart == currActivitiesHourStart || currActivitiesSpan >= actToJoinHourStart || actToJoinSpan >= currActivitiesHourStart){
                        return RedirectToAction("dashboard");
                    }
                }

                Participant newAttn = new Participant();
                newAttn.FunThingId = funId;
                newAttn.UserId = (int) sessionId;
                newAttn.isGoing = going;
                dbContext.Add(newAttn);
                dbContext.SaveChanges();
                return RedirectToAction("dashboard");
            }
         



            if(hasDecided.isGoing == going)//if clicking on same button again
            {
                dbContext.Remove(hasDecided);
            }
            else{//if clicking on other button
                hasDecided.isGoing = going;
            }
            dbContext.SaveChanges();

            return RedirectToAction("dashboard"); 
        }













        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
