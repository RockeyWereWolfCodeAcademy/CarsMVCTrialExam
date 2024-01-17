using CarsMVCTrialExam.Areas.Admin.ViewModels.AdminBlogVM;
using CarsMVCTrialExam.Contexts;
using CarsMVCTrialExam.Helpers;
using CarsMVCTrialExam.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarsMVCTrialExam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminBlogController : Controller
    {
        readonly CarsDbContext _context;

        public AdminBlogController(CarsDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _context.Blogs.Select(b => new AdminBlogListVM
            {
                Id = b.Id,
                Title = b.Title,
                ImgUrl = b.ImgUrl,
            }).ToListAsync();
            return View(data);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var data = await _context.Blogs.FindAsync(id);
            if (data == null) return NotFound();
            System.IO.File.Delete(Path.Combine(PathConstants.RootPath, data.ImgUrl));
            _context.Blogs.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminBlogCreateVM vm)
        {  
            if (vm.Image != null)
            {
                if (!vm.Image.CheckType("image"))
                {
                    ModelState.AddModelError("Image", "File must be image");
                }
                if (!vm.Image.IsValidSize(1000))
                {
                    ModelState.AddModelError("Image", "Image size must be lesser tha 1mb");
                }
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var dataToCreate = new Blog
            {
                Title = vm.Title,
                Description = vm.Description,
                ImgUrl = vm.Image.SaveFileAsync(PathConstants.BlogImagePath).Result,
            };
            await _context.Blogs.AddAsync(dataToCreate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();
            var data = await _context.Blogs.FindAsync(id);
            if (data == null) return NotFound();
            return View(new AdminBlogUpdateVM
            {
                Title = data.Title,
                Description = data.Description,
                ImgUrl = data.ImgUrl,
            });
        }
        [HttpPost]
        public async Task<IActionResult> Update(AdminBlogUpdateVM vm, int? id)
        {
            if (id == null) return BadRequest();
            var data = await _context.Blogs.FindAsync(id);
            if (data == null) return NotFound();
            if (vm.Image != null)
            {
                if (!vm.Image.CheckType("image"))
                {
                    ModelState.AddModelError("Image", "File must be image");
                }
                if (!vm.Image.IsValidSize(1000))
                {
                    ModelState.AddModelError("Image", "Image size must be lesser than 1mb");
                }
            }
            if (!ModelState.IsValid)
            {
                vm.ImgUrl = data.ImgUrl;
                return View(vm);
            }
            data.Title = vm.Title;
            data.Description = vm.Description;
            if (vm.Image != null)
                data.ImgUrl = await vm.Image.SaveFileAsync(PathConstants.BlogImagePath);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
