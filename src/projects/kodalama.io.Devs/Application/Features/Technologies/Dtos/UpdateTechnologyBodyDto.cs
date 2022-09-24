using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Dtos
{
    public class UpdateTechnologyBodyDto
    {
        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; }
    }
}
