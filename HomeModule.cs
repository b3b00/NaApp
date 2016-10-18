namespace NaApp
{
    using System.Text;
    using Nancy;


    public class Model {
        public string Value {get; set;}
    }

    public class HomeModule : NancyModule
    {

        public Response sendHtml(string html) {
             var htmlBytes = Encoding.UTF8.GetBytes(html);
            Response response = new Response() {
                ContentType = "text/html",
                Contents = s => s.Write(htmlBytes,0,htmlBytes.Length)
            };
            return response;
        }

        public HomeModule()
        {
           
            Get("/", _ => View["index.html", null]);
            
            Get("/nancy", _ => View["nancy.html", null]);
            
            Get("/hello/{who}",parameters => View["hello.sshtml", new Model{Value= parameters.who}]);
        

            Get("/form", _ => View["form.html",null]);

            Post("/form/set", parameters => View["result.sshtml",new Model{Value= this.Request.Form.In}]); 
            
        }
    }
}
