using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Usertracking
{
    public class AVGStabilizer
    {
	    private static int default_stack_size=10;
        
	    public float[] stack;
	    public int stack_size;
	    public int lastindex;

        public DateTime[] timestamp_stack;
        

        public Boolean use_error_thresholding;
        public float min_error_threshold;
        public float max_error_threshold;

        public AVGStabilizer(){
            this.create_stack(default_stack_size);
            use_error_thresholding = false;
            min_error_threshold = 0;
            max_error_threshold = 0;
        }

	    public AVGStabilizer(int stacksize){
            this.create_stack(stacksize);
            use_error_thresholding = false;
            min_error_threshold = 0;
            max_error_threshold = 0;
        }

        private void create_stack(int stacksize)
        {
            this.lastindex = 0;
            this.stack_size = stacksize;

            this.stack = new float[this.stack_size];
            this.timestamp_stack = new DateTime[this.stack_size];
        }

        public void clearstack()
        {
            this.stack = new float[0];
            this.timestamp_stack = new DateTime[0];
            this.lastindex = 0;
        }

        ~AVGStabilizer(){
            this.clearstack();
        }

        public void stabilize1(ref float var)
        {
            float avgweight;
            int i;
            float sum_value = var;


            //First shift all the values to the end of the stack
            for (i = this.lastindex; i > 0; i--)
            {
                this.stack.SetValue(this.stack[i - 1], i);
            }

            //Do some correction based on distance since last value
            if (this.use_error_thresholding && (this.lastindex > 0))
            {
                float dist = Math.Abs(this.stack[1] - var);
                if (this.min_error_threshold < dist && dist < this.max_error_threshold)
                    var = this.stack[1];
            }
            //Add current value to index 0
            try
            {
                this.stack.SetValue(var, 0);

                if (this.lastindex < this.stack_size - 1)
                    this.lastindex++;

                avgweight = (float)1 / (this.lastindex + 1);

                sum_value = 0;
                //Calculate the average
                for (i = 0; i <= this.lastindex; i++)
                {
                    sum_value += (float)this.stack[i] * avgweight;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: {0} " + e.Message);
            }
            var = sum_value;

            this.stack.SetValue(var, 0);
        }

        public void stabilize2(ref float var)
        {
            float avgweight;
            int i;
            float sum_value = var;


            //First shift all the values to the end of the stack
            for (i = this.lastindex; i > 0; i--)
            {
                this.stack.SetValue(this.stack[i - 1], i);
            }

            //Do some correction based on distance since last value
            if (this.use_error_thresholding && (this.lastindex > 0))
            {
                float dist = Math.Abs(this.stack[1] - var);
                if (this.min_error_threshold < dist && dist < this.max_error_threshold)
                    var = this.stack[1];
            }
            //Add current value to index 0
            try
            {
                this.stack.SetValue(var, 0);

                if (this.lastindex < this.stack_size - 1)
                    this.lastindex++;
                else
                {
                    avgweight = (float)1 / (this.lastindex + 1);

                    sum_value = 0;
                    //Calculate the average
                    for (i = 0; i <= this.lastindex; i++)
                    {
                        sum_value += (float)this.stack[i] * avgweight;
                    }

                    var = sum_value;

                    this.lastindex = 1;
                    this.stack.SetValue(var, 0);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: {0} " + e.Message);
            }
        }

	    public void stabilize3(ref float var){
            float avgweight;
            int i;
            float sum_value = var;

            
            //First shift all the values to the end of the stack
            for (i = this.lastindex; i > 0; i--)
            {
                this.stack.SetValue(this.stack[i - 1], i);
            }

            //Do some correction based on distance since last value
            if (this.use_error_thresholding && (this.lastindex>0))
            {
                float dist= Math.Abs(this.stack[1] - var);
                if (this.min_error_threshold < dist  && dist < this.max_error_threshold)
                    var = this.stack[1];
            }
            //Add current value to index 0
            try
            {
                this.stack.SetValue(var, 0);

                if (this.lastindex < this.stack_size - 1)
                {
                    var = this.stack[this.lastindex];
                    this.lastindex++;
                }
                else
                {
                    avgweight = (float)1 / (this.lastindex + 1);

                    sum_value = 0;
                    //Calculate the average
                    for (i = 0; i <= this.lastindex; i++)
                    {
                        sum_value += (float)this.stack[i] * avgweight;
                    }

                    var = sum_value;

                    this.lastindex = 1;
                    this.stack.SetValue(var, 0);
                }
            }catch (Exception e) {
                Console.WriteLine("ERROR: {0} " + e.Message);
            }
        }

        public void stabilize4(ref float var)
        {
            float sum_value = var;

          
            //Do some correction based on distance since last value
            if (this.use_error_thresholding && (this.lastindex > 0))
            {
                float dist = Math.Abs(this.stack[this.lastindex] - var);
                if (this.min_error_threshold < dist && dist < this.max_error_threshold)
                    var = this.stack[this.lastindex];
            }
            //Add current value to index 0
            try
            {
                this.stack.SetValue(var, this.lastindex);
                this.lastindex++;

                if (this.lastindex < this.stack_size - 1)
                {
                    var = this.stack[0];
                }
                else
                {
                    Array.Sort(this.stack);

                    sum_value = this.stack[(int) Math.Round(this.stack_size / 2.0f)];
                    var = sum_value;
                    
                    this.lastindex = 1;
                    this.stack.SetValue(var, 0);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: {0} " + e.Message);
            }
        }

        public void stabilize5(ref float var)
        {
            float sum_value = var;


            //Do some correction based on distance since last value
            if (this.use_error_thresholding && (this.lastindex > 0))
            {
                float dist = Math.Abs(this.stack[this.lastindex] - var);
                if (this.min_error_threshold < dist && dist < this.max_error_threshold)
                    var = this.stack[this.lastindex];
            }
            //Add current value to index 0
            try
            {
                this.stack.SetValue(var, this.lastindex);
                this.lastindex++;

                if (this.lastindex < this.stack_size - 1)
                {
                    var = this.stack[0];
                }
                else
                {
                    //calculate median
                    Array.Sort(this.stack);

                    if (Math.IEEERemainder(this.stack_size, 2)==0)
                        sum_value = (this.stack[(int)Math.Floor(this.stack_size / 2.0f)] + this.stack[(int)Math.Floor(this.stack_size / 2.0f)+1])/2;
                    else
                        sum_value = this.stack[(int)Math.Round(this.stack_size / 2.0f)];

                    var = sum_value;

                    this.lastindex = 1;
                    this.stack.SetValue(var, 0);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: {0} " + e.Message);
            }
        }

        public void stabilize6(ref float var)
        {   //replica from stabilize3, but it uses timestamps to calculate avgweight
            float avgweight;
            int i;
            float sum_value = var;
            long dt;


            //First shift all the values to the end of the stack
            for (i = this.lastindex; i > 0; i--)
            {
                this.stack.SetValue(this.stack[i - 1], i);
                this.timestamp_stack.SetValue(this.timestamp_stack[i - 1], i);
            }

            //Do some correction based on distance since last value
            if (this.use_error_thresholding && (this.lastindex > 0))
            {
                float dist = Math.Abs(this.stack[1] - var);
                if (this.min_error_threshold < dist && dist < this.max_error_threshold)
                    var = this.stack[1];
            }
            //Add current value to index 0
            try
            {
                this.stack.SetValue(var, 0);
                this.timestamp_stack.SetValue(DateTime.Now, 0);

                if (this.lastindex < this.stack_size - 1)
                {
                    var = this.stack[this.lastindex];
                    this.lastindex++;
                }
                else
                {


                    sum_value = 0;
                    //Calculate the average with delta times
                    for (i = 0; i <= this.lastindex; i++)
                    {
                        if (i < this.lastindex)
                            //dt = (this.timestamp_stack[i].Ticks - this.timestamp_stack[i + 1].Ticks);
                            dt = (this.timestamp_stack[i].Second - this.timestamp_stack[i + 1].Second);
                        else
                            dt = 1;

                        if (dt <= 0) dt = 1;

                        avgweight = 1.0f / (this.stack_size) * 1.0f / dt;
                        sum_value += (float)this.stack[i] * avgweight;
                        Console.WriteLine("i = " + i + "  t = " + this.timestamp_stack[i].Second + "." + this.timestamp_stack[i].Millisecond + "  (" + this.timestamp_stack[i].Ticks + ")  v= " + this.stack[i] + "  dt = " + dt);
                    }

                    var = sum_value;

                    this.lastindex = 1;
                    this.stack.SetValue(var, 0);
                    this.timestamp_stack.SetValue(DateTime.Now, 0);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: {0} " + e.Message);
            }
        }

        public void stabilize(ref float var) {
            //this.stabilize6(ref var);
        }
    }
}
