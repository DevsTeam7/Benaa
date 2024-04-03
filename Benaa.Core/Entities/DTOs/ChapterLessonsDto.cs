namespace Benaa.Core.Entities.DTOs
{
    public class ChapterLessonsDto
    {
        public required CreateChapterDto createChapter { get; set; }
        public required List<CreateLessonDto> createLessons { get; set; }
    }
}
