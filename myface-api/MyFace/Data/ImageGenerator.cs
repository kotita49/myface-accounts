using System.Collections.Generic;
using System.Linq;

namespace MyFace.Data
{
    public class ImageGenerator
    {
        private static IEnumerable<int> _brokenImages = new List<int>
        {

        };
        
        public static string GetPostImage(int index)
        {
            return GetImage(100 + index, 1600, 900);
        }

        public static string GetProfileImage(string username)
        {
            return $"https://robohash.org/{username}?set=any&bgset=any";
        }

        public static string GetCoverImage(int index)
        {
            return GetImage(600 + index, 2400, 900);
        }

        private static string GetImage(int index, int width, int height)
        {
            if (_brokenImages.Contains(index))
            {
                return null;
            }

            return $"https://picsum.photos/id/{index}/{width}/{height}";
        }
    }
}