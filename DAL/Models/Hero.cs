using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    [Table("heroes")]
    public class Hero : BaseEntity
    {

        /// <summary>
        /// 
        /// </summary>
        [Column("id")]
        [Required, MaxLength(40)]
        public int Id { get; set; }


        [Column("name")]
        [Required, MaxLength(40)]
        public string Name { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Column("city_id")]
        public int CityId { get; set; }

        public virtual City City { get; set; }

        public virtual List<HeroPower> HeroPowers { get; set; }
    }
}
