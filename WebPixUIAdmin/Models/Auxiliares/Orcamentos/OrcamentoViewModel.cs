using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPixUIAdmin.Models.Auxiliares.Orcamentos
{
    public class OrcamentoViewModel:BaseModel
    {
        public DateTime DataEvento { get; set; }
        public string Local { get; set; }
        public TimeSpan InicioEvento { get; set; }
        public TimeSpan FimEvento { get; set; }
        public string Mensagem { get; set; }
        public string Email { get; set; }
        public InteracaoViewModel Interacoes { get; set; }
        public StatusViewModel estado { get; set; }
    }
}