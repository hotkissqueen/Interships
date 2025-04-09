internal class Program
{
    public class Interval
    {
        public int Color { get; }
        public int L { get; }
        public int R { get; }

        public Interval(int color, int l, int r) { Color = color; L = l; R = r; }
    }
    public static void Main(string[] args)
    {//N like houses count - M like colors count
        int[] input = Console.ReadLine()
                           .Split()
                           .Select(int.Parse)
                           .ToArray();
        int N = input[0], M = input[1];

        //s like final coloring (houses colors)
        int[] s = Console.ReadLine()
                           .Split()
                           .Select(int.Parse)
                           .ToArray();
        int[] lastUsage = ComputeUsage(s, M);

        if (!ValidateColoring(s, lastUsage, M))
        {
            Console.WriteLine("-1"); return;
        }
        List<Interval> operations = ComputeIntervals(s, N, M);

        Console.WriteLine(operations.Count);
        foreach (var op in operations) { Console.WriteLine($"{op.Color} {op.L} {op.R}");
        }
    }

    private static int[] ComputeUsage(int[] s, int M)
    {
        int[] lastUsage = new int[M + 1];
        for (int i = 0; i < s.Length; i++) { lastUsage[s[i]] = i; }
        return lastUsage;
    }

    private static bool ValidateColoring(int[] s, int[] lastUsage, int M)
    {
        bool[] inStack = new bool[M + 1];
        Stack<int> stack = new();

        for (int i = 0; i < s.Length; i++)
        {
            int current = s[i];
            if (stack.Count == 0 || stack.Peek() == current)
            {
                if (stack.Count == 0) { stack.Push(current); inStack[current] = true; }
            }
            else
            {
                if (inStack[current]) { return false; }
                stack.Push(current);
                inStack[current] = true;
            }
            while (stack.Count > 0 && i == lastUsage[stack.Peek()])
            {
                int finished = stack.Pop();
                inStack[finished] = false;
            }
        }
        return true;
    }

    private static List<Interval> ComputeIntervals(int[] s, int N, int M)
    {
        int[] L = new int[M + 1];
        int[] R = new int[M + 1];
        for (int color = 0; color <= M; color++)
        {
            L[color] = int.MaxValue;
            R[color] = -1;
        }

        for (int i = 0; i < N; i++)
        {
            int color = s[i];
            L[color] = Math.Min(L[color], i);
            R[color] = Math.Max(R[color], i);
        }

        List<Interval> operations = new List<Interval>();
        for (int color = 1; color <= M; color++)
        {
            if (R[color] != -1)
            { operations.Add(new Interval(color, L[color] + 1, R[color] + 1)); }
        }
        operations.Sort((a, b) =>
        {
            if (a.L == b.L)
                return b.R.CompareTo(a.R);
            return a.L.CompareTo(b.L);
        });

        return operations;
    }
}