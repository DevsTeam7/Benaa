using Benaa.Core.Entities.General;

namespace Benaa.Core.Entities.DTOs
{
    public class ChapterLessonsDto
    {
        public required CreateChapterDto createChapter { get; set; }
        public required List<CreateLessonDto> createLessons { get; set; }

        //public class Respnse
        //{
        //    public required CourseChapter courseChapter { get; set; }
        //    public required List<CourseLesson> courseLessons { get; set; }
        //}
    }
}
