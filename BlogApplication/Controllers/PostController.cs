using BlogApplication.Data;
using BlogApplication.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogApplication.Controllers
{
    public class PostController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _Iwebhistenvironment;
        private readonly string[] _Allowedextentions = {".jpg",".jpeg",".png"};    

        public PostController(AppDbContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _Iwebhistenvironment = webHostEnvironment;


        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var postViewModel = new PostViewModel();
            postViewModel.Categories =  _context.Categories.Select(c=>
                    new SelectListItem{
                        Value = c.Id.ToString(),
                        Text = c.Name,  
                    }
            ).ToList();
             
            return View(postViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                var inputFileExtension = Path.GetExtension(postViewModel.FeatureImage.FileName).ToLower();
                bool isAllowd = _Allowedextentions.Contains(inputFileExtension);
                if (!isAllowd)
                {
                    ModelState.AddModelError("", "Invalid Image format.Allowed Format are .jpg .jpeg .png");
                    return View(); 
                }

                postViewModel.Post.FeatureImagePath =  await UploadFileFolder(postViewModel.FeatureImage);
                await _context.Posts.AddAsync(postViewModel.Post);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(postViewModel);
        }

        private async Task<string> UploadFileFolder(IFormFile file)
        {
            var inputFileExtension = Path.GetExtension(file.FileName);
            var filename = Guid.NewGuid().ToString() + inputFileExtension;
            var wwwRootPath = _Iwebhistenvironment.WebRootPath;
            var imagesFolderPath = Path.Combine(wwwRootPath, "images");

            if (!Directory.Exists(imagesFolderPath))
            {
                Directory.CreateDirectory(imagesFolderPath);
            }

            var filePath = Path.Combine(imagesFolderPath, filename);
            try
            {
                using(var fileStream = new FileStream(filePath,FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            catch (Exception ex)
            {
                return "Error Uploading Images:" + ex.Message;
            }
            return "/Images/" + filename;
        }
    }
}
