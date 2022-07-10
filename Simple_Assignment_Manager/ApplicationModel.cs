using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows;

namespace Simple_Assignment_Manager
{
    public class ApplicationModel
    {
        public Task start_task_obj = null;

        public Module start_module_obj = null;

        private int encryption_key = 174;

        private const string default_tasks_data_file_path = "./tasks_data.daf";

        private const string default_modules_data_file_path = "./modules_data.daf";

        public string encrypt_string(string decrypted_string)
        {
            string encrypted_string = "";

            foreach(char current_char in decrypted_string)
            {
                if (current_char != ',')
                {
                    encrypted_string += (char)(current_char + encryption_key);
                }
                else
                {
                    encrypted_string += current_char;
                }
            }

            //MessageBox.Show(encrypted_string);

            return encrypted_string;
        }

        public string decrypt_string(string encrypted_string)
        {
            if (encrypted_string == "" || encrypted_string == null)
            {
                return encrypted_string;
            }

            string decrypted_string = "";

            foreach (char current_char in encrypted_string)
            {
                if (current_char != ',')
                {
                    decrypted_string += (char)(current_char - encryption_key);
                }
                else
                {
                    decrypted_string += current_char;
                }
            }

            return decrypted_string;
        }

        public void save_tasks_data()
        {
            if (!File.Exists(default_tasks_data_file_path))
            {
                File.Create(default_tasks_data_file_path).Close();
            }

            StreamWriter tasks_data_file_writer = new StreamWriter(default_tasks_data_file_path);

            Task temp_task_obj = start_task_obj;

            while (temp_task_obj != null)
            {
                //MessageBox.Show($"Current task being saved: {temp_task_obj.task_name}");

                tasks_data_file_writer.WriteLine(encrypt_string($"{temp_task_obj.task_name},{temp_task_obj.task_type},{temp_task_obj.module_name},{temp_task_obj.deadline_date_str},{temp_task_obj.task_status}"));

                temp_task_obj = temp_task_obj.next_task_obj;
            }

            tasks_data_file_writer.Close();
        }

        public void load_tasks_data()
        {
            if (!File.Exists(default_tasks_data_file_path))
            {
                File.Create(default_tasks_data_file_path).Close();

                return;
            }

            StreamReader tasks_data_file_reader = new StreamReader(default_tasks_data_file_path);

            string raw_task_data_line = "lorem ipsum";

            Task prev_task_obj = start_task_obj;

            while (raw_task_data_line != "" && raw_task_data_line != null)
            {
                raw_task_data_line = decrypt_string(tasks_data_file_reader.ReadLine());

                //MessageBox.Show($"Current raw task data text: {raw_task_data_line}");

                if (raw_task_data_line == "" || raw_task_data_line == null)
                {
                    break;
                }

                string[] task_data_line = raw_task_data_line.Split(",");

                Task new_task = new Task(task_data_line[0], task_data_line[1], task_data_line[2], task_data_line[3], task_data_line[4]);

                if (start_task_obj == null)
                {
                    start_task_obj = new_task;

                    prev_task_obj = start_task_obj;
                }
                else
                {
                    prev_task_obj.next_task_obj = new_task;

                    prev_task_obj = prev_task_obj.next_task_obj;
                }
            }

            tasks_data_file_reader.Close();
        }

        public void save_modules_data()
        {
            if (!File.Exists(default_modules_data_file_path))
            {
                File.Create(default_modules_data_file_path).Close();
            }

            StreamWriter modules_data_file_writer = new StreamWriter(default_modules_data_file_path);

            Module temp_module_obj = start_module_obj;

            while (temp_module_obj != null)
            {
                //MessageBox.Show($"Current task being saved: {temp_task_obj.task_name}");

                modules_data_file_writer.WriteLine(encrypt_string($"{temp_module_obj.module_name},{temp_module_obj.module_grade},{temp_module_obj.module_credits}"));

                temp_module_obj = temp_module_obj.next_module_obj;
            }

            modules_data_file_writer.Close();
        }

        public void load_modules_data()
        {
            if (!File.Exists(default_modules_data_file_path))
            {
                File.Create(default_modules_data_file_path).Close();

                return;
            }

            StreamReader modules_data_file_reader = new StreamReader(default_modules_data_file_path);

            string raw_module_data_line = "lorem ipsum";

            Module prev_module_obj = start_module_obj;

            while (raw_module_data_line != "" && raw_module_data_line != null)
            {
                raw_module_data_line = decrypt_string(modules_data_file_reader.ReadLine());

                //MessageBox.Show($"Current raw task data text: {raw_module_data_line}");

                if (raw_module_data_line == "" || raw_module_data_line == null)
                {
                    break;
                }

                string[] module_data_line = raw_module_data_line.Split(",");

                Module new_module = new Module(module_data_line[0], module_data_line[1], Convert.ToInt32(module_data_line[2]));

                if (start_module_obj == null)
                {
                    start_module_obj = new_module;

                    prev_module_obj = start_module_obj;
                }
                else
                {
                    prev_module_obj.next_module_obj = new_module;

                    prev_module_obj = prev_module_obj.next_module_obj;
                }
            }

            modules_data_file_reader.Close();
        }

        public void save_all_data()
        {
            save_modules_data();

            save_tasks_data();
        }

        public void load_all_data()
        {
            load_modules_data();

            load_tasks_data();
        }

        public void add_task(Task new_task)
        {
            if (start_task_obj == null)
            {
                start_task_obj = new_task;

                return;
            }

            Task temp_task_obj = start_task_obj;

            while (temp_task_obj.next_task_obj != null)
            {
                temp_task_obj = temp_task_obj.next_task_obj;
            }

            temp_task_obj.next_task_obj = new_task;
        }

