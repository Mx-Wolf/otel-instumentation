using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Aweton.Mxw.BackEndApi.Controllers
{
  public class ResponseExceptionFilter(ILogger<ResponseExceptionFilter> logger):IActionFilter, IOrderedFilter
  {
    public void OnActionExecuting(ActionExecutingContext context){}

    public void OnActionExecuted(ActionExecutedContext context)
    {
      context.Result = context.Exception switch
      {
        ValidationException validationException => WithBadRequest(context, validationException),
        _ => context.Result,
      };
    }

    private IActionResult WithBadRequest(ActionExecutedContext context, ValidationException validationException)
    {
      logger.ValidationError(validationException);
      context.ExceptionHandled = true;
      return new ObjectResult(validationException.Errors)
      {
        StatusCode = (int)HttpStatusCode.BadRequest
      };
    }

    public int Order => int.MaxValue - 1000;
  }
}