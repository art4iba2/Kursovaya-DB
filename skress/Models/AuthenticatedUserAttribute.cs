using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace skress.Models
{
    public class AuthenticatedUserAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Session.TryGetValue("Role", out _))
            {
                // Если идентификатор сотрудника не найден в сессии, перенаправляем пользователя на страницу входа
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Privacy",
                }));
            }

            base.OnActionExecuting(context);
        }
    }
}