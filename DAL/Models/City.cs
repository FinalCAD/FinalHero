using DAL.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    [Table("cities")]
    public class City :BaseEntity
    {

        /// <summary>
        /// name of the city
        /// </summary>
        [Column("name")]
        [Required,MaxLength(100)]
        public string Name { get; set; }


        /// <summary>
        /// list of all the hero in a city
        /// </summary>
        public virtual ICollection<Hero> Heroes { get; set; }
    }
}
