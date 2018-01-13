using System.Web.Mvc;

namespace Sanabel.Presentation.Areas.Cases
{
    public class CasesAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Cases";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Cases_default",
                "Cases/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "Sanabel.Presentation.MVC.Areas.Cases.Controllers" }
            );
        }
    }
}