using System;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PokeAPI.Utilities {
public class JsonModelBinder : IModelBinder {
    public Task BindModelAsync(ModelBindingContext bindingContext) {
      if (bindingContext == null) {
        throw new ArgumentNullException(nameof(bindingContext));
      }

      var valueProviderResult = bindingContext.ValueProvider
        .GetValue(bindingContext.ModelName);
      if (valueProviderResult != ValueProviderResult.None) {
        bindingContext.ModelState
          .SetModelValue(bindingContext.ModelName, valueProviderResult);

        var valueAsString = valueProviderResult.FirstValue;
        var result = JsonConvert.DeserializeObject(valueAsString, bindingContext.ModelType);
        if (result != null) {
          bindingContext.Result = ModelBindingResult.Success(result);
          return Task.CompletedTask;
        }
      }

      return Task.CompletedTask;
    }
  }
}