        public Task find_task_by_name(string chosen_task_name)
        {
            Task temp_task_obj = start_task_obj;

            while (temp_task_obj != null)
            {
                if (temp_task_obj.task_name == chosen_task_name)
                {
                    return temp_task_obj;
                }

                temp_task_obj = temp_task_obj.next_task_obj;
            }

            return null;
        }

        public void remove_task_by_name(string chosen_task_name)
        {
            Task temp_task_obj = start_task_obj;

            Task prev_temp_task_obj = null;

            while (temp_task_obj != null)
            {
                if (temp_task_obj.task_name == chosen_task_name)
                {
                    if (prev_temp_task_obj != null)
                    {
                        prev_temp_task_obj.next_task_obj = temp_task_obj.next_task_obj;

                        temp_task_obj = null;
                    }
                    else
                    {
                        start_task_obj = temp_task_obj.next_task_obj;

                        temp_task_obj = null;
                    }

                    return;
                }

                prev_temp_task_obj = temp_task_obj;

                temp_task_obj = temp_task_obj.next_task_obj;
            }
        }

        public void edit_task_by_name(string chosen_task_name, string new_task_name, string new_task_type, string new_module_name, string new_task_deadline_str)
        {
            Task chosen_task_obj = find_task_by_name(chosen_task_name);

            chosen_task_obj.task_name = new_task_name;

            chosen_task_obj.task_type = new_task_type;

            chosen_task_obj.module_name = new_module_name;

            chosen_task_obj.deadline_date_str = new_task_deadline_str;
        }

        public void add_module(Module new_module)
        {
            if (start_module_obj == null)
            {
                start_module_obj = new_module;

                return;
            }

            Module temp_module_obj = start_module_obj;

            while (temp_module_obj.next_module_obj != null)
            {
                temp_module_obj = temp_module_obj.next_module_obj;
            }

            temp_module_obj.next_module_obj = new_module;
        }

        public Module find_module_by_name(string chosen_module_name)
        {
            Module temp_module_obj = start_module_obj;

            while (temp_module_obj != null)
            {
                if (temp_module_obj.module_name == chosen_module_name)
                {
                    return temp_module_obj;
                }

                temp_module_obj = temp_module_obj.next_module_obj;
            }

            return null;
        }

        public void remove_module_by_name(string chosen_module_name)
        {
            Module temp_module_obj = start_module_obj;

            Module prev_temp_module_obj = null;

            while (temp_module_obj != null)
            {
                if (temp_module_obj.module_name == chosen_module_name)
                {
                    if (prev_temp_module_obj != null)
                    {
                        prev_temp_module_obj.next_module_obj = temp_module_obj.next_module_obj;

                        temp_module_obj = null;
                    }
                    else
                    {
                        start_module_obj = temp_module_obj.next_module_obj;

                        temp_module_obj = null;
                    }

                    return;
                }

                prev_temp_module_obj = temp_module_obj;

                temp_module_obj = temp_module_obj.next_module_obj;
            }
        }

        public void edit_module_by_name(string chosen_module_name, string new_module_name, string new_module_grade, int new_module_credits)
        {
            Module chosen_module_obj = find_module_by_name(chosen_module_name);

            chosen_module_obj.module_name = new_module_name;

            chosen_module_obj.module_grade = new_module_grade;

            chosen_module_obj.module_credits = new_module_credits;
        }

        public void export_tasks_data_to_csv(string tasks_csv_file_path)
        {
            if (!tasks_csv_file_path.Contains(".csv"))
            {
                tasks_csv_file_path += ".csv";
            }

            File.Create(tasks_csv_file_path).Close();

            StreamWriter new_tasks_csv_file_writer = new StreamWriter(tasks_csv_file_path);

            new_tasks_csv_file_writer.WriteLine("Task Name,Task Type,Module Name,Deadline,Task Status");

            Task temp_task_obj = start_task_obj;

            while (temp_task_obj != null)
            {
                new_tasks_csv_file_writer.WriteLine($"{temp_task_obj.task_name},{temp_task_obj.task_type},{temp_task_obj.module_name},{temp_task_obj.deadline_date_str},{temp_task_obj.task_status}");

                temp_task_obj = temp_task_obj.next_task_obj;
            }

            new_tasks_csv_file_writer.Close();
        }

        public void export_modules_data_to_csv(string modules_csv_file_path)
        {
            if (!modules_csv_file_path.Contains(".csv"))
            {
                modules_csv_file_path += ".csv";
            }

            File.Create(modules_csv_file_path).Close();

            StreamWriter new_modules_csv_file_writer = new StreamWriter(modules_csv_file_path);

            new_modules_csv_file_writer.WriteLine("Module Name,Module Grade,Module Credits");

            Module temp_module_obj = start_module_obj;

            while (temp_module_obj != null)
            {
                new_modules_csv_file_writer.WriteLine($"{temp_module_obj.module_name},{temp_module_obj.module_grade},{temp_module_obj.module_credits}");

                temp_module_obj = temp_module_obj.next_module_obj;
            }

            new_modules_csv_file_writer.Close();
        }

        public void bulk_create_files(string chosen_folder_path, string chosen_prefix, string chosen_file_extension, int files_count, string initial_content)
        {
            for (int i = 0; i < files_count; i++)
            {
                string final_file_path = chosen_folder_path + "/" + chosen_prefix + Convert.ToString(i + 1) + chosen_file_extension;

                File.Create(final_file_path).Close();

                if (initial_content != "")
                {
                    StreamWriter created_file_writer = new StreamWriter(final_file_path);

                    created_file_writer.Write(initial_content);

                    created_file_writer.Close();
                }
            }
        }
    }
}
