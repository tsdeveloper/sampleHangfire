using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HangFire.Mailer.Models
{

    [Table("tbClientesEventos")]
    public class ClienteEvento
    {
       
        [Key]
        [StringLength(6)]
        public string cdEvento { get; set; }

       
        [StringLength(4)]
        public string cdCliente { get; set; }
        public string dsSMTPEmailMensagens { get; set; }
        public string dsSenhaEmailMensagens { get; set; }
        public string dsEmailMensagens { get; set; }
        public string dsEmailEvento { get; set; }

      
    }
}