using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMobile2.Models
{
    public class ArbeitsZeitMeldung
    {
        [Key]
        [JsonProperty(PropertyName = "artikelNummer")]
        public string ArtikelNummer { get; set; }

        [JsonProperty(PropertyName = "anfangDatum")]
        public DateTime AnfangDatum { get; set; }

        [JsonProperty(PropertyName = "anfangUhrzeit")]
        public DateTime AnfangUhrZeit { get; set; }

        [JsonProperty(PropertyName = "endeDatum")]
        public DateTime EndeDatum { get; set; }

        [JsonProperty(PropertyName = "endeUhrzeit")]
        public DateTime EndeUhrZeit { get; set; }
        
        [ForeignKey("ServiceAuftrag")]
        public string ServiceAuftragNummer { get; set; }
        
        public virtual ServiceAuftrag ServiceAuftrag { get; set; }

        public void ApplyChanges(ArbeitsZeitMeldung azm)
        {
            //ArtikelNummer = azm.ArtikelNummer;
            AnfangDatum = azm.AnfangDatum;
            AnfangUhrZeit = azm.AnfangUhrZeit;
            EndeDatum = azm.EndeDatum;
            EndeUhrZeit = azm.EndeUhrZeit;
            ServiceAuftragNummer = azm.ServiceAuftragNummer;
        }
    }

    public class ArbeitsZeitMeldungComparer : IEqualityComparer<ArbeitsZeitMeldung>
    {
        public bool Equals(ArbeitsZeitMeldung x, ArbeitsZeitMeldung y)
        {
            return x.ArtikelNummer.Equals(y.ArtikelNummer, StringComparison.Ordinal);
        }

        public int GetHashCode(ArbeitsZeitMeldung obj)
        {
            return obj.ArtikelNummer.GetHashCode();
        }
    }
}