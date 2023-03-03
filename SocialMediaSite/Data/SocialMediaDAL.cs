using SocialMediaSite.Interfaces;
using SocialMediaSite.Models;

namespace SocialMediaSite.Data
{
    public class SocialMediaDAL : IDataAccessLayer
    {
        private ApplicationDbContext db;
            
        public SocialMediaDAL(ApplicationDbContext indb)
        {
            this.db = indb;
        }

        public void AddPost(Post post)
        {
            db.Posts.Add(post);
            db.SaveChanges();
        }

        public void AddProfile(Profile profile)
        {
            db.Profiles.Add(profile);
            db.SaveChanges();
        }

		public void EditProfile(Profile profile)
		{
			db.Profiles.Update(profile);
            db.SaveChanges();
		}

		public Post GetPost(int? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetPosts()
        {
            return db.Posts;
        }

        public Profile GetProfile(string UserName)
        {
            return db.Profiles.Where(p => p.Username == UserName).FirstOrDefault();
        }

		public Profile GetProfile(int id)
		{
			return db.Profiles.Where(p => p.Id == id).FirstOrDefault();
		}

		public IEnumerable<Profile> GetProfiles()
        {
            return db.Profiles;
        }
    }
}
