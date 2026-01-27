using System;
using System.Web;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pouya.Models
{
    public class FeedbackPageMV
    {
        public FeedbackModel CreateForm {  get; set; }

        public List <Feedback> FeedBackList {  get; set; }
        
    }
}