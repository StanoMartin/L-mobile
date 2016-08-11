using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMobile2.Models
{
    public class Machine
    {
        [Key]
        [JsonProperty(PropertyName = "machinenNummer")]
        public string MachinenNummer { get; set; }

        [JsonProperty(PropertyName = "bezeichnung")]
        public string Bezeichnung { get; set; }

        public void ApplyChanges(Machine machine)
        {
            //MachinenNummer = machine.MachinenNummer;
            Bezeichnung = machine.Bezeichnung;
        }
    }
}