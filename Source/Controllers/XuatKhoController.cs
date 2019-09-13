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
    public class XuatKhoController : Controller
    {
        private QuanLyTaiSanCNTTEntities2 db = new QuanLyTaiSanCNTTEntities2();

        // GET: /XuatKho/
        public async Task<ActionResult> Index()
        {
            //Thiết bị
            var dsMaTB = new List<int>();
            var qMaTB = from d in db.THIETBIs
                        orderby d.MATB
                        select d.MATB;
            dsMaTB.AddRange(qMaTB.ToList());
            ViewBag.MATB = new SelectList(dsMaTB);

            //Đơn vị xuất
            var dsMaDV_XUAT = new List<string>();
            var qMaDV_XUAT = from d in db.DON_VI
                             orderby d.TEN_DON_VI
                             select d.TEN_DON_VI;
            dsMaDV_XUAT.AddRange(qMaDV_XUAT.ToList());
            ViewBag.MADV_XUAT = new SelectList(dsMaDV_XUAT);

            //Đơn vị nhận
            var dsMaDV_NHAN = new List<string>();
            var qMaDV_NHAN = from d in db.DON_VI
                             orderby d.TEN_DON_VI
                             select d.TEN_DON_VI;
            dsMaDV_NHAN.AddRange(qMaDV_NHAN.ToList());
            ViewBag.MADV_NHAN = new SelectList(dsMaDV_NHAN);

            var xuat_kho = db.XUAT_KHO.Include(x => x.DON_VI).Include(x => x.DON_VI1).Include(x => x.NGUOI_DUNG).Include(x => x.NGUOI_DUNG1).Include(x => x.THIETBI);
            return View(await xuat_kho.ToListAsync());
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

        // GET: /XuatKho/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            XUAT_KHO xuat_kho = await db.XUAT_KHO.FindAsync(id);
            if (xuat_kho == null)
            {
                return HttpNotFound();
            }
            return View(xuat_kho);
        }

        // GET: /XuatKho/Create
        public ActionResult Create()
        {
            ViewBag.MADV_XUAT = new SelectList(db.DON_VI, "MA_DON_VI", "TEN_DON_VI");
            ViewBag.MADV_NHAN = new SelectList(db.DON_VI, "MA_DON_VI", "TEN_DON_VI");
            ViewBag.MAND_XUAT = new SelectList(db.NGUOI_DUNG, "MA_ND", "TEN_ND");
            ViewBag.MAND_NHAN = new SelectList(db.NGUOI_DUNG, "MA_ND", "TEN_ND");
            ViewBag.MATB = new SelectList(db.THIETBIs, "MATB", "TENTB");
            return View();
        }

        // POST: /XuatKho/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string maTB, FormCollection form, string SAVE)
        {
            if (!String.IsNullOrEmpty(SAVE))
            {
                //Tạo xuất kho
                var xuat_kho_create = new XUAT_KHO();
                xuat_kho_create.MATB = Int32.Parse(maTB);

                var temp = form["MADV_XUAT"].ToString();
                xuat_kho_create.MADV_XUAT = (from p in db.DON_VI
                                             where p.TEN_DON_VI.ToString() == temp
                                             select p.MA_DON_VI).FirstOrDefault();

                temp = form["MADV_NHAN"].ToString();
                xuat_kho_create.MADV_NHAN = (from p in db.DON_VI
                                             where p.TEN_DON_VI.ToString() == temp
                                             select p.MA_DON_VI).FirstOrDefault();

                xuat_kho_create.NGAY_XUAT = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.XUAT_KHO.Add(xuat_kho_create);
                    await db.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index");

            //if (ModelState.IsValid)
            //{
            //    db.XUAT_KHO.Add(xuat_kho);
            //    await db.SaveChangesAsync();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.MADV_XUAT = new SelectList(db.DON_VI, "MA_DON_VI", "TEN_DON_VI", xuat_kho.MADV_XUAT);
            //ViewBag.MADV_NHAN = new SelectList(db.DON_VI, "MA_DON_VI", "TEN_DON_VI", xuat_kho.MADV_NHAN);
            //ViewBag.MAND_XUAT = new SelectList(db.NGUOI_DUNG, "MA_ND", "TEN_ND", xuat_kho.MANS_XUAT);
            //ViewBag.MAND_NHAN = new SelectList(db.NGUOI_DUNG, "MA_ND", "TEN_ND", xuat_kho.MANS_NHAN);
            //ViewBag.MATB = new SelectList(db.THIETBIs, "MATB", "TENTB", xuat_kho.MATB);
            //return View(xuat_kho);
        }

        // GET: /XuatKho/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            XUAT_KHO xuat_kho = await db.XUAT_KHO.FindAsync(id);
            if (xuat_kho == null)
            {
                return HttpNotFound();
            }
            ViewBag.MADV_XUAT = new SelectList(db.DON_VI, "MA_DON_VI", "TEN_DON_VI", xuat_kho.MADV_XUAT);
            ViewBag.MADV_NHAN = new SelectList(db.DON_VI, "MA_DON_VI", "TEN_DON_VI", xuat_kho.MADV_NHAN);
            ViewBag.MAND_XUAT = new SelectList(db.NGUOI_DUNG, "MA_ND", "TEN_ND", xuat_kho.MANS_XUAT);
            ViewBag.MAND_NHAN = new SelectList(db.NGUOI_DUNG, "MA_ND", "TEN_ND", xuat_kho.MANS_NHAN);
            ViewBag.MATB = new SelectList(db.THIETBIs, "MATB", "TENTB", xuat_kho.MATB);
            return View(xuat_kho);
        }

        // POST: /XuatKho/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MA_XUAT_KHO,MATB,MADV_XUAT,MADV_NHAN,MAND_XUAT,MAND_NHAN,NGAY_THUC_HIEN")] XUAT_KHO xuat_kho)
        {
            if (ModelState.IsValid)
            {
                db.Entry(xuat_kho).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MADV_XUAT = new SelectList(db.DON_VI, "MA_DON_VI", "TEN_DON_VI", xuat_kho.MADV_XUAT);
            ViewBag.MADV_NHAN = new SelectList(db.DON_VI, "MA_DON_VI", "TEN_DON_VI", xuat_kho.MADV_NHAN);
            ViewBag.MAND_XUAT = new SelectList(db.NGUOI_DUNG, "MA_ND", "TEN_ND", xuat_kho.MANS_XUAT);
            ViewBag.MAND_NHAN = new SelectList(db.NGUOI_DUNG, "MA_ND", "TEN_ND", xuat_kho.MANS_NHAN);
            ViewBag.MATB = new SelectList(db.THIETBIs, "MATB", "TENTB", xuat_kho.MATB);
            return View(xuat_kho);
        }

        // GET: /XuatKho/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            XUAT_KHO xuat_kho = await db.XUAT_KHO.FindAsync(id);
            if (xuat_kho == null)
            {
                return HttpNotFound();
            }
            return View(xuat_kho);
        }

        // POST: /XuatKho/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            XUAT_KHO xuat_kho = await db.XUAT_KHO.FindAsync(id);
            db.XUAT_KHO.Remove(xuat_kho);
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
