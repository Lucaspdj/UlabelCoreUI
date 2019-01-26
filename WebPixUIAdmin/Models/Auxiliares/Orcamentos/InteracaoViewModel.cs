using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPixUIAdmin.Models.Auxiliares.Orcamentos
{
    public class InteracaoViewModel : BaseModel
    {
        public int Orcamento { get; set; }
        public string Assunto { get; set; }
        public string Mensagem { get; set; }
    }
}