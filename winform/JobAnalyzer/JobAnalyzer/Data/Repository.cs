using System.Security.Policy;
using DocumentFormat.OpenXml.Bibliography;
using JobAnalyzer.BLL;

namespace JobAnalyzer.Data;

internal static class Repository
{
    public static JobObject FindJobsByUrl(string url)
    {
        using (JobSearchDbContex contex = new JobSearchDbContex())
        {
            try
            {

                var found = contex.JobObjects.FirstOrDefault(j => j.JobPost.Equals(url));
                if (found != null)
                {
                    return found;
                }
                return null;
            }
            catch (Exception exp)
            {

                throw;
            }
        }
    }

    public static JobObject? SaveJobObject(JobObject job)
    {
        using (JobSearchDbContex contex = new JobSearchDbContex())
        {
            job.JobPost = Uri.UnescapeDataString(job.JobPost);
            if (FindJobsByUrl(job.JobPost) == null)
            {
                contex.JobObjects.Add(job);
                contex.SaveChanges();
                return job;
            }
            return null;
        }
    }
}
