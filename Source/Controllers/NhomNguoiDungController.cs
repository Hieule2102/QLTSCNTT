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
    public class NhomNguoiDungController : Controller
    {
        private QuanLyTaiSanCNTTEntities2 db = new QuanLyTaiSanCNTTEntities2();

        // GET: /NhomNguoiDung/
        public async Task<ActionResult> Index()
        {
            ModelState.AddModelError("**", "Không được thêm trùng mã nhóm người dùng đã có");
            var nhom_nguoi_dung = db.NHOM_NGUOI_DUNG.Include(n => n.NHOM_ND_CHUCNANG);
            return View(await nhom_nguoi_dung.ToListAsync());
        }

        // GET: /NhomNguoiDung/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHOM_NGUOI_DUNG nhom_nguoi_dung = await db.NHOM_NGUOI_DUNG.FindAsync(id);
            if (nhom_nguoi_dung == null)
            {
                return HttpNotFound();
            }
            return View(nhom_nguoi_dung);
        }

        // GET: /NhomNguoiDung/Create
        public ActionResult Create()
        {
            ViewBag.MA_NHOM = new SelectList(db.NHOM_ND_CHUCNANG, "MA_NHOM", "MA_NHOM");
            return View();
        }

        // POST: /NhomNguoiDung/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MA_NHOM,TEN_NHOM,GHI_CHU")] NHOM_NGUOI_DUNG nhom_nguoi_dung)
        {
            //var nhom_nguoi_dung = new NHOM_NGUOI_DUNG();
            //nhom_nguoi_dung.MA_NHOM = form["MA_NHOM"];
            //nhom_nguoi_dung.TEN_NHOM = form["TEN_NHOM"];
            //nhom_nguoi_dung.GHI_CHU = form["GHI_CHU"];
            if (db.NHOM_NGUOI_DUNG.Count(a => a.MA_NHOM == nhom_nguoi_dung.MA_NHOM) > 0)
            {
                return new HttpStatusCodeResult(404, "Trùng mã nhóm người dùng");
            }

            else if (ModelState.IsValid)
            {
                db.NHOM_NGUOI_DUNG.Add(nhom_nguoi_dung);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // GET: /NhomNguoiDung/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHOM_NGUOI_DUNG nhom_nguoi_dung = await db.NHOM_NGUOI_DUNG.FindAsync(id);
            if (nhom_nguoi_dung == null)
            {
                return HttpNotFound();
            }
            ViewBag.MA_NHOM = new SelectList(db.NHOM_ND_CHUCNANG, "MA_NHOM", "MA_NHOM", nhom_nguoi_dung.MA_NHOM);
            return View(nhom_nguoi_dung);
        }

        // POST: /NhomNguoiDung/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MA_NHOM,TEN_NHOM,GHI_CHU")] NHOM_NGUOI_DUNG nhom_nguoi_dung)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhom_nguoi_dung).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MA_NHOM = new SelectList(db.NHOM_ND_CHUCNANG, "MA_NHOM", "MA_NHOM", nhom_nguoi_dung.MA_NHOM);
            return View(nhom_nguoi_dung);
        }

        // GET: /NhomNguoiDung/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHOM_NGUOI_DUNG nhom_nguoi_dung = await db.NHOM_NGUOI_DUNG.FindAsync(id);
            if (nhom_nguoi_dung == null)
            {
                return HttpNotFound();
            }
            return View(nhom_nguoi_dung);
        }

        // POST: /NhomNguoiDung/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            NHOM_NGUOI_DUNG nhom_nguoi_dung = await db.NHOM_NGUOI_DUNG.FindAsync(id);
            db.NHOM_NGUOI_DUNG.Remove(nhom_nguoi_dung);
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
