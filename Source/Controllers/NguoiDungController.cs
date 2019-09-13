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
    public class NguoiDungController : Controller
    {
        private QuanLyTaiSanCNTTEntities2 db = new QuanLyTaiSanCNTTEntities2();

        // GET: /NguoiDung/
        public async Task<ActionResult> Index(string tenNhom, string donVi)
        {
            //Đơn vị
            var dsTenDonVi = new List<string>();
            var qTenDonVi = (from d in db.DON_VI
                             orderby d.TEN_DON_VI
                             select d.TEN_DON_VI);

            dsTenDonVi.AddRange(qTenDonVi.Distinct());
            ViewBag.donVi = new SelectList(dsTenDonVi);

            //Nhóm người dùng
            var dsTenNhom = new List<string>();
            var qTenNhom = (from d in db.NHOM_NGUOI_DUNG
                            orderby d.TEN_NHOM
                            select d.TEN_NHOM);

            dsTenNhom.AddRange(qTenNhom.Distinct());
            ViewBag.tenNhom = new SelectList(dsTenNhom);

            var nguoi_dung = db.NGUOI_DUNG.Include(n => n.DON_VI);

            //Tìm đơn vị và nhóm người dùng
            if (!String.IsNullOrEmpty(donVi) && !String.IsNullOrEmpty(tenNhom))
            {
                var nhom_ND = new List<NGUOI_DUNG>();
                foreach (var a in db.NHOM_ND)
                {
                    if (a.NHOM_NGUOI_DUNG.TEN_NHOM == tenNhom)
                    {
                        nhom_ND.Add(a.NGUOI_DUNG);
                    }
                }
                nhom_ND = nhom_ND.Where(data => data.DON_VI.TEN_DON_VI == donVi).ToList();
                return View(nhom_ND);
            }
            else if (!String.IsNullOrEmpty(donVi))
            {
                nguoi_dung = nguoi_dung.Where(data => data.DON_VI.TEN_DON_VI == donVi);
            }
            else if (!String.IsNullOrEmpty(tenNhom))
            {
                var nhom_ND = new List<NGUOI_DUNG>();
                foreach (var a in db.NHOM_ND)
                {
                    if (a.NHOM_NGUOI_DUNG.TEN_NHOM == tenNhom)
                    {
                        nhom_ND.Add(a.NGUOI_DUNG);
                    }
                }
                return View(nhom_ND.ToList());
            }
            return View(await nguoi_dung.ToListAsync());
        }

        // GET: /NguoiDung/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGUOI_DUNG nguoi_dung = await db.NGUOI_DUNG.FindAsync(id);
            if (nguoi_dung == null)
            {
                return HttpNotFound();
            }
            return View(nguoi_dung);
        }

        // GET: /NguoiDung/Create
        public ActionResult Create()
        {
            ViewBag.MA_DON_VI = new SelectList(db.DON_VI, "MA_DON_VI", "TEN_DON_VI");
            return View();
        }

        // POST: /NguoiDung/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MA_ND,TEN_ND,MA_DON_VI,EMAIL,TEN_DANG_NHAP,MAT_KHAU")] NGUOI_DUNG nguoi_dung)
        {
            if (ModelState.IsValid)
            {
                db.NGUOI_DUNG.Add(nguoi_dung);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MA_DON_VI = new SelectList(db.DON_VI, "MA_DON_VI", "TEN_DON_VI", nguoi_dung.MA_DON_VI);
            return View(nguoi_dung);
        }

        // GET: /NguoiDung/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGUOI_DUNG nguoi_dung = await db.NGUOI_DUNG.FindAsync(id);
            if (nguoi_dung == null)
            {
                return HttpNotFound();
            }
            ViewBag.MA_DON_VI = new SelectList(db.DON_VI, "MA_DON_VI", "TEN_DON_VI", nguoi_dung.MA_DON_VI);
            return View(nguoi_dung);
        }

        // POST: /NguoiDung/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MA_ND,TEN_ND,MA_DON_VI,EMAIL,TEN_DANG_NHAP,MAT_KHAU")] NGUOI_DUNG nguoi_dung)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nguoi_dung).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MA_DON_VI = new SelectList(db.DON_VI, "MA_DON_VI", "TEN_DON_VI", nguoi_dung.MA_DON_VI);
            return View(nguoi_dung);
        }

        // GET: /NguoiDung/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGUOI_DUNG nguoi_dung = await db.NGUOI_DUNG.FindAsync(id);
            if (nguoi_dung == null)
            {
                return HttpNotFound();
            }
            return View(nguoi_dung);
        }

        // POST: /NguoiDung/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            NGUOI_DUNG nguoi_dung = await db.NGUOI_DUNG.FindAsync(id);
            db.NGUOI_DUNG.Remove(nguoi_dung);
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
