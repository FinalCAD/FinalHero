using DAL.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    [Table("heroes_powers")]
    public class HeroPower : BaseEntity
    {

        /// <summary>
        /// 
        /// </summary>
        [Column("hero_id")]
        public int HeroId { get; set; }

        public virtual Hero Hero { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Column("power_id")]
        public int PowerId { get; set; }

        public virtual Power Power { get; set; }


    }
}
