using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace TRMDataManager.App_Start
{

    /// <summary>
    /// 04 This opertion filter will add a parameter to every operation.
    /// Add an access token everytime to be able to authenticate
    /// </summary>
    
    public class AuthorizationOperationFilter : IOperationFilter
    {
        
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            // 04 check to see if there are no paramters
            if(operation.parameters == null)
            {
                operation.parameters = new List<Parameter>();
            }
            // 04 create and add the parameter
            operation.parameters.Add(new Parameter
            {
                name = "Authorization",
                @in = "header",
                description = "access token",
                required = false,
                type = "string"
            });
        }
    }
}