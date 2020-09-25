using BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Extensions
{
    public static class HeroDetailedExtension
    {
        public static string GetDescription(this HeroDetailedDTO dto)
        {
            var description = dto.Name;
            if (dto.City == null)
            {
                description += " doesn't live anywhere and";
            }
            else if (dto.City.Name == null)
            {
                description += " lives somewhere unknown and";
            }
            else
            {
                description += $" lives in {dto.City.Name} and";
            }
            if (dto.Powers.Count == 0)
            {
                description += " has no powers";
            }
            else
            {
                if (dto.Powers.Count == 1)
                    description += " has this power :";
                else
                    description += " has these powers :";
                foreach (PowerDTO power in dto.Powers)
                {
                    description += $" {power.Name},";
                }
                description = description.Remove(description.Length - 1);
            }
            
            return description;
        }
    }
}
