using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_managment
{
    public enum ProjectStatus
    {
        Active,
        Standby,
        Finished
    }
    public enum TaskStatus
    {
        Active,
        Finished,
        Delayed
    }
    public class Project
    {
        public string projectName;
        public string projectDescription;
        public DateTime projectStartDate;
        public DateTime projectEndDate;
        public ProjectStatus projectStatus;
        public Project(string name, string decription, DateTime startDate, DateTime endDate)
        {
            projectName = name;
            projectDescription = decription;
            projectStartDate = startDate.Date;
            projectEndDate = endDate.Date;
            projectStatus = ProjectStatus.Active;
        }
    }
    public class Task
    {
        public string taskName;
        public string taskDescription;
        public DateTime taskDeadlineDate;
        public TaskStatus taskStatus;
        public Project projectParent;
        public Task(string name, string description, DateTime deadline, Project project)
        {
            taskName = name;
            taskDescription = description;
            taskDeadlineDate = deadline;
            taskStatus = TaskStatus.Active;
        }
    }
}
