using DAL.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    public class BaseEntity:IBaseEntity
    {
        [Column("id")]
        public int Id { get; set; }
   
    }
}
