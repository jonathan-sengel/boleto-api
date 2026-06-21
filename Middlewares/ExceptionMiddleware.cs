using BoletoAPI.DTOs;
using BoletoAPI.Exceptions;

namespace BoletoAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(new ApiErroDto { Mensagem = ex.Message });
            }
            catch (RegraDeNegocioException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new ApiErroDto { Mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new ApiErroDto { Mensagem = ex.Message });
            }
        }
    }
}
