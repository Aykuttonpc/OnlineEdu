using OnlineEdu.Entity.Entities;

namespace OnlineEdu.WebUI.DTOs.TeacherSocialDtos
{
    public class CreateTeacherSocialDtos
    {
        public string Url { get; set; }
        public string SocialMediaName { get; set; }
        public string Icon { get; set; }
        public int TeacherId { get; set; }

    }
}
