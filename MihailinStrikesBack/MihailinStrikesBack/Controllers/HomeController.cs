
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MihailinStrikesBack.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;



namespace MihailinStrikesBack.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db;
        private IWebHostEnvironment _app;
        public int IdNow = -1;

        private IHttpContextAccessor _httpContextAccessor;
        public HomeController(ApplicationContext context, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment app)
        {
            db = context;
            _httpContextAccessor = httpContextAccessor;
            _app = app;
        }
        [HttpPost]
        public async Task<IActionResult> AddImage(IFormFile file)
        {
            if (file != null)
            {
                string path = "/Files/" + file.FileName;
                int i = 0;
                while (System.IO.File.Exists(_app.WebRootPath + path))
                {
                    try
                    {
                        i++;
                        string a = Path.GetFileNameWithoutExtension(path);
                        if (a.Substring(a.Length - 3) == $"({i - 1})")
                        {
                            a = a.Substring(0, a.Length - 3);
                        }
                        path = "/Files/" + a + Convert.ToString("(" + i + ")") + Path.GetExtension(path);


                    }
                    catch
                    {

                    }

                }
                using (var fileStream = new FileStream(_app.WebRootPath + path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);



                    Images newfile = new Images
                    {
                        ImageName = file.FileName,
                        PathImage = path,
                    };
                    
                    db.Images.Add(newfile);

                    await db.SaveChangesAsync();
                    return RedirectToAction("AddImages");
                }
               
            }
            return NotFound();
        }
        public IActionResult AddImages()
        {
            return View(db.Images.ToList());
        }
        public async Task<ActionResult> Index(int? id, string login,int role, string email, int page = 1, SortState sortOrder = SortState.IdAsc)
        {

            // var users = db.Users.OrderBy(m => m.Id);
            IQueryable<User> users = db.Users;
           
            //Фильтрация или поиск
            if (id != null && id > 0)
            {
                users = users.Where(p => p.Id == id);
            }
            if (!string.IsNullOrEmpty(login))
            {
                users = users.Where(p => p.Login.Contains(login));
            }
            //Сортировка
            switch (sortOrder)
            {
                case SortState.IdAsc:
                    {
                        users = users.OrderBy(m => m.Id);
                        break;
                    }
                case SortState.IdDesc:
                    {
                        users = users.OrderByDescending(m => m.Id);
                        break;
                    }
                case SortState.EmailAsc:
                    {
                        users = users.OrderBy(m => m.Email);
                        break;
                    }
                case SortState.EmailDesc:
                    {
                        users = users.OrderByDescending(m => m.Email);
                        break;
                    }
                case SortState.LoginAsc:
                    {
                        users = users.OrderBy(m => m.Login);
                        break;
                    }
                case SortState.LoginDesc:
                    {
                        users = users.OrderByDescending(m => m.Login);
                        break;
                    }
                     case SortState.RoleAsc:
                    {
                        users = users.OrderBy(m => m.Role_Id);
                        break;
                    }
                case SortState.RoleDesc:
                    {
                        users = users.OrderByDescending(m => m.Role_Id);
                        break;
                    }
                default:
                    {
                        users = users.OrderBy(m => m.Id);
                        break;
                    }

            }
            //ПОГинация
            var count = await users.CountAsync();
            int pagesize = 4;
            var item = await users.Skip((page - 1) * pagesize).Take(pagesize).ToListAsync();
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = new PageViewModel(count, page, pagesize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(id, email, login,role),
                Users = item,
                

            };
            return View(viewModel);
        }
        public async Task<ActionResult> ListUser(int? id, string login,int role, string email, int page = 1, SortState sortOrder = SortState.IdAsc)
        {

            // var users = db.Users.OrderBy(m => m.Id);
            IQueryable<User> users = db.Users;
            //Фильтрация или поиск
            if (id != null && id > 0)
            {
                users = users.Where(p => p.Id == id);
            }
            if (!string.IsNullOrEmpty(login))
            {
                users = users.Where(p => p.Login.Contains(login));
            }
            //Сортировка
            switch (sortOrder)
            {
                //case SortState.IdAsc:
                //    {
                //        users = users.OrderBy(m => m.Id);
                //        break;
                //    }
                //case SortState.IdDesc:
                //    {
                //        users = users.OrderByDescending(m => m.Id);
                //        break;
                //    }
                case SortState.EmailAsc:
                    {
                        users = users.OrderBy(m => m.Email);
                        break;
                    }
                case SortState.EmailDesc:
                    {
                        users = users.OrderByDescending(m => m.Email);
                        break;
                    }
                case SortState.LoginAsc:
                    {
                        users = users.OrderBy(m => m.Login);
                        break;
                    }
                case SortState.LoginDesc:
                    {
                        users = users.OrderByDescending(m => m.Login);
                        break;
                    }
               
                        default:
                    {
                        users = users.OrderBy(m => m.Id);
                        break;
                    }

            }
            //ПОГинация
            var count = await users.CountAsync();
            int pagesize = 5;
            var item = await users.Skip((page - 1) * pagesize).Take(pagesize).ToListAsync();
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = new PageViewModel(count, page, pagesize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(id, email, login,role),
                Users = item
            };
            return View(viewModel);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(User user, DateTime date12, IFormFile file, int role)
        {

            user.BirthDate = date12;
            user.Role_Id = role;

            if (file != null)
            {
                string path = "/Files/" + file.FileName;
                int i = 0;
                while (System.IO.File.Exists(_app.WebRootPath + path))
                {
                    try
                    {
                        i++;
                        string a = Path.GetFileNameWithoutExtension(path);
                        if (a.Substring(a.Length - 3) == $"({i - 1})")
                        {
                            a = a.Substring(0, a.Length - 3);
                        }
                        path = "/Files/" + a + Convert.ToString("(" + i + ")") + Path.GetExtension(path);


                    }
                    catch
                    {

                    }

                }
                using (var fileStream = new FileStream(_app.WebRootPath + path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);


                    Images newfile = new Images
                    {

                        ImageName = file.FileName,
                        PathImage = path,
                    };
                    db.Images.Add(newfile);
                    await db.SaveChangesAsync();
                    user.ID_Image = db.Images.FirstOrDefault(predicate => predicate.PathImage == path);
                    db.Users.Add(user);

                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

            }
            return NotFound();
        }
        [HttpGet]
        [ActionName("Delete")]
        public async Task<ActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (user != null)
                {
                    return View(user);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (user != null)
                {
                    db.Users.Remove(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
        public async Task<ActionResult> Edit(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (user != null)
                {
                    return View(user);
                }
            }
            return NotFound();
        }
    
        [HttpPost]
        public async Task<ActionResult> Edit(User user)
        {
            db.Users.Update(user);
            await db.SaveChangesAsync();
                return RedirectToAction("Index");
            
        }
        public async Task<ActionResult> Edit_User(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (user != null)
                {
                    return View(user);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<ActionResult> Edit_User(IFormFile file,User userLog )
        {
           
            if (file != null)
            {
                string path = "/Files/" + file.FileName;
                int i = 0;
                while (System.IO.File.Exists(_app.WebRootPath + path))
                {
                    try
                    {
                        i++;
                        string a = Path.GetFileNameWithoutExtension(path);
                        if (a.Substring(a.Length - 3) == $"({i - 1})")
                        {
                            a = a.Substring(0, a.Length - 3);
                        }
                        path = "/Files/" + a + Convert.ToString("(" + i + ")") + Path.GetExtension(path);


                    }
                    catch
                    {

                    }

                }
                using (var fileStream = new FileStream(_app.WebRootPath + path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);


                    Images newfile = new Images
                    {

                        ImageName = file.FileName,
                        PathImage = path,
                    };
                    db.Images.Add(newfile);
                    await db.SaveChangesAsync();
                    userLog.ID_Image = db.Images.FirstOrDefault(predicate => predicate.PathImage == path);
                    db.Users.Update(userLog);
                    await db.SaveChangesAsync();


                    return RedirectToAction("UserPage");
                }

            }
            return NotFound();

        }
        public IActionResult HomeEnter()
        {
            string cookieValueLog = _httpContextAccessor.HttpContext.Request.Cookies["Login"];
            string cookieValuePas = _httpContextAccessor.HttpContext.Request.Cookies["Password"];
            ViewData["Login"] = cookieValueLog;
            ViewData["Password"] = cookieValuePas;
            return View();
        }
        public IActionResult Registration()
        {
            return View();
        }

        public IActionResult test()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> ListUser(LogUser userLog)
        {


            if (userLog != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(predicate => predicate.Login == userLog.Login);
                if (user != null)
                {
                    User user_log = await db.Users.FirstOrDefaultAsync(predicate => predicate.Password == userLog.Password);
                    if (user_log != null)
                    {
                       
                        CookieOptions cookieLogin = new CookieOptions();
                        cookieLogin.Expires = DateTime.Now.AddDays(7);
                        Response.Cookies.Append("Login", user.Login, cookieLogin); // создание cookie файла
                        CookieOptions cookiePas = new CookieOptions();
                        cookiePas.Expires = DateTime.Now.AddDays(7);
                        Response.Cookies.Append("Pas", user.Login, cookiePas); // создание cookie файла
                        IdNow = user.Id;                                       //return View(await db.Users.ToListAsync());
                        HttpContext.Session.SetString("ID_Users", Convert.ToString(user_log.Id));
                        //HttpContext.Session.SetString("Login_Users", user_log.Login);
                        //HttpContext.Session.SetString("Desc_Users", user_log.Description);
                        //HttpContext.Session.SetString("Password_Users", user_log.Password);
                        //    HttpContext.Session.SetString("Birth_Users", Convert.ToString(user_log.BirthDate));
                        //HttpContext.Session.SetString("Email_Users",user_log.Email);
                        // HttpContext.Session.SetString("Desc_Users", user_log.Description);// return RedirectToAction("Index");
                        //HttpContext.Session.SetString("ImageID", user_log.ID_Image.ToString());

                        if (user.Role_Id == 0)
                        {
                            return RedirectToAction("UserPage");
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                        //return RedirectToAction("UserPage");
                        // return $"Вот данные: {cookieValue}";
                    }

                }
                else
                {
                    User user_email = await db.Users.FirstOrDefaultAsync(predicate => predicate.Email == userLog.Login);
                    if (user_email != null)
                    {
                        User user_log = await db.Users.FirstOrDefaultAsync(predicate => predicate.Password == userLog.Password);
                        if (user_log != null)
                        {
                            CookieOptions cookieLogin = new CookieOptions();
                            cookieLogin.Expires = DateTime.Now.AddDays(7);
                            Response.Cookies.Append("Login", user_email.Login, cookieLogin); // создание cookie файла
                            CookieOptions cookiePas = new CookieOptions();
                            cookiePas.Expires = DateTime.Now.AddDays(7);
                            Response.Cookies.Append("Pas", user_email.Login, cookiePas); // создание cookie файла
                            IdNow = user_email.Id;                                       //return View(await db.Users.ToListAsync());
                            HttpContext.Session.SetString("ID_Users", Convert.ToString(user_log.Id));
                            //HttpContext.Session.SetString("Login_Users", user_log.Login);
                            //HttpContext.Session.SetString("Password_Users", user_log.Password);
                            //HttpContext.Session.SetString("Birth_Users", Convert.ToString(user_log.BirthDate));
                            //HttpContext.Session.SetString("Email_Users", user_log.Email);
                            //  HttpContext.Session.SetString("Desc_Users",user_log.Description);// return RedirectToAction("Index");
                            //HttpContext.Session.SetString("ImageID", user_log.ID_Image.ToString());

                            if (user.Role_Id == 0)
                            {
                                return RedirectToAction("UserPage");
                            }
                            else
                            {
                                return RedirectToAction("Index");
                            }
                            //return RedirectToAction("UserPage");
                            // return $"Вот данные: {cookieValue}";
                        }
                    }
                }

                // return RedirectToAction("RegOffer");
            }
            return RedirectToAction("RegOffer");


        }
        [HttpPost]

        public async Task<IActionResult> Index(LogUser userLog)
        {


            if (userLog != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(predicate => predicate.Login == userLog.Login);
                if (user != null)
                {
                    User user_log = await db.Users.FirstOrDefaultAsync(predicate => predicate.Password == userLog.Password);
                    if (user_log != null)
                    {
                        CookieOptions cookieLogin = new CookieOptions();
                        cookieLogin.Expires = DateTime.Now.AddDays(7);
                        Response.Cookies.Append("Login", user.Login, cookieLogin); // создание cookie файла
                        CookieOptions cookiePas = new CookieOptions();
                        cookiePas.Expires = DateTime.Now.AddDays(7);
                        Response.Cookies.Append("Pas", user.Login, cookiePas); // создание cookie файла
                        IdNow = user.Id;                                       //return View(await db.Users.ToListAsync());
                                                                               // return RedirectToAction("Index");
                        return RedirectToAction("UserPage");
                        //return RedirectToAction("UserPage");
                        // return $"Вот данные: {cookieValue}";
                    }
                }

                return RedirectToAction("RegOffer");
            }
            return RedirectToAction("RegOffer");


        }
        [HttpPost]

        public async Task<ActionResult> Registration(User user, DateTime date12, IFormFile file, int role)
        {
            user.BirthDate = date12;
            user.Role_Id = role;
         
            if (file != null)
            {
                string path = "/Files/" + file.FileName;
                int i = 0;
                while (System.IO.File.Exists(_app.WebRootPath + path))
                {
                    try
                    {
                        i++;
                        string a = Path.GetFileNameWithoutExtension(path);
                        if (a.Substring(a.Length - 3) == $"({i - 1})")
                        {
                            a = a.Substring(0, a.Length - 3);
                        }
                        path = "/Files/" + a + Convert.ToString("(" + i + ")") + Path.GetExtension(path);


                    }
                    catch
                    {

                    }

                }
                using (var fileStream = new FileStream(_app.WebRootPath + path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);


                    Images newfile = new Images
                    {
                       
                        ImageName = file.FileName,
                        PathImage = path,
                    };
                    db.Images.Add(newfile);
                    await db.SaveChangesAsync();
                    user.ID_Image = db.Images.FirstOrDefault(predicate => predicate.PathImage == path);
                    db.Users.Add(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("HomeEnter");
                }

            }
            return NotFound();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (user != null)
                {
                    return View(user);
                }
            }
            return NotFound();
        }
        public async Task<IActionResult> OtherUser(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.Include(b => b.ID_Image).FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (user != null)
                {
                    return View(user);
                }
            }
            return NotFound();
        }
        public async Task<IActionResult> DetailsImage(int? id)
        {
            if (id != null)
            {
                Images user = await db.Images.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (user != null)
                {
                    return View(user);
                }
            }
            return NotFound();
        }
        public async Task<IActionResult> RegOffer()
        {
            return View();
        }
        // [HttpPost]
        public async Task<IActionResult> UserPage()
        {
            //User user_log = await db.Users.FirstOrDefaultAsync(predicate => predicate.Id == id);
            //ViewData["LoginUser"] = user_log.Login;
            //ViewData["EmailUser"] = user_log.Email;
            //ViewData["ImageUser"] = user_log.ID_Image.PathImage;
            //ViewData["DateUser"] = user_log.BirthDate;
            //ViewData["DescUser"] = user_log.Description;
            //ViewBag.ID_User = HttpContext.Session.GetInt32("ID_Users");
            //ViewBag.Login = HttpContext.Session.GetString("Login_Users");
            //ViewBag.Email = HttpContext.Session.GetString("Email_Users");
            //ViewBag.Description = HttpContext.Session.GetString("Desc_Users");
            //ViewBag.Birth = HttpContext.Session.GetString("Birth_Users");
            int id = Int32.Parse(HttpContext.Session.GetString("ID_Users"));
            User user = await db.Users.Include(b => b.ID_Image).FirstOrDefaultAsync(predicate => predicate.Id == id);
            // ViewBag.ImageID = HttpContext.Session.GetString("ImageID");
            return View(user);
        }
        public async Task<ActionResult> ListUserReturn()
        {
            return RedirectToAction("ListUser");
        }
     
        public async Task<ActionResult> IndexImages(int? id, string path, string name, int page = 1, SortStateImage sortOrder = SortStateImage.ImageIdAsc)
        {

            // var users = db.Users.OrderBy(m => m.Id);
         
            IQueryable<Images> images = db.Images;
            //Фильтрация или поиск
            if (id != null && id > 0)
            {
                images = images.Where(p => p.Id == id);
            }
            if (!string.IsNullOrEmpty(name))
            {
                images = images.Where(p => p.ImageName.Contains(name));
            }
            //Сортировка
            switch (sortOrder)
            {
                case SortStateImage.ImageIdAsc:
                    {
                        images = images.OrderBy(m => m.Id);
                        break;
                    }
                case SortStateImage.ImageIdDesc:
                    {
                        images = images.OrderByDescending(m => m.Id);
                        break;
                    }
                case SortStateImage.ImageNameAsc:
                    {
                        images = images.OrderBy(m => m.ImageName);
                        break;
                    }
                case SortStateImage.ImageNameDesc:
                    {
                        images = images.OrderByDescending(m => m.ImageName);
                        break;
                    }
                case SortStateImage.ImagePathAsc:
                    {
                        images = images.OrderBy(m => m.PathImage);
                        break;
                    }
                case SortStateImage.ImagePathDesc:
                    {
                        images = images.OrderByDescending(m => m.PathImage);
                        break;
                    }
                default:
                    {
                        images = images.OrderBy(m => m.Id);
                        break;
                    }

            }
            //ПОГинация
            var count = await images.CountAsync();
            int pagesize = 4;
            var item = await images.Skip((page - 1) * pagesize).Take(pagesize).ToListAsync();
            IndexViewModel viewModelImage = new IndexViewModel
            {
                PageViewModel = new PageViewModel(count, page, pagesize),
                SortViewImage = new SortViewImage(sortOrder),
                FilterViewImage = new FilterViewImage(id, name, path),
                Image = item

            };
            return View(viewModelImage);
        }
        public async Task<IActionResult> ImagesIndex()
        {
            return RedirectToAction("IndexImages");
        }

        [HttpGet]
        [ActionName("DeleteImage")]
        public async Task<ActionResult> ConfirmDeleteImage(int? id)
        {
            if (id != null)
            {
                Images user = await db.Images.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (user != null)
                {
                    System.IO.File.Delete(_app.WebRootPath + user.PathImage);
                    return View(user);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id != null)
            {
                Images user = await db.Images.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (user != null)
                {
                    db.Images.Remove(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("IndexImages");
                }
            }
            return NotFound();
        }
        public async Task<IActionResult> Posts(int? id)
        {
            var post2Spravs = await db.Post2Sprav.Include(b=>b.Image_ID).Include(a=>a.Post_ID).Where(p=>p.Post_ID.User.Id== id).ToListAsync();
           
            IndexViewModel viewModel = new IndexViewModel
            {
                Post2Sprav = post2Spravs

            };
            
            return View(viewModel);
        }
        public async Task<IActionResult> PostsOther(int? id)
        {
            var post2Spravs = await db.Post2Sprav.Include(b => b.Image_ID).Include(a => a.Post_ID).Where(p => p.Post_ID.User.Id == id).ToListAsync();

            IndexViewModel viewModel = new IndexViewModel
            {
                Post2Sprav = post2Spravs

            };

            return View(viewModel);
        }
        public IActionResult PostAdd()
        {    
                return View();      
        }
        public async Task<IActionResult> Add_post(IFormFile file, Post post)
        {
            Post2Sprav sprav;
            sprav = new Post2Sprav();
            DateTime time= DateTime.Now;
            post.DatePost = time;
            int id = Int32.Parse(HttpContext.Session.GetString("ID_Users"));
            post.User = db.Users.FirstOrDefault(predicate => predicate.Id ==id);
            db.Posts.Add(post);
            await db.SaveChangesAsync();
            if (file != null)
            {
                string path = "/Files/" + file.FileName;
                int i = 0;
                while (System.IO.File.Exists(_app.WebRootPath + path))
                {
                    try
                    {
                        i++;
                        string a = Path.GetFileNameWithoutExtension(path);
                        if (a.Substring(a.Length - 3) == $"({i - 1})")
                        {
                            a = a.Substring(0, a.Length - 3);
                        }
                        path = "/Files/" + a + Convert.ToString("(" + i + ")") + Path.GetExtension(path);


                    }
                    catch
                    {

                    }

                }
                using (var fileStream = new FileStream(_app.WebRootPath + path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);


                    Images newfile = new Images
                    {

                        ImageName = file.FileName,
                        PathImage = path,
                    };
                 
                    db.Images.Add(newfile);
                    await db.SaveChangesAsync();

                    sprav.Post_ID =db.Posts.FirstOrDefault(predicate=>predicate.DatePost==post.DatePost);
                    sprav.Image_ID = db.Images.FirstOrDefault(predicate => predicate.PathImage == newfile.PathImage);
                    db.Post2Sprav.Add(sprav);
                    await db.SaveChangesAsync();
                    return RedirectToAction("UserPage");
                }

            }
            return NotFound();
        }
        public async Task<IActionResult> PostDelete(int? id)
        {
            if (id != null)
            {
                Post2Sprav post = await db.Post2Sprav.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (post != null)
                {
                    db.Post2Sprav.Remove(post);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Posts");
                }
            }
            return NotFound();
        }
        public async Task<ActionResult> IndexPosts(int? id, string path, string name, int page = 1, SortStatePost sortOrder = SortStatePost.PostIDAsc)
        {

            // var users = db.Users.OrderBy(m => m.Id);

            IQueryable<Post2Sprav> posts = db.Post2Sprav.Include(b => b.Image_ID).Include(a => a.Post_ID);
            //Фильтрация или поиск
            if (id != null && id > 0)
            {
                posts = posts.Where(p => p.Id == id);
            }
            if (!string.IsNullOrEmpty(name))
            {
                posts = posts.Where(p => p.Post_ID.Title.Contains(name));
            }
            //Сортировка
            switch (sortOrder)
            {
                case SortStatePost.PostIDAsc:
                    {
                        posts = posts.OrderBy(m => m.Id);
                        break;
                    }
                case SortStatePost.PostIDDesc:
                    {
                        posts = posts.OrderByDescending(m => m.Id);
                        break;
                    }
                case SortStatePost.PostTitleAsc:
                    {
                        posts = posts.OrderBy(m => m.Post_ID.Title);
                        break;
                    }
                case SortStatePost.PostTitleDesc:
                    {
                        posts = posts.OrderByDescending(m => m.Post_ID.Title);
                        break;
                    }
                case SortStatePost.PostDateAsc:
                    {
                        posts = posts.OrderBy(m => m.Post_ID.DatePost);
                        break;
                    }
                case SortStatePost.PostDateDesc:
                    {
                        posts = posts.OrderByDescending(m => m.Post_ID.DatePost);
                        break;
                    }
                default:
                    {
                        posts = posts.OrderBy(m => m.Id);
                        break;
                    }

            }
            //ПОГинация
            var count = await posts.CountAsync();
            int pagesize = 4;
            var item = await posts.Skip((page - 1) * pagesize).Take(pagesize).ToListAsync();
            IndexViewModel viewModelImage = new IndexViewModel
            {
                PageViewModel = new PageViewModel(count, page, pagesize),
                SortViewPosts = new SortViewPosts(sortOrder),
                FilterViewPost = new FilterViewPost(id,name),
                Post2Sprav = item

            };
            return View(viewModelImage);
        }
    }
}
