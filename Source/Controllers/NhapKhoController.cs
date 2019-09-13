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
    public class NhapKhoController : Controller
    {
        private QuanLyTaiSanCNTTEntities2 db = new QuanLyTaiSanCNTTEntities2();

        // GET: /NhapKho/
        public async Task<ActionResult> Index(string ma_NhomTB)
        {
            //Nhóm thiết bị
            var dsNhomTB = new List<string>();
            var qNhomTB = (from d in db.NHOM_THIETBI
                           orderby d.TEN_NHOM
                           select d.TEN_NHOM);
            dsNhomTB.AddRange(qNhomTB.Distinct());
            ViewBag.MA_NHOMTB = new SelectList(dsNhomTB);

            //Loại thiết bị
            var dsLoaiTB = new List<string>();
            var qLoaiTB = (from d in db.LOAI_THIETBI
                           orderby d.TEN_LOAI
                           select d.TEN_LOAI);
            dsLoaiTB.AddRange(qLoaiTB.Distinct());
            ViewBag.MA_LOAITB = new SelectList(dsLoaiTB);

            //Đơn vị
            var dsTenDonVi = new List<string>();
            var qTenDonVi = (from d in db.DON_VI
                             orderby d.TEN_DON_VI
                             select d.TEN_DON_VI);
            dsTenDonVi.AddRange(qTenDonVi.Distinct());
            ViewBag.MA_DON_VI = new SelectList(dsTenDonVi);

            //Nhà cung cấp
            var dsNCC = new List<string>();
            var qNCC = (from d in db.NHA_CUNG_CAP
                        orderby d.TEN_NCC
                        select d.TEN_NCC);
            dsNCC.AddRange(qNCC.Distinct());
            ViewBag.MA_NCC = new SelectList(dsNCC);

            //CPU
            var dsCPU = new List<string>();
            var qCPU = (from d in db.DM_CPU
                        orderby d.TEN_CPU
                        select d.TEN_CPU);
            dsCPU.AddRange(qCPU.Distinct());
            ViewBag.CPU = new SelectList(dsCPU);

            //RAM
            var dsRAM = new List<string>();
            var qRAM = (from d in db.DM_RAM
                        orderby d.TEN_RAM
                        select d.TEN_RAM);
            dsRAM.AddRange(qRAM.Distinct());
            ViewBag.RAM = new SelectList(dsRAM);

            //Ổ cứng
            var dsOCung = new List<string>();
            var qOCung = (from d in db.DM_O_CUNG
                          orderby d.TEN_O_CUNG
                          select d.TEN_O_CUNG);
            dsOCung.AddRange(qOCung.Distinct());
            ViewBag.O_CUNG = new SelectList(dsOCung);

            //Màn hình
            var dsManHInh = new List<string>();
            var qManHInh = (from d in db.DM_MAN_HINH
                            orderby d.TEN_MAN_HINH
                            select d.TEN_MAN_HINH);
            dsManHInh.AddRange(qManHInh.Distinct());
            ViewBag.MAN_HINH = new SelectList(dsManHInh);

            //VGA
            var dsVGA = new List<string>();
            var qVGA = (from d in db.DM_VGA
                        orderby d.TEN_VGA
                        select d.TEN_VGA);
            dsVGA.AddRange(qVGA.Distinct());
            ViewBag.VGA = new SelectList(dsVGA);

            //HDH
            var dsHDH = new List<string>();
            var qHDH = (from d in db.DM_HDH
                        orderby d.TEN_HDH
                        select d.TEN_HDH);
            dsHDH.AddRange(qHDH.Distinct());
            ViewBag.HDH = new SelectList(dsHDH);

            var nhap_kho = db.NHAP_KHO.Include(n => n.DON_VI).Include(n => n.NGUOI_DUNG).Include(n => n.THIETBI);
            return View(await nhap_kho.ToListAsync());
            //await nhap_kho.ToListAsync()
        }

        [HttpPost]
        public ActionResult get_LOAITB(string nhom_TB)
        {
            var dsLoaiTB = new List<string>();
            //Tìm loại thiết bị
            if (!String.IsNullOrEmpty(nhom_TB))
            {
                var qLoaiTB = (from d in db.LOAI_THIETBI
                               where d.NHOM_THIETBI.TEN_NHOM == nhom_TB
                               orderby d.TEN_LOAI
                               select d.TEN_LOAI);
                dsLoaiTB.AddRange(qLoaiTB.ToList());
            }

            return Json(dsLoaiTB, JsonRequestBehavior.AllowGet);
        }   

        // GET: /NhapKho/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHAP_KHO nhap_kho = await db.NHAP_KHO.FindAsync(id);
            if (nhap_kho == null)
            {
                return HttpNotFound();
            }
            return View(nhap_kho);
        }



        // GET: /NhapKho/Create
        public ActionResult Create(string value)
        {
            ViewBag.MADV_NHAP = new SelectList(db.DON_VI, "MA_DON_VI", "TEN_DON_VI");
            ViewBag.MAND_NHAP = new SelectList(db.NGUOI_DUNG, "MA_ND", "TEN_ND");
            ViewBag.MATB = new SelectList(db.THIETBIs, "MATB", "TENTB");
            return View();
        }

        // POST: /NhapKho/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FormCollection form, string SAVE, HttpPostedFileBase HINH_ANH)
        {
            //, string hinh_anh
            if (!String.IsNullOrEmpty(SAVE))
            {
                //Tạo thiết bị
                var thiet_Bi = new THIETBI();
                thiet_Bi.TENTB = form["TENTB"];
                thiet_Bi.SO_SERIAL = form["SO_SERIAL"];
                //thiet_Bi.GIA_TIEN = Decimal.Parse(form["GIA_TIEN"]);
                thiet_Bi.THOI_HAN_BAO_HANH = form["THOI_HAN_BAO_HANH"];
                thiet_Bi.TINH_TRANG = "Mới nhập";

                var temp = form["MA_LOAITB"].ToString();
                thiet_Bi.MA_LOAITB = (from p in db.LOAI_THIETBI
                                      where p.TEN_LOAI == temp
                                      select p.MA_LOAITB).FirstOrDefault();

                if (!String.IsNullOrEmpty(form["MA_DON_VI"]))
                {
                    temp = form["MA_DON_VI"].ToString();
                    thiet_Bi.MA_DV = (from p in db.DON_VI
                                      where p.TEN_DON_VI == temp
                                      select p.MA_DON_VI).First();
                }

                if (!String.IsNullOrEmpty(form["MA_NCC"]))
                {
                    temp = form["MA_NCC"].ToString();
                    thiet_Bi.MA_NCC = (from p in db.NHA_CUNG_CAP
                                       where p.TEN_NCC == temp
                                       select p.MA_NCC).First();
                }

                //thiet_Bi.NGAY_GD = DateTime.Now;
                if (ModelState.IsValid)
                {
                    db.THIETBIs.Add(thiet_Bi);
                    await db.SaveChangesAsync();
                }

                //Tạo cấu hình
                var cau_Hinh = new CAU_HINH();
                temp = form["TENTB"].ToString();
                cau_Hinh.MATB = 17;
                    //(from p in db.THIETBIs
                    // where p.TENTB == temp
                    // select p.MATB).First();
                if(!String.IsNullOrEmpty(form["CPU"]))
                {
                    temp = form["CPU"].ToString();
                    cau_Hinh.CPU = (from p in db.DM_CPU
                                    where p.TEN_CPU == temp
                                    select p.MA_CPU).First();
                }

                if (!String.IsNullOrEmpty(form["MAN_HINH"]))
                {
                    temp = form["MAN_HINH"].ToString();
                    cau_Hinh.MAN_HINH = (from p in db.DM_MAN_HINH
                                         where p.TEN_MAN_HINH == temp
                                         select p.MA_MAN_HINH).First();
                }

                if (!String.IsNullOrEmpty(form["RAM"]))
                {
                    temp = form["RAM"].ToString();
                    cau_Hinh.RAM = (from p in db.DM_RAM
                                    where p.TEN_RAM == temp
                                    select p.MA_RAM).First();
                }

                if (!String.IsNullOrEmpty(form["HDH"]))
                {
                    temp = form["HDH"].ToString();
                    cau_Hinh.HE_DIEU_HANH = (from p in db.DM_HDH
                                             where p.TEN_HDH == temp
                                             select p.MA_HDH).First();
                }

                if (!String.IsNullOrEmpty(form["O_CUNG"]))
                {
                    temp = form["O_CUNG"].ToString();
                    cau_Hinh.O_CUNG = (from p in db.DM_O_CUNG
                                       where p.TEN_O_CUNG == temp
                                       select p.MA_O_CUNG).First();
                }

                if (!String.IsNullOrEmpty(form["VGA"]))
                {
                    temp = form["VGA"].ToString();
                    cau_Hinh.VGA = (from p in db.DM_VGA
                                    where p.TEN_VGA == temp
                                    select p.MA_VGA).First();
                }

                if (ModelState.IsValid)
                {
                    db.CAU_HINH.Add(cau_Hinh);
                    await db.SaveChangesAsync();
                }

                //Tạo nhập kho
                var nhap_Kho_Create = new NHAP_KHO();

                temp = form["TENTB"].ToString();
                nhap_Kho_Create.MATB = (from p in db.THIETBIs
                                        where p.TENTB == temp
                                        select p.MATB).First();

                if (!String.IsNullOrEmpty(form["MA_DON_VI"]))
                {
                    temp = form["MA_DON_VI"].ToString();
                    nhap_Kho_Create.MADV_NHAP = (from p in db.DON_VI
                                                 where p.TEN_DON_VI == temp
                                                 select p.MA_DON_VI).FirstOrDefault();
                }

                nhap_Kho_Create.NGAY_NHAP = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.NHAP_KHO.Add(nhap_Kho_Create);
                    await db.SaveChangesAsync();
                }

                //Tạo hình ảnh
                if (!String.IsNullOrEmpty(HINH_ANH.ToString()))
                {
                    var hinh_Anh = new HINH_ANH();

                    temp = form["TENTB"].ToString();
                    hinh_Anh.MATB = (from p in db.THIETBIs
                                     where p.TENTB == temp
                                     select p.MATB).First();

                    string ImageName = System.IO.Path.GetFileName(HINH_ANH.FileName);
                    string physicalPath = Server.MapPath("~/Images/" + ImageName);
                    // save image in folder
                    HINH_ANH.SaveAs(physicalPath);

                    hinh_Anh.HINH1 = ImageName;

                    if (ModelState.IsValid)
                    {
                        db.HINH_ANH.Add(hinh_Anh);
                        await db.SaveChangesAsync();
                    }
                }
            }
            //else if (!String.IsNullOrEmpty(REFESH))
            //{
            //    return RedirectToAction("Index");
            //}
            return RedirectToAction("Index");

            //ViewBag.MADV_NHAP = new SelectList(db.DON_VI, "MA_DON_VI", "TEN_DON_VI", nhap_kho.MADV_NHAP);
            //ViewBag.MAND_NHAP = new SelectList(db.NGUOI_DUNG, "MA_ND", "TEN_ND", nhap_kho.MANS_NHAP);
            //ViewBag.MATB = new SelectList(db.THIETBIs, "MATB", "TENTB", nhap_kho.MATB);
            //return View(nhap_kho);
        }

        // GET: /NhapKho/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHAP_KHO nhap_kho = await db.NHAP_KHO.FindAsync(id);
            if (nhap_kho == null)
            {
                return HttpNotFound();
            }
            ViewBag.MADV_NHAP = new SelectList(db.DON_VI, "MA_DON_VI", "TEN_DON_VI", nhap_kho.MADV_NHAP);
            ViewBag.MAND_NHAP = new SelectList(db.NGUOI_DUNG, "MA_ND", "TEN_ND", nhap_kho.MANS_NHAP);
            ViewBag.MATB = new SelectList(db.THIETBIs, "MATB", "TENTB", nhap_kho.MATB);
            return View(nhap_kho);
        }

        // POST: /NhapKho/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MA_NHAPKHO,MATB,MADV_NHAP,MAND_NHAP,NGAY")] NHAP_KHO nhap_kho)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhap_kho).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MADV_NHAP = new SelectList(db.DON_VI, "MA_DON_VI", "TEN_DON_VI", nhap_kho.MADV_NHAP);
            ViewBag.MAND_NHAP = new SelectList(db.NGUOI_DUNG, "MA_ND", "TEN_ND", nhap_kho.MANS_NHAP);
            ViewBag.MATB = new SelectList(db.THIETBIs, "MATB", "TENTB", nhap_kho.MATB);
            return View(nhap_kho);
        }

        // GET: /NhapKho/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHAP_KHO nhap_kho = await db.NHAP_KHO.FindAsync(id);
            if (nhap_kho == null)
            {
                return HttpNotFound();
            }
            return View(nhap_kho);
        }

        // POST: /NhapKho/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            NHAP_KHO nhap_kho = await db.NHAP_KHO.FindAsync(id);
            db.NHAP_KHO.Remove(nhap_kho);
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
