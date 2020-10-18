using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISTE442_presentation.Models
{
    [Table("contact")]
    public class Contact
    {
        public int contactId { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string email { get; set; }
        public string phone_num { get; set; }
    }
}
