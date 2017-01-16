namespace NaApp
{
    using Microsoft.AspNetCore.Builder;
    using Nancy.Owin;
    using System.Linq;

    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            using (var db = new BloggingContext())
            {
                ;
                // if (db.Blogs.ToList().Count == 0)
                // {
                //     Blog myBlog = new Blog { Url = "http://localhost:5000/blog", Name="Test Blog" };
                //     db.Blogs.Add(myBlog);
                //     db.SaveChanges();
                // }
            }
            app.UseOwin(x => x.UseNancy());
        }
    }

}
