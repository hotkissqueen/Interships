class Program
{
    static int N, M;
    static long K;
    static List<Edge> edges = new List<Edge>();
    static void Main()
    {
        var parts = Console.ReadLine().Split();
        N = int.Parse(parts[0]);
        M = int.Parse(parts[1]);
        K = long.Parse(parts[2]);
        for (int i = 0; i < M; i++)
        {
            var line = Console.ReadLine().Split();
            int u = int.Parse(line[0]);
            int v = int.Parse(line[1]);
            long s = long.Parse(line[2]);
            edges.Add(new Edge(u, v, s));
        }
        if (K == 0)
        {
            Console.WriteLine(0);
            return;
        }
        long low = 0;
        long high = edges.Max(e => e.s);
        long ans = -1;
        while (low <= high)
        {
            long mid = (low + high) / 2;
            if (CanAchieve(mid))
            {
                ans = mid;
                high = mid - 1;
            }
            else low = mid + 1;
        }
        Console.WriteLine(ans);
    }
    static bool CanAchieve(long level)
    {
        List<int>[] graph = new List<int>[N + 1];
        int[] indeg = new int[N + 1];
        for (int i = 0; i <= N; i++) graph[i] = [];
        foreach (var e in edges)
        {
            if (e.s <= level)
            {
                graph[e.u].Add(e.v);
                indeg[e.v]++;
            }
        }
        Queue<int> queue = new Queue<int>();
        for (int i = 1; i <= N; i++)
        {
            if (indeg[i] == 0) queue.Enqueue(i);
        }
        int cnt = 0;
        List<int> topo = [];
        while (queue.Count > 0)
        {
            int cur = queue.Dequeue();
            topo.Add(cur);
            cnt++;
            foreach (int nxt in graph[cur])
            {
                indeg[nxt]--;
                if (indeg[nxt] == 0) queue.Enqueue(nxt);
            }
        }
        if (cnt < N) return true;
        long[] dp = new long[N + 1];
        long maxPath = 0;
        foreach (int u in topo)
        {
            foreach (int v in graph[u])
            {
                dp[v] = Math.Max(dp[v], dp[u] + 1);
                maxPath = Math.Max(maxPath, dp[v]);
                if (maxPath >= K) return true;
            }
        }
        return maxPath >= K;
    }
}
class Edge
{
    public int u, v;
    public long s;
    public Edge(int u, int v, long s) { this.u = u; this.v = v; this.s = s; }
}