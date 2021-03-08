using System.Collections.Generic;
using System.Linq;
using MyFace.Models.Database;
using System.Security.Cryptography;
using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace MyFace.Data
{
    public static class SampleUsers
    {
        public static int NumberOfUsers = 100;
        
        private static IList<IList<string>> _data = new List<IList<string>>
        {
            new List<string> { "Kania", "Placido", "kplacido0", "kplacido0@qq.com" },
            new List<string> { "Scotty", "Gariff", "sgariff1", "sgariff1@biblegateway.com" },
            new List<string> { "Colly", "Burgiss", "cburgiss2", "cburgiss2@amazon.co.uk" },
            new List<string> { "Barnie", "Percival", "bpercival3", "bpercival3@cmu.edu" },
            new List<string> { "Brandon", "Narraway", "bnarraway4", "bnarraway4@trellian.com" },
            new List<string> { "Carlos", "Sakins", "csakins5", "csakins5@spiegel.de" },
            new List<string> { "Zeke", "Barkworth", "zbarkworth6", "zbarkworth6@symantec.com" },
            new List<string> { "Henrietta", "Verick", "hverick7", "hverick7@adobe.com" },
            new List<string> { "Maxine", "Lovett", "mlovett8", "mlovett8@mac.com" },
            new List<string> { "Adorne", "Smyth", "asmyth9", "asmyth9@nyu.edu" },
            new List<string> { "Roslyn", "O' Scallan", "roscallana", "roscallana@marriott.com" },
            new List<string> { "Kalindi", "Bevington", "kbevingtonb", "kbevingtonb@wired.com" },
            new List<string> { "Silva", "Cow", "scowc", "scowc@yellowbook.com" },
            new List<string> { "Gnni", "Northage", "gnorthaged", "gnorthaged@lulu.com" },
            new List<string> { "Jobi", "Balsom", "jbalsome", "jbalsome@ox.ac.uk" },
            new List<string> { "Laurice", "Galgey", "lgalgeyf", "lgalgeyf@usa.gov" },
            new List<string> { "Orsola", "Laurant", "olaurantg", "olaurantg@reverbnation.com" },
            new List<string> { "Dante", "Mabley", "dmableyh", "dmableyh@scientificamerican.com" },
            new List<string> { "Shantee", "Guillond", "sguillondi", "sguillondi@gravatar.com" },
            new List<string> { "Mendy", "Djuricic", "mdjuricicj", "mdjuricicj@tuttocitta.it" },
            new List<string> { "Ralph", "Adamovicz", "radamoviczk", "radamoviczk@salon.com" },
            new List<string> { "Zsa zsa", "Goodacre", "zgoodacrel", "zgoodacrel@histats.com" },
            new List<string> { "Fleurette", "Blow", "fblowm", "fblowm@who.int" },
            new List<string> { "Zelda", "Pritchett", "zpritchettn", "zpritchettn@wordpress.org" },
            new List<string> { "Kaitlyn", "Filshin", "kfilshino", "kfilshino@so-net.ne.jp" },
            new List<string> { "Aube", "Goneau", "agoneaup", "agoneaup@barnesandnoble.com" },
            new List<string> { "Natala", "Mackrill", "nmackrillq", "nmackrillq@google.es" },
            new List<string> { "Ev", "Wadly", "ewadlyr", "ewadlyr@adobe.com" },
            new List<string> { "Aurora", "Feedham", "afeedhams", "afeedhams@house.gov" },
            new List<string> { "Farly", "Chestney", "fchestneyt", "fchestneyt@usda.gov" },
            new List<string> { "Chico", "Guilloux", "cguillouxu", "cguillouxu@senate.gov" },
            new List<string> { "Julianna", "Huckstepp", "jhucksteppv", "jhucksteppv@ycombinator.com" },
            new List<string> { "Bev", "Sancto", "bsanctow", "bsanctow@spiegel.de" },
            new List<string> { "Shara", "Jeeves", "sjeevesx", "sjeevesx@behance.net" },
            new List<string> { "Legra", "Jereatt", "ljereatty", "ljereatty@prnewswire.com" },
            new List<string> { "Katey", "Ternouth", "kternouthz", "kternouthz@webmd.com" },
            new List<string> { "Val", "McMenamin", "vmcmenamin10", "vmcmenamin10@noaa.gov" },
            new List<string> { "Shannan", "Greenhalf", "sgreenhalf11", "sgreenhalf11@gravatar.com" },
            new List<string> { "Sterling", "Fellgate", "sfellgate12", "sfellgate12@surveymonkey.com" },
            new List<string> { "Brina", "Dickens", "bdickens13", "bdickens13@zimbio.com" },
            new List<string> { "Boniface", "McKaile", "bmckaile14", "bmckaile14@jalbum.net" },
            new List<string> { "Vincenty", "Aishford", "vaishford15", "vaishford15@wordpress.org" },
            new List<string> { "Kathye", "Gauford", "kgauford16", "kgauford16@xrea.com" },
            new List<string> { "Chico", "Seelbach", "cseelbach17", "cseelbach17@over-blog.com" },
            new List<string> { "Enoch", "Winsper", "ewinsper18", "ewinsper18@devhub.com" },
            new List<string> { "Brandie", "Welds", "bwelds19", "bwelds19@chicagotribune.com" },
            new List<string> { "Bethanne", "Kerin", "bkerin1a", "bkerin1a@acquirethisname.com" },
            new List<string> { "Margo", "Tompkins", "mtompkins1b", "mtompkins1b@mayoclinic.com" },
            new List<string> { "Allie", "Clever", "aclever1c", "aclever1c@slideshare.net" },
            new List<string> { "North", "Denny", "ndenny1d", "ndenny1d@simplemachines.org" },
            new List<string> { "Ted", "Scorah", "tscorah1e", "tscorah1e@devhub.com" },
            new List<string> { "Leone", "McGow", "lmcgow1f", "lmcgow1f@unblog.fr" },
            new List<string> { "Abbie", "Jannasch", "ajannasch1g", "ajannasch1g@sakura.ne.jp" },
            new List<string> { "Marika", "Dommett", "mdommett1h", "mdommett1h@infoseek.co.jp" },
            new List<string> { "Edith", "Norcop", "enorcop1i", "enorcop1i@behance.net" },
            new List<string> { "Ernest", "Baline", "ebaline1j", "ebaline1j@amazon.co.uk" },
            new List<string> { "Rowen", "Dorcey", "rdorcey1k", "rdorcey1k@gravatar.com" },
            new List<string> { "Pasquale", "Surplice", "psurplice1l", "psurplice1l@paypal.com" },
            new List<string> { "Tim", "Dyott", "tdyott1m", "tdyott1m@yellowbook.com" },
            new List<string> { "Tedd", "Connachan", "tconnachan1n", "tconnachan1n@so-net.ne.jp" },
            new List<string> { "Jacquetta", "McClelland", "jmcclelland1o", "jmcclelland1o@nifty.com" },
            new List<string> { "Nelli", "Maund", "nmaund1p", "nmaund1p@myspace.com" },
            new List<string> { "Morie", "Anselmi", "manselmi1q", "manselmi1q@nytimes.com" },
            new List<string> { "Gabie", "Antoniazzi", "gantoniazzi1r", "gantoniazzi1r@dailymail.co.uk" },
            new List<string> { "Menard", "Engelbrecht", "mengelbrecht1s", "mengelbrecht1s@over-blog.com" },
            new List<string> { "Mike", "Tommasetti", "mtommasetti1t", "mtommasetti1t@bluehost.com" },
            new List<string> { "Eldin", "Fredy", "efredy1u", "efredy1u@mozilla.com" },
            new List<string> { "Pris", "McCowen", "pmccowen1v", "pmccowen1v@jalbum.net" },
            new List<string> { "Joey", "Dossettor", "jdossettor1w", "jdossettor1w@webnode.com" },
            new List<string> { "Daisey", "Ogdahl", "dogdahl1x", "dogdahl1x@nyu.edu" },
            new List<string> { "Melanie", "Searle", "msearle1y", "msearle1y@loc.gov" },
            new List<string> { "Beatrix", "MacLise", "bmaclise1z", "bmaclise1z@hugedomains.com" },
            new List<string> { "Missy", "Hillitt", "mhillitt20", "mhillitt20@multiply.com" },
            new List<string> { "Belinda", "Tumielli", "btumielli21", "btumielli21@php.net" },
            new List<string> { "Raynor", "Dupey", "rdupey22", "rdupey22@fc2.com" },
            new List<string> { "Inigo", "Heineke", "iheineke23", "iheineke23@ihg.com" },
            new List<string> { "Ilaire", "Angric", "iangric24", "iangric24@apache.org" },
            new List<string> { "Estel", "Steljes", "esteljes25", "esteljes25@livejournal.com" },
            new List<string> { "Lyell", "Ashard", "lashard26", "lashard26@umn.edu" },
            new List<string> { "Darren", "Devons", "ddevons27", "ddevons27@economist.com" },
            new List<string> { "Warden", "Undrell", "wundrell28", "wundrell28@mozilla.org" },
            new List<string> { "Iris", "Langworthy", "ilangworthy29", "ilangworthy29@timesonline.co.uk" },
            new List<string> { "Marten", "Minards", "mminards2a", "mminards2a@statcounter.com" },
            new List<string> { "Kerry", "Bennion", "kbennion2b", "kbennion2b@spotify.com" },
            new List<string> { "Olivette", "Norridge", "onorridge2c", "onorridge2c@cpanel.net" },
            new List<string> { "Rosalinde", "Traske", "rtraske2d", "rtraske2d@bbc.co.uk" },
            new List<string> { "Gaultiero", "McCard", "gmccard2e", "gmccard2e@utexas.edu" },
            new List<string> { "Zonnya", "Capstaff", "zcapstaff2f", "zcapstaff2f@theatlantic.com" },
            new List<string> { "Ingelbert", "Sleford", "isleford2g", "isleford2g@qq.com" },
            new List<string> { "Nicol", "Nary", "nnary2h", "nnary2h@google.com.hk" },
            new List<string> { "Jasun", "Lukianov", "jlukianov2i", "jlukianov2i@blogs.com" },
            new List<string> { "Alison", "Durkin", "adurkin2j", "adurkin2j@sitemeter.com" },
            new List<string> { "Claudius", "Coronas", "ccoronas2k", "ccoronas2k@tamu.edu" },
            new List<string> { "Jakie", "Keener", "jkeener2l", "jkeener2l@princeton.edu" },
            new List<string> { "Gilbertine", "Wynett", "gwynett2m", "gwynett2m@epa.gov" },
            new List<string> { "Syd", "Cordelle", "scordelle2n", "scordelle2n@blogger.com" },
            new List<string> { "Stafford", "Deport", "sdeport2o", "sdeport2o@wix.com" },
            new List<string> { "Zacharie", "Perchard", "zperchard2p", "zperchard2p@qq.com" },
            new List<string> { "Jane", "Iceton", "jiceton2q", "jiceton2q@lulu.com" },
            new List<string> { "Marjy", "Beadell", "mbeadell2r", "mbeadell2r@delicious.com" }
        };
        
        public static IEnumerable<User> GetUsers()
        {
            return Enumerable.Range(0, NumberOfUsers).Select(CreateRandomUser);
        }

        private static User CreateRandomUser(int index)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
    
            string FakePassword = "password";

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string HashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: FakePassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return new User
            {
                FirstName = _data[index][0],
                LastName = _data[index][1],
                Username = _data[index][2],
                Email = _data[index][3],
                ProfileImageUrl = ImageGenerator.GetProfileImage(_data[index][2]),
                CoverImageUrl = ImageGenerator.GetCoverImage(index),
                Hashed_password = HashedPassword,
                Salt = Convert.ToBase64String(salt)
            };
        }
    }
}