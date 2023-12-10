using FluentValidation.AspNetCore;
using FluentValidation;
using GameHub.Web.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GameHub.Web.Infrastructure.Configurations
{
    public static class ValidationConfiguration
    {
        private const string ErrorMessage = "Validation Error";

        public static void InitValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation(v =>
            {
                v.DisableDataAnnotationsValidation = true;
            });

            services.AddValidatorsFromAssemblyContaining<Program>();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = c =>
                {
                    var errors = c.ModelState.Keys
                        .Where(key => (c.ModelState[key]?.Errors?.Count ?? default) > default(int))
                        .ToDictionary(k => k, k => c.ModelState[k]!.Errors.Select(e => e.ErrorMessage).ToArray());

                    return new BadRequestObjectResult(new ApiResponse<ValidationErrorResponse>(ErrorMessage, StatusCodes.Status400BadRequest)
                    {
                        Payload = new ValidationErrorResponse
                        {
                            Errors = errors
                        }
                    });
                };
            });
        }
    }
}
