using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    [Table("powers")]
    public class Power : BaseEntity
    {


        /// <summary>
        /// 
        /// </summary>
        [Column("name")]
        [Required, MaxLength(40)]
        public string Name { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Column("description")]
        [MaxLength(255)]
        public string Description { get; set; }


        /// <summary>
        /// List of all hero that have a power
        /// </summary>
        public virtual ICollection<HeroPower> HeroPowers { get; set; }
    }
}
