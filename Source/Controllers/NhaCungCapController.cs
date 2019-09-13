﻿using System;
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
    public class NhaCungCapController : Controller
    {
        private QuanLyTaiSanCNTTEntities2 db = new QuanLyTaiSanCNTTEntities2();

        // GET: /NhaCungCap/
        public async Task<ActionResult> Index()
        {
            return View(await db.NHA_CUNG_CAP.ToListAsync());
        }

        // GET: /NhaCungCap/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHA_CUNG_CAP nha_cung_cap = await db.NHA_CUNG_CAP.FindAsync(id);
            if (nha_cung_cap == null)
            {
                return HttpNotFound();
            }
            return View(nha_cung_cap);
        }

        // GET: /NhaCungCap/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /NhaCungCap/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FormCollection form)
        {
            //[Bind(Include = "TEN_NCC,DIA_CHI,DIEN_THOAI,FAX,GHI_CHU")] NHA_CUNG_CAP nha_cung_cap, 
            var nha_cung_cap = new NHA_CUNG_CAP();
            nha_cung_cap.TEN_NCC = form["TEN_NCC"];
            nha_cung_cap.DIA_CHI = form["DIA_CHI"];
            nha_cung_cap.DIEN_THOAI = form["DIEN_THOAI"];
            nha_cung_cap.FAX = form["FAX"];
            nha_cung_cap.GHI_CHU = form["GHI_CHU"];
            if (ModelState.IsValid)
            {
                db.NHA_CUNG_CAP.Add(nha_cung_cap);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
            //return View(nha_cung_cap);
        }

        // GET: /NhaCungCap/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHA_CUNG_CAP nha_cung_cap = await db.NHA_CUNG_CAP.FindAsync(id);
            if (nha_cung_cap == null)
            {
                return HttpNotFound();
            }
            return View(nha_cung_cap);
        }

        // POST: /NhaCungCap/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MA_NCC,TEN_NCC,MST,DIA_CHI,DIEN_THOAI,FAX,GHI_CHU")] NHA_CUNG_CAP nha_cung_cap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nha_cung_cap).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(nha_cung_cap);
        }

        // GET: /NhaCungCap/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHA_CUNG_CAP nha_cung_cap = await db.NHA_CUNG_CAP.FindAsync(id);
            if (nha_cung_cap == null)
            {
                return HttpNotFound();
            }
            return View(nha_cung_cap);
        }

        // POST: /NhaCungCap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            NHA_CUNG_CAP nha_cung_cap = await db.NHA_CUNG_CAP.FindAsync(id);
            db.NHA_CUNG_CAP.Remove(nha_cung_cap);
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
