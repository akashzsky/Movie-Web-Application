using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieWebApp.Controllers
{
    public class ActorController : Controller
    {
        // GET: Actor
        public ActionResult Index()
        {
            using (MoviesEntities entities = new MoviesEntities())
            {
                var list = entities.Actors.ToList();
                return View(list);

            }
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Actor actor)
        {
            using (MoviesEntities entities = new MoviesEntities())
            {
                entities.Actors.Add(actor);
                entities.SaveChanges();

            }
            ViewBag.Added = " New Actor Has been Added ";
            return View();
        }
        
        public ActionResult Details(string id)
        {
            using (MoviesEntities entities = new MoviesEntities())
            {
                int val = Int32.Parse(id);
                var user = entities.Actors.FirstOrDefault(x => x.Id == val);
                return View(user);
            }
        }
        [HttpGet]
        public ActionResult EditActor(String  id)
        {
            using (MoviesEntities entities = new MoviesEntities())
            {
                var Id = Int32.Parse(id);
                var user = entities.Actors.FirstOrDefault(x => x.Id == Id);
                return View(user);
            }
        
        }
        [HttpPost]
        public ActionResult EditActor(Actor actor)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("EditSave", new { newactor = actor });
            }
            return View(actor);
        }
       
        public ActionResult EditSave(Actor actor)
        {
            using (MoviesEntities entities = new MoviesEntities())
            {
                var user = entities.Actors.FirstOrDefault(x=> x.Id == actor.Id);
                if(user != null)
                {
                    user.Name = actor.Name;
                    user.DOB = actor.DOB;
                    user.Bio = actor.Bio;
                    user.Sex = actor.Sex;
                    entities.SaveChanges();
                }
                

            }
            return RedirectToAction("Details", new { id = actor.Id.ToString() });
        }


    }
}