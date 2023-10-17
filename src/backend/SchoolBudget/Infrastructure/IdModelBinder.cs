using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SchoolBudget.Infrastructure;

public class IdModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext is null)
            throw new ArgumentNullException(nameof(bindingContext));

        var modelName = bindingContext.ModelName;

        // Try to fetch the value of the argument by name
        var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

        var value = valueProviderResult.FirstValue;
        // Check if the argument value is null or empty

        if (string.IsNullOrEmpty(value))
            return Task.CompletedTask;

        if (!long.TryParse(value, out var id))
        {
            // Non-integer arguments result in model state errors
            bindingContext.ModelState.TryAddModelError(
                modelName, "Id must be a long integer.");

            return Task.CompletedTask;
        }

        var model = Activator.CreateInstance(bindingContext.ModelType, id);
        bindingContext.Result = ModelBindingResult.Success(model);
        return Task.CompletedTask;
    }
}