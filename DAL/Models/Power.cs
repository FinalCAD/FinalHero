using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;

namespace DAL.Models
{
    [Table("powers")]
    public class Power : BaseEntity
    {
        /// <summary>
        /// Power's name
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Power's description
        /// </summary>
        [Column("description")]
        public string Description { get; set; }
    }
}
