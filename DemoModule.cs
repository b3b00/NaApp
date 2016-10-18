namespace NaApp
{
    using System.Text;
    using Nancy;


    public class Model {
        public string Value {get; set;}
    }

    public class HomeModule : NancyModule
    {

        public HomeModule()
        {
           
            Get("/demo", _ => View["views/demo/index.html", null]);
            
            Get("/demo/nancy", _ => View["views/demo/nancy.html", null]);
            
            Get("/demo/hello/{who}",parameters => View["views/demo/hello.sshtml", new Model{Value= parameters.who}]);
        

            Get("/demo/form", _ => View["views/demo/form.html",null]);

            Post("/demo/form/set", parameters => View["views/demo/result.sshtml",new Model{Value= this.Request.Form.In}]); 
            
        }
    }
}
