using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Source.Models;

namespace Source.Controllers
{
    public class DonViController : Controller
    {
        private QuanLyTaiSanCNTTEntities2 db = new QuanLyTaiSanCNTTEntities2();

        // GET: /DonVi/
        public async Task<ActionResult> Index()
        {
            return View(await db.DON_VI.ToListAsync());
        }

        // GET: /DonVi/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DON_VI don_vi = await db.DON_VI.FindAsync(id);
            if (don_vi == null)
            {
                return HttpNotFound();
            }
            return View(don_vi);
        }

        // GET: /DonVi/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /DonVi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MA_DON_VI,TEN_DON_VI,DIA_CHI,DIEN_THOAI,FAX")] DON_VI don_vi)
        {
            //var don_vi = new DON_VI();
            //don_vi.TEN_DON_VI = form["TEN_DON_VI"];
            //don_vi.DIA_CHI = form["DIA_CHI"];
            //don_vi.DIEN_THOAI = form["DIEN_THOAI"];
            //don_vi.FAX = form["FAX"];
            if (ModelState.IsValid)
            {
                db.DON_VI.Add(don_vi);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
            //return View(don_vi);
        }

        // GET: /DonVi/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DON_VI don_vi = await db.DON_VI.FindAsync(id);
            if (don_vi == null)
            {
                return HttpNotFound();
            }
            return View(don_vi);
        }

        // POST: /DonVi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MA_DON_VI,TEN_DON_VI,DIA_CHI,DIEN_THOAI,FAX")] DON_VI don_vi)
        {

            if (ModelState.IsValid)
            {
                db.Entry(don_vi).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(don_vi);
        }

        // GET: /DonVi/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DON_VI don_vi = await db.DON_VI.FindAsync(id);
            if (don_vi == null)
            {
                return HttpNotFound();
            }
            return View(don_vi);
        }

        // POST: /DonVi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DON_VI don_vi = await db.DON_VI.FindAsync(id);
            db.DON_VI.Remove(don_vi);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
