using MotorcycleWeb.Models;
using MotorcycleWeb.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MotorcycleWeb.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin,Employee")]

    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Product
        public ActionResult Index(string searchText, int? page)
        {
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Product> items = db.Products.OrderBy(x => x.Id);
            if (!string.IsNullOrEmpty(searchText))
            {
                items = items.Where(x => x.Alias.Contains(searchText) || x.Title.Contains(searchText));
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }
        public ActionResult Add()
        {
            var product = db.ProductCategories.ToList();
            ViewBag.ProductCategoryList = product;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product model,List<string> Images, List<int> rDefault)
        {
            if(ModelState.IsValid)
            {
                if (Images != null && Images.Count > 0)
                {
                    for (int i = 0; i < Images.Count; i++)
                    {
                        bool isDefault = (i + 1 == rDefault[0]);

                        if (isDefault)
                        {
                            // Gán ảnh mặc định cho Product
                            model.Image = Images[i];
                        }

                        // Luôn add vào bảng ProductImage
                        model.ProductImage.Add(new ProductImage
                        {
                            ProductId = model.Id,
                            Image = Images[i],
                            IsDefault = isDefault
                        });
                    }
                }
                model.CreateDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                if (string.IsNullOrEmpty(model.SeoTitle))
                {
                    model.SeoTitle = model.Title;
                }

                     if (string.IsNullOrEmpty(model.Alias))
                     model.Alias = MotorcycleWeb.Models.Common.Filter.FilterChar(model.Title);
                db.Products.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var product = db.ProductCategories.ToList();
            ViewBag.ProductCategoryList = product;
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            var item = db.Products.Include("ProductImage").FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                // Xóa tất cả ảnh liên quan trước
                if (item.ProductImage != null && item.ProductImage.Any())
                {
                    db.ProductImages.RemoveRange(item.ProductImage);
                }

                db.Products.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public ActionResult Edit(int id)
        {
            var product = db.Products.Find(id);
            var products = db.Products.Include("ProductImage").FirstOrDefault(x => x.Id == id);
            ViewBag.ProductCategoryList = new SelectList(
                db.ProductCategories.ToList(),
                "Id", "Title",
                product.ProductCategoryId // <-- giá trị đang được chọn
            );

            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model, List<string> Images, List<int> rDefault)
        {
            if (ModelState.IsValid)
            {
                var product = db.Products.Include("ProductImage")
                                         .FirstOrDefault(x => x.Id == model.Id);

                if (product == null) return HttpNotFound();

                // cập nhật các trường cơ bản
                product.Title = model.Title;
                product.Price = model.Price;
                product.PriceSale = model.PriceSale;
                product.Quantity = model.Quantity;
                product.OriginalPrice = model.OriginalPrice;
                product.ProductCategoryId = model.ProductCategoryId;
                product.SeoTitle = string.IsNullOrEmpty(model.SeoTitle) ? model.Title : model.SeoTitle;
                product.Alias = MotorcycleWeb.Models.Common.Filter.FilterChar(model.Title);
                product.ModifiedDate = DateTime.Now;

                // xử lý ảnh
                // xóa ảnh cũ
                if (Images != null && Images.Count > 0)
                {
                    product.ProductImage.Clear();
                    for (int i = 0; i < Images.Count; i++)
                    {
                        bool isDefault = (i + 1 == rDefault[0]);
                        if (isDefault) product.Image = Images[i];

                        product.ProductImage.Add(new ProductImage
                        {
                            ProductId = product.Id,
                            Image = Images[i],
                            IsDefault = isDefault
                        });
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductCategoryList = db.ProductCategories.ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = db.Products.Find(Convert.ToInt32(item));
                        db.Products.Remove(obj);
                        db.SaveChanges();

                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                db.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive });
            }
            return Json(true);
        }
        [HttpPost]
        public ActionResult IsHome(int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                item.IsHome = !item.IsHome;
                db.SaveChanges();
                return Json(new { success = true, isHome = item.IsHome });
            }
            return Json(true);
        }
        [HttpPost]
        public ActionResult IsSale(int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                item.IsSale = !item.IsSale;
                db.SaveChanges();
                return Json(new { success = true, isSale = item.IsSale });
            }
            return Json(true);
        }
    }
}