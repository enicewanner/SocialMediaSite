using System.ComponentModel.DataAnnotations;

namespace SocialMediaSite.Models
{
	public class Profile
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Username { get; set; }


		public string Description { get; set; }

		public string ProfileImage { get; set; }


		public Profile()
		{

		}

	}
}
