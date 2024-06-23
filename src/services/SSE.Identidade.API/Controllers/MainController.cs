using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SSE.Identidade.API.Controllers
{
    public abstract class MainController : Controller 
    {

        protected ICollection<string> Errors = new List<string>();
        protected ActionResult CustomResponse(object result = null)
        {
            if (OperationValid())
            {
                return Ok (result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                {"Mensagens:", Errors.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState) 
        {
            var errors = modelState.Values.SelectMany(equals => equals.Errors);
            foreach (var error in errors) 
            { 
                AddErrorProcess(error.ErrorMessage);
            }

            return CustomResponse();
        }

        protected bool OperationValid()
        {
            return !Errors.Any();
        }

        protected void AddErrorProcess(string error)
        {
            Errors.Add(error);
        }

        protected void RemoveErrorsProcess()
        {
            Errors.Clear();
        }
    }
}
