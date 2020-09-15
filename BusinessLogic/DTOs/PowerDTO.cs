using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.DTOs
{
    public class PowerDTO
    {
        public int Id { get; set; }

        public int HeroID { get; set; }

        public virtual HeroPower Hero { get; set; }

        public int PowerId { get; set; }

    }
}
