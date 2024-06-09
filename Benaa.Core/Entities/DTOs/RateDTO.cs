using Benaa.Core.Entities.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benaa.Core.Entities.DTOs
{
    public class RateDTORequest
	{

            public required float Stars { get; set; }
            public required Guid CourseId { get; set; }
            public required string Content { get; set; }


        //public class Response
        //{
        //    public Guid Id { get; set; }    
        //    public required float Stars { get; set; }
        //    public required Guid CourseId { get; set; }
        //    public DateTime CreatedAt { get; set; }  
        //    public required string FullName { get; set; }
        //    public required string Content { get; set; }

        //}
    }
}
