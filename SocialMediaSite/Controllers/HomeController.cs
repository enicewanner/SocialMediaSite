using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaSite.Models;
using SocialMediaSite.Interfaces;
using System.Security.Claims;
using System.Diagnostics;
using Microsoft.Extensions.Hosting;

namespace SocialMediaSite.Controllers
{
	

	[Authorize]
	public class HomeController : Controller
	{

		IDataAccessLayer dal;
		public HomeController(IDataAccessLayer indal)
		{
			dal = indal;
		}

		[AllowAnonymous]
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[AllowAnonymous]
		public IActionResult About()
		{
			return View();
		}

		[HttpGet]
		public IActionResult EditProfile(int id)
		{
			//if (Username == null)
			//	return NotFound();

			Profile targetProfile = dal.GetProfile(id);

			//if (targetProfile == null)
			//	return NotFound();

			return View(targetProfile);
		}

		[HttpPost]
		public IActionResult EditProfile(Profile profile)
		{
			profile.Username = dal.GetProfile(User.FindFirstValue(ClaimTypes.Name)).Username;
			dal.EditProfile(profile);
			return View();
		}

        public IActionResult MyPage()
        {
			string user = User.FindFirstValue(ClaimTypes.Name);
			//int currentUser = User.Identities.
			Profile profile;

			//foreach(Profile prof in dal.GetProfiles())
			//{
			//	if (prof.Username == user)
			//	{
			//		profile = dal.GetProfile(user);
			//		return View(profile);

			//	}

			//}

			profile = dal.GetProfile(user);

			//CreateProfile(profile);
			return View(profile);
			


        }
        public IActionResult OtherPage()
        {
			//Profile profile = dal.GetProfile();

            return View();
        }

		public void CreateProfile(Profile profile)
		{
			if (ModelState.IsValid)
			{
				profile.Username = User.FindFirstValue(ClaimTypes.Name);
				profile.Description = "Edit in Edit Profile Page";
				profile.ProfileImage = "https://static.vecteezy.com/system/resources/thumbnails/009/292/244/small/default-avatar-icon-of-social-media-user-vector.jpg";
				dal.AddProfile(profile);
			}
		}

		public IActionResult Feed()
		{
			return View(dal.GetPosts());
		}

		[HttpGet]
		public IActionResult AddPost()
		{
			return View();
		}

		[HttpPost]
		public IActionResult AddPost(Post post)
		{
			if (ModelState.IsValid)
			{
				post.Poster = dal.GetProfile(User.FindFirstValue(ClaimTypes.Name)).Username;
				dal.AddPost(post);
				return RedirectToAction("Feed", "Home");
			}

			return View();
		}

		public IActionResult UserPosts(int id) 
		{
			return View(dal.GetPosts().Where(n => n.Poster == (dal.GetProfile(id)).Username));
		}



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}