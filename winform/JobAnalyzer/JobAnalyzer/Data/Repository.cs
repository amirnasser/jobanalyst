using JobAnalyzer.BLL;
using Microsoft.EntityFrameworkCore;

namespace JobAnalyzer.Data;

public static class Repository
{
    public static JobObject FindJobsByUrl(string url)
    {
        using (JobSearchDbContex contex = new JobSearchDbContex())
        {
            try
            {
                var found = contex.JobObjects.FirstOrDefault(j => j.JobPostUrl.ToString().Contains(url));
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

    public static JobObject FindJobsById(string id)
    {
        using (JobSearchDbContex contex = new JobSearchDbContex())
        {
            try
            {
                var found = contex.JobObjects.FirstOrDefault(j => j.id.Equals(id));
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

    public static Response<JobObject> SaveJobObject(JobObject job)
    {
        try
        {
            using (JobSearchDbContex contex = new JobSearchDbContex())
            {
                job.JobPostUrl = new Uri(Uri.UnescapeDataString(job.JobPostUrl.ToString()));
                if (FindJobsById(job.id) == null)
                {
                    contex.JobObjects.Add(job);

                }
                else
                {
                    contex.JobObjects.Update(job);
                }

                contex.SaveChanges();
                return new Response<JobObject>(job);
            }
        }
        catch (Exception exp)
        {
            Utilities.Logger.Error(exp.Message);
            Utilities.Logger.Error(exp.StackTrace);
            return new Response<JobObject>(exp);
        }
    }

    public static Response<List<JobObject>> GetAllJobs()
    {
        try
        {
            using (JobSearchDbContex contex = new JobSearchDbContex())
            {
                var jobs = contex.JobObjects.ToList();
                return new Response<List<JobObject>>(jobs);
            }
        }
        catch (Exception exp)
        {
            Utilities.Logger.Error(exp);
            return new Response<List<JobObject>>(exp);
        }
    }

    public static Response DeleteJob(object id)
    {
        try
        {
            using (JobSearchDbContex contex = new JobSearchDbContex())
            {
                var job = contex.JobObjects.FirstOrDefault(j => j.id.Equals(id));
                if (job != null)
                {
                    var jobs = contex.JobObjects.Remove(job);
                    return new Response();
                }
                throw new Exception("Job not found");
            }
        }
        catch (Exception exp)
        {
            Utilities.Logger.Error(exp);
            return new Response(exp);
        }
    }
}
