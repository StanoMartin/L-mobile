using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using LMobile2.Models;

namespace LMobile2
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            // Web API configuration and services
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<ServiceAuftrag>("ServiceAuftrage");
            builder.EntitySet<ArbeitsZeitMeldung>("ArbeitsZeitMeldungs");
            builder.EntitySet<Kunde>("Kundes");
            builder.EntitySet<Machine>("Machines");
            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: "oserviceauftragdata",
                model: builder.GetEdmModel());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
    }
}
