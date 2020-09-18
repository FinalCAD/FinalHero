using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Models.Interfaces;

namespace DAL.Models
{
    public abstract class BaseEntity : IBaseEntity
    {
        /// <summary>
        /// Entity's id
        /// </summary>
        [Column("id")]
        public int Id { get; set; }
    }
}
