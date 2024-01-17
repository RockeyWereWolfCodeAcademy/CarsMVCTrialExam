using CarsMVCTrialExam.Areas.Admin.ViewModels;
using CarsMVCTrialExam.Contexts;
using CarsMVCTrialExam.ViewModels.BlogVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CarsMVCTrialExam.Controllers
{
    public class HomeController : Controller
    {
        readonly CarsDbContext _context;

        public HomeController(CarsDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult>  Index()
        {
            var data = await _context.Blogs.Select(b => new BlogListVM
            {
                Title = b.Title,
                Description = b.Description,
                ImgUrl = b.ImgUrl
            }).ToListAsync();
            return View(data);
        }
    }
}