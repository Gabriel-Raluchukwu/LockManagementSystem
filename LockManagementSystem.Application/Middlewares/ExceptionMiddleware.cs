using System.Net;
using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Application.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LockManagementSystem.Application.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware>_logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext httpContext)
    {
        var response = httpContext.Response;
        response.ContentType = "application/json";
        var responseModel = new ResponseModel<string>();
        try
        {
            await _next.Invoke(httpContext);
        }
        catch (BadRequestException badRequestException)
        {
            _logger.LogError(badRequestException, Constants.ExceptionHandlerMessage);
            response.StatusCode = (int) HttpStatusCode.BadRequest;
            responseModel.Message = badRequestException.Message;
            
            await WriteResponse(response, responseModel);
        }
        catch (NotFoundException notFoundException)
        {
            _logger.LogError(notFoundException, Constants.ExceptionHandlerMessage);
            response.StatusCode = (int) HttpStatusCode.NotFound;
            responseModel.Message = notFoundException.Message;
            
            await WriteResponse(response, responseModel);
        }
        catch (UnauthorizedAccessException unauthorizedAccessException)
        {
            _logger.LogError(unauthorizedAccessException, "Unauthorized Access");
            response.StatusCode = (int) HttpStatusCode.Unauthorized;
            responseModel.Message = unauthorizedAccessException.Message;
            
            await WriteResponse(response, responseModel);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, Constants.ExceptionHandlerMessage);
            response.StatusCode = (int) HttpStatusCode.InternalServerError;
            responseModel.Message = "Server error.";

            await WriteResponse(response, responseModel);
        }
    }

    private async Task WriteResponse(HttpResponse response, ResponseModel<string> responseModel)
    {
        var responseString = JsonConvert.SerializeObject(responseModel);
        await response.WriteAsync(responseString);
    }
}