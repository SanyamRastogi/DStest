using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DStest
{
    public class StackUsingQueue
    {
        public Queue<int> q = new Queue<int>();
        public void Push(int val)
        {
            int s = q.Count();
            q.Enqueue(val);

            for (int i = 0; i < s; i++)
            {
                q.Enqueue(q.First());

                q.Dequeue();
            }
        }

        public void Pop()
        {
            if (q.Count() == 0)
                Console.WriteLine("No elements\n");
            else
                q.Dequeue();
        }

        public int Top()
        {
            return q.First();
        }

        public bool IsEmpty()
        {
            return q.Count() <= 0;
        }
    }

    class Program
    {
        public static int HistogramArea(int[] hist, int n)
        {
            Stack<int> s = new Stack<int>();
            int maxarea = 0, tp, iter_area = 0, i = 0;
            while (i < n)
            {
                if (s.Count() == 0 || hist[s.Peek()] <= hist[i])
                    s.Push(i++);
                else
                {
                    tp = s.Peek();
                    s.Pop();
                    iter_area = hist[tp] * ((s.Count() == 0) ? i : (i - s.Peek() - 1));
                    if (maxarea < iter_area)
                        maxarea = iter_area;
                }
            }
            while (s.Count() != 0)
            {
                tp = s.Peek();
                s.Pop();
                iter_area = hist[tp] * ((s.Count() == 0) ? i : (i - s.Peek() - 1));
                if (maxarea < iter_area)
                    maxarea = iter_area;
            }
            return maxarea;
        }

        public static int MinCoinReq(int[] arr, int n, int amt)
        {
            int[] dp = new int[amt + 1];
            dp[0] = 0;
            for (int i = 1; i <= amt; i++)
            {
                dp[i] = int.MaxValue;
            }
            for (int i = 1; i <= amt; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i >= arr[j])
                    {
                        int val = dp[i - arr[j]];
                        if (val <= int.MaxValue && val + 1 < dp[i])
                            dp[i] = val + 1;
                    }
                }
            }
            return dp[amt];
        }

        public static void PrintSum(int[] arr, int n)
        {
            int sum = 0;
            for (int i = 0; i < n; i++)
                sum += arr[i];

            // dp[i][j] would be true if arr[0..i-1] has
            // a subset with sum equal to j.
            bool[,] dp = new bool[n + 1, sum + 1];
            // There is always a subset with 0 sum
            for (int i = 0; i <= n; i++)
                dp[i, 0] = true;

            // Fill dp[][] in bottom up manner
            for (int i = 1; i <= n; i++)
            {
                dp[i, arr[i - 1]] = true;
                for (int j = 1; j <= sum; j++)
                {
                    // Sums that were achievable
                    // without current array element
                    if (dp[i - 1, j] == true)
                    {
                        dp[i, j] = true;
                        dp[i, j + arr[i - 1]] = true;
                    }
                }
            }

            // Print last row elements
            for (int j = 0; j <= sum; j++)
                if (dp[n, j] == true)
                    Console.WriteLine(j + " ");
        }

        public static int GetBrcketReversal(string input)
        {
            if (input.Length % 2 != 0)
            {
                Console.WriteLine("can't be made");
                return -1;
            }
            int count = 0; int validsubstringLen = 0;
            char[] arr = input.ToCharArray();
            Stack<char> st = new Stack<char>();
            for (int i = 0; i < input.Length; i++)
            {
                if (st.Count != 0 && st.Peek() == '{' && arr[i] == '}')
                {
                    st.Pop();
                    validsubstringLen += 2;
                }
                else
                    st.Push(arr[i]);
            }
            int cl = 0, op = 0;
            while (st.Count() > 0)
            {
                if (st.Peek() == '}')
                    ++cl;
                else
                    ++op;
                st.Pop();
            }
            count = Convert.ToInt32(Math.Ceiling((decimal)cl / 2) + Math.Ceiling((decimal)op / 2));

            return count;
        }
        public static int GetCorrectDistBwLetters(string word)
        {
            int result = 0;
            char[] arr = word.ToCharArray();
            for (int i = 0; i < word.Length - 1; i++)
            {
                for (int j = i + 1; j < word.Length; j++)
                {
                    if (Math.Abs(arr[i] - arr[j]) == Math.Abs(i - j))
                    {
                        Console.WriteLine("({0},{1})", arr[i], arr[j]);
                        result++;
                    }
                }
            }
            return result;
        }
        public static void CalculateN(int N)
        {
            int rem = 0, sum = 0, len, nine = 9, dist = 0;
            for (len = 1; ; len++)
            {
                sum += nine * len;
                dist += nine;

                if (sum >= N)
                {
                    sum -= nine * len;
                    dist -= nine;
                    N -= sum;
                    break;
                }
                nine *= 10;
            }

            double diff = Math.Ceiling((double)N / (double)len);

            int d = N % len;
            if (d == 0)
                d = len;

            string str = (dist + diff).ToString();

            Console.WriteLine(str[d - 1]);
        }

        public static string ReplaceChartoBeSame(string fromstr, string tostr, int k)
        {
            int res = 0;
            Dictionary<char, int> hs = new Dictionary<char, int>();
            for (int i = 0; i < fromstr.Length; i++)
            {
                if (hs.ContainsKey(fromstr[i]))
                    hs[fromstr[i]] += 1;
                else
                    hs.Add(fromstr[i], 1);
            }
            for (int i = 0; i < tostr.Length; i++)
            {
                if (hs.ContainsKey(tostr[i]))
                    hs[tostr[i]] -= 1;
                if (hs.ContainsKey(tostr[i]) && hs[tostr[i]] == 0)
                    hs.Remove(tostr[i]);
            }
            if (k >= hs.Count())
                return "Yes";
            else
                return "No";
        }

        static void Main(string[] args)
        {
            unsafe
            {
                string key = "abc";
                Dictionary<string, int> ht = new Dictionary<string, int>();
                ht.Add(key, 123);

                fixed (char* p = key)
                {
                    p[0] = 'x';
                }

                Console.WriteLine(key); // xbc
                                        //Console.WriteLine(ht["xbc"]); // Not found!
            }
            Console.WriteLine("Max Area"+ HistogramArea(new int[] { 6, 2, 5, 4, 5, 2, 6 }, 7));
            Console.WriteLine("min coins" + MinCoinReq(new int[] { 1, 5, 10 }, 3, 30));
            string fromstr = "anagram", tostr = "grammar";
            Console.WriteLine(ReplaceChartoBeSame(fromstr, tostr, 1));
            PrintSum(new int[] { 1 }, 1);
            GetBrcketReversal("{}{{}}}}");
            Console.WriteLine(GetCorrectDistBwLetters("observation"));
            CalculateN(50);
            StackUsingQueue sq = new StackUsingQueue();
            sq.Push(10);
            sq.Push(20);
            sq.Push(30);
            sq.Push(40);
            Console.WriteLine(sq.Top());
            sq.Pop();
            sq.Push(30);
            //sq.Pop();
            Console.WriteLine(sq.Top());
            Console.ReadLine();
        }
    }
}
