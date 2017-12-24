using System.Web.Mvc;

namespace Sanabel.Presentation.Areas.Volunteers
{
    public class VolunteersAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Volunteers";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Volunteers_default",
                "Volunteers/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}