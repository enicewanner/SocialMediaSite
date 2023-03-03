using SocialMediaSite.Models;

namespace SocialMediaSite.Interfaces
{
    public interface IDataAccessLayer
    {
        IEnumerable<Post> GetPosts();
        void AddPost(Post post);
        Post GetPost(int? id);

        IEnumerable<Profile> GetProfiles();
        void AddProfile(Profile profile);
        Profile GetProfile(string Username);
        Profile GetProfile(int id);

        void EditProfile (Profile profile);

        
    }
}
