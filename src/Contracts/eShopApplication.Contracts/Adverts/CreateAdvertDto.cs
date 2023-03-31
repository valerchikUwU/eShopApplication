using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Contracts.Adverts
{
    public class CreateAdvertDto
    {


        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; set; }



        public string Description { get; set; }
        

       
    }
}
