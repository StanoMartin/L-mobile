using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMobile2.Models
{
    public class ServiceAuftrag
    {
        [Key]
        [JsonProperty(PropertyName = "auftragsNummer")]
        public string AuftragsNummer { get; set; }

        [JsonProperty(PropertyName = "auftragsArt")]
        public string AuftragsArt { get; set; }

        [ForeignKey("Kunde")]
        [JsonProperty(PropertyName = "kundenNummer")]
        public string KundenNummer { get; set; }

        [ForeignKey("Machine")]
        [JsonProperty(PropertyName = "machinenNummer")]
        public string MachinenNummer { get; set; }

        public virtual Kunde Kunde { get; set; }
        public virtual Machine Machine { get; set; }

        public virtual ICollection<ArbeitsZeitMeldung> ArbeitsZeitMeldungen { get; set; }

        public void ApplyChanges(ServiceAuftrag auftrag)
        {
            AuftragsNummer = auftrag.AuftragsNummer;
            AuftragsArt = auftrag.AuftragsArt;
            KundenNummer = auftrag.KundenNummer;
            MachinenNummer = auftrag.MachinenNummer;
        }
    }
}
