using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFormPractice.Domain.DTOs.Response
{
    public class CustomQuestionDTO
    {
        public string QuestionId { get; set; }
        public string QuestionType { get; set; }
        public string Question { get; set; }
        public List<string> Choices { get; set; }
        public int MaxChoicesAllowed { get; set; }
    }
}
