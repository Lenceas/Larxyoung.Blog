namespace Larxyoung.Blog.Application;

public class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //config.ForType<TestEntity, TestWebModel>()
        //        .Map(dest => dest.CTime, src => src.CTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"))
        //        .Map(dest => dest.MTime, src => src.MTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"));
    }
}
