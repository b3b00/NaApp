namespace NaApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Nancy;


    public class BlogModule : NancyModule
    {

        private Blog GetTheBlog(BloggingContext db) {
            Blog theBlog = (from blg in db.Blogs 
                    where blg.Url != null
                    select blg).First();
            return theBlog;
        }

        private List<Post> GetPosts(BloggingContext db, Blog theBlog) {
            IEnumerable<Post> postEnum = from p in db.Posts
                            where p.BlogId == theBlog.BlogId
                            select p;
            List<Post> posts = postEnum.ToList<Post>();
            return posts;
        }

        private Post GetThePost(BloggingContext db, int postId) {
            Post post = (from p in db.Posts
                where p.PostId == postId 
                select p).First();
            return post;
        }

        private object Home()
        {
            using (var db = new BloggingContext())
            {
                Blog theBlog = GetTheBlog(db);

                List<Post> posts = GetPosts(db,theBlog);
                

                return View["views/blog/home.sshtml", theBlog];
            }
        }


        private object ViewPost(int postId) 
        {
            using (var db = new BloggingContext())
            {
                Post post = GetThePost(db,postId);
                // List<Post> posts = db.Posts.Where(p => p.PostId == postId).ToList();
                return View["views/blog/post.sshtml", post];
            }
        }

        private object Add(dynamic form)
        {
            using (var db = new BloggingContext())
            {
                 Blog theBlog = GetTheBlog(db);

                //TODO : add the Posts
                Post newPost = new Post() {
                    Title = form.title,
                    Content = form.content,
                    Blog = theBlog,
                    BlogId = theBlog.BlogId
                };
                db.Posts.Add(newPost);                
                db.SaveChanges();

                
                List<Post> posts = GetPosts(db,theBlog);

                return View["views/blog/home.sshtml", theBlog];
            }
        }

        private object Update(dynamic form)
        {
            using (var db = new BloggingContext())
            {
                Blog theBlog = GetTheBlog(db);

                Post thePost = GetThePost(db,form.postid);
               
                thePost.Content = form.Content;
                thePost.Title = form.Title;
                
                db.Posts.Update(thePost);
                db.SaveChanges();

                
                List<Post> posts = GetPosts(db,theBlog);

                return View["views/blog/home.sshtml", theBlog];
            }
        }

        private object ShowEdit(int postId)
        {
            using (var db = new BloggingContext())
            {
                Post post = GetThePost(db,postId);                
                return View["views/blog/edit.sshtml", post];
            }
            throw new NotImplementedException();
        }

        public BlogModule()
        {
            Get("/blog", _ => Home());

            Get("/blog/new",_ => View["views/blog/new.html",null]);

            Get("/blog/post/{id}",parameters  => ViewPost(parameters.id));

            Get("/blog/edit/{id}", parameters =>  ShowEdit(parameters.id) );

            Post("/blog/add", parameters =>  Add(this.Request.Form) );

            Post("/blog/update", parameters =>  Update(this.Request.Form) );

            
        }

        
    }
}