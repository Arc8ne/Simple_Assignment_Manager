using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Globalization;

namespace Simple_Assignment_Manager
{
    public class Task
    {
        public string task_name;

        public string task_type;

        public string module_name;

        public string deadline_date_str;

        public string task_status;

        public Task next_task_obj;

        public Task(string chosen_name, string chosen_type, string chosen_module_name, string deadline_date_text, string new_task_status)
        {
            task_name = chosen_name;

            task_type = chosen_type;

            module_name = chosen_module_name;

            deadline_date_str = deadline_date_text;

            task_status = new_task_status;

            next_task_obj = null;

            string[] chosen_time = deadline_date_str.Split("/");

            CultureInfo original_culture = Thread.CurrentThread.CurrentCulture;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");

            //e.g. 6/1/2008 (short date string)
            string[] current_time = DateTime.Now.ToShortDateString().Split("/");

            if (task_status != "Completed")
            {
                if (Convert.ToInt32(current_time[2]) > Convert.ToInt32(chosen_time[2]))
                {
                    task_status = "Overdue";
                }

                if (Convert.ToInt32(current_time[1]) > Convert.ToInt32(chosen_time[1]))
                {
                    task_status = "Overdue";
                }

                if (Convert.ToInt32(current_time[0]) >= Convert.ToInt32(chosen_time[0]))
                {
                    task_status = "Overdue";
                }
            }

            Thread.CurrentThread.CurrentCulture = original_culture;
        }
    }
}
