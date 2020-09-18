using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    [Table("heroes")]
    public class Hero : BaseEntity
    {
        /// <summary>
        /// Hero's name
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// The hero's city id
        /// </summary>
        [Column("city_id")]       
        public int? CityId { get; set; }

        /// <summary>
        /// Associated city
        /// </summary>
        [ForeignKey("CityId")]
        public City City { get; set; }


    }
}
