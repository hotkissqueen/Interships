internal class Program
{

    private static void Main(string[] args)
    {
        string[] input = Console.ReadLine().Split();
        int[] house = Console.ReadLine()
                             .Split()
                             .Select(int.Parse)
                             .ToArray();
        int[] obogrevatel = Console.ReadLine()
                                   .Split()
                                   .Select(int.Parse)
                                   .ToArray();
        Array.Sort(house);
        Array.Sort(obogrevatel);

        int minRadius = MinimumRadius(house, obogrevatel);
        Console.WriteLine($"{minRadius}");
    }
    static int MinimumRadius(int[] houses, int[] obogrevateli)
    {
        int maxDistance = 0;

        foreach (int house in houses) { int obogrevatelDistance = ClosestObogrevatel(house, obogrevateli);
            maxDistance = Math.Max(maxDistance, obogrevatelDistance);
        }
        return maxDistance;
    }
    static int ClosestObogrevatel(int house, int[] obogrevatel)
    {
        int low = 0, high = obogrevatel.Length - 1;

        while (low < high)
        {
            int mid = low + (high - low) / 2;
            if (obogrevatel[mid] < house)
                low = mid + 1;
            else
                high = mid;
        }

        int closestDistance = Math.Abs(obogrevatel[low] - house);
        if(low > 0) {closestDistance=Math.Min(closestDistance, Math.Abs(obogrevatel[low-1] - house));}
        return closestDistance;
    }
}