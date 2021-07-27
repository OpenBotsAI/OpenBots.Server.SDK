using OpenBots.Server.SDK.Api;
using OpenBots.Server.SDK.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenBots.Server.SDK.HelperMethods
{
    public class JobMethods
    {
        public static Job UpdateJobStatus(UserInfo userInfo, string agentId, string jobId, JobStatusType status, JobErrorViewModel errorModel = null, int count = 0)
        {
            var jobsApi = GetApiInstance(userInfo.Token, userInfo.ServerUrl);

            try
            {
                return jobsApi.ApiVapiVersionJobsIdStatusStatusPutWithHttpInfo(agentId, jobId, status, userInfo.ApiVersion, userInfo.OrganizationId, errorModel).Data;
            }
            catch (Exception ex)
            {
                if (UtilityMethods.GetErrorCode(ex) == "401" && count < 2)
                {
                    UtilityMethods.RefreshToken(userInfo);
                    count++;
                    return UpdateJobStatus(userInfo, agentId, jobId, status, errorModel, count);
                }
                else if (ex.Message != "One or more errors occurred.")
                    throw new InvalidOperationException("Exception when calling JobsApi.UpdateJobStatus: " + ex.Message);
                else
                    throw new InvalidOperationException(ex.InnerException.Message);
            }
        }

        public static int UpdateJobPatch(UserInfo userInfo, string id, List<Operation> body, int count = 0)
        {
            var jobsApi = GetApiInstance(userInfo.Token, userInfo.ServerUrl);

            try
            {
                return jobsApi.ApiVapiVersionJobsIdPatchWithHttpInfo(id, userInfo.ApiVersion, userInfo.OrganizationId, body).StatusCode;
            }
            catch (Exception ex)
            {
                if (UtilityMethods.GetErrorCode(ex) == "401" && count < 2)
                {
                    UtilityMethods.RefreshToken(userInfo);
                    count++;
                    return UpdateJobPatch(userInfo, id, body, count);
                }
                else if (ex.Message != "One or more errors occurred.")
                    throw new InvalidOperationException("Exception when calling JobsApi.UpdateJobPatch: " + ex.Message);
                else
                    throw new InvalidOperationException(ex.InnerException.Message);
            }
        }

        public static JobViewModel GetJobViewModel(UserInfo userInfo, string jobId, int count = 0)
        {
            var jobsApi = GetApiInstance(userInfo.Token, userInfo.ServerUrl);

            try
            {
                return jobsApi.ApiVapiVersionJobsViewIdGetWithHttpInfo(jobId, userInfo.ApiVersion, userInfo.OrganizationId).Data;
            }
            catch (Exception ex)
            {
                if (UtilityMethods.GetErrorCode(ex) == "401" && count < 2)
                {
                    UtilityMethods.RefreshToken(userInfo);
                    count++;
                    return GetJobViewModel(userInfo, jobId, count);
                }
                else if (ex.Message != "One or more errors occurred.")
                    throw new InvalidOperationException("Exception when calling JobsApi.GetJobViewModel: " + ex.Message);
                else
                    throw new InvalidOperationException(ex.InnerException.Message);
            }
        }

        public static int UpdateJobViewModel(UserInfo userInfo, string jobId, CreateJobViewModel body, int count = 0)
        {
            var jobsApi = GetApiInstance(userInfo.Token, userInfo.ServerUrl);

            try
            {
                return jobsApi.ApiVapiVersionJobsIdPutWithHttpInfo(jobId, userInfo.ApiVersion, userInfo.OrganizationId, body).StatusCode;
            }
            catch (Exception ex)
            {
                if (UtilityMethods.GetErrorCode(ex) == "401" && count < 2)
                {
                    UtilityMethods.RefreshToken(userInfo);
                    count++;
                    return UpdateJobViewModel(userInfo, jobId, body, count);
                }
                else if (ex.Message != "One or more errors occurred.")
                    throw new InvalidOperationException("Exception when calling JobsApi.UpdateJobViewModel: " + ex.Message);
                else
                    throw new InvalidOperationException(ex.InnerException.Message);
            }
        }

        public static void AddJobCheckpoint(UserInfo userInfo, string jobId, JobCheckpoint body, int count = 0)
        {
            var jobsApi = GetApiInstance(userInfo.Token, userInfo.ServerUrl);

            try
            {
                jobsApi.ApiVapiVersionJobsJobIdAddCheckpointPostAsyncWithHttpInfo(jobId, userInfo.ApiVersion, userInfo.OrganizationId, body).Wait();
            }
            catch (Exception ex)
            {
                if (UtilityMethods.GetErrorCode(ex) == "401" && count < 2)
                {
                    UtilityMethods.RefreshToken(userInfo);
                    count++;
                    AddJobCheckpoint(userInfo, jobId, body, count);
                }
                else if (ex.Message != "One or more errors occurred.")
                    throw new InvalidOperationException("Exception when calling JobsApi.AddJobCheckpoint: " + ex.Message);
                else
                    throw new InvalidOperationException(ex.InnerException.Message);
            }
        }

        public static List<JobCheckpoint> GetJobCheckpoints(UserInfo userInfo, string jobId, int count = 0)
        {
            var jobsApi = GetApiInstance(userInfo.Token, userInfo.ServerUrl);

            try
            {
                return jobsApi.GetJobCheckpointAsyncWithHttpInfo(jobId, userInfo.ApiVersion, userInfo.OrganizationId).Result.Data.Items;
            }
            catch (Exception ex)
            {
                if (UtilityMethods.GetErrorCode(ex) == "401" && count < 2)
                {
                    UtilityMethods.RefreshToken(userInfo);
                    count++;
                    return GetJobCheckpoints(userInfo, jobId, count);
                }
                else if (ex.Message != "One or more errors occurred.")
                    throw new InvalidOperationException("Exception when calling JobsApi.GetJobCheckpoints: " + ex.Message);
                else
                    throw new InvalidOperationException(ex.InnerException.Message);
            }
        }

        public static JobCheckpoint GetJobCheckpointById(UserInfo userInfo, string jobId, string checkpointId, int count = 0)
        {
            var jobsApi = GetApiInstance(userInfo.Token, userInfo.ServerUrl);

            try
            {
                string filter = $"Id eq guid'{checkpointId}'";
                var result = jobsApi.GetJobCheckpointAsyncWithHttpInfo(jobId, userInfo.ApiVersion, userInfo.OrganizationId, filter).Result.Data;
                return result.Items.FirstOrDefault();
            }
            catch (Exception ex)
            {
                if (UtilityMethods.GetErrorCode(ex) == "401" && count < 2)
                {
                    UtilityMethods.RefreshToken(userInfo);
                    count++;
                    return GetJobCheckpointById(userInfo, jobId, checkpointId, count);
                }
                else if (ex.Message != "One or more errors occurred.")
                    throw new InvalidOperationException("Exception when calling JobsApi.GetJobCheckpointById: " + ex.Message);
                else
                    throw new InvalidOperationException(ex.InnerException.Message);
            }
        }

        public static string GetJobStatus(UserInfo userInfo, string jobId)
        {
            var job = GetJobViewModel(userInfo, jobId);

            if (job != null)
                return job.JobStatus.ToString();
            else throw new NullReferenceException("Job does not exist or could not be found.");
        }

        private static JobsApi GetApiInstance(string token, string serverUrl)
        {
            var apiInstance = new JobsApi(serverUrl);
            apiInstance.Configuration.AccessToken = token;

            return apiInstance;
        }

    }
}
