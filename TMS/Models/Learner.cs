using System;
using System.Collections.Generic;
using System.Text;


namespace TMS.Models
{
    public class Learner
    {
        public string LearnerId { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }

        public Learner(string learnerId, string name,
                       string contactNumber, string email)
        {
            LearnerId = learnerId;
            Name = name;
            ContactNumber = contactNumber;
            Email = email;
        }
    }
}