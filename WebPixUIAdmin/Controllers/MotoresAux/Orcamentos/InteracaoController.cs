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
    public class InteracaoController : Controller
    {
        private int IDCliente = PixCore.PixCoreValues.IDCliente;

        // GET: Interacao
        public ActionResult Index()
        {

            var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();
            var url = keyUrl + "Seguranca/WpOrcamento/BuscarInteracao/" + IDCliente + "/" + PixCoreValues.UsuarioLogado.IdUsuario;
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            InteracaoViewModel[] Interacao = jss.Deserialize<InteracaoViewModel[]>(result);

            var InteracaoFiltrado = Interacao.Where(x => x.idCliente == IDCliente).ToList();

            return View(InteracaoFiltrado);
        }

        // GET: Interacao/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();
            var url = keyUrl + "Seguranca/WpOrcamento/BuscarInteracao/" + IDCliente + "/" + PixCoreValues.UsuarioLogado.IdUsuario;
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            InteracaoViewModel[] InteracaoViewModel = jss.Deserialize<InteracaoViewModel[]>(result);

            if (InteracaoViewModel == null)
            {
                return HttpNotFound();
            }
            return View(InteracaoViewModel.Where(z => z.ID == id).FirstOrDefault());

        }

        // GET: Interacao/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Interacao/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Orcamento,Assunto,Mensagem,Nome,Descricao,DataCriacao,DateAlteracao,UsuarioCriacao,UsuarioEdicao,Ativo,Status,idCliente")] InteracaoViewModel InteracaoViewModel)
        {
            if (ModelState.IsValid)
            {
                InteracaoViewModel.DataCriacao = DateTime.Now;
                InteracaoViewModel.DateAlteracao = DateTime.Now;
                InteracaoViewModel.idCliente = IDCliente;
                InteracaoViewModel.UsuarioCriacao = PixCoreValues.UsuarioLogado.IdUsuario;
                InteracaoViewModel.UsuarioEdicao = PixCoreValues.UsuarioLogado.IdUsuario;

                using (var client = new WebClient())
                {
                    var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();
                    var url = keyUrl + "Seguranca/WpOrcamento/SalvarInteracao/" + IDCliente + "/" + PixCoreValues.UsuarioLogado.IdUsuario;
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    var Envio = new { Interacao = InteracaoViewModel };
                    var data = JsonConvert.SerializeObject(Envio);  // jss.Serialize(Envio);
                    var result = client.UploadString(url, "POST", data);
                }
                return RedirectToAction("Index");
            }

            return View(InteracaoViewModel);
        }

        // GET: Interacao/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();
            var url = keyUrl + "Seguranca/WpOrcamento/BuscarInteracao/" + IDCliente + "/" + PixCoreValues.UsuarioLogado.IdUsuario;
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            InteracaoViewModel[] InteracaoViewModel = jss.Deserialize<InteracaoViewModel[]>(result);

            if (InteracaoViewModel == null)
            {
                return HttpNotFound();
            }
            return View(InteracaoViewModel.Where(z => z.ID == id).FirstOrDefault());
        }

        // POST: Interacao/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Orcamento,Assunto,Mensagem,Nome,Descricao,DataCriacao,DateAlteracao,UsuarioCriacao,UsuarioEdicao,Ativo,Status,idCliente")] InteracaoViewModel InteracaoViewModel)
        {
            if (ModelState.IsValid)
            {
                InteracaoViewModel.DataCriacao = DateTime.Now;
                InteracaoViewModel.DateAlteracao = DateTime.Now;
                InteracaoViewModel.idCliente = IDCliente;
                InteracaoViewModel.UsuarioCriacao = PixCoreValues.UsuarioLogado.IdUsuario;
                InteracaoViewModel.UsuarioEdicao = PixCoreValues.UsuarioLogado.IdUsuario;

                using (var client = new WebClient())
                {
                    var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();
                    var url = keyUrl + "Seguranca/WpOrcamento/SalvarInteracao/" + IDCliente + "/" + PixCoreValues.UsuarioLogado.IdUsuario;
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    var Envio = new { Interacao = InteracaoViewModel };
                    var data = jss.Serialize(Envio);
                    var result = client.UploadString(url, "POST", data);
                }
                return RedirectToAction("Index");
            }

            return View(InteracaoViewModel);
        }

        // GET: Interacao/Delete/5
        public ActionResult Delete(int? id)
        {

            return View();
        }

        // POST: Interacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //InteracaoViewModel InteracaoViewModel = db.InteracaoViewModels.Find(id);
            //db.InteracaoViewModels.Remove(InteracaoViewModel);
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
