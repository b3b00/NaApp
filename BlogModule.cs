namespace NaApp
{    
    using System.Collections.Generic;
    using System.Linq;    
    using System.Threading.Tasks;
    using Nancy;


    public class BlogModule : NancyModule
    {

        private object Home()
        {
            using (var db = new BloggingContext())
            {
                List<Blog> blogs = db.Blogs.Where(b => b.Url != null).ToList();
                Blog theBlog = blogs[0];
                List<Post> posts = db.Posts.Where(p => p.BlogId == theBlog.BlogId).ToList();

                return View["views/blog/home.sshtml", theBlog];
            }
        }


        private object ViewPost(int postId) 
        {
            using (var db = new BloggingContext())
            {
                List<Post> posts = db.Posts.Where(p => p.PostId == postId).ToList();
                return View["views/blog/post.sshtml", posts[0]];
            }
        }

        private object Add(dynamic form)
        {
            using (var db = new BloggingContext())
            {
                List<Blog> blogs = db.Blogs.Where(b => b.Url != null).ToList();
                Blog theBlog = blogs[0];

                //TODO : add the Posts
                Post newPost = new Post() {
                    Title = form.title,
                    Content = form.content,
                    Blog = theBlog,
                    BlogId = theBlog.BlogId
                };
                db.Posts.Add(newPost);
                db.SaveChanges();

                
                List<Post> posts = db.Posts.Where(p => p.BlogId == theBlog.BlogId).ToList();

                return View["views/blog/home.sshtml", theBlog];
            }
        }


        public BlogModule()
        {
            Get("/blog", _ => Home());

            Get("/blog/new",_ => View["views/blog/new.html",null]);

            Get("/blog/post/{id}",parameters  => ViewPost(parameters.id));

            Post("/blog/add", parameters =>  Add(this.Request.Form) );
        }

        
    }
}