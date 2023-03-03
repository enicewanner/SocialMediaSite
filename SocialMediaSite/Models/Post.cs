using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;


namespace SocialMediaSite.Models
{
	public class Post
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Poster { get; set; }
		public string? Text { get; set; }
		public string? Image { get; set; }

		public Post()
		{

		}

		public Post(string poster, string text, string? image)
		{
			this.Poster = poster;
			this.Text = text;
			this.Image = image;
		}




	}
}
