using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HFWEBAPI.Models;
using Microsoft.Extensions.Configuration;
using HFWEBAPI.DataAccess;

namespace HFWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private IConfiguration config;

        public ArticleController(IConfiguration Configuration)
        {
            config = Configuration;
        }

        //[Route("api/[controller]")]
        [HttpGet]
        public async Task<IEnumerable<ArticleEntity>> Get()
        {
            IArticleRepository<ArticleEntity> Respository = new ArticleRepository<ArticleEntity>(config);
            var results = await Respository.GetItemsAsync("Article");
            List<ArticleEntity> articles = new List<ArticleEntity>();
            foreach (var result in results)
            {
                //if (result.isActive == true)
                //{
                    articles.Add(result);
                //}
            }
            return articles;
        }

        //[Route("api/[controller]/{id:int}")]
        [HttpGet("{Id}")]
        public async Task<ArticleEntity> GetAsync(string Id)
        {
            IArticleRepository<ArticleEntity> Respository = new ArticleRepository<ArticleEntity>(config);
            var articles = await Respository.GetItemsAsync(d => d.id == Id && d.isActive == true, "Article");
            ArticleEntity article = new ArticleEntity();
            foreach (var ar in articles)
            {
                article = ar;
                break;
            }
            return article;
        }

        //[Route("api/[controller]")]
        [HttpPost]
        public async Task<bool> Post([FromBody] ArticleEntity article)
        {
            try
            {
                IArticleRepository<ArticleEntity> Respository = new ArticleRepository<ArticleEntity>(config);
                //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                //var user = JsonConvert.DeserializeObject<UserEntity>(requestBody);

                article.id = null;
                await Respository.CreateItemAsync(article, "Article");

                return true;
            }
            catch
            {
                return false;
            }
        }

        //[Route("api/[controller]/{id:int}")]
        [HttpPut]
        public async Task<bool> Put([FromBody] ArticleEntity article)
        {
            try
            {
                IArticleRepository<ArticleEntity> Respository = new ArticleRepository<ArticleEntity>(config);
                //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                //var user = JsonConvert.DeserializeObject<UserEntity>(requestBody);

                await Respository.UpdateItemAsync(article.id, article, "Article");

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
