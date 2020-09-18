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
        /// HP's hero_id
        /// </summary>
        [Column("hero_id")]
        public int HeroId { get; set; }

        /// <summary>
        /// HP's power_id
        /// </summary>
        [Column("power_id")]
        public int PowerId { get; set; }

        /// <summary>
        /// Associated power
        /// </summary>
        [ForeignKey("PowerId")]
        public Power Power { get; set; }
    }
}
