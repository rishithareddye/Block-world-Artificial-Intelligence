using System;
using System.Collections.Generic;

namespace TestMoving
{
    class Logic
    {
        public bool[] Empty_locations(Dictionary<char, int[]> dict)
        {
            bool[] log = { true, true, true, true };

            foreach (KeyValuePair<char, int[]> kvp in dict)
            {
                int[] arr = new int[2];
                arr = kvp.Value;
                if (arr[1] == 0)
                {
                    log[arr[0]] = false;
                }
                // Console.WriteLine("Key = {0}, Value = {1}, {2}", kvp.Key, arr[0], arr[1]);
            }
            return log;

        }
        public int count_top(char key, Dictionary<char, int[]> dict)
        {
            int count = 0;
            int[] tmp = new int[2];
            tmp = dict[key];
            foreach (KeyValuePair<char, int[]> kvp in dict)
            {
                int[] arr = new int[2];
                arr = kvp.Value;
                if (arr[0] == tmp[0] && arr[1] > tmp[1])
                {
                    count++;
                }
                // Console.WriteLine("Key = {0}, Value = {1}, {2}", kvp.Key, arr[0], arr[1]);
            }
            return count;
        }
        public char getkey(int nx, int ny, Dictionary<char, int[]> dict)
        {
            char key = '*';
            foreach (KeyValuePair<char, int[]> kvp in dict)
            {
                int[] arr = new int[2];
                arr = kvp.Value;
                if (arr[0] == nx && arr[1] == ny)
                {
                    key = kvp.Key;
                    break;
                }
                // Console.WriteLine("Key = {0}, Value = {1}, {2}", kvp.Key, arr[0], arr[1]);
            }
            return key;
        }
        public int[] swap(char key, int no, int no1, Dictionary<char, int[]> dict)
        {
            bool flag = false;
            int[] tmp = new int[2];
            bool[] log = new bool[4];
            log = Empty_locations(dict);
            for (int i = 0; i < 4; i++)
            {
                if (log[i])
                {
                    if (i != no)
                    {
                        tmp[0] = i;
                        tmp[1] = 0;
                        flag = true;
                        //dict[key] = new int[] { tmp[0], tmp[1] };
                        break;

                    }
                }
            }
            if (!flag)
            {
                foreach (KeyValuePair<char, int[]> kvp in dict)
                {

                    int[] arr = new int[2];
                    arr = kvp.Value;
                    int loc = (arr[0] + 1) % 4;
                    if (loc == no || loc == no1)
                    {
                        continue;
                    }
                    else
                    {
                        char newkey = getkey(loc, 0, dict);
                        int top = count_top(newkey, dict);
                        int[] arr1 = new int[2];
                        arr1[0] = loc;
                        arr1[1] = top + 1;
                        // dict[key] = new int[] { tmp[0], tmp[1] };
                        tmp[0] = loc;
                        tmp[1] = top + 1;
                    }
                    // Console.WriteLine("Key = {0}, Value = {1}, {2}", kvp.Key, arr[0], arr[1]);
                }
            }
            return tmp;
        }

        public Dictionary<char, int[]> parse_input(String input)
        {
            Dictionary<char, int[]> dict = new Dictionary<char, int[]>();


            string[] words = input.Split('^');
            int count_table = 0;
            // int count_on = 0;

            foreach (String st in words)
            {
                if (st.StartsWith("TABLE"))
                {
                    //string tmp = count_table.ToString() + " " + "0";
                    int[] arr = new int[2];
                    arr[0] = count_table;
                    arr[1] = 0;
                    dict.Add(st[6], new int[] { arr[0], arr[1] });

                    count_table++;

                }

            }

            foreach (String st in words)
            {
                if (st.StartsWith("ON"))
                {
                    int[] tmp = new int[2];
                    tmp = dict[st[3]];
                    int cnt = tmp[1];
                    dict.Add(st[5], new int[] { tmp[0], ++cnt });

                }

            }

            return dict;

        }
        public int ComputeSteps(char[] moved, int[][] stepsmoved, Dictionary<char, int[]> olddict, Dictionary<char, int[]> newdict)
        {
            int steps = 0;
            try
            {
                foreach (KeyValuePair<char, int[]> kvp in newdict)
                {

                    char newkey = kvp.Key;
                    int[] n = kvp.Value;
                    char oldkey = newkey;
                    Console.WriteLine(oldkey);
                    int[] o = olddict[oldkey];
                    if (o[0] == n[0] && o[1] == n[1])
                    {

                    }
                    else
                    {
                        //Console.WriteLine(newkey);
                        int top = count_top(oldkey, olddict);
                        while (top != 0)
                        {
                            char temp = getkey(o[0], o[1] + top, olddict);
                            int[] s = swap(temp, n[0], o[0], olddict);
                            int[] t1 = olddict[temp];
                            t1[0] = s[0];
                            t1[1] = s[1];
                            moved[steps] = temp;
                            stepsmoved[steps] = new int[] { s[0], s[1] };
                            steps++;
                            top = count_top(oldkey, olddict);
                            Console.WriteLine(temp + "->" + s[0] + " " + s[1] + "1");
                        }
                        char key = getkey(n[0], n[1], olddict);
                        if (key != '*')
                        {
                            int[] t = olddict[key];
                            top = count_top(key, olddict);
                            while (top != 0)
                            {
                                char temp = getkey(n[0], n[1] + top, olddict);
                                int[] s = swap(temp, n[0], o[0], olddict);
                                int[] t3 = olddict[temp];
                                t3[0] = s[0];
                                t3[1] = s[1];
                                moved[steps] = temp;
                                stepsmoved[steps] = new int[] { s[0], s[1] };
                                steps++;
                                top = count_top(key, olddict);
                                Console.WriteLine(temp + "->" + s[0] + " " + s[1] + "2");
                            }
                            char temp1 = getkey(n[0], n[1], olddict);
                            int[] s1 = swap(temp1, n[0], o[0], olddict);
                            int[] t1 = olddict[temp1];
                            t1[0] = s1[0];
                            t1[1] = s1[1];
                            moved[steps] = temp1;
                            stepsmoved[steps] = new int[] { s1[0], s1[1] };
                            steps++;
                            Console.WriteLine(temp1 + "->" + s1[0] + " " + s1[1] + "3");
                        }
                        o[0] = n[0];
                        o[1] = n[1];
                        int[] s2 = new int[] { n[0], n[1] };
                        Console.WriteLine(newkey + " " + n[0] + n[1]);
                        moved[steps] = newkey;
                        stepsmoved[steps] = new int[] { s2[0], s2[1] };
                        steps++;
                        Console.WriteLine(newkey + "->" + s2[0] + " " + s2[1] + "4");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return steps;
        }
        public Dictionary<char, int[]> sort_Dictionary(Dictionary<char, int[]> new_dict)
        {
            Dictionary<char, int[]> sorted_new_dict = new Dictionary<char, int[]>();
            foreach (KeyValuePair<char, int[]> kvp in new_dict)
            {
                char key = kvp.Key;
                int[] val = kvp.Value;
                if (sorted_new_dict.ContainsKey(key))
                {

                }
                else
                {
                    sorted_new_dict.Add(key, val);
                    foreach (KeyValuePair<char, int[]> kvp1 in new_dict)
                    {
                        char key1 = kvp1.Key;
                        int[] val1 = kvp1.Value;
                        if (key1 != key)
                        {
                            if (val1[0] == val[0])
                            {
                                sorted_new_dict.Add(key1, val1);
                            }
                        }
                    }
                }
            }
            return sorted_new_dict;
        }
    }
}
