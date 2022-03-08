using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheanSMS
{
   public class GetallSMSDto
    {
        public bool Status_cd { get; set; }

        public List<GetAllSMSDetailsDTO> Data { get; set; }
        public string status_Desc { get; set; }
    }

    public class GetAllSMSDetailsDTO
    {
        public string PhoneNo { get; set; }

        public string Message { get; set; }
        public bool Acknowledgement { get; set; }
    }
}