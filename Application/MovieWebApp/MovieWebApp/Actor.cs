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
    using System.Web.Mvc;

    public partial class Actor
    {
        public Actor()
        {
            this.MovieActor_Map = new HashSet<MovieActor_Map>();
           
        }
    
        public long Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string Bio { get; set; }
    
        public virtual ICollection<MovieActor_Map> MovieActor_Map { get; set; }
        
    }
}