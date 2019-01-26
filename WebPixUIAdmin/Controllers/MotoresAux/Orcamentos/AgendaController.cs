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
    public class AgendaController : Controller
    {
        private int IDCliente = PixCore.PixCoreValues.IDCliente;

        // GET: Agenda
        public ActionResult Index()
        {

            var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();
            var url = keyUrl + "Seguranca/WpOrcamento/BuscarAgenda/" + IDCliente + "/" + PixCoreValues.UsuarioLogado.IdUsuario;
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            AgendaViewModel[] Agenda = jss.Deserialize<AgendaViewModel[]>(result);

            var AgendaFiltrado = Agenda.Where(x => x.idCliente == IDCliente).ToList();

            return View(AgendaFiltrado);
        }

        // GET: Agenda/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();
            var url = keyUrl + "Seguranca/WpOrcamento/BuscarAgenda/" + IDCliente + "/" + PixCoreValues.UsuarioLogado.IdUsuario;
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            AgendaViewModel[] AgendaViewModel = jss.Deserialize<AgendaViewModel[]>(result);

            if (AgendaViewModel == null)
            {
                return HttpNotFound();
            }
            return View(AgendaViewModel.Where(z => z.ID == id).FirstOrDefault());

        }

        // GET: Agenda/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agenda/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Orcamento,Nome,Descricao,DataCriacao,DateAlteracao,UsuarioCriacao,UsuarioEdicao,Ativo,Status,idCliente")] AgendaViewModel AgendaViewModel)
        {
            if (ModelState.IsValid)
            {
                AgendaViewModel.DataCriacao = DateTime.Now;
                AgendaViewModel.DateAlteracao = DateTime.Now;
                AgendaViewModel.idCliente = IDCliente;
                AgendaViewModel.UsuarioCriacao = PixCoreValues.UsuarioLogado.IdUsuario;
                AgendaViewModel.UsuarioEdicao = PixCoreValues.UsuarioLogado.IdUsuario;

                using (var client = new WebClient())
                {
                    var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();
                    var url = keyUrl + "Seguranca/WpOrcamento/SalvarAgenda/" + IDCliente + "/" + PixCoreValues.UsuarioLogado.IdUsuario;
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    var Envio = new { agenda = AgendaViewModel };
                    var data = JsonConvert.SerializeObject(Envio);  // jss.Serialize(Envio);
                    var result = client.UploadString(url, "POST", data);
                }
                return RedirectToAction("Index");
            }

            return View(AgendaViewModel);
        }

        // GET: Agenda/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();
            var url = keyUrl + "Seguranca/WpOrcamento/BuscarAgenda/" + IDCliente + "/" + PixCoreValues.UsuarioLogado.IdUsuario;
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            AgendaViewModel[] AgendaViewModel = jss.Deserialize<AgendaViewModel[]>(result);

            if (AgendaViewModel == null)
            {
                return HttpNotFound();
            }
            return View(AgendaViewModel.Where(z => z.ID == id).FirstOrDefault());
        }

        // POST: Agenda/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Orcamento,Nome,Descricao,DataCriacao,DateAlteracao,UsuarioCriacao,UsuarioEdicao,Ativo,Status,idCliente")] AgendaViewModel AgendaViewModel)
        {
            if (ModelState.IsValid)
            {
                AgendaViewModel.DataCriacao = DateTime.Now;
                AgendaViewModel.DateAlteracao = DateTime.Now;
                AgendaViewModel.idCliente = IDCliente;
                AgendaViewModel.UsuarioCriacao = PixCoreValues.UsuarioLogado.IdUsuario;
                AgendaViewModel.UsuarioEdicao = PixCoreValues.UsuarioLogado.IdUsuario;

                using (var client = new WebClient())
                {
                    var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();
                    var url = keyUrl + "Seguranca/WpOrcamento/SalvarAgenda/" + IDCliente + "/" + PixCoreValues.UsuarioLogado.IdUsuario;
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    var Envio = new { agenda = AgendaViewModel };
                    var data = jss.Serialize(Envio);
                    var result = client.UploadString(url, "POST", data);
                }
                return RedirectToAction("Index");
            }

            return View(AgendaViewModel);
        }

        // GET: Agenda/Delete/5
        public ActionResult Delete(int? id)
        {

            return View();
        }

        // POST: Agenda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //AgendaViewModel AgendaViewModel = db.AgendaViewModels.Find(id);
            //db.AgendaViewModels.Remove(AgendaViewModel);
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
