using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BusinessLogic.DTOs.Responses
{
    
    public class DifferentialDTO<T>
    {
        [JsonPropertyName("entities")]
        public virtual List<T> Entities { get; set; }

        [JsonPropertyName("meta")]
        public ListMetaData Meta { get; set; }
    }




    public class ListMetaData
    {
        /// <summary>
        /// offset of the list
        /// </summary>
        [JsonPropertyName("count")]
        public int Count { get; set; }


        /// <summary>
        /// length of the possible data
        /// </summary>
        [JsonPropertyName("total_count")]
        public int TotalCount { get; set; }
    }
}
