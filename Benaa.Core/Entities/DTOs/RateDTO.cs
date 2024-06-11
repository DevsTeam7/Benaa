
namespace Benaa.Core.Entities.DTOs
{
    public class RateDTORequest
	{

            public required float Stars { get; set; }
            public required Guid CourseId { get; set; }
            public string? Content { get; set; }


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
