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
    public class DieuChuyenThietBiController : Controller
    {
        private QuanLyTaiSanCNTTEntities2 db = new QuanLyTaiSanCNTTEntities2();

        // GET: /DieuChuyenThietBi/
        public async Task<ActionResult> Index()
        {
            //Thiết bị
            var dsMaTB = new List<int>();
            var qMaTB = from d in db.THIETBIs
                        orderby d.MATB
                        select d.MATB;
            dsMaTB.AddRange(qMaTB.ToList());
            ViewBag.MATB = new SelectList(dsMaTB);


            //Đơn vị quản lý
            var dsMaDV_QL = new List<string>();
            var qMaDV_QL = from d in db.DON_VI
                           orderby d.TEN_DON_VI
                           select d.TEN_DON_VI;
            dsMaDV_QL.AddRange(qMaDV_QL.ToList());
            ViewBag.MADV_QL = new SelectList(dsMaDV_QL);

            //Đơn vị nhận
            var dsMaDV_NHAN = new List<string>();
            var qMaDV_NHAN = from d in db.DON_VI
                             orderby d.TEN_DON_VI
                             select d.TEN_DON_VI;
            dsMaDV_NHAN.AddRange(qMaDV_NHAN.ToList());
            ViewBag.MADV_NHAN = new SelectList(dsMaDV_NHAN);

            var dieu_chuyen_thiet_bi = db.DIEU_CHUYEN_THIET_BI.Include(d => d.DON_VI).Include(d => d.DON_VI1).Include(d => d.THIETBI);
            return View(await dieu_chuyen_thiet_bi.ToListAsync());
        }

        //Tìm tên thiết bị
        [HttpPost]
        public ActionResult get_TENTB(string maTB)
        {
            int temp = Int32.Parse(maTB);
            //Tìm tên thiết bị
            if (!String.IsNullOrEmpty(maTB))
            {
                var tenTB = (from d in db.THIETBIs
                             where d.MATB == temp
                             select d.TENTB).First();
                return Json(tenTB, JsonRequestBehavior.AllowGet);
            }

            return null;
        }   

        // GET: /DieuChuyenThietBi/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DIEU_CHUYEN_THIET_BI dieu_chuyen_thiet_bi = await db.DIEU_CHUYEN_THIET_BI.FindAsync(id);
            if (dieu_chuyen_thiet_bi == null)
            {
                return HttpNotFound();
            }
            return View(dieu_chuyen_thiet_bi);
        }

        // GET: /DieuChuyenThietBi/Create
        public ActionResult Create()
        {
            ViewBag.MADV_QL = new SelectList(db.DON_VI, "MA_DON_VI", "TEN_DON_VI");
            ViewBag.MADV_NHAN = new SelectList(db.DON_VI, "MA_DON_VI", "TEN_DON_VI");
            ViewBag.MATB = new SelectList(db.THIETBIs, "MATB", "TENTB");
            return View();
        }

        // POST: /DieuChuyenThietBi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string maTB, FormCollection form, string SAVE)
        {
            if (!String.IsNullOrEmpty(SAVE))
            {
                //Tạo điều chuyển thiết bị
                var dieu_chuyen_thiet_bi_create = new DIEU_CHUYEN_THIET_BI();
                dieu_chuyen_thiet_bi_create.MATB = Int32.Parse(maTB);

                var temp = form["MADV_QL"].ToString();
                dieu_chuyen_thiet_bi_create.MADV_QL = (from p in db.DON_VI
                                                       where p.TEN_DON_VI.ToString() == temp
                                                       select p.MA_DON_VI).FirstOrDefault();
                temp = form["MADV_NHAN"].ToString();
                dieu_chuyen_thiet_bi_create.MADV_NHAN = (from p in db.DON_VI
                                                         where p.TEN_DON_VI.ToString() == temp
                                                         select p.MA_DON_VI).FirstOrDefault();

                dieu_chuyen_thiet_bi_create.NGAY_CHUYEN = DateTime.Now;
                dieu_chuyen_thiet_bi_create.GHI_CHU = form["GHI_CHU"];

                if (ModelState.IsValid)
                {
                    db.DIEU_CHUYEN_THIET_BI.Add(dieu_chuyen_thiet_bi_create);
                    await db.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index");
        //    if (ModelState.IsValid)
        //    {
        //        db.DIEU_CHUYEN_THIET_BI.Add(dieu_chuyen_thiet_bi);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.MADV_QL = new SelectList(db.DON_VI, "MA_DON_VI", "TEN_DON_VI", dieu_chuyen_thiet_bi.MADV_QL);
        //    ViewBag.MADV_NHAN = new SelectList(db.DON_VI, "MA_DON_VI", "TEN_DON_VI", dieu_chuyen_thiet_bi.MADV_NHAN);
        //    ViewBag.MATB = new SelectList(db.THIETBIs, "MATB", "TENTB", dieu_chuyen_thiet_bi.MATB);
        //    return View(dieu_chuyen_thiet_bi);
        }

        // GET: /DieuChuyenThietBi/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DIEU_CHUYEN_THIET_BI dieu_chuyen_thiet_bi = await db.DIEU_CHUYEN_THIET_BI.FindAsync(id);
            if (dieu_chuyen_thiet_bi == null)
            {
                return HttpNotFound();
            }
            ViewBag.MADV_QL = new SelectList(db.DON_VI, "MA_DON_VI", "TEN_DON_VI", dieu_chuyen_thiet_bi.MADV_QL);
            ViewBag.MADV_NHAN = new SelectList(db.DON_VI, "MA_DON_VI", "TEN_DON_VI", dieu_chuyen_thiet_bi.MADV_NHAN);
            ViewBag.MATB = new SelectList(db.THIETBIs, "MATB", "TENTB", dieu_chuyen_thiet_bi.MATB);
            return View(dieu_chuyen_thiet_bi);
        }

        // POST: /DieuChuyenThietBi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MA_DIEU_CHUYEN,MATB,MANS_THEO_DOI,MADV_QL,MANS_QL,MADV_NHAN,MANS_NHAN,NGAY_CHUYEN,GHI_CHU")] DIEU_CHUYEN_THIET_BI dieu_chuyen_thiet_bi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dieu_chuyen_thiet_bi).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MADV_QL = new SelectList(db.DON_VI, "MA_DON_VI", "TEN_DON_VI", dieu_chuyen_thiet_bi.MADV_QL);
            ViewBag.MADV_NHAN = new SelectList(db.DON_VI, "MA_DON_VI", "TEN_DON_VI", dieu_chuyen_thiet_bi.MADV_NHAN);
            ViewBag.MATB = new SelectList(db.THIETBIs, "MATB", "TENTB", dieu_chuyen_thiet_bi.MATB);
            return View(dieu_chuyen_thiet_bi);
        }

        // GET: /DieuChuyenThietBi/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DIEU_CHUYEN_THIET_BI dieu_chuyen_thiet_bi = await db.DIEU_CHUYEN_THIET_BI.FindAsync(id);
            if (dieu_chuyen_thiet_bi == null)
            {
                return HttpNotFound();
            }
            return View(dieu_chuyen_thiet_bi);
        }

        // POST: /DieuChuyenThietBi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DIEU_CHUYEN_THIET_BI dieu_chuyen_thiet_bi = await db.DIEU_CHUYEN_THIET_BI.FindAsync(id);
            db.DIEU_CHUYEN_THIET_BI.Remove(dieu_chuyen_thiet_bi);
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
