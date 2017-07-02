using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rest.Repository;
using Rest.Models;

namespace Rest.Controllers
{
    [Route("[controller]")]
    public class PostsController : Controller
    {

        [HttpGet]
        public IEnumerable<Post> GetPosts()
        {
            ConnectManager c = new ConnectManager();

            //for(int i = 0; i < 20; i++)
            //c.CreatePost(new Post {
            //    Name = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
            //    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
            //});

            return c.GetPosts();
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetPostById(int id)
        {
            ConnectManager c = new ConnectManager();

            Post post = c.GetPostById(id);
            if (post != null)
            {
                return Ok(post);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult CreatePost([FromBody]Post post)
        {
            ConnectManager c = new ConnectManager();

            if(c.CreatePost(post))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut]
        public IActionResult UpdatePost([FromBody]Post post)
        {
            ConnectManager c = new ConnectManager();
            if (c.UpdatePost(post))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeletePost(int id)
        {
            ConnectManager c = new ConnectManager();
            if(c.DeletePost(id))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
