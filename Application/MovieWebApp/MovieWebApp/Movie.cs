//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MovieWebApp
{
    using System;
    using System.Collections.Generic;
  

    public partial class Movie
    {
        public Movie()
        {
            this.MovieActor_Map = new HashSet<MovieActor_Map>();
            
        }
    
        public long Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> ReleaseYear { get; set; }
        public string Cast { get; set; }
        public string Plot { get; set; }
        public string Poster { get; set; }
    
        public virtual ICollection<MovieActor_Map> MovieActor_Map { get; set; }
       
    }
}
