using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieWebApp.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        public ActionResult Index()
        {
            using (MoviesEntities entities = new MoviesEntities())
            {
                var list = entities.Movies.ToList();
                return View(list);

            }
        }

        [HttpGet]
        public ActionResult AddMovie()
        {
            // MovieViewModel movie = new MovieViewModel();

            using (MoviesEntities entities = new MoviesEntities())
            {
                var list = entities.Actors.ToList();
                ViewBag.List = new MultiSelectList(list, "Id", "Name");
                return View();
            }
            //return View();
        }
        [HttpPost]
        public ActionResult AddMovie(String[] Id, FormCollection frm, HttpPostedFileBase file)
        {
            string Picture = null;
            try
            {
                if (file.ContentLength > 0)
                {
                    //extract just the  name.jpeg of pic
                    string picname = Path.GetFileName(file.FileName);

                    //setting physical addrss of the pic file in server machine
                    string ServerPhysicalAddress = Path.Combine(Server.MapPath("~/Content/Images"), picname);

                    //saving the file in server physical address
                    file.SaveAs(ServerPhysicalAddress);

                    Picture = picname;// just a temporary variable storing the picture name

                    //extracting image using physical address
                    Image img = Image.FromFile(ServerPhysicalAddress);

                    //Now doing some operations with height and width for saving the thumbnail-- this is copied bdw 😁
                    int imgHeight = 100;
                    int imgWidth = 100;
                    if (img.Width < img.Height)
                    {
                        //portrait image  
                        imgHeight = 100;
                        var imgRatio = (float)imgHeight / (float)img.Height;
                        imgWidth = Convert.ToInt32(img.Height * imgRatio);
                    }
                    else if (img.Height < img.Width)
                    {
                        //landscape image  
                        imgWidth = 100;
                        var imgRatio = (float)imgWidth / (float)img.Width;
                        imgHeight = Convert.ToInt32(img.Height * imgRatio);
                    }
                    //-- next line will extract thumbnail out of the image "img"
                    Image thumb = img.GetThumbnailImage(imgWidth, imgHeight, () => false, IntPtr.Zero);

                    //-- Saving thumb to the mentioned path folder with the same name as that of original pic"
                    thumb.Save(Path.Combine(Server.MapPath("~/Content/Thumbnails"), picname));
                }
            }
            catch
            {
                ViewBag.file_error = "can't get the file!";
            }

            String Cast = null;
            for(int i = 0; i < Id.Length; i++)
            {
                using (MoviesEntities entities = new MoviesEntities())
                {
                    int id = Int32.Parse(Id[i]);
                    Cast += entities.Actors.FirstOrDefault(x => x.Id == id).Name;
                    if(i!=Id.Length-1)
                        Cast += ", ";
                }
            }
            


            Movie movie = new Movie();

            movie.Name = frm["Name"];
            movie.ReleaseYear = Int32.Parse(frm["Year"]);
            movie.Cast = Cast;
            movie.Plot = frm["Plot"];
            movie.Poster = Picture; 

            using (MoviesEntities entities = new MoviesEntities())
            {
                entities.Movies.Add(movie);
                entities.SaveChanges();

            }

            long MovieId;
            using (MoviesEntities entities = new MoviesEntities())
            {
                String MovieName = frm["Name"];
                MovieId = entities.Movies.FirstOrDefault(x => x.Name == MovieName).Id;
            }
            for (int i = 0; i < Id.Length; i++)
            {
                using (MoviesEntities entities = new MoviesEntities())
                {
                    int id = Int32.Parse(Id[i]);
                    MovieActor_Map ma = new MovieActor_Map();

                    ma.ActorId = id;
                    ma.MovieId = MovieId;
                    
                    entities.MovieActor_Map.Add(ma);
                    entities.SaveChanges();
                }
            }

            // ViewBag.Added = " New Movie Has been Added ";

            return RedirectToAction("Index");
        }
    }
    public class MovieViewModel
    {
        public Actor actor { get; set; }
        public Movie movie { get; set; }
    }
}