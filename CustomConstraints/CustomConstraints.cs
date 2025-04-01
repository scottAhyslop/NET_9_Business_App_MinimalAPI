
namespace NET_9_Business_App_MinimalAPI.CustomConstraints
{
    public class CustomConstraint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (!values.ContainsKey(routeKey)) return false;
            if (values[routeKey] is null) return false;
            
            if (values[routeKey].ToString().Equals("manager", 
                StringComparison.OrdinalIgnoreCase) || values[routeKey].ToString().Equals("developer", 
                StringComparison.OrdinalIgnoreCase)) return true;

            return false;
        }
    }
}
