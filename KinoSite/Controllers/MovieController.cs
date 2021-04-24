using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using KinoSite.Models;
using KinoSite.Data;

namespace KinoSite.Controllers
{
    public class MovieController : Controller
    {
        ApplicationContext context;
        public MovieController(ApplicationContext context)
        {
            this.context = context;
        }
    }
}
