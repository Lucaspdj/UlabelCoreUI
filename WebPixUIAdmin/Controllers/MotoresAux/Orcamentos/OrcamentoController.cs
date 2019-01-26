using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebPixUIAdmin.Models.Auxiliares.Orcamentos;
using WebPixUIAdmin.PixCore;

namespace WebPixUIAdmin.Controllers.MotoresAux.Orcamentos
{
    public class OrcamentoController : Controller
    {
        private int IDCliente = PixCore.PixCoreValues.IDCliente;

        // GET: Orcamento
        public ActionResult Index()
        {

            var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();
            var url = keyUrl + "Seguranca/WpOrcamento/BuscarOrcamento/" + IDCliente + "/" + PixCoreValues.UsuarioLogado.IdUsuario;
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            OrcamentoViewModel[] Orcamento = jss.Deserialize<OrcamentoViewModel[]>(result);

            var OrcamentoFiltrado = Orcamento.Where(x => x.idCliente == IDCliente).ToList();

            return View(OrcamentoFiltrado);
        }

        // GET: Orcamento/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();
            var url = keyUrl + "Seguranca/WpOrcamento/BuscarOrcamento/" + IDCliente + "/" + PixCoreValues.UsuarioLogado.IdUsuario;
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            OrcamentoViewModel[] OrcamentoViewModel = jss.Deserialize<OrcamentoViewModel[]>(result);

            if (OrcamentoViewModel == null)
            {
                return HttpNotFound();
            }
            return View(OrcamentoViewModel.Where(z => z.ID == id).FirstOrDefault());

        }

        // GET: Orcamento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orcamento/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DataEvento,Local,InicioEvento,FimEvento,Mensagem,Email,Interacoes,estado,Nome,Descricao,DataCriacao,DateAlteracao,UsuarioCriacao,UsuarioEdicao,Ativo,Status,idCliente")] OrcamentoViewModel OrcamentoViewModel)
        {
            if (ModelState.IsValid)
            {
                OrcamentoViewModel.DataCriacao = DateTime.Now;
                OrcamentoViewModel.DateAlteracao = DateTime.Now;
                OrcamentoViewModel.idCliente = IDCliente;
                OrcamentoViewModel.UsuarioCriacao = PixCoreValues.UsuarioLogado.IdUsuario;
                OrcamentoViewModel.UsuarioEdicao = PixCoreValues.UsuarioLogado.IdUsuario;

                using (var client = new WebClient())
                {
                    var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();
                    var url = keyUrl + "Seguranca/WpOrcamento/SalvarOrcamento/" + IDCliente + "/" + PixCoreValues.UsuarioLogado.IdUsuario;
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    var Envio = new { Orcamento = OrcamentoViewModel };
                    var data = JsonConvert.SerializeObject(Envio);  // jss.Serialize(Envio);
                    var result = client.UploadString(url, "POST", data);
                }
                return RedirectToAction("Index");
            }

            return View(OrcamentoViewModel);
        }

        // GET: Orcamento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();
            var url = keyUrl + "Seguranca/WpOrcamento/BuscarOrcamento/" + IDCliente + "/" + PixCoreValues.UsuarioLogado.IdUsuario;
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            OrcamentoViewModel[] OrcamentoViewModel = jss.Deserialize<OrcamentoViewModel[]>(result);

            if (OrcamentoViewModel == null)
            {
                return HttpNotFound();
            }
            return View(OrcamentoViewModel.Where(z => z.ID == id).FirstOrDefault());
        }

        // POST: Orcamento/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DataEvento,Local,InicioEvento,FimEvento,Mensagem,Email,Interacoes,estado,Nome,Descricao,DataCriacao,DateAlteracao,UsuarioCriacao,UsuarioEdicao,Ativo,Status,idCliente")] OrcamentoViewModel OrcamentoViewModel)
        {
            if (ModelState.IsValid)
            {
                OrcamentoViewModel.DataCriacao = DateTime.Now;
                OrcamentoViewModel.DateAlteracao = DateTime.Now;
                OrcamentoViewModel.idCliente = IDCliente;
                OrcamentoViewModel.UsuarioCriacao = PixCoreValues.UsuarioLogado.IdUsuario;
                OrcamentoViewModel.UsuarioEdicao = PixCoreValues.UsuarioLogado.IdUsuario;

                using (var client = new WebClient())
                {
                    var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();
                    var url = keyUrl + "Seguranca/WpOrcamento/SalvarOrcamento/" + IDCliente + "/" + PixCoreValues.UsuarioLogado.IdUsuario;
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    var Envio = new { Orcamento = OrcamentoViewModel };
                    var data = jss.Serialize(Envio);
                    var result = client.UploadString(url, "POST", data);
                }
                return RedirectToAction("Index");
            }

            return View(OrcamentoViewModel);
        }

        // GET: Orcamento/Delete/5
        public ActionResult Delete(int? id)
        {

            return View();
        }

        // POST: Orcamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //OrcamentoViewModel OrcamentoViewModel = db.OrcamentoViewModels.Find(id);
            //db.OrcamentoViewModels.Remove(OrcamentoViewModel);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                /// db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
