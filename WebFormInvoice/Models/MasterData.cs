using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFormInvoice.Models
{
    public class MasterData
    {
        public int Number { get; set; }
        public List<Currency> ListOfCurrency { get; set; }
        public List<Language> ListOfLanguage { get; set; }
        public List<UOM> ListOfUOM { get; set; }
        public List<Consumen> ListOfConsumen { get; set; }
    }
}
