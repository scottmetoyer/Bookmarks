using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bookmarks.Infrastructure
{
    public class StringSplitModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ValueProvider.GetValue(bindingContext.ModelName) == null)
            {
                return new string[] { };
            }

            string attemptedValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).AttemptedValue;
            return !String.IsNullOrEmpty(attemptedValue) ? attemptedValue.Split(',') : new string[] { };
        }
    }
}