using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    [Table("cities")]
    public class City : BaseEntity
    {
        /// <summary>
        /// City's name
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Associated heroes
        /// </summary>
        public virtual List<Hero> Heroes { get; set; }
    }
}
