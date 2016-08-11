using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMobile2.Models
{
    public class Kunde
    {
        [Key]
        [JsonProperty(PropertyName = "kundenNummer")]
        public string KundenNummer { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        public void ApplyChanges(Kunde kunde)
        {
            //KundenNummer = kunde.KundenNummer;
            Name = kunde.Name;
        }
    }
}