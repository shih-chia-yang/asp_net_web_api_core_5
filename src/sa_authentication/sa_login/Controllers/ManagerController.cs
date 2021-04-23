using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sa_login.Domain;

namespace sa_login.Controllers
{
    [Authorize(Policy="Admin")]
    public class ManagerController : Controller
    {

        private readonly IAuthorizationService _authorizationService;

        public ManagerController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> PayExpense(User inputModel)
        {
            var result = await _authorizationService.AuthorizeAsync(User, inputModel, "HasExpenseCredit");
            if(!result.Succeeded)
                return Forbid();
            return View();
        }


    }
}