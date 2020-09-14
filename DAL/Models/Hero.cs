using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    [Table("heroes")]
    public class Hero:BaseEntity
    {

        /// <summary>
        /// 
        /// </summary>
        [Column("name")]
        [Required,MaxLength(40)]
        public string name { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Column("city_id")]
        public int CityId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("CityId")]
        public City City { get; set; }
    }
}
