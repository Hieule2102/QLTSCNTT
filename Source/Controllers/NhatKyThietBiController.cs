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
    public class NhatKyThietBiController : Controller
    {
        private QuanLyTaiSanCNTTEntities2 db = new QuanLyTaiSanCNTTEntities2();

        // GET: /NhatKyThietBi/
        public async Task<ActionResult> Index(string trangThai, string searchString)
        {
            //Trạng thái
            var dsTrangThai = new List<string>();
            var qTrangThai = (from d in db.THIETBIs
                              orderby d.TINH_TRANG
                              select d.TINH_TRANG);

            dsTrangThai.AddRange(qTrangThai.Distinct());
            ViewBag.trangThai = new SelectList(dsTrangThai);

            var thietbis = db.THIETBIs.Include(t => t.DON_VI).Include(t => t.LOAI_THIETBI).Include(t => t.NHA_CUNG_CAP);

            //Tìm trạng thái
            if (!String.IsNullOrEmpty(trangThai))
            {
                thietbis = thietbis.Where(data => data.TINH_TRANG == trangThai);
            }
            //Tìm tên thiết bị
            if (!String.IsNullOrEmpty(searchString))
            {
                thietbis = thietbis.Where(data => data.TENTB.Contains(searchString));
            }
            return View(await thietbis.ToListAsync());
        }

        // GET: /NhatKyThietBi/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THIETBI thietbi = await db.THIETBIs.FindAsync(id);
            if (thietbi == null)
            {
                return HttpNotFound();
            }
            return View(thietbi);
        }

        // GET: /NhatKyThietBi/Create
        public ActionResult Create()
        {
            ViewBag.MA_DV = new SelectList(db.DON_VI, "MA_DON_VI", "TEN_DON_VI");
            ViewBag.MA_LOAITB = new SelectList(db.LOAI_THIETBI, "MA_LOAITB", "TEN_LOAI");
            ViewBag.MA_NHA_CUNG_CAP = new SelectList(db.NHA_CUNG_CAP, "MA_NCC", "TEN_NCC");
            return View();
        }

        // POST: /NhatKyThietBi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MATB,TENTB,SO_SERIAL,GIA_TIEN,THOI_HAN_BAO_HANH,TINH_TRANG,MA_LOAITB,MANS_QL,MA_DV,MA_NHA_CUNG_CAP,NGAY_GD")] THIETBI thietbi)
        {
            if (ModelState.IsValid)
            {
                db.THIETBIs.Add(thietbi);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MA_DV = new SelectList(db.DON_VI, "MA_DON_VI", "TEN_DON_VI", thietbi.MA_DV);
            ViewBag.MA_LOAITB = new SelectList(db.LOAI_THIETBI, "MA_LOAITB", "TEN_LOAI", thietbi.MA_LOAITB);
            ViewBag.MA_NHA_CUNG_CAP = new SelectList(db.NHA_CUNG_CAP, "MA_NCC", "TEN_NCC", thietbi.MA_NCC);
            return View(thietbi);
        }

        // GET: /NhatKyThietBi/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THIETBI thietbi = await db.THIETBIs.FindAsync(id);
            if (thietbi == null)
            {
                return HttpNotFound();
            }
            ViewBag.MA_DV = new SelectList(db.DON_VI, "MA_DON_VI", "TEN_DON_VI", thietbi.MA_DV);
            ViewBag.MA_LOAITB = new SelectList(db.LOAI_THIETBI, "MA_LOAITB", "TEN_LOAI", thietbi.MA_LOAITB);
            ViewBag.MA_NHA_CUNG_CAP = new SelectList(db.NHA_CUNG_CAP, "MA_NCC", "TEN_NCC", thietbi.MA_NCC);
            return View(thietbi);
        }

        // POST: /NhatKyThietBi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MATB,TENTB,SO_SERIAL,GIA_TIEN,THOI_HAN_BAO_HANH,TINH_TRANG,MA_LOAITB,MANS_QL,MA_DV,MA_NHA_CUNG_CAP,NGAY_GD")] THIETBI thietbi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thietbi).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MA_DV = new SelectList(db.DON_VI, "MA_DON_VI", "TEN_DON_VI", thietbi.MA_DV);
            ViewBag.MA_LOAITB = new SelectList(db.LOAI_THIETBI, "MA_LOAITB", "TEN_LOAI", thietbi.MA_LOAITB);
            ViewBag.MA_NHA_CUNG_CAP = new SelectList(db.NHA_CUNG_CAP, "MA_NCC", "TEN_NCC", thietbi.MA_NCC);
            return View(thietbi);
        }

        // GET: /NhatKyThietBi/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THIETBI thietbi = await db.THIETBIs.FindAsync(id);
            if (thietbi == null)
            {
                return HttpNotFound();
            }
            return View(thietbi);
        }

        // POST: /NhatKyThietBi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            THIETBI thietbi = await db.THIETBIs.FindAsync(id);
            db.THIETBIs.Remove(thietbi);
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
