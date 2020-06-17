using System;
using System.Linq;
using System.Threading.Tasks;
using WordPressPCL;
using WordPressPCL.Models;

namespace WordPressTest
{
    class Program
    {
        static void Main(string[] args)
        {
            CreatePost().Wait();
        }

        private static async Task CreatePost()
        {
            try
            {
                WordPressClient client = await GetClient();
                if (await client.IsValidJWToken())
                {
                    var post = new Post
                    {
                        Title = new Title("Post FROM API with JWT"),
                        Content = new Content("Test de autenticacion con JWT y crear un post")
                    };
                    await client.Posts.Create(post);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
            }
        }

        private static async Task<WordPressClient> GetClient()
        {
            // JWT authentication
            var client = new WordPressClient("http://192.168.0.7:8080/wp-json/");
            client.AuthMethod = AuthMethod.JWT;
            await client.RequestJWToken("cristian", "123456");
            return client;
        }
    }
}