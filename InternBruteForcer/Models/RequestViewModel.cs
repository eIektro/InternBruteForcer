using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDOS_Saldırı.Models
{
    public class RequestViewModel
    {
        //    { formUrl: formUrl, formUsername: formUsername, formPassword: formPassword, formRequestsNumber: formRequestsNumber
        //}
        public string formUrl { get; set; }
        public string formUsernameAreaId { get; set; }
        public string formPasswordAreaId { get; set; }

        public string formLoginButtonId { get; set; }
        public string formRequestsNumber { get; set; }

       
        

    }
}
