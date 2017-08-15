using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GTE_API.Models
{
    public class QQLogin
    {
        [Required(ErrorMessage ="Username is required")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }

        string _phoneNum;
        public string SearchValue
        {
            get
            {
                try
                {
                    // strip the +1 from the phone number if they are there
                    if (_phoneNum.Contains("+"))
                        _phoneNum = _phoneNum.Remove(0, 1);

                    if (_phoneNum.Substring(0, 1) == "1")
                        _phoneNum = _phoneNum.Remove(0, 1);

                }
                catch { }

                return _phoneNum;
            }
            set
            {
                _phoneNum = value;
            }
        }


        
    }
}