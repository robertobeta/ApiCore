using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ApiCore.ViewModels
{
    public class MensajesViewModel
    {
        [JsonProperty("mensaje")]
        public string mensaje { get; set; }
        [JsonProperty("tipo")]
        public int tipo { get; set; }
    }
}
