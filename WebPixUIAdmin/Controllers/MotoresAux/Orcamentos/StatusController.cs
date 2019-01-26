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

namespace WebPixUIAdmin.Controllers.MotoresAux.Statuss
{
    public class StatusController : Controller
    {
        private int IDCliente = PixCore.PixCoreValues.IDCliente;

        // GET: Status
        public ActionResult Index()
        {

            var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();
            var url = keyUrl + "Seguranca/WpStatus/BuscarStatus/" + IDCliente + "/" + PixCoreValues.UsuarioLogado.IdUsuario;
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            StatusViewModel[] Status = jss.Deserialize<StatusViewModel[]>(result);

            var StatusFiltrado = Status.Where(x => x.idCliente == IDCliente).ToList();

            return View(StatusFiltrado);
        }

        // GET: Status/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();
            var url = keyUrl + "Seguranca/WpStatus/BuscarStatus/" + IDCliente + "/" + PixCoreValues.UsuarioLogado.IdUsuario;
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            StatusViewModel[] StatusViewModel = jss.Deserialize<StatusViewModel[]>(result);

            if (StatusViewModel == null)
            {
                return HttpNotFound();
            }
            return View(StatusViewModel.Where(z => z.ID == id).FirstOrDefault());

        }

        // GET: Status/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Status/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nome,Descricao,DataCriacao,DateAlteracao,UsuarioCriacao,UsuarioEdicao,Ativo,Status,idCliente")] StatusViewModel StatusViewModel)
        {
            if (ModelState.IsValid)
            {
                StatusViewModel.DataCriacao = DateTime.Now;
                StatusViewModel.DateAlteracao = DateTime.Now;
                StatusViewModel.idCliente = IDCliente;
                StatusViewModel.UsuarioCriacao = PixCoreValues.UsuarioLogado.IdUsuario;
                StatusViewModel.UsuarioEdicao = PixCoreValues.UsuarioLogado.IdUsuario;

                using (var client = new WebClient())
                {
                    var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();
                    var url = keyUrl + "Seguranca/WpStatus/SalvarStatus/" + IDCliente + "/" + PixCoreValues.UsuarioLogado.IdUsuario;
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    var Envio = new { Status = StatusViewModel };
                    var data = JsonConvert.SerializeObject(Envio);  // jss.Serialize(Envio);
                    var result = client.UploadString(url, "POST", data);
                }
                return RedirectToAction("Index");
            }

            return View(StatusViewModel);
        }

        // GET: Status/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();
            var url = keyUrl + "Seguranca/WpStatus/BuscarStatus/" + IDCliente + "/" + PixCoreValues.UsuarioLogado.IdUsuario;
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(url));
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            StatusViewModel[] StatusViewModel = jss.Deserialize<StatusViewModel[]>(result);

            if (StatusViewModel == null)
            {
                return HttpNotFound();
            }
            return View(StatusViewModel.Where(z => z.ID == id).FirstOrDefault());
        }

        // POST: Status/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Descricao,DataCriacao,DateAlteracao,UsuarioCriacao,UsuarioEdicao,Ativo,Status,idCliente")] StatusViewModel StatusViewModel)
        {
            if (ModelState.IsValid)
            {
                StatusViewModel.DataCriacao = DateTime.Now;
                StatusViewModel.DateAlteracao = DateTime.Now;
                StatusViewModel.idCliente = IDCliente;
                StatusViewModel.UsuarioCriacao = PixCoreValues.UsuarioLogado.IdUsuario;
                StatusViewModel.UsuarioEdicao = PixCoreValues.UsuarioLogado.IdUsuario;

                using (var client = new WebClient())
                {
                    var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();
                    var url = keyUrl + "Seguranca/WpStatus/SalvarStatus/" + IDCliente + "/" + PixCoreValues.UsuarioLogado.IdUsuario;
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    var Envio = new { Status = StatusViewModel };
                    var data = jss.Serialize(Envio);
                    var result = client.UploadString(url, "POST", data);
                }
                return RedirectToAction("Index");
            }

            return View(StatusViewModel);
        }

        // GET: Status/Delete/5
        public ActionResult Delete(int? id)
        {

            return View();
        }

        // POST: Status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //StatusViewModel StatusViewModel = db.StatusViewModels.Find(id);
            //db.StatusViewModels.Remove(StatusViewModel);
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
