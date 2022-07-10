using System;
using System.Collections.Generic;
using System.Text;

namespace Simple_Assignment_Manager
{
    public class Module
    {
        public string module_name;

        public string module_grade;

        public int module_credits;

        public Module next_module_obj;

        public Module(string new_module_name, string new_module_grade, int new_module_credits)
        {
            module_name = new_module_name;

            module_grade = new_module_grade;

            module_credits = new_module_credits;

            next_module_obj = null;
        }
    }
}
