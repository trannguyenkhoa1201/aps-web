﻿using ProjectA.Data;
using ProjectA.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProjectA.Controllers
{
    [Area("Admin")]
    public class TheLoaiController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TheLoaiController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
			var Theloai = _db.TheLoai.ToList();
            ViewBag.Theloai = Theloai;
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            

            return View();
        }
        [HttpPost]
        public IActionResult Create(TheLoai theLoai)
        {
            if (ModelState.IsValid) {
                // them thong tin
                _db.TheLoai.Add(theLoai);
                // luu thong tin
                _db.SaveChanges();

                return RedirectToAction("Index");


            }
            return View();

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var theloai = _db.TheLoai.Find(id);
            return View(theloai);
        }
        [HttpPost]
        public IActionResult Edit(TheLoai theloai)
        {
            if (ModelState.IsValid)
            {
                // Thêm thông tin vào bảng TheLoai
                _db.TheLoai.Update(theloai);
                // Lưu lại
                _db.SaveChanges();
                // Chuyển trang về index
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var theloai = _db.TheLoai.Find(id);
            return View(theloai);
        }
        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {
            var theloai= _db.TheLoai.Find(id);
            if (theloai == null)
            {
                return NotFound();
            }
            _db.TheLoai.Remove(theloai); _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Detail(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var theloai = _db.TheLoai.Find(id);
            return View(theloai);
        }
        [HttpGet]
        public IActionResult Search(string SearchString)
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                // Sử dụng LINQ để tìm kiếm
                var theloai = _db.TheLoai.Where(tl => tl.Name.Contains(SearchString)).ToList();
                ViewBag.TheLoai = theloai;
            }
            else
            {
                var theloai = _db.TheLoai.ToList();
                ViewBag.TheLoai = theloai;
            }
            return View("Index"); // Sử dụng lại View Index
        }

    }
}
